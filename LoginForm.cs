namespace ScriptForge;

public partial class LoginForm : Form
{
    public string ConnectionString { get; private set; }
    public string ConnectionInfo { get; private set; }

    public LoginForm()
    {
        InitializeComponent();
        Theme.Primary(btnConnect);
        Theme.Secondary(btnCancel);
        cmbAuth.SelectedIndex = 0;
    }

    private void cmbAuth_SelectedIndexChanged(object sender, EventArgs e)
    {
        var sql = cmbAuth.SelectedIndex == 0;
        txtUser.Enabled = sql;
        txtPwd.Enabled = sql;
    }

    private async void btnConnect_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        if (string.IsNullOrWhiteSpace(txtServer.Text) || string.IsNullOrWhiteSpace(txtDb.Text))
        {
            lblError.Text = "Indica servidor y base de datos.";
            return;
        }

        var cs = DbService.BuildConnectionString(txtServer.Text.Trim(), txtDb.Text.Trim(),
            cmbAuth.SelectedIndex == 1, txtUser.Text, txtPwd.Text);

        btnConnect.Enabled = false;
        btnConnect.Text = "Conectando…";
        UseWaitCursor = true;
        try
        {
            await DbService.TestAsync(cs);
            ConnectionString = cs;
            ConnectionInfo = $"{txtServer.Text.Trim()}  ›  {txtDb.Text.Trim()}";
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            lblError.Text = "No se pudo conectar: " + ex.Message;
        }
        finally
        {
            btnConnect.Enabled = true;
            btnConnect.Text = "Conectar";
            UseWaitCursor = false;
        }
    }
}
