using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Compact_RAM_Cleaner
{
    public class CustomRadioButton : RadioButton
    {
        readonly Color _checkedColor = Color.FromArgb(117, 162, 247);
        readonly Color _uncheckedColor = Color.FromArgb(41, 42, 47);

        protected override void OnMouseEnter(EventArgs eventargs)
        {
            base.OnMouseEnter(eventargs);
            ForeColor = SystemColors.Control;
            Refresh();
        }

        protected override void OnMouseLeave(EventArgs eventargs)
        {
            base.OnMouseLeave(eventargs);
            ForeColor = SystemColors.ControlDark;
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var backgroundSize = 15f;
            var checkSize = 8f;
            var backgroundRect = new RectangleF
            {
                X = 1,
                Y = (Height - backgroundSize) / 2,
                Width = backgroundSize,
                Height = backgroundSize,
            };
            var checkRect = new RectangleF
            {
                X = backgroundRect.X + ((backgroundRect.Width - checkSize) / 2),
                Y = (Height - checkSize) / 2,
                Width = checkSize,
                Height = checkSize,
            };

            using (var backgroundBrush = new SolidBrush(_uncheckedColor))
            using (var textBrush = new SolidBrush(ForeColor))
            {
                g.Clear(BackColor);
                g.FillEllipse(backgroundBrush, backgroundRect);

                if (Checked)
                {
                    backgroundBrush.Color = _checkedColor;
                    g.FillEllipse(backgroundBrush, checkRect);
                }

                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                g.DrawString(Text, Font, textBrush, backgroundSize + 4, (Height - TextRenderer.MeasureText(Text, Font).Height) / 2);
            }
        }
    }
}
