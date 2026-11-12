namespace ScriptForge
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlBrand = new Panel();
            lblBrand = new Label();
            lblTagline = new Label();
            lblVersion = new Label();
            pnlForm = new Panel();
            lblHeading = new Label();
            lblServer = new Label();
            txtServer = new TextBox();
            lblDb = new Label();
            txtDb = new TextBox();
            lblAuth = new Label();
            cmbAuth = new ComboBox();
            lblUser = new Label();
            txtUser = new TextBox();
            lblPwd = new Label();
            txtPwd = new TextBox();
            lblError = new Label();
            btnConnect = new Button();
            btnCancel = new Button();
            pnlBrand.SuspendLayout();
            pnlForm.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBrand
            // 
            pnlBrand.BackColor = Color.FromArgb(31, 42, 68);
            pnlBrand.Controls.Add(lblVersion);
            pnlBrand.Controls.Add(lblTagline);
            pnlBrand.Controls.Add(lblBrand);
            pnlBrand.Dock = DockStyle.Left;
            pnlBrand.Name = "pnlBrand";
            pnlBrand.Size = new Size(230, 400);
            pnlBrand.TabIndex = 1;
            // 
            // lblBrand
            // 
            lblBrand.AutoSize = true;
            lblBrand.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblBrand.ForeColor = Color.White;
            lblBrand.Location = new Point(22, 40);
            lblBrand.Name = "lblBrand";
            lblBrand.Text = "ScriptForge";
            // 
            // lblTagline
            // 
            lblTagline.Font = new Font("Segoe UI", 10F);
            lblTagline.ForeColor = Color.FromArgb(180, 190, 210);
            lblTagline.Location = new Point(25, 92);
            lblTagline.Name = "lblTagline";
            lblTagline.Size = new Size(180, 90);
            lblTagline.Text = "Generador de scripts de despliegue para SQL Server.";
            // 
            // lblVersion
            // 
            lblVersion.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI", 8.5F);
            lblVersion.ForeColor = Color.FromArgb(120, 135, 165);
            lblVersion.Location = new Point(25, 366);
            lblVersion.Name = "lblVersion";
            lblVersion.Text = "v1.0";
            // 
            // pnlForm
            // 
            pnlForm.BackColor = Color.White;
            pnlForm.Controls.Add(btnCancel);
            pnlForm.Controls.Add(btnConnect);
            pnlForm.Controls.Add(lblError);
            pnlForm.Controls.Add(txtPwd);
            pnlForm.Controls.Add(lblPwd);
            pnlForm.Controls.Add(txtUser);
            pnlForm.Controls.Add(lblUser);
            pnlForm.Controls.Add(cmbAuth);
            pnlForm.Controls.Add(lblAuth);
            pnlForm.Controls.Add(txtDb);
            pnlForm.Controls.Add(lblDb);
            pnlForm.Controls.Add(txtServer);
            pnlForm.Controls.Add(lblServer);
            pnlForm.Controls.Add(lblHeading);
            pnlForm.Dock = DockStyle.Fill;
            pnlForm.Name = "pnlForm";
            pnlForm.TabIndex = 0;
            // 
            // lblHeading
            // 
            lblHeading.AutoSize = true;
            lblHeading.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeading.ForeColor = Color.FromArgb(30, 41, 59);
            lblHeading.Location = new Point(32, 26);
            lblHeading.Name = "lblHeading";
            lblHeading.Text = "Conectar al servidor";
            // 
            // lblServer
            // 
            lblServer.AutoSize = true;
            lblServer.ForeColor = Color.FromArgb(100, 116, 139);
            lblServer.Location = new Point(32, 76);
            lblServer.Name = "lblServer";
            lblServer.Text = "Servidor";
            // 
            // txtServer
            // 
            txtServer.Font = new Font("Segoe UI", 10F);
            txtServer.Location = new Point(32, 96);
            txtServer.Name = "txtServer";
            txtServer.Size = new Size(340, 25);
            txtServer.TabIndex = 0;
            txtServer.Text = "localhost";
            // 
            // lblDb
            // 
            lblDb.AutoSize = true;
            lblDb.ForeColor = Color.FromArgb(100, 116, 139);
            lblDb.Location = new Point(32, 132);
            lblDb.Name = "lblDb";
            lblDb.Text = "Base de datos";
            // 
            // txtDb
            // 
            txtDb.Font = new Font("Segoe UI", 10F);
            txtDb.Location = new Point(32, 152);
            txtDb.Name = "txtDb";
            txtDb.Size = new Size(340, 25);
            txtDb.TabIndex = 1;
            // 
            // lblAuth
            // 
            lblAuth.AutoSize = true;
            lblAuth.ForeColor = Color.FromArgb(100, 116, 139);
            lblAuth.Location = new Point(32, 188);
            lblAuth.Name = "lblAuth";
            lblAuth.Text = "Autenticación";
            // 
            // cmbAuth
            // 
            cmbAuth.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAuth.Font = new Font("Segoe UI", 10F);
            cmbAuth.FormattingEnabled = true;
            cmbAuth.Items.AddRange(new object[] { "Autenticación de SQL Server", "Autenticación de Windows" });
            cmbAuth.Location = new Point(32, 208);
            cmbAuth.Name = "cmbAuth";
            cmbAuth.Size = new Size(340, 25);
            cmbAuth.TabIndex = 2;
            cmbAuth.SelectedIndexChanged += cmbAuth_SelectedIndexChanged;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.ForeColor = Color.FromArgb(100, 116, 139);
            lblUser.Location = new Point(32, 244);
            lblUser.Name = "lblUser";
            lblUser.Text = "Usuario";
            // 
            // txtUser
            // 
            txtUser.Font = new Font("Segoe UI", 10F);
            txtUser.Location = new Point(32, 264);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(165, 25);
            txtUser.TabIndex = 3;
            // 
            // lblPwd
            // 
            lblPwd.AutoSize = true;
            lblPwd.ForeColor = Color.FromArgb(100, 116, 139);
            lblPwd.Location = new Point(207, 244);
            lblPwd.Name = "lblPwd";
            lblPwd.Text = "Contraseña";
            // 
            // txtPwd
            // 
            txtPwd.Font = new Font("Segoe UI", 10F);
            txtPwd.Location = new Point(207, 264);
            txtPwd.Name = "txtPwd";
            txtPwd.Size = new Size(165, 25);
            txtPwd.TabIndex = 4;
            txtPwd.UseSystemPasswordChar = true;
            // 
            // lblError
            // 
            lblError.ForeColor = Color.FromArgb(220, 38, 38);
            lblError.Location = new Point(32, 298);
            lblError.Name = "lblError";
            lblError.Size = new Size(340, 36);
            // 
            // btnConnect
            // 
            btnConnect.BackColor = Color.FromArgb(47, 111, 237);
            btnConnect.FlatAppearance.BorderSize = 0;
            btnConnect.FlatStyle = FlatStyle.Flat;
            btnConnect.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnConnect.ForeColor = Color.White;
            btnConnect.Location = new Point(32, 338);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(180, 40);
            btnConnect.TabIndex = 5;
            btnConnect.Text = "Conectar";
            btnConnect.UseVisualStyleBackColor = false;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(222, 338);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(150, 40);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Salir";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            AcceptButton = btnConnect;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(640, 400);
            Controls.Add(pnlForm);
            Controls.Add(pnlBrand);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ScriptForge – Conexión";
            pnlBrand.ResumeLayout(false);
            pnlBrand.PerformLayout();
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlBrand;
        private Label lblBrand;
        private Label lblTagline;
        private Label lblVersion;
        private Panel pnlForm;
        private Label lblHeading;
        private Label lblServer;
        private TextBox txtServer;
        private Label lblDb;
        private TextBox txtDb;
        private Label lblAuth;
        private ComboBox cmbAuth;
        private Label lblUser;
        private TextBox txtUser;
        private Label lblPwd;
        private TextBox txtPwd;
        private Label lblError;
        private Button btnConnect;
        private Button btnCancel;
    }
}
