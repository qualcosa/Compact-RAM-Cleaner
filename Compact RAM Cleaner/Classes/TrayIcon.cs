using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Compact_RAM_Cleaner.Memory;

namespace Compact_RAM_Cleaner
{
    public class TrayIcon : IMemoryUsageProvider
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)] extern static bool DestroyIcon(IntPtr handle);
        readonly float[] _positions = new float[] { 0.0f, 0.5f, 1f };
        readonly NotifyIcon _icon;

        public int CurrentUsage { get; private set; }
        public string CurrentUsageString { get; private set; }
        public ulong AvailableMemoryInBytes { get; private set; }
        public ulong TotalMemoryInBytes { get; private set; }

        Color _textColor = Color.White;
        public Color TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                Update();
            }
        }

        bool _textShadow = true;
        public bool TextShadow
        {
            get => _textShadow;
            set
            {
                _textShadow = value;
                Update();
            }
        }

        Color _textShadowColor = Color.Black;
        public Color TextShadowColor
        {
            get => _textShadowColor;
            set
            {
                _textShadowColor = value;
                Update();
            }
        }

        readonly Color[] _colors = new Color[] { Color.FromArgb(26, 115, 232), Color.FromArgb(113, 83, 141), Color.FromArgb(200, 50, 50) };
        public Color[] Colors
        {
            get => _colors;
            set
            {
                _colors[0] = value[0];
                _colors[1] = value[1];
                _colors[2] = value[2];
                Update();
            }
        }

        public Action OnMiddleMouseClick = () => Cleaner.ClearRAM();

        public TrayIcon(NotifyIcon icon, Action onClick)
        {
            _icon = icon;
            _icon.ContextMenuStrip = new ContextMenuStrip();

            _icon.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                    onClick();

                else if (e.Button == MouseButtons.Middle)
                    OnMiddleMouseClick?.Invoke();
            };

            new List<Action>
            {
                () => Cleaner.ClearRAM(),
                () => { Cleaner.ClearRAM(); Cleaner.ClearCache(); },
                () => Process.Start("taskmgr"),
                () => Application.Exit(),
            }.ForEach(x =>
            {
                var menu = new ToolStripMenuItem { Text = Translations.GetString($"Tray{_icon.ContextMenuStrip.Items.Count + 1}") };
                menu.Click += (s, e) => x();
                _icon.ContextMenuStrip.Items.Add(menu);
            });

            TotalMemoryInBytes = TotalPhysicalMemory;
        }

        public void Update()
        {
            AvailableMemoryInBytes = AvailablePhysicalMemory;
            CurrentUsage = (int)((TotalMemoryInBytes - AvailableMemoryInBytes) * 100 / TotalMemoryInBytes);
            CurrentUsageString = CurrentUsage.ToString();

            using (var bitmap = new Bitmap(16, 16))
            using (var g = Graphics.FromImage(bitmap))
            using (var textBrush = new SolidBrush(TextColor))
            using (var textShadowBrush = new SolidBrush(TextShadowColor))
            using (var lgb = new LinearGradientBrush(new Rectangle(1, 1, 15, 15), Colors[0], Colors[2], 270F))
            using (var font = new Font("Tahoma", 8F))
            using (var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                lgb.InterpolationColors = new ColorBlend(3) { Colors = Colors, Positions = _positions };
                g.FillRectangle(lgb, 0, 15 - 15 * CurrentUsage / 100, 15, 15);

                g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

                if (TextShadow)
                    g.DrawString(CurrentUsageString, font, textShadowBrush, 8, 9, sf);
                g.DrawString(CurrentUsageString, font, textBrush, 8, 8, sf);

                var icon = Icon.FromHandle(bitmap.GetHicon());
                _icon.Icon = icon;
                DestroyIcon(icon.Handle);
            }
        }

        public void UpdateStrings()
        {
            for (int i = 0; i < _icon.ContextMenuStrip.Items.Count; i++)
                _icon.ContextMenuStrip.Items[i].Text = Translations.GetString($"Tray{i + 1}");
        }
    }
}
