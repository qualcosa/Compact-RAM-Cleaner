using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static Compact_RAM_Cleaner.Helpers;
using static Compact_RAM_Cleaner.SaveSystem;

namespace Compact_RAM_Cleaner
{
    public partial class Settings : FormWithShadow
    {
        readonly Form1 _form1;
        readonly TrayIcon _trayIcon;
        readonly Dictionary<Panel, Color[]> _themes = new Dictionary<Panel, Color[]>();

        public Settings(Form1 form1, TrayIcon icon)
        {
            InitializeComponent();
            GetAllControlsOfType<Panel>(this).ForEach(x => x.EnableDoubleBuffer());
            _form1 = form1;
            _trayIcon = icon;

            InitializeForm();
            InitializeTabs();

            InitializeTab1();
            InitializeTab2();
            InitializeTab3();

            CheckSettings();
            InitializeThemes();
        }

        #region Init

        #region Form
        void InitializeForm()
        {
            Size = new Size(420, 320);
            Icon = Icon.ExtractAssociatedIcon(Paths.ApplicationExe);

            Paint += (s, e) =>
            {
                using (var pen = new Pen(BackColor, 2))
                    e.Graphics.DrawLine(pen, 0, Height, Width, Height);
            };

            Load += async (s, e) => await StartAnimation(this);

            KeyDown += (s, e) =>
            {
                if (e.KeyValue == (char)Keys.Escape)
                    ExitAnimationAndClose(this);
            };

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

            var defaultColor = ClosePanel.BackColor;
            ClosePanel.MouseEnter += (s, e) => ClosePanel.BackColor = Color.FromArgb(defaultColor.R + 20, defaultColor.G + 20, defaultColor.B + 20);
            ClosePanel.MouseLeave += (s, e) => ClosePanel.BackColor = defaultColor;

            foreach (var panel in Controls.OfType<Panel>())
            {
                panel.MouseDown += (s, e) =>
                {
                    panel.Capture = false;
                    Capture = false;
                    Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                    base.WndProc(ref m);
                };
            }

            LabelResetSettings.Click += (s, e) =>
            {
                if (MessageBox.Show(Translations.GetString("ResetSettings") + "?", "Compact RAM Cleaner", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (File.Exists(Paths.IniFile))
                        File.Delete(Paths.IniFile);

                    Cmd($"taskkill /f /im \"{ExeName}\" & \"{Paths.ApplicationExe}\"");
                    Environment.Exit(0);
                }
            };
        }
        #endregion

        #region Tabs
        void InitializeTabs()
        {
            TabsPanel.Paint += (s, e) =>
            {
                using (var pen = new Pen(TabsPanel.BackColor, 2))
                    e.Graphics.DrawLine(pen, 0, TabsPanel.Height, TabsPanel.Width, TabsPanel.Height);
            };

            var selectors = TabsPanel.Controls.OfType<Panel>().Where(x => x.Name.Contains("TabSelect")).OrderBy(x => x.Name);
            var tabs = Controls.OfType<Panel>().Where(x => x.Name.Contains("TabPanel")).OrderBy(x => x.Name);
            var tabsDict = selectors.Zip(tabs, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);

            Panel _currentTab = null;
            var location = tabsDict.ElementAt(0).Value.Location;

            foreach (var item in tabsDict)
            {
                var selector = item.Key;
                var tab = item.Value;

                tab.Location = location;

                selector.MouseEnter += (s, e) => selector.BackColor = Color.FromArgb(68, 69, 74);
                selector.MouseLeave += (s, e) => selector.BackColor = tab.Visible ? Color.FromArgb(56, 57, 62) : Color.FromArgb(32, 33, 36);

                selector.Click += (s, e) =>
                {
                    SwitchTab(selector, tab);
                    Refresh();
                };

                selector.Paint += (s, e) =>
                {
                    using (var font = new Font("Tahoma", 8.25F))
                    using (var pen = new Pen(Color.FromArgb(117, 162, 247), 5))
                    using (var brush = new SolidBrush(SystemColors.ControlDark))
                    {
                        if (tab.Visible)
                            e.Graphics.DrawLine(pen, 0, 0, 0, selector.Height);

                        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                        e.Graphics.DrawString(Translations.GetString(selector.Name), font, brush, 10, 8);
                    }
                };
            }

            void SwitchTab(Panel selector, Panel tab)
            {
                if (_currentTab == tab) return;

                foreach (var item in tabsDict)
                {
                    if (selector != item.Key)
                    {
                        item.Key.BackColor = Color.FromArgb(32, 34, 37);
                        item.Value.Visible = false;
                    }
                    else
                    {
                        selector.BackColor = Color.FromArgb(67, 69, 74);
                        tab.Visible = true;
                    }
                }

                _currentTab = tab;
            }
        }
        #endregion

        #region Tab1
        void InitializeTab1()
        {
            RadioButtonEN.Click += (s, e) =>
            {
                Translations.Language = Language.English;
                UpdateTranslation();
                Save("Language", "en");
            };

            RadioButtonRU.Click += (s, e) =>
            {
                Translations.Language = Language.Russian;
                UpdateTranslation();
                Save("Language", "ru");
            };

            RadioButtonUA.Click += (s, e) =>
            {
                Translations.Language = Language.Ukrainian;
                UpdateTranslation();
                Save("Language", "ua");
            };

            AutoUpdateCheck.Click += (s, e) =>
            {
                if (AutoUpdateCheck.Checked)
                    Save("AutoUpdate", "true");
                else
                    Delete("AutoUpdate");
            };

            AutorunCheck.Click += (s, e) =>
            {
                using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"))
                {
                    if (AutorunCheck.Checked)
                        key.SetValue("Compact RAM Cleaner", $"\"{Paths.ApplicationExe}\" silent");
                    else
                        key.DeleteValue("Compact RAM Cleaner", false);
                }
            };

            AutoClearCheck.Click += (s, e) =>
            {
                Numeric1.Enabled = !AutoClearCheck.Checked;

                if (AutoClearCheck.Checked)
                {
                    Cleaner.EnableAutoCleaner((int)Numeric1.Value);
                    Save("AutoCleanerValue", $"{Numeric1.Value}");
                }
                else
                {
                    Cleaner.DisableAutoCleaner();
                    Delete("AutoCleanerValue");
                }
            };

            CleaningResultsCheck.Click += (s, e) =>
            {
                Popup.ShowCleaningResult = CleaningResultsCheck.Checked;

                if (CleaningResultsCheck.Checked)
                    Delete("NotifyEnabled");
                else
                    Save("NotifyEnabled", "false");
            };

            StartMinimizedCheck.Click += (s, e) =>
            {
                if (StartMinimizedCheck.Checked)
                    Save("StartMinimized", "true");
                else
                    Delete("StartMinimized");
            };
        }
        #endregion

        #region Tab2
        void InitializeTab2()
        {
            _themes.Add(TrayTheme0, new Color[] { CustomTrayColor1.Color, CustomTrayColor2.Color, CustomTrayColor3.Color });
            _themes.Add(TrayTheme1, new Color[] { Color.FromArgb(26, 115, 232), Color.FromArgb(113, 83, 141), Color.FromArgb(200, 50, 50) });
            _themes.Add(TrayTheme2, new Color[] { Color.FromArgb(30, 255, 0), Color.FromArgb(255, 246, 0), Color.FromArgb(200, 0, 0) });
            _themes.Add(TrayTheme3, new Color[] { Color.FromArgb(0, 130, 250), Color.FromArgb(80, 190, 60), Color.FromArgb(250, 200, 50) });

            groupPanel3.Paint += (s, e) =>
            {
                using (var pen = new Pen(SystemColors.ControlDarkDark))
                    e.Graphics.DrawLine(pen, 28, LabelMouseClick.Location.Y + (LabelMouseClick.Height / 2), groupPanel3.Width - 28, LabelMouseClick.Location.Y + (LabelMouseClick.Height / 2));
            };

            groupPanel4.Paint += (s, e) =>
            {
                using (var pen = new Pen(SystemColors.ControlDarkDark))
                    e.Graphics.DrawLine(pen, 28, LabelCustomStyle.Location.Y + (LabelCustomStyle.Height / 2), groupPanel3.Width - 28, LabelCustomStyle.Location.Y + (LabelCustomStyle.Height / 2));
            };

            ColorsPanel.Paint += (s, e) =>
            {
                using (var pen = new Pen(SystemColors.ControlDarkDark))
                    e.Graphics.DrawLine(pen, 2, 2, 2, ColorsPanel.Height - 2);
            };

            CheckBoxTextShadow.Click += (s, e) =>
            {
                _trayIcon.TextShadow = CheckBoxTextShadow.Checked;
                TrayTextShadowColor.Visible = CheckBoxTextShadow.Checked;

                if (CheckBoxTextShadow.Checked)
                    Delete("TrayTextShadow");
                else
                    Save("TrayTextShadow", "false");
            };

            TrayTextColor.OnColorChanged += (s, e) =>
            {
                var color = TrayTextColor.Color;
                _trayIcon.TextColor = color;
                Save("TrayTextColor", $"{color.R};{color.G};{color.B};");
            };

            TrayTextShadowColor.OnColorChanged += (s, e) =>
            {
                var color = TrayTextShadowColor.Color;
                _trayIcon.TextShadowColor = color;
                Save("TrayTextShadowColor", $"{color.R};{color.G};{color.B};");
            };

            radioButton1.Click += (s, e) =>
            {
                _trayIcon.OnMiddleMouseClick = () => Cleaner.ClearRAM();
                Delete("MiddleMouseClick");
            };

            radioButton2.Click += (s, e) =>
            {
                _trayIcon.OnMiddleMouseClick = () => Process.Start("taskmgr");
                Save("MiddleMouseClick", "taskmgr");
            };
        }
        #endregion

        #region Tab3
        void InitializeTab3()
        {
            AboutIcon.Paint += (s, e) => e.Graphics.DrawIcon(Icon, (AboutIcon.Width - Icon.Width) / 2, (AboutIcon.Height - Icon.Height) / 2);

            AboutLink1.Click += (s, e) => Process.Start("https://github.com/qualcosa/Compact-RAM-Cleaner");
            AboutLink2.Text = $"{Translations.GetString("Version")}: {Application.ProductVersion.Remove(Application.ProductVersion.Length - 2)}";
            AboutLink2.Click += (s, e) => Process.Start("https://github.com/qualcosa/Compact-RAM-Cleaner/releases");
            AboutLink3.Click += (s, e) =>
            {
                if (UpdateSystem.IsUpdateAvailable(true))
                    UpdateSystem.UpdateAndRestart();
            };

            AboutLink4.Click += (s, e) => Process.Start("https://github.com/qualcosa");
        }
        #endregion

        #endregion

        #region Private
        void CheckSettings()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"))
            {
                if (key?.GetValue("Compact RAM Cleaner") != null)
                    AutorunCheck.Checked = true;
            }

            if (!SaveSystem.Load()) return;

            if (TryGetValue("Language", out var language))
            {
                switch (language)
                {
                    case "ru":
                        Translations.Language = Language.Russian;
                        RadioButtonRU.Checked = true;
                        break;
                    case "ua":
                        Translations.Language = Language.Ukrainian;
                        RadioButtonUA.Checked = true;
                        break;
                    default:
                        Translations.Language = Language.English;
                        RadioButtonEN.Checked = true;
                        break;
                }

                UpdateTranslation();
            }

            if (GetValue("ClearCache") == "true")
                _form1.ClearCache = true;

            if (GetValue("AutoUpdate") == "true")
            {
                AutoUpdateCheck.Checked = true;
                if (UpdateSystem.IsUpdateAvailable(false))
                    UpdateSystem.UpdateAndRestart();
            }

            if (TryGetValue("AutoCleanerValue", out var autoCleanerValue))
            {
                AutoClearCheck.Checked = true;

                try
                {
                    Numeric1.Value = Convert.ToInt32(autoCleanerValue);
                }
                catch { }

                Numeric1.Enabled = false;
                Cleaner.EnableAutoCleaner((int)Numeric1.Value);
            }

            if (GetValue("NotifyEnabled") == "false")
            {
                CleaningResultsCheck.Checked = false;
                Popup.ShowCleaningResult = false;
            }

            if (GetValue("StartMinimized") == "true")
            {
                StartMinimizedCheck.Checked = true;
                _form1.StartMinimized = true;
            }



            if (TryGetValue("TrayTextColor", out var trayTextColor))
            {
                if (TryGetColor(trayTextColor, out var color))
                    TrayTextColor.Color = color;
            }

            if (GetValue("TrayTextShadow") == "false")
            {
                CheckBoxTextShadow.Checked = false;
                TrayTextShadowColor.Visible = false;
                _trayIcon.TextShadow = false;
            }

            if (TryGetValue("TrayTextShadowColor", out var trayTextShadowColor))
            {
                if (TryGetColor(trayTextShadowColor, out var color))
                    TrayTextShadowColor.Color = color;
            }

            if (GetValue("MiddleMouseClick") == "taskmgr")
            {
                radioButton2.Checked = true;
                _trayIcon.OnMiddleMouseClick = () => Process.Start("taskmgr");
            }

            if (TryGetValue("CustomColors", out var colors))
            {
                try
                {
                    Match m = Regex.Match(colors, "([0-9]+);([0-9]+);([0-9]+);([0-9]+);([0-9]+);([0-9]+);([0-9]+);([0-9]+);([0-9]+);");

                    CustomTrayColor1.Color = Color.FromArgb(GetValue(1), GetValue(2), GetValue(3));
                    CustomTrayColor2.Color = Color.FromArgb(GetValue(4), GetValue(5), GetValue(6));
                    CustomTrayColor3.Color = Color.FromArgb(GetValue(7), GetValue(8), GetValue(9));
                    _themes[TrayTheme0] = new Color[] { CustomTrayColor1.Color, CustomTrayColor2.Color, CustomTrayColor3.Color };

                    int GetValue(int id) => Convert.ToInt32(m.Groups[id].Value);
                }
                catch { }
            }

            if (TryGetValue("Theme", out var theme))
            {
                try
                {
                    var value = Convert.ToInt32(theme);
                    var item = _themes.ElementAtOrDefault(value);
                    if (item.Key != null)
                        ApplyTheme(item.Key);
                }
                catch { }
            }
        }

        void InitializeThemes()
        {
            var customColors = new ColorDialogProvider[] { CustomTrayColor1, CustomTrayColor2, CustomTrayColor3 };

            for (int i = 0; i < customColors.Length; i++)
            {
                customColors[i].OnColorChanged += (s, e) =>
                {
                    ApplyTheme(TrayTheme0);
                    Save("Theme", "0");

                    string value = "";
                    foreach (var item in customColors)
                        value += $"{item.Color.R};{item.Color.G};{item.Color.B};";

                    Save("CustomColors", value);
                };
            }

            foreach (var panel in _themes.Keys)
            {
                bool highlight = false;

                panel.MouseEnter += (s, e) =>
                {
                    highlight = true;
                    panel.Refresh();
                };

                panel.MouseLeave += (s, e) =>
                {
                    highlight = false;
                    panel.Refresh();
                };

                if (panel == TrayTheme1)
                {
                    panel.Click += (s, e) =>
                    {
                        ApplyTheme(panel);
                        Delete("Theme");
                    };
                }
                else
                {
                    panel.Click += (s, e) =>
                    {
                        ApplyTheme(panel);
                        Save("Theme", panel.Name.Replace("TrayTheme", ""));
                    };
                }

                panel.Paint += (s, e) =>
                {
                    var colors = _themes[panel];
                    var rect = new Rectangle(1, 1, panel.Width - 2, panel.Height - 2);

                    using (var lgb = new LinearGradientBrush(rect, colors[0], colors[2], 270F))
                    {
                        lgb.InterpolationColors = new ColorBlend(3) { Colors = colors, Positions = new float[] { 0.0f, 0.5f, 1f } };
                        e.Graphics.FillRoundedRectangle(lgb, rect, 6);
                    }

                    bool selected = colors.SequenceEqual(_trayIcon.Colors);

                    if (selected || highlight)
                        using (var pen = new Pen(Color.FromArgb(117, 162, 247), 3))
                            e.Graphics.DrawRoundedRectangle(pen, new Rectangle(1, 1, panel.Width - 3, panel.Height - 3), 6);

                    if (selected)
                    {
                        using (var brush = new SolidBrush(Color.FromArgb(117, 162, 247)))
                        using (var check = new Pen(Color.White, 2))
                        {
                            e.Graphics.FillRoundedRectangle(brush, new Rectangle(panel.Width / 2 + 2, panel.Height / 2 + 2, panel.Width / 2 - 3, panel.Height / 2 - 3), 5);

                            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            check.StartCap = LineCap.Round;
                            check.EndCap = LineCap.Round;

                            e.Graphics.DrawLine(check, 25, 30, 29, 34);
                            e.Graphics.DrawLine(check, 29, 34, 35, 26);
                        }
                    }
                };
            }
        }

        void ApplyTheme(Panel panel)
        {
            bool customTheme = panel.Name.Contains("0");

            if (customTheme)
                _themes[TrayTheme0] = new Color[] { CustomTrayColor1.Color, CustomTrayColor2.Color, CustomTrayColor3.Color };

            _trayIcon.Colors = _themes[panel];

            groupPanel4.Refresh();

            ColorsPanel.Visible = customTheme;
        }

        public bool TryGetColor(string s, out Color color)
        {
            try
            {
                Match m = Regex.Match(s, "([0-9]+);([0-9]+);([0-9]+);");
                color = Color.FromArgb(Convert.ToInt32(m.Groups[1].Value), Convert.ToInt32(m.Groups[2].Value), Convert.ToInt32(m.Groups[3].Value));
                return true;
            }

            catch
            {
                color = Color.Empty;
                return false;
            }
        }

        void UpdateTranslation()
        {
            LabelResetSettings.Text = Translations.GetString("ResetSettings");
            groupPanel1.Title = Translations.GetString("GeneralSettings");
            AutoUpdateCheck.Text = Translations.GetString("AutoUpdate");
            AutorunCheck.Text = Translations.GetString("Autorun");
            AutoClearCheck.Text = Translations.GetString("AutoClear");
            CleaningResultsCheck.Text = Translations.GetString("ShowCleaningResults");
            StartMinimizedCheck.Text = Translations.GetString("StartMinimized");
            groupPanel2.Title = Translations.GetString("Language");

            groupPanel3.Title = Translations.GetString("GeneralSettings");
            LabelTextColor.Text = Translations.GetString("TextColor");
            TrayTextColor.Location = new Point(LabelTextColor.Location.X + LabelTextColor.Width, TrayTextColor.Location.Y);
            CheckBoxTextShadow.Text = Translations.GetString("TextShadow");
            TrayTextShadowColor.Location = new Point(CheckBoxTextShadow.Location.X + CheckBoxTextShadow.Width - 2, TrayTextShadowColor.Location.Y);

            LabelMouseClick.Text = Translations.GetString("MiddleMouseClick");
            radioButton1.Text = Translations.GetString("ClearRAM");
            radioButton2.Text = Translations.GetString("OpenTaskManager");

            groupPanel4.Title = Translations.GetString("Style");
            LabelCustomStyle.Text = Translations.GetString("Custom");

            AboutLabel1.Text = Translations.GetString("AboutString1");
            AboutLabel2.Text = Translations.GetString("AboutString2");
            AboutLink2.Text = $"{Translations.GetString("Version")}: {Application.ProductVersion.Remove(Application.ProductVersion.Length - 2)}";
            AboutLink3.Text = Translations.GetString("CheckUpdates");

            NumericContainer.Location = new Point(AutoClearCheck.Location.X + AutoClearCheck.Width, NumericContainer.Location.Y);

            radioButton2.Location = new Point(radioButton1.Location.X + radioButton1.Width, radioButton2.Location.Y);

            TabsPanel.Refresh();

            _form1.UpdateStrings();
            _trayIcon.UpdateStrings();
        }
        #endregion
    }
}
