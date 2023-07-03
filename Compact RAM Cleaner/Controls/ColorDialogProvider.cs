using System;
using System.Drawing;
using System.Windows.Forms;

namespace Compact_RAM_Cleaner
{
    public class ColorDialogProvider : Panel
    {
        Color _color = Color.FromArgb(160, 160, 160);
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                Invalidate();
                OnColorChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        bool _drawOutline = false;
        readonly Color _outlineColor = Color.FromArgb(117, 162, 247);
        readonly int _outlineThickness = 2;
        readonly int _cornerRadius = 4;

        public event EventHandler OnColorChanged;

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _drawOutline = true;
            Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _drawOutline = false;
            Refresh();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            var cd = new ColorDialog { Color = _color, FullOpen = true };
            if (cd.ShowDialog() == DialogResult.OK)
                Color = cd.Color;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var rect = new Rectangle(2, 2, Width - 4, Height - 4);

            using (var brush = new SolidBrush(_color))
                e.Graphics.FillRoundedRectangle(brush, rect, _cornerRadius);

            if (_drawOutline)
            {
                using (var pen = new Pen(_outlineColor, _outlineThickness))
                    e.Graphics.DrawRoundedRectangle(pen, rect, _cornerRadius);
            }
        }
    }
}
