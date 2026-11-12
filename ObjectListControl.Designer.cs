namespace ScriptForge
{
    partial class ObjectListControl
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
            lblCount = new Label();
            btnNone = new Button();
            btnAll = new Button();
            txtSearch = new TextBox();
            grid = new DataGridView();
            colSelected = new DataGridViewCheckBoxColumn();
            colSchema = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colAction = new DataGridViewComboBoxColumn();
            colColumns = new DataGridViewButtonColumn();
            colColumnsInfo = new DataGridViewTextBoxColumn();
            colGrant = new DataGridViewCheckBoxColumn();
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(245, 247, 250);
            pnlTop.Controls.Add(lblCount);
            pnlTop.Controls.Add(btnNone);
            pnlTop.Controls.Add(btnAll);
            pnlTop.Controls.Add(txtSearch);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(760, 52);
            pnlTop.TabIndex = 1;
            // 
            // lblCount
            // 
            lblCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCount.ForeColor = Color.FromArgb(100, 116, 139);
            lblCount.Location = new Point(528, 17);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(220, 20);
            lblCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnNone
            // 
            btnNone.Location = new Point(468, 10);
            btnNone.Name = "btnNone";
            btnNone.Size = new Size(90, 30);
            btnNone.TabIndex = 2;
            btnNone.Text = "Limpiar";
            btnNone.UseVisualStyleBackColor = true;
            btnNone.Click += btnNone_Click;
            // 
            // btnAll
            // 
            btnAll.Location = new Point(324, 10);
            btnAll.Name = "btnAll";
            btnAll.Size = new Size(138, 30);
            btnAll.TabIndex = 1;
            btnAll.Text = "Marcar visibles";
            btnAll.UseVisualStyleBackColor = true;
            btnAll.Click += btnAll_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(12, 13);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Buscar por nombre…";
            txtSearch.Size = new Size(300, 23);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AutoGenerateColumns = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.Columns.AddRange(new DataGridViewColumn[] { colSelected, colSchema, colName, colAction, colColumns, colColumnsInfo, colGrant });
            grid.Dock = DockStyle.Fill;
            grid.Name = "grid";
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.TabIndex = 0;
            grid.CellContentClick += grid_CellContentClick;
            grid.CellFormatting += grid_CellFormatting;
            grid.CellValueChanged += grid_CellValueChanged;
            grid.CurrentCellDirtyStateChanged += grid_CurrentCellDirtyStateChanged;
            grid.DataError += grid_DataError;
            // 
            // colSelected
            // 
            colSelected.DataPropertyName = "Selected";
            colSelected.FillWeight = 9F;
            colSelected.HeaderText = "Incluir";
            colSelected.Name = "colSelected";
            // 
            // colSchema
            // 
            colSchema.DataPropertyName = "Schema";
            colSchema.FillWeight = 12F;
            colSchema.HeaderText = "Esquema";
            colSchema.Name = "colSchema";
            colSchema.ReadOnly = true;
            // 
            // colName
            // 
            colName.DataPropertyName = "Name";
            colName.FillWeight = 38F;
            colName.HeaderText = "Nombre";
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colAction
            // 
            colAction.DataPropertyName = "Action";
            colAction.FillWeight = 14F;
            colAction.FlatStyle = FlatStyle.Flat;
            colAction.HeaderText = "Acción";
            colAction.Items.AddRange(new object[] { "CREATE", "ALTER" });
            colAction.Name = "colAction";
            // 
            // colColumns
            // 
            colColumns.FillWeight = 14F;
            colColumns.FlatStyle = FlatStyle.Flat;
            colColumns.HeaderText = "Columnas (ALTER)";
            colColumns.Name = "colColumns";
            colColumns.Text = "Elegir…";
            colColumns.UseColumnTextForButtonValue = true;
            // 
            // colColumnsInfo
            // 
            colColumnsInfo.DataPropertyName = "ColumnsInfo";
            colColumnsInfo.FillWeight = 14F;
            colColumnsInfo.HeaderText = "";
            colColumnsInfo.Name = "colColumnsInfo";
            colColumnsInfo.ReadOnly = true;
            // 
            // colGrant
            // 
            colGrant.DataPropertyName = "Grant";
            colGrant.FillWeight = 12F;
            colGrant.HeaderText = "Dar permiso";
            colGrant.Name = "colGrant";
            // 
            // ObjectListControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(grid);
            Controls.Add(pnlTop);
            Name = "ObjectListControl";
            Size = new Size(760, 420);
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTop;
        private Label lblCount;
        private Button btnNone;
        private Button btnAll;
        private TextBox txtSearch;
        private DataGridView grid;
        private DataGridViewCheckBoxColumn colSelected;
        private DataGridViewTextBoxColumn colSchema;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewComboBoxColumn colAction;
        private DataGridViewButtonColumn colColumns;
        private DataGridViewTextBoxColumn colColumnsInfo;
        private DataGridViewCheckBoxColumn colGrant;
    }
}
