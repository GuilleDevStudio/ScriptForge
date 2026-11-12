namespace ScriptForge;

/// <summary>Paleta y estilos comunes. Cambia los colores aquí para retocar toda la app.</summary>
public static class Theme
{
    public static readonly Color Navy = Color.FromArgb(31, 42, 68);
    public static readonly Color Accent = Color.FromArgb(47, 111, 237);
    public static readonly Color AccentHover = Color.FromArgb(37, 92, 200);
    public static readonly Color Background = Color.FromArgb(245, 247, 250);
    public static readonly Color Border = Color.FromArgb(226, 232, 240);
    public static readonly Color Text = Color.FromArgb(30, 41, 59);
    public static readonly Color Muted = Color.FromArgb(100, 116, 139);
    public static readonly Color Danger = Color.FromArgb(220, 38, 38);
    public static readonly Color SelectedRow = Color.FromArgb(232, 240, 254);
    public static readonly Color HeaderRow = Color.FromArgb(241, 245, 249);

    public static void Primary(Button b)
    {
        b.FlatStyle = FlatStyle.Flat;
        b.FlatAppearance.BorderSize = 0;
        b.FlatAppearance.MouseOverBackColor = AccentHover;
        b.BackColor = Accent;
        b.ForeColor = Color.White;
        b.Cursor = Cursors.Hand;
        b.UseVisualStyleBackColor = false;
    }

    public static void Secondary(Button b)
    {
        b.FlatStyle = FlatStyle.Flat;
        b.FlatAppearance.BorderSize = 1;
        b.FlatAppearance.BorderColor = Border;
        b.FlatAppearance.MouseOverBackColor = HeaderRow;
        b.BackColor = Color.White;
        b.ForeColor = Text;
        b.Cursor = Cursors.Hand;
        b.UseVisualStyleBackColor = false;
    }

    public static void StyleGrid(DataGridView g)
    {
        g.EnableHeadersVisualStyles = false;
        g.BorderStyle = BorderStyle.None;
        g.BackgroundColor = Color.White;
        g.GridColor = Border;
        g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        g.ColumnHeadersHeight = 38;
        g.RowHeadersVisible = false;
        g.AllowUserToResizeRows = false;
        g.RowTemplate.Height = 32;

        var h = g.ColumnHeadersDefaultCellStyle;
        h.BackColor = HeaderRow;
        h.ForeColor = Muted;
        h.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        h.SelectionBackColor = HeaderRow;
        h.SelectionForeColor = Muted;

        g.DefaultCellStyle.BackColor = Color.White;
        g.DefaultCellStyle.ForeColor = Text;
        g.DefaultCellStyle.SelectionBackColor = SelectedRow;
        g.DefaultCellStyle.SelectionForeColor = Text;
    }

    /// <summary>Pestañas planas a todo el ancho con subrayado de color en la activa.</summary>
    public static void StyleTabs(TabControl t)
    {
        t.DrawMode = TabDrawMode.OwnerDrawFixed;
        t.SizeMode = TabSizeMode.Fixed;
        t.Padding = new Point(0, 0);

        bool fitting = false;
        void FitTabs()
        {
            if (fitting) return;   // cambiar ItemSize vuelve a lanzar Resize: evita el bucle
            var size = new Size(Math.Max(60, t.Width / Math.Max(1, t.TabCount) - 1), 40);
            if (t.ItemSize == size) return;
            fitting = true;
            try { t.ItemSize = size; }
            finally { fitting = false; }
        }
        t.Resize += (_, _) => FitTabs();
        FitTabs();

        t.DrawItem += (s, e) =>
        {
            var tab = (TabControl)s;
            bool sel = e.Index == tab.SelectedIndex;
            var r = e.Bounds;

            using (var bg = new SolidBrush(sel ? Color.White : HeaderRow))
                e.Graphics.FillRectangle(bg, r);

            if (sel)
            {
                using var line = new SolidBrush(Accent);
                e.Graphics.FillRectangle(line, r.Left, r.Bottom - 3, r.Width, 3);
            }

            TextRenderer.DrawText(e.Graphics, tab.TabPages[e.Index].Text,
                new Font("Segoe UI", 9.5F, sel ? FontStyle.Bold : FontStyle.Regular),
                r, sel ? Accent : Muted,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        };
    }
}
