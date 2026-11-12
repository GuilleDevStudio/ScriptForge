using System.ComponentModel;
using System.Data;

namespace ScriptForge;

/// <summary>Elegir tablas, ver sus datos y marcar las filas para generar los INSERT.</summary>
public partial class DataPickerControl : UserControl
{
    const string SelCol = "__sel";

    readonly Dictionary<string, DbObj> _tables = new();      // "dbo.Tabla" -> objeto
    readonly Dictionary<string, DataTable> _data = new();    // datos ya cargados por tabla
    string _currentKey;

    public event EventHandler SelectionChanged;

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DbService Db { get; set; }

    public DataPickerControl()
    {
        InitializeComponent();
        Theme.StyleGrid(grid);
        Theme.Primary(btnLoad);
        Theme.Secondary(btnAll);
        Theme.Secondary(btnSel);
        Theme.Secondary(btnNone);
    }

    public void SetTables(IEnumerable<DbObj> tables)
    {
        _tables.Clear();
        _data.Clear();
        _currentKey = null;
        cmbTable.Items.Clear();
        grid.DataSource = null;

        foreach (var t in tables)
        {
            var key = $"{t.Schema}.{t.Name}";
            _tables[key] = t;
            cmbTable.Items.Add(key);
        }
        UpdateInfo();
    }

    [Browsable(false)]
    public int SelectedRowCount =>
        _data.Values.Sum(dt => dt.Rows.Cast<DataRow>().Count(IsSelected));

    /// <summary>Tablas con al menos una fila marcada.</summary>
    public List<DataSelection> GetSelections() =>
        _data.Where(kv => kv.Value.Rows.Cast<DataRow>().Any(IsSelected))
             .Select(kv => new DataSelection(_tables[kv.Key], kv.Value, chkIdentity.Checked, chkSkip.Checked))
             .ToList();

    static bool IsSelected(DataRow r) => r[SelCol] is bool b && b;

    // ---------- Cargar ----------
    private async void btnLoad_Click(object sender, EventArgs e)
    {
        if (Db == null) return;

        var key = cmbTable.Text.Trim();
        if (!_tables.TryGetValue(key, out var table))
        {
            MessageBox.Show("Elige una tabla de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (_data.TryGetValue(key, out var old) && old.Rows.Cast<DataRow>().Any(IsSelected))
        {
            var r = MessageBox.Show("Ya hay filas marcadas en esta tabla. ¿Recargar y perder la selección?",
                "Recargar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;
        }

        btnLoad.Enabled = false;
        UseWaitCursor = true;
        try
        {
            var dt = await Db.GetDataAsync(table, (int)numTop.Value, txtWhere.Text);

            var sel = new DataColumn(SelCol, typeof(bool));
            dt.Columns.Add(sel);
            sel.SetOrdinal(0);
            foreach (DataRow row in dt.Rows) row[SelCol] = false;

            _data[key] = dt;
            _currentKey = key;
            Bind(dt);
            UpdateInfo();
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error al cargar los datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnLoad.Enabled = true;
            UseWaitCursor = false;
        }
    }

    void Bind(DataTable dt)
    {
        grid.DataSource = dt;
        foreach (DataGridViewColumn c in grid.Columns)
        {
            if (c.Name == SelCol)
            {
                c.HeaderText = "Incluir";
                c.Width = 70;
                c.Frozen = true;
                c.ReadOnly = false;
            }
            else c.ReadOnly = true;
        }
        grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        if (grid.Columns.Contains(SelCol)) grid.Columns[SelCol].Width = 70;
    }

    private void cmbTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        var key = cmbTable.Text.Trim();
        if (_data.TryGetValue(key, out var dt)) { _currentKey = key; Bind(dt); }
        else { _currentKey = null; grid.DataSource = null; }
        UpdateInfo();
    }

    // ---------- Selección ----------
    DataTable Current => _currentKey != null && _data.TryGetValue(_currentKey, out var dt) ? dt : null;

    private void btnAll_Click(object sender, EventArgs e) => SetAll(true);
    private void btnNone_Click(object sender, EventArgs e) => SetAll(false);

    void SetAll(bool value)
    {
        var dt = Current;
        if (dt == null) return;
        foreach (DataRow r in dt.Rows) r[SelCol] = value;
        Changed();
    }

    private void btnSel_Click(object sender, EventArgs e)
    {
        if (Current == null) return;
        foreach (DataGridViewRow row in grid.SelectedRows)
            if (row.DataBoundItem is DataRowView drv) drv.Row[SelCol] = true;
        Changed();
    }

    void Changed()
    {
        grid.Refresh();
        UpdateInfo();
        SelectionChanged?.Invoke(this, EventArgs.Empty);
    }

    void UpdateInfo()
    {
        var dt = Current;
        if (dt == null)
        {
            lblInfo.Text = _data.Count == 0
                ? "Elige una tabla y pulsa «Cargar datos»."
                : "Esta tabla aún no está cargada. Pulsa «Cargar datos».";
            return;
        }

        int sel = dt.Rows.Cast<DataRow>().Count(IsSelected);
        int total = SelectedRowCount;
        int tables = _data.Count(kv => kv.Value.Rows.Cast<DataRow>().Any(IsSelected));
        lblInfo.Text = $"{_currentKey}: {sel} de {dt.Rows.Count} filas marcadas   ·   " +
                       $"Total a generar: {total} fila(s) en {tables} tabla(s)";
    }

    // ---------- Eventos del grid ----------
    private void grid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
        if (grid.IsCurrentCellDirty) grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }

    private void grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || grid.Columns[e.ColumnIndex].Name != SelCol) return;
        grid.InvalidateRow(e.RowIndex);
        UpdateInfo();
        SelectionChanged?.Invoke(this, EventArgs.Empty);
    }

    private void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (grid.Rows[e.RowIndex].DataBoundItem is DataRowView drv && IsSelected(drv.Row))
            e.CellStyle.BackColor = Theme.SelectedRow;
    }

    // Columnas binarias, etc.: no mostrar errores del grid
    private void grid_DataError(object sender, DataGridViewDataErrorEventArgs e) => e.ThrowException = false;
}
