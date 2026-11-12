using System.ComponentModel;

namespace ScriptForge;

/// <summary>Lista de objetos (tablas, vistas, procs o funciones) con selección y filtro.</summary>
public partial class ObjectListControl : UserControl
{
    readonly BindingList<DbObj> _list = new();
    ObjKind _kind;

    public event EventHandler SelectionChanged;
    public event EventHandler<DbObj> PickColumnsRequested;

    public ObjectListControl()
    {
        InitializeComponent();
        Theme.StyleGrid(grid);
        Theme.Secondary(btnAll);
        Theme.Secondary(btnNone);
        grid.DataSource = _list;
        Kind = ObjKind.Table;
    }

    /// <summary>Define qué columnas se muestran según el tipo de objeto.</summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ObjKind Kind
    {
        get => _kind;
        set
        {
            _kind = value;
            colColumns.Visible = colColumnsInfo.Visible = value == ObjKind.Table;
            colGrant.Visible = value is ObjKind.Proc or ObjKind.Function;
        }
    }

    [Browsable(false)]
    public IEnumerable<DbObj> Items => _list;

    [Browsable(false)]
    public int SelectedCount => _list.Count(o => o.Selected);

    public void SetObjects(IEnumerable<DbObj> objs)
    {
        _list.RaiseListChangedEvents = false;
        _list.Clear();
        foreach (var o in objs) _list.Add(o);
        _list.RaiseListChangedEvents = true;
        _list.ResetBindings();
        UpdateCount();
    }

    /// <summary>Repinta la lista tras cambiar objetos por código.</summary>
    public void RefreshView()
    {
        grid.Refresh();
        UpdateCount();
        SelectionChanged?.Invoke(this, EventArgs.Empty);
    }

    void UpdateCount() => lblCount.Text = $"{_list.Count} objetos · {SelectedCount} seleccionados";

    // ---------- Eventos del grid ----------
    private void grid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
        if (grid.IsCurrentCellDirty) grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }

    private void grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        grid.InvalidateRow(e.RowIndex);
        UpdateCount();
        SelectionChanged?.Invoke(this, EventArgs.Empty);
    }

    private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && grid.Columns[e.ColumnIndex] == colColumns)
            PickColumnsRequested?.Invoke(this, (DbObj)grid.Rows[e.RowIndex].DataBoundItem);
    }

    private void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (grid.Rows[e.RowIndex].DataBoundItem is DbObj o && o.Selected)
            e.CellStyle.BackColor = Theme.SelectedRow;
    }

    private void grid_DataError(object sender, DataGridViewDataErrorEventArgs e) => e.ThrowException = false;

    // ---------- Filtro y botones ----------
    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (grid.DataSource == null) return;
        var cm = (CurrencyManager)grid.BindingContext[grid.DataSource];
        cm.SuspendBinding();
        foreach (DataGridViewRow row in grid.Rows)
        {
            var o = (DbObj)row.DataBoundItem;
            row.Visible = string.IsNullOrWhiteSpace(txtSearch.Text)
                || o.Full.Contains(txtSearch.Text.Trim(), StringComparison.OrdinalIgnoreCase);
        }
        cm.ResumeBinding();
    }

    private void btnAll_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in grid.Rows)
            if (row.Visible) ((DbObj)row.DataBoundItem).Selected = true;
        RefreshView();
    }

    private void btnNone_Click(object sender, EventArgs e)
    {
        foreach (var o in _list) { o.Selected = false; o.Grant = false; }
        RefreshView();
    }
}
