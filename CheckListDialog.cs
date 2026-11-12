namespace ScriptForge;

/// <summary>Diálogo genérico con lista de casillas (columnas, usuarios...).</summary>
public partial class CheckListDialog : Form
{
    public CheckListDialog()
    {
        InitializeComponent();
        Theme.Primary(btnOk);
        Theme.Secondary(btnCancel);
        Theme.Secondary(btnAll);
        Theme.Secondary(btnNone);
    }

    public CheckListDialog(string title, string prompt, IEnumerable<string> items, IEnumerable<string> preselected = null)
        : this()
    {
        Text = title;
        lblPrompt.Text = prompt;
        var pre = new HashSet<string>(preselected ?? Enumerable.Empty<string>());
        foreach (var i in items) clb.Items.Add(i, pre.Contains(i));
    }

    public List<string> Selected => clb.CheckedItems.Cast<string>().ToList();

    private void btnAll_Click(object sender, EventArgs e) => SetAll(true);
    private void btnNone_Click(object sender, EventArgs e) => SetAll(false);

    void SetAll(bool value)
    {
        for (int i = 0; i < clb.Items.Count; i++) clb.SetItemChecked(i, value);
    }
}
