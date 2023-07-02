using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Compact_RAM_Cleaner.Helpers;
using static Compact_RAM_Cleaner.Memory;

namespace Compact_RAM_Cleaner
{
    public partial class Form1 : FormWithShadow
    {
        readonly bool _pageFileEnabled;
        readonly TrayIcon _trayIcon;
        readonly MemoryUsageVisualization _usageVisualization;
        readonly Settings _settings;

        public bool ClearCache
        {
            get => ClearButton2.Checked;
            set => ClearButton2.Checked = value;
        }

        public bool StartMinimized;

        public Form1()
        {
            InitializeComponent();
            GetAllControlsOfType<Panel>(this).ForEach(x => x.EnableDoubleBuffer());
            _pageFileEnabled = GetPageFileMaxSize() > 0;
            _trayIcon = new TrayIcon(NotifyIcon1, () => { Show(); WindowState = FormWindowState.Normal; });
            _usageVisualization = new MemoryUsageVisualization(MainPanel, _trayIcon, ClearMemory);
            _settings = new Settings(this, _trayIcon);

            InitializeForm();
            InitializeTitle();
            InitializeClearButton();

            _trayIcon.Update();
            UpdateForm();
            UpdateValues();
        }

        #region Init

        #region Form
        void InitializeForm()
        {
            Icon = Icon.ExtractAssociatedIcon(Paths.ApplicationExe);

            if (!StartMinimized)
                StartMinimized = Environment.GetCommandLineArgs().Any(x => x.EndsWith("silent"));

            Load += async (s, e) =>
            {
                if (StartMinimized)
                {
                    Hide();
                    Opacity = 1;
                }
                else
                {
                    WindowState = FormWindowState.Normal;
                    await StartAnimation(this);
                }
            };

            Resize += async (s, e) =>
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    Hide();
                    ClearTypePanel.Visible = false;
                }
                else await StartAnimation(this);
            };

            KeyDown += (s, e) =>
            {
                if (e.KeyValue == (char)Keys.Escape)
                    ClearTypePanel.Visible = false;
            };

            Paint += (s, e) =>
            {
                using (var pen = new Pen(BackColor, 2))
                    e.Graphics.DrawLine(pen, 0, Height, Width, Height);
            };

            var controls = new List<Control> { TitlePanel, AppName };

            new List<Panel> { Panel1, Panel2 }.ForEach(x =>
            {
                x.Paint += (s, e) =>
                {
                    using (var brush = new SolidBrush(Color.FromArgb(56, 57, 62)))
                        e.Graphics.FillRoundedRectangle(brush, new Rectangle(1, 1, x.Width - 2, x.Height - 2), 16);
                };

                controls.Add(x);
                controls.AddRange(x.Controls.OfType<Label>());
            });

            controls.ForEach(x =>
            {
                x.MouseDown += (s, a) =>
                {
                    x.Capture = false;
                    Capture = false;
                    Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                    base.WndProc(ref m);
                };
            });
        }
        #endregion

        #region Title
        void InitializeTitle()
        {
            ClosePanel.Click += (s, e) => ExitAnimationAndClose(this);

            ClosePanel.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(200, 200, 200)))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawLine(pen, 7, 7, ClosePanel.Width - 7, ClosePanel.Height - 7);
                    e.Graphics.DrawLine(pen, 7, ClosePanel.Height - 7, ClosePanel.Width - 7, 7);
                }
            };

            SettingsPanel.Click += (s, e) =>
            {
                ClearTypePanel.Visible = false;
                _settings.ShowDialog();
            };

            SettingsPanel.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(160, 160, 160)))
                {
                    e.Graphics.DrawLine(pen, 5, 7, ClosePanel.Width - 5, 7);
                    e.Graphics.DrawLine(pen, 5, 12, ClosePanel.Width - 5, 12);
                    e.Graphics.DrawLine(pen, 5, 17, ClosePanel.Width - 5, 17);
                }
            };

            MinimizePanel.Click += async (s, e) =>
            {
                await ExitAnimation(this);
                WindowState = FormWindowState.Minimized;
            };

            MinimizePanel.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(160, 160, 160)))
                    e.Graphics.DrawLine(pen, 5, 12, MinimizePanel.Width - 5, 12);
            };

            var defaultColor = ClosePanel.BackColor;
            new List<Panel> { ClosePanel, SettingsPanel, MinimizePanel }.ForEach(x =>
            {
                x.MouseEnter += (s, e) => x.BackColor = Color.FromArgb(defaultColor.R + 20, defaultColor.G + 20, defaultColor.B + 20);
                x.MouseLeave += (s, e) => x.BackColor = defaultColor;
            });
        }
        #endregion

        #region ClearButton
        void InitializeClearButton()
        {
            ClearButton.Click += (s, e) => ClearMemory();

            var defaultColor = BackColor;
            var highlightColor = Color.FromArgb(defaultColor.R + 10, defaultColor.G + 10, defaultColor.B + 10);
            var color1 = defaultColor;
            var color2 = defaultColor;

            ClearButton.MouseEnter += (s, e) =>
            {
                color1 = highlightColor;
                color2 = highlightColor;
                ClearButton.Refresh();
            };

            ClearButton.MouseLeave += (s, e) =>
            {
                color1 = defaultColor;
                color2 = defaultColor;
                ClearButton.Refresh();
            };

            ClearButton.Paint += (s, e) =>
            {
                using (var brush = new SolidBrush(color1))
                using (var font = new Font("Tahoma", 10F))
                using (var textBrush = new SolidBrush(Color.FromArgb(160, 160, 160)))
                {
                    e.Graphics.FillRoundedRectangle(brush, new Rectangle(1, 1, ClearButton.Width - 2, ClearButton.Height - 2), 10);
                    e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    e.Graphics.DrawString(Translations.GetString("Clear"), font, textBrush, 6, 3);
                }
            };

            ExpandPanel.MouseEnter += (s, e) =>
            {
                color2 = highlightColor;
                ExpandPanel.Refresh();
            };

            ExpandPanel.MouseLeave += (s, e) =>
            {
                color2 = defaultColor;
                ExpandPanel.Refresh();
            };

            ExpandPanel.Paint += (s, e) =>
            {
                using (var brush = new SolidBrush(color2))
                    e.Graphics.FillRoundedRectangle(brush, new Rectangle(1, 1, ExpandPanel.Width - 2, ExpandPanel.Height - 2), 8);

                int size = 4;
                int x = ExpandPanel.Width / 2;
                int y = ExpandPanel.Height / 2 - 1 + size;
                int sqrt = (int)(size * Math.Sqrt(3));
                var points = new Point[] { new Point(x, y), new Point(x - size, y - sqrt), new Point(x + size, y - sqrt), };

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(Color.FromArgb(160, 160, 160)))
                    e.Graphics.FillPolygon(brush, points);
            };

            ExpandPanel.Click += (s, e) => ClearTypePanel.Visible = !ClearTypePanel.Visible;

            ClearTypePanel.Paint += (s, e) =>
            {
                using (var brush = new SolidBrush(Color.FromArgb(56, 57, 62)))
                    e.Graphics.FillRoundedRectangle(brush, new Rectangle(0, 0, ClearTypePanel.Width, ClearTypePanel.Height), 10);
            };

            ClearButton1.Click += async (s, e) =>
            {
                await Task.Delay(20);
                ClearTypePanel.Visible = false;
                SaveSystem.Delete("ClearCache");
            };

            ClearButton2.Click += async (s, e) =>
            {
                await Task.Delay(20);
                ClearTypePanel.Visible = false;
                SaveSystem.Save("ClearCache", "true");
            };

        }
        #endregion

        #endregion

        #region Public
        public void UpdateStrings()
        {
            PhysicalMemoryLabel.Text = Translations.GetString("PhysicalMemory");
            PageFileLabel.Text = Translations.GetString("PageFile");

            ClearButton1.Text = Translations.GetString("RAM");
            ClearButton2.Text = Translations.GetString("RAMAndCache");

            using (var font = new Font("Tahoma", 8.25F))
            {
                ClearButton1.Size = new Size(TextRenderer.MeasureText(ClearButton1.Text, font).Width + 24, ClearButton1.Height);
                ClearButton2.Size = new Size(TextRenderer.MeasureText(ClearButton2.Text, font).Width + 24, ClearButton2.Height);
            }

            ClearButton.Size = new Size(Translations.GetString("Clear").Length < 6 ? 58 : 83, ClearButton.Height);
            ClearButton.Location = new Point((ClearButton.Parent.Width - ClearButton.Width) / 2, ClearButton.Location.Y);
            ClearButton.Refresh();

            ExpandPanel.Location = new Point(ClearButton.Width - ExpandPanel.Width - 2, ExpandPanel.Location.Y);

            ClearTypePanel.Location = new Point(ClearButton.Location.X + ClearButton.Width - 22, ClearTypePanel.Location.Y);
            ClearTypePanel.Size = new Size(Width - ClearTypePanel.Location.X - 12, ClearTypePanel.Height);

            UpdateForm();
        }
        #endregion

        #region Private
        void ClearMemory()
        {
            ClearTypePanel.Visible = false;
            Cleaner.ClearRAM();

            if (ClearCache)
                Cleaner.ClearCache();
        }

        async void UpdateValues()
        {
            while (true)
            {
                _trayIcon.Update();

                if (WindowState == FormWindowState.Normal)
                    UpdateForm();

                await Task.Delay(1000);
            }
        }

        void UpdateForm()
        {
            _usageVisualization.Update();

            var max = BytesToGigabytes(_trayIcon.TotalMemoryInBytes);
            var usage = BytesToGigabytes(_trayIcon.TotalMemoryInBytes - _trayIcon.AvailableMemoryInBytes);
            PhysicalMemoryData.Text = $"{usage:0.0} / {max:0.0} {Translations.GetString("GB")} ({_trayIcon.CurrentUsageString}%)";

            if (!_pageFileEnabled) return;
            var maximumSize = GetPageFileMaxSize();
            if (maximumSize == 0) return;

            var currentUsage = GetPageFileUsage();
            PageFileData.Text = $"{currentUsage:0.0}/{maximumSize:0.0} {Translations.GetString("GB")} ({currentUsage / maximumSize * 100}%)";
        }
        #endregion
    }
}
