namespace ScriptForge
{
    partial class CheckListDialog
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
            pnlPrompt = new Panel();
            lblPrompt = new Label();
            pnlTools = new Panel();
            btnNone = new Button();
            btnAll = new Button();
            clb = new CheckedListBox();
            pnlBottom = new Panel();
            btnCancel = new Button();
            btnOk = new Button();
            pnlPrompt.SuspendLayout();
            pnlTools.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPrompt
            // 
            pnlPrompt.BackColor = Color.White;
            pnlPrompt.Controls.Add(lblPrompt);
            pnlPrompt.Dock = DockStyle.Top;
            pnlPrompt.Name = "pnlPrompt";
            pnlPrompt.Size = new Size(400, 56);
            pnlPrompt.TabIndex = 3;
            // 
            // lblPrompt
            // 
            lblPrompt.Dock = DockStyle.Fill;
            lblPrompt.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPrompt.ForeColor = Color.FromArgb(30, 41, 59);
            lblPrompt.Name = "lblPrompt";
            lblPrompt.Padding = new Padding(16, 0, 0, 0);
            lblPrompt.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlTools
            // 
            pnlTools.BackColor = Color.FromArgb(245, 247, 250);
            pnlTools.Controls.Add(btnNone);
            pnlTools.Controls.Add(btnAll);
            pnlTools.Dock = DockStyle.Top;
            pnlTools.Location = new Point(0, 56);
            pnlTools.Name = "pnlTools";
            pnlTools.Size = new Size(400, 44);
            pnlTools.TabIndex = 2;
            // 
            // btnNone
            // 
            btnNone.Location = new Point(110, 7);
            btnNone.Name = "btnNone";
            btnNone.Size = new Size(90, 30);
            btnNone.TabIndex = 1;
            btnNone.Text = "Ninguno";
            btnNone.UseVisualStyleBackColor = true;
            btnNone.Click += btnNone_Click;
            // 
            // btnAll
            // 
            btnAll.Location = new Point(14, 7);
            btnAll.Name = "btnAll";
            btnAll.Size = new Size(90, 30);
            btnAll.TabIndex = 0;
            btnAll.Text = "Todos";
            btnAll.UseVisualStyleBackColor = true;
            btnAll.Click += btnAll_Click;
            // 
            // clb
            // 
            clb.BorderStyle = BorderStyle.None;
            clb.CheckOnClick = true;
            clb.Dock = DockStyle.Fill;
            clb.Font = new Font("Segoe UI", 10F);
            clb.FormattingEnabled = true;
            clb.IntegralHeight = false;
            clb.Name = "clb";
            clb.TabIndex = 0;
            // 
            // pnlBottom
            // 
            pnlBottom.BackColor = Color.FromArgb(245, 247, 250);
            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Controls.Add(btnOk);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(400, 56);
            pnlBottom.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(294, 10);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(92, 36);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(190, 10);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(98, 36);
            btnOk.TabIndex = 0;
            btnOk.Text = "Aceptar";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // CheckListDialog
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(400, 480);
            Controls.Add(clb);
            Controls.Add(pnlBottom);
            Controls.Add(pnlTools);
            Controls.Add(pnlPrompt);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CheckListDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Seleccionar";
            pnlPrompt.ResumeLayout(false);
            pnlTools.ResumeLayout(false);
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPrompt;
        private Label lblPrompt;
        private Panel pnlTools;
        private Button btnNone;
        private Button btnAll;
        private CheckedListBox clb;
        private Panel pnlBottom;
        private Button btnCancel;
        private Button btnOk;
    }
}
