using System.Diagnostics;

namespace ScriptForge;

public partial class MainForm : Form
{
    DbService _db;

    // Constructor sin parámetros: lo necesita el diseñador de Visual Studio.
    public MainForm()
    {
        InitializeComponent();

        ctlTablas.Kind = ObjKind.Table;
        ctlVistas.Kind = ObjKind.View;
        ctlProcs.Kind = ObjKind.Proc;
        ctlFunciones.Kind = ObjKind.Function;

        Theme.StyleTabs(tabMain);
        Theme.Primary(btnGenerar);

        foreach (var c in Lists)
            c.SelectionChanged += (_, _) => UpdateSummary();

        ctlTablas.PickColumnsRequested += OnPickColumns;
        ctlDatos.SelectionChanged += (_, _) => UpdateSummary();
    }

    public MainForm(string connectionString, string connectionInfo) : this()
    {
        _db = new DbService(connectionString);
        lblSubtitle.Text = connectionInfo;
        Text = $"ScriptForge – {connectionInfo}";
    }

    ObjectListControl[] Lists => new[] { ctlTablas, ctlVistas, ctlProcs, ctlFunciones };

    // ---------- Carga ----------
    private async void MainForm_Shown(object sender, EventArgs e)
    {
        if (_db == null) return;   // modo diseñador

        lblStatus.Text = "Cargando objetos…";
        btnGenerar.Enabled = false;
        try
        {
            var all = await _db.GetObjectsAsync();
            foreach (var c in Lists)
                c.SetObjects(all.Where(o => o.Kind == c.Kind));

            ctlDatos.Db = _db;
            ctlDatos.SetTables(all.Where(o => o.Kind == ObjKind.Table));

            lblStatus.Text = $"{all.Count} objetos cargados";
            UpdateSummary();
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Error al cargar";
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally { btnGenerar.Enabled = true; }
    }

    void UpdateSummary()
    {
        static string Tab(string name, int n) => n > 0 ? $"{name} ({n})" : name;

        tabTablas.Text = Tab("Tablas", ctlTablas.SelectedCount);
        tabVistas.Text = Tab("Vistas", ctlVistas.SelectedCount);
        tabProcs.Text = Tab("Procedimientos", ctlProcs.SelectedCount);
        tabFunciones.Text = Tab("Funciones", ctlFunciones.SelectedCount);
        tabDatos.Text = Tab("Datos", ctlDatos.SelectedRowCount);
        tabMain.Invalidate();

        var selected = Lists.SelectMany(c => c.Items).Where(o => o.Selected).ToList();
        var grants = selected.Count(o => o.Grant);
        var rows = ctlDatos.SelectedRowCount;
        var parts = new List<string>();
        if (selected.Count > 0) parts.Add($"{selected.Count} objeto(s) seleccionado(s)");
        if (grants > 0) parts.Add($"{grants} con permisos");
        if (rows > 0) parts.Add($"{rows} fila(s) de datos");
        lblSelection.Text = parts.Count == 0 ? "Nada seleccionado" : string.Join(" · ", parts);
    }

    // ---------- Columnas para ALTER de tablas ----------
    private async void OnPickColumns(object sender, DbObj o)
    {
        try
        {
            var cols = (await _db.GetColumnsAsync(o)).Select(c => c.Name).ToList();
            using var dlg = new CheckListDialog($"Columnas de {o.Full}",
                "Columnas a añadir o modificar", cols, o.Columns);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            o.Columns = dlg.Selected;
            if (o.Columns.Count > 0) { o.Selected = true; o.Action = "ALTER"; }
            ((ObjectListControl)sender).RefreshView();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // ---------- Generación ----------
    private async void btnGenerar_Click(object sender, EventArgs e)
    {
        var selected = Lists.SelectMany(c => c.Items).Where(o => o.Selected).ToList();
        var dataSel = ctlDatos.GetSelections();
        if (selected.Count == 0 && dataSel.Count == 0)
        {
            MessageBox.Show("No has seleccionado ningún objeto ni filas de datos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var bad = selected.Where(o => o.Kind == ObjKind.Table && o.Action == "ALTER" && o.Columns.Count == 0).ToList();
        if (bad.Count > 0)
        {
            MessageBox.Show("Estas tablas están en ALTER pero sin columnas elegidas:\n\n" +
                string.Join("\n", bad.Select(b => b.Full)), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Usuarios para los permisos
        var grantees = new List<string>();
        if (selected.Any(o => o.Grant && (o.Kind == ObjKind.Proc || o.Kind == ObjKind.Function)))
        {
            try
            {
                var principals = await _db.GetPrincipalsAsync();
                using var dlg = new CheckListDialog("Permisos", "Usuarios o roles a los que dar permiso", principals);
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                grantees = dlg.Selected;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // Ubicación
        using var fbd = new FolderBrowserDialog { Description = "Elige dónde crear la carpeta de scripts" };
        if (fbd.ShowDialog(this) != DialogResult.OK) return;
        var folder = Path.Combine(fbd.SelectedPath, $"Release_{DateTime.Now:yyyyMMdd_HHmm}");

        btnGenerar.Enabled = false;
        lblStatus.Text = "Generando…";
        try
        {
            var files = await new ScriptGenerator(_db).GenerateAsync(selected, dataSel, grantees, folder);
            lblStatus.Text = $"{files.Count} fichero(s) generados";
            Process.Start(new ProcessStartInfo("explorer.exe", $"\"{folder}\"") { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Error";
            MessageBox.Show(ex.Message, "Error al generar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally { btnGenerar.Enabled = true; }
    }
}
