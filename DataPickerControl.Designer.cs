namespace ScriptForge
{
    partial class DataPickerControl
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            pnlTop = new Panel();
            btnNone = new Button();
            btnSel = new Button();
            btnAll = new Button();
            chkSkip = new CheckBox();
            chkIdentity = new CheckBox();
            btnLoad = new Button();
            txtWhere = new TextBox();
            lblWhere = new Label();
            numTop = new NumericUpDown();
            lblTop = new Label();
            cmbTable = new ComboBox();
            lblTable = new Label();
            grid = new DataGridView();
            pnlBottom = new Panel();
            lblInfo = new Label();
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numTop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(245, 247, 250);
            pnlTop.Controls.Add(btnNone);
            pnlTop.Controls.Add(btnSel);
            pnlTop.Controls.Add(btnAll);
            pnlTop.Controls.Add(chkSkip);
            pnlTop.Controls.Add(chkIdentity);
            pnlTop.Controls.Add(btnLoad);
            pnlTop.Controls.Add(txtWhere);
            pnlTop.Controls.Add(lblWhere);
            pnlTop.Controls.Add(numTop);
            pnlTop.Controls.Add(lblTop);
            pnlTop.Controls.Add(cmbTable);
            pnlTop.Controls.Add(lblTable);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(900, 92);
            pnlTop.TabIndex = 2;
            // 
            // lblTable
            // 
            lblTable.AutoSize = true;
            lblTable.ForeColor = Color.FromArgb(100, 116, 139);
            lblTable.Location = new Point(12, 15);
            lblTable.Name = "lblTable";
            lblTable.Text = "Tabla";
            // 
            // cmbTable
            // 
            cmbTable.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbTable.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbTable.FormattingEnabled = true;
            cmbTable.Location = new Point(54, 11);
            cmbTable.Name = "cmbTable";
            cmbTable.Size = new Size(260, 23);
            cmbTable.TabIndex = 0;
            cmbTable.SelectedIndexChanged += cmbTable_SelectedIndexChanged;
            // 
            // lblTop
            // 
            lblTop.AutoSize = true;
            lblTop.ForeColor = Color.FromArgb(100, 116, 139);
            lblTop.Location = new Point(330, 15);
            lblTop.Name = "lblTop";
            lblTop.Text = "Máx. filas";
            // 
            // numTop
            // 
            numTop.Location = new Point(398, 11);
            numTop.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numTop.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numTop.Name = "numTop";
            numTop.Size = new Size(78, 23);
            numTop.TabIndex = 1;
            numTop.Value = new decimal(new int[] { 500, 0, 0, 0 });
            // 
            // lblWhere
            // 
            lblWhere.AutoSize = true;
            lblWhere.ForeColor = Color.FromArgb(100, 116, 139);
            lblWhere.Location = new Point(490, 15);
            lblWhere.Name = "lblWhere";
            lblWhere.Text = "WHERE";
            // 
            // txtWhere
            // 
            txtWhere.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtWhere.Location = new Point(542, 11);
            txtWhere.Name = "txtWhere";
            txtWhere.PlaceholderText = "opcional, ej.  Activo = 1";
            txtWhere.Size = new Size(236, 23);
            txtWhere.TabIndex = 2;
            // 
            // btnLoad
            // 
            btnLoad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoad.Location = new Point(786, 8);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(106, 30);
            btnLoad.TabIndex = 3;
            btnLoad.Text = "Cargar datos";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // chkIdentity
            // 
            chkIdentity.AutoSize = true;
            chkIdentity.Checked = true;
            chkIdentity.CheckState = CheckState.Checked;
            chkIdentity.Location = new Point(12, 55);
            chkIdentity.Name = "chkIdentity";
            chkIdentity.TabIndex = 4;
            chkIdentity.Text = "Incluir columna IDENTITY";
            chkIdentity.UseVisualStyleBackColor = true;
            // 
            // chkSkip
            // 
            chkSkip.AutoSize = true;
            chkSkip.Checked = true;
            chkSkip.CheckState = CheckState.Checked;
            chkSkip.Location = new Point(196, 55);
            chkSkip.Name = "chkSkip";
            chkSkip.TabIndex = 5;
            chkSkip.Text = "Omitir filas que ya existan (por PK)";
            chkSkip.UseVisualStyleBackColor = true;
            // 
            // btnAll
            // 
            btnAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAll.Location = new Point(560, 50);
            btnAll.Name = "btnAll";
            btnAll.Size = new Size(96, 30);
            btnAll.TabIndex = 6;
            btnAll.Text = "Todas";
            btnAll.UseVisualStyleBackColor = true;
            btnAll.Click += btnAll_Click;
            // 
            // btnSel
            // 
            btnSel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSel.Location = new Point(662, 50);
            btnSel.Name = "btnSel";
            btnSel.Size = new Size(130, 30);
            btnSel.TabIndex = 7;
            btnSel.Text = "Marcar resaltadas";
            btnSel.UseVisualStyleBackColor = true;
            btnSel.Click += btnSel_Click;
            // 
            // btnNone
            // 
            btnNone.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNone.Location = new Point(798, 50);
            btnNone.Name = "btnNone";
            btnNone.Size = new Size(94, 30);
            btnNone.TabIndex = 8;
            btnNone.Text = "Ninguna";
            btnNone.UseVisualStyleBackColor = true;
            btnNone.Click += btnNone_Click;
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            grid.Dock = DockStyle.Fill;
            grid.Name = "grid";
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.TabIndex = 0;
            grid.CellFormatting += grid_CellFormatting;
            grid.CellValueChanged += grid_CellValueChanged;
            grid.CurrentCellDirtyStateChanged += grid_CurrentCellDirtyStateChanged;
            grid.DataError += grid_DataError;
            // 
            // pnlBottom
            // 
            pnlBottom.BackColor = Color.FromArgb(245, 247, 250);
            pnlBottom.Controls.Add(lblInfo);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(900, 32);
            pnlBottom.TabIndex = 1;
            // 
            // lblInfo
            // 
            lblInfo.Dock = DockStyle.Fill;
            lblInfo.ForeColor = Color.FromArgb(100, 116, 139);
            lblInfo.Name = "lblInfo";
            lblInfo.Padding = new Padding(12, 0, 0, 0);
            lblInfo.Text = "Elige una tabla y pulsa «Cargar datos».";
            lblInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // DataPickerControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(grid);
            Controls.Add(pnlBottom);
            Controls.Add(pnlTop);
            Name = "DataPickerControl";
            Size = new Size(900, 520);
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numTop).EndInit();
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Button btnNone;
        private Button btnSel;
        private Button btnAll;
        private CheckBox chkSkip;
        private CheckBox chkIdentity;
        private Button btnLoad;
        private TextBox txtWhere;
        private Label lblWhere;
        private NumericUpDown numTop;
        private Label lblTop;
        private ComboBox cmbTable;
        private Label lblTable;
        private DataGridView grid;
        private Panel pnlBottom;
        private Label lblInfo;
    }
}
