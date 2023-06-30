using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Compact_RAM_Cleaner
{
    public class MemoryUsageVisualization
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport("Gdi32.dll", EntryPoint = "DeleteObject")]
        static extern IntPtr DeleteObject(IntPtr hObject);

        readonly Panel _panel;
        Color _backgroundFillColor;
        readonly IMemoryUsageProvider _memoryUsageProvider;

        public MemoryUsageVisualization(Panel panel, IMemoryUsageProvider memoryUsageProvider, Action onClick = null)
        {
            _panel = panel;
            _backgroundFillColor = _panel.BackColor;
            _memoryUsageProvider = memoryUsageProvider;

            PaintPanel();

            if (onClick != null)
            {
                var defaultColor = _backgroundFillColor;

                _panel.Click += (s, e) => onClick();

                _panel.MouseEnter += (s, e) =>
                {
                    _backgroundFillColor = Color.FromArgb(defaultColor.R + 10, defaultColor.G + 10, defaultColor.B + 10);
                    _panel.Refresh();
                };

                _panel.MouseLeave += (s, e) =>
                {
                    _backgroundFillColor = defaultColor;
                    _panel.Refresh();
                };
            }
        }

        public void Update() => _panel.Refresh();

        void PaintPanel()
        {
            int width = _panel.Width - 2;
            int height = _panel.Height - 2;

            _panel.Paint += (s, e) =>
            {
                IntPtr ptr = CreateRoundRectRgn(0, 0, _panel.Width, _panel.Height, _panel.Width, _panel.Width);
                _panel.Region = Region.FromHrgn(ptr);
                DeleteObject(ptr);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var backgroundPen = new Pen(Color.FromArgb(32, 33, 36), 2))
                using (var background = new SolidBrush(_backgroundFillColor))
                using (var pen = new Pen(Color.FromArgb(117, 162, 247), 4) { StartCap = LineCap.Round, EndCap = LineCap.Round })
                {
                    e.Graphics.FillPie(background, 5, 5, width - 10, height - 10, -90, 360);
                    e.Graphics.DrawArc(backgroundPen, 4, 4, width - 8, height - 8, -90, 360);
                    e.Graphics.DrawArc(pen, 4, 4, width - 8, height - 8, -90, (int)Math.Round(360.0 / 100 * _memoryUsageProvider.CurrentUsage));
                }

                using (var font = new Font("Tahoma", 12F))
                using (var font2 = new Font("Tahoma", 7F))
                using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    GetPositions(out var usagePosition, out var percentPosition);
                    e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    e.Graphics.DrawString(_memoryUsageProvider.CurrentUsageString, font, SystemBrushes.Control, usagePosition, _panel.Height / 2, sf);
                    e.Graphics.DrawString("%", font2, SystemBrushes.ControlDark, percentPosition, _panel.Height / 2 + 2, sf);

                }
            };
        }

        void GetPositions(out int usagePosition, out int percentPosition)
        {
            usagePosition = _panel.Width / 2;
            percentPosition = _panel.Width / 2;

            if (_memoryUsageProvider.CurrentUsage > 10 && _memoryUsageProvider.CurrentUsage < 100)
            {
                usagePosition += -1;
                percentPosition += 15;
            }
            else if (_memoryUsageProvider.CurrentUsage < 10)
            {
                percentPosition += 10;
            }
            else
            {
                usagePosition += -2;
                percentPosition += 19;
            }
        }
    }
}
