namespace ScriptForge
{
    partial class MainForm
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
            pnlHeader = new Panel();
            lblConnected = new Label();
            lblSubtitle = new Label();
            lblTitle = new Label();
            tabMain = new TabControl();
            tabTablas = new TabPage();
            ctlTablas = new ObjectListControl();
            tabVistas = new TabPage();
            ctlVistas = new ObjectListControl();
            tabProcs = new TabPage();
            ctlProcs = new ObjectListControl();
            tabFunciones = new TabPage();
            ctlFunciones = new ObjectListControl();
            tabDatos = new TabPage();
            ctlDatos = new DataPickerControl();
            pnlFooter = new Panel();
            btnGenerar = new Button();
            lblSelection = new Label();
            lblStatus = new Label();
            lineFooter = new Panel();
            pnlHeader.SuspendLayout();
            tabMain.SuspendLayout();
            tabTablas.SuspendLayout();
            tabVistas.SuspendLayout();
            tabProcs.SuspendLayout();
            tabFunciones.SuspendLayout();
            tabDatos.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(31, 42, 68);
            pnlHeader.Controls.Add(lblConnected);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1040, 68);
            pnlHeader.TabIndex = 2;
            // 
            // lblConnected
            // 
            lblConnected.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblConnected.ForeColor = Color.FromArgb(74, 222, 128);
            lblConnected.Location = new Point(900, 24);
            lblConnected.Name = "lblConnected";
            lblConnected.Size = new Size(124, 20);
            lblConnected.Text = "● Conectado";
            lblConnected.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(180, 190, 210);
            lblSubtitle.Location = new Point(26, 42);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Text = "servidor › base de datos";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(24, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Text = "ScriptForge";
            // 
            // tabMain
            // 
            tabMain.Controls.Add(tabTablas);
            tabMain.Controls.Add(tabVistas);
            tabMain.Controls.Add(tabProcs);
            tabMain.Controls.Add(tabFunciones);
            tabMain.Controls.Add(tabDatos);
            tabMain.Dock = DockStyle.Fill;
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.TabIndex = 0;
            // 
            // tabTablas
            // 
            tabTablas.BackColor = Color.White;
            tabTablas.Controls.Add(ctlTablas);
            tabTablas.Name = "tabTablas";
            tabTablas.Text = "Tablas";
            // 
            // ctlTablas
            // 
            ctlTablas.Dock = DockStyle.Fill;
            ctlTablas.Name = "ctlTablas";
            ctlTablas.TabIndex = 0;
            // 
            // tabVistas
            // 
            tabVistas.BackColor = Color.White;
            tabVistas.Controls.Add(ctlVistas);
            tabVistas.Name = "tabVistas";
            tabVistas.Text = "Vistas";
            // 
            // ctlVistas
            // 
            ctlVistas.Dock = DockStyle.Fill;
            ctlVistas.Name = "ctlVistas";
            ctlVistas.TabIndex = 0;
            // 
            // tabProcs
            // 
            tabProcs.BackColor = Color.White;
            tabProcs.Controls.Add(ctlProcs);
            tabProcs.Name = "tabProcs";
            tabProcs.Text = "Procedimientos";
            // 
            // ctlProcs
            // 
            ctlProcs.Dock = DockStyle.Fill;
            ctlProcs.Name = "ctlProcs";
            ctlProcs.TabIndex = 0;
            // 
            // tabFunciones
            // 
            tabFunciones.BackColor = Color.White;
            tabFunciones.Controls.Add(ctlFunciones);
            tabFunciones.Name = "tabFunciones";
            tabFunciones.Text = "Funciones";
            // 
            // ctlFunciones
            // 
            ctlFunciones.Dock = DockStyle.Fill;
            ctlFunciones.Name = "ctlFunciones";
            ctlFunciones.TabIndex = 0;
            // 
            // tabDatos
            // 
            tabDatos.BackColor = Color.White;
            tabDatos.Controls.Add(ctlDatos);
            tabDatos.Name = "tabDatos";
            tabDatos.Text = "Datos";
            // 
            // ctlDatos
            // 
            ctlDatos.Dock = DockStyle.Fill;
            ctlDatos.Name = "ctlDatos";
            ctlDatos.TabIndex = 0;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.Controls.Add(btnGenerar);
            pnlFooter.Controls.Add(lblSelection);
            pnlFooter.Controls.Add(lblStatus);
            pnlFooter.Controls.Add(lineFooter);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(1040, 64);
            pnlFooter.TabIndex = 1;
            // 
            // btnGenerar
            // 
            btnGenerar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGenerar.BackColor = Color.FromArgb(47, 111, 237);
            btnGenerar.FlatAppearance.BorderSize = 0;
            btnGenerar.FlatStyle = FlatStyle.Flat;
            btnGenerar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGenerar.ForeColor = Color.White;
            btnGenerar.Location = new Point(836, 12);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(190, 42);
            btnGenerar.TabIndex = 0;
            btnGenerar.Text = "Generar scripts…";
            btnGenerar.UseVisualStyleBackColor = false;
            btnGenerar.Click += btnGenerar_Click;
            // 
            // lblSelection
            // 
            lblSelection.AutoSize = true;
            lblSelection.ForeColor = Color.FromArgb(100, 116, 139);
            lblSelection.Location = new Point(24, 36);
            lblSelection.Name = "lblSelection";
            lblSelection.Text = "Nada seleccionado";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(30, 41, 59);
            lblStatus.Location = new Point(24, 14);
            lblStatus.Name = "lblStatus";
            lblStatus.Text = "Listo";
            // 
            // lineFooter
            // 
            lineFooter.BackColor = Color.FromArgb(226, 232, 240);
            lineFooter.Dock = DockStyle.Top;
            lineFooter.Name = "lineFooter";
            lineFooter.Size = new Size(1040, 1);
            lineFooter.TabIndex = 3;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1040, 700);
            Controls.Add(tabMain);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9.5F);
            MinimumSize = new Size(860, 560);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ScriptForge";
            Shown += MainForm_Shown;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            tabMain.ResumeLayout(false);
            tabTablas.ResumeLayout(false);
            tabVistas.ResumeLayout(false);
            tabProcs.ResumeLayout(false);
            tabFunciones.ResumeLayout(false);
            tabDatos.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblConnected;
        private Label lblSubtitle;
        private Label lblTitle;
        private TabControl tabMain;
        private TabPage tabTablas;
        private ObjectListControl ctlTablas;
        private TabPage tabVistas;
        private ObjectListControl ctlVistas;
        private TabPage tabProcs;
        private ObjectListControl ctlProcs;
        private TabPage tabFunciones;
        private ObjectListControl ctlFunciones;
        private TabPage tabDatos;
        private DataPickerControl ctlDatos;
        private Panel pnlFooter;
        private Button btnGenerar;
        private Label lblSelection;
        private Label lblStatus;
        private Panel lineFooter;
    }
}
