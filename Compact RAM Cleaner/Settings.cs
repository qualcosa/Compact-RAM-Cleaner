using Compact_RAM_Cleaner.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compact_RAM_Cleaner
{
    public partial class Settings : Shadows
    {
        readonly Form1 f;
        public Bitmap toggleOn = Resources.Toggle_On;
        public Bitmap toggleOff = Resources.Toggle_Off;
        void Settings_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.FromArgb(48, 49, 54), 2), 0, Height, Width, Height);
        }
        void ClosePanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawLine(f.pen, 6, 6, ClosePanel.Width - 6, ClosePanel.Height - 6);
            e.Graphics.DrawLine(f.pen, 6, ClosePanel.Height - 6, ClosePanel.Width - 6, 6);
        }
        void TabPanel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(SystemColors.ControlDark, 2), 21, label1.Location.Y + 10, TabPanel2.Width - 21, label1.Location.Y + 10);
        }
        void TrayColor0_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new LinearGradientBrush(new Rectangle(0, 0, 28, 28), ColorPanel1.BackColor, ColorPanel3.BackColor, 270F)
            {
                InterpolationColors = new ColorBlend(3)
                { Colors = new Color[3] { ColorPanel1.BackColor, ColorPanel2.BackColor, ColorPanel3.BackColor }, Positions = new float[3] { 0.0f, 0.5f, 1f } }
            }, 0, 0, 28, 28);
        }
        void TrayColor1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new LinearGradientBrush(new Rectangle(0, 0, 28, 28), Color.FromArgb(30, 255, 0), Color.FromArgb(200, 0, 0), 270F)
            {
                InterpolationColors = new ColorBlend(3)
                { Colors = new Color[3] { Color.FromArgb(30, 255, 0), Color.FromArgb(255, 246, 0), Color.FromArgb(200, 0, 0) }, Positions = new float[3] { 0.0f, 0.5f, 1f } }
            }, 0, 0, 28, 28);
        }
        void TrayColor2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new LinearGradientBrush(new Rectangle(0, 0, 28, 28), Color.FromArgb(30, 255, 0), Color.FromArgb(200, 0, 0), 270F)
            {
                InterpolationColors = new ColorBlend(3)
                { Colors = new Color[3] { Color.FromArgb(26, 115, 232), Color.FromArgb(150, 50, 200), Color.FromArgb(200, 50, 50) }, Positions = new float[3] { 0.0f, 0.5f, 1f } }
            }, 0, 0, 28, 28);
        }
        void TrayColor3_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new LinearGradientBrush(new Rectangle(0, 0, 28, 28), Color.FromArgb(0, 130, 250), Color.FromArgb(250, 200, 50), 270F)
            {
                InterpolationColors = new ColorBlend(3)
                { Colors = new Color[3] { Color.FromArgb(0, 130, 250), Color.FromArgb(80, 190, 60), Color.FromArgb(250, 200, 50) }, Positions = new float[3] { 0.0f, 0.5f, 1f } }
            }, 0, 0, 28, 28);
        }

        public Settings(Form1 form1)
        {
            f = form1;
            InitializeComponent();
            linkLabel1.Click += (s, e) => Process.Start("https://github.com/qualcosa/Compact-RAM-Cleaner");
            linkLabel2.Text = $"{Lang.X("Версия")}: {Application.ProductVersion.Remove(Application.ProductVersion.Length - 2)}";
            linkLabel2.Click += (s, e) => Process.Start("https://github.com/qualcosa/Compact-RAM-Cleaner/releases");
            linkLabel3.Click += (s, e) => { if (Methods.UpdateCheck()) Methods.UpdateNow(); else Popup.Show(Lang.X("У вас актуальная версия")); };
            linkLabel4.Click += (s, e) => Process.Start("https://github.com/qualcosa");
            new List<Control> { PanelCurrent2, PanelCurrent3, PanelCurrent4 }.ForEach(x => { x.Location = new Point(5, 12); x.Size = new Size(2, 0); });
            new List<Control> { TabPanel1, TabPanel2, TabPanel3, TabPanel4 }.ForEach(x => x.Location = new Point(116, 22));
            Size = new Size(400, 272);

            LabelControl1.Click += (s, e) => SwitchTabs(TabPanel1, PanelCurrent1);
            LabelControl2.Click += (s, e) => SwitchTabs(TabPanel2, PanelCurrent2);
            LabelControl3.Click += (s, e) => SwitchTabs(TabPanel3, PanelCurrent3);
            LabelControl4.Click += (s, e) => SwitchTabs(TabPanel4, PanelCurrent4);
            async void SwitchTabs(Panel panel, Panel panel2)
            {
                panel.Visible = true;
                foreach (Panel p in Controls.OfType<Panel>().Where(x => x.Name.Contains("TabPanel") && !x.Name.Contains(panel.Name)))
                    p.Visible = false;
                if (!panel2.Visible)
                {
                    PanelCurrent1.Visible = false;
                    PanelCurrent2.Visible = false;
                    PanelCurrent3.Visible = false;
                    PanelCurrent4.Visible = false;
                    PanelCurrent1.Location = new Point(5, 12);
                    PanelCurrent2.Location = new Point(5, 12);
                    PanelCurrent3.Location = new Point(5, 12);
                    PanelCurrent4.Location = new Point(5, 12);
                    PanelCurrent1.Size = new Size(2, 0);
                    PanelCurrent2.Size = new Size(2, 0);
                    PanelCurrent3.Size = new Size(2, 0);
                    PanelCurrent4.Size = new Size(2, 0);
                    panel2.Visible = true;
                    while (panel2.Height < 14)
                    {
                        panel2.Size = new Size(2, panel2.Height + 2);
                        panel2.Location = new Point(5, panel2.Location.Y - 1);
                        await Task.Delay(7);
                    }
                }
            }
            void SetColor()
            {
                Refresh();
                f.c1 = ColorPanel1.BackColor;
                f.c2 = ColorPanel2.BackColor;
                f.c3 = ColorPanel3.BackColor;

                if (File.Exists(Methods.ini))
                {
                    List<string> list = File.ReadAllLines(Methods.ini).ToList().Where(x => !x.Contains("CustomColors=") && !x.Contains("Theme=")).ToList();
                    list.Add("Theme=0");
                    list.Add($"CustomColors={ColorPanel1.BackColor.R};{ColorPanel1.BackColor.G};{ColorPanel1.BackColor.B};{ColorPanel2.BackColor.R};{ColorPanel2.BackColor.G};{ColorPanel2.BackColor.B};{ColorPanel3.BackColor.R};{ColorPanel3.BackColor.G};{ColorPanel3.BackColor.B};");
                    using (StreamWriter sw = File.CreateText(Methods.ini))
                        list.ForEach(x => sw.WriteLine(x));
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(Methods.ini))
                    {
                        sw.WriteLine("Theme=0");
                        sw.WriteLine($"CustomColors={ColorPanel1.BackColor.R};{ColorPanel1.BackColor.G};{ColorPanel1.BackColor.B};{ColorPanel2.BackColor.R};{ColorPanel2.BackColor.G};{ColorPanel2.BackColor.B};{ColorPanel3.BackColor.R};{ColorPanel3.BackColor.G};{ColorPanel3.BackColor.B};");
                    }
                }
            }
            checkBox1.Click += (s, e) =>
            {
                if (checkBox1.Checked)
                {
                    SwitchTheme(TrayColor0);
                    ColorsPanel.Visible = true;
                    SetColor();
                }
                else
                {
                    SwitchTheme(TrayColor1);
                    ColorsPanel.Visible = false;
                    f.c1 = Color.FromArgb(30, 255, 0);
                    f.c2 = Color.FromArgb(255, 246, 0);
                    f.c3 = Color.FromArgb(200, 0, 0);
                }
            };

            ColorPanel1.Click += (s, e) =>
            {
                ColorDialog cd = new ColorDialog { Color = ColorPanel1.BackColor, FullOpen = true };
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    ColorPanel1.BackColor = cd.Color;
                    SetColor();
                }
            };
            ColorPanel2.Click += (s, e) =>
            {
                ColorDialog cd = new ColorDialog { Color = ColorPanel2.BackColor, FullOpen = true };
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    ColorPanel2.BackColor = cd.Color;
                    SetColor();
                }
            };
            ColorPanel3.Click += (s, e) =>
            {
                ColorDialog cd = new ColorDialog { Color = ColorPanel3.BackColor, FullOpen = true };
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    ColorPanel3.BackColor = cd.Color;
                    SetColor();
                }
            };

            TrayColor1.Click += (s, e) =>
            {
                if (File.Exists(Methods.ini))
                {
                    List<string> list = File.ReadAllLines(Methods.ini).ToList().Where(x => !x.Contains("Theme=")).ToList();
                    using (StreamWriter sw = File.CreateText(Methods.ini))
                        list.ForEach(x => sw.WriteLine(x));
                }
                SwitchTheme(TrayColor1);
                f.c1 = Color.FromArgb(30, 255, 0);
                f.c2 = Color.FromArgb(255, 246, 0);
                f.c3 = Color.FromArgb(200, 0, 0);
            };
            TrayColor2.Click += (s, e) =>
            {
                if (File.Exists(Methods.ini))
                {
                    List<string> list = File.ReadAllLines(Methods.ini).ToList().Where(x => !x.Contains("Theme=")).ToList();
                    list.Add("Theme=2");
                    using (StreamWriter sw = File.CreateText(Methods.ini))
                        list.ForEach(x => sw.WriteLine(x));
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(Methods.ini))
                        sw.WriteLine("Theme=2");
                }
                SwitchTheme(TrayColor2);
                f.c1 = Color.FromArgb(26, 115, 232);
                f.c2 = Color.FromArgb(150, 50, 200);
                f.c3 = Color.FromArgb(200, 50, 50);
            };
            TrayColor3.Click += (s, e) =>
            {
                if (File.Exists(Methods.ini))
                {
                    List<string> list = File.ReadAllLines(Methods.ini).ToList().Where(x => !x.Contains("Theme=")).ToList();
                    list.Add("Theme=3");
                    using (StreamWriter sw = File.CreateText(Methods.ini))
                        list.ForEach(x => sw.WriteLine(x));
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(Methods.ini))
                        sw.WriteLine("Theme=3");
                }
                SwitchTheme(TrayColor3);
                f.c1 = Color.FromArgb(0, 130, 250);
                f.c2 = Color.FromArgb(80, 190, 60);
                f.c3 = Color.FromArgb(250, 200, 50);
            };

            ClosePanel.Click += (s, e) => Methods.Exit(this);
            TitlePanel.MouseDown += (s, a) => { TitlePanel.Capture = false; Capture = false; Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero); base.WndProc(ref m); };
            Load += async (s, e) => { for (Opacity = 0; Opacity < 1; Opacity += .1) await Task.Delay(10); };
            radioButton2.CheckedChanged += (s, e) =>
            {
                ChangeLang();
                if (File.Exists(Methods.ini))
                {
                    List<string> list = File.ReadAllLines(Methods.ini).ToList().Where(x => !x.Contains("Language=")).ToList();
                    list.Add($"Language={(radioButton2.Checked ? "en" : "ru")}");
                    using (StreamWriter sw = File.CreateText(Methods.ini))
                        list.ForEach(x => sw.WriteLine(x));
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(Methods.ini))
                        sw.WriteLine($"Language={(radioButton2.Checked ? "en" : "ru")}");
                }
            };
            Setting1.Click += (s, e) =>
            {
                Setting1.BackgroundImage = Setting1.BackgroundImage == toggleOn ? toggleOff : toggleOn;
                if (File.Exists(Methods.ini))
                {
                    List<string> list = File.ReadAllLines(Methods.ini).ToList().Where(x => !x.Contains("AutoUpdate=")).ToList();
                    if (Setting1.BackgroundImage == toggleOn)
                        list.Add("AutoUpdate=true");
                    using (StreamWriter sw = File.CreateText(Methods.ini))
                        list.ForEach(x => sw.WriteLine(x));
                }
                else
                {
                    if (Setting1.BackgroundImage == toggleOn)
                        using (StreamWriter sw = File.CreateText(Methods.ini))
                            sw.WriteLine("AutoUpdate=true");
                }
            };
            Setting2.Click += (s, e) =>
            {
                Setting2.BackgroundImage = Setting2.BackgroundImage == toggleOn ? toggleOff : toggleOn;
                Numeric1.Enabled = Setting2.BackgroundImage != toggleOn;

                if (File.Exists(Methods.ini))
                {
                    List<string> list = File.ReadAllLines(Methods.ini).ToList().Where(x => !x.Contains("AutoCleanerValue=")).ToList();
                    if (Setting2.BackgroundImage == toggleOn)
                    {
                        f.AutoCleaner();
                        list.Add($"AutoCleanerValue={Numeric1.Value}");
                    }
                    using (StreamWriter sw = File.CreateText(Methods.ini))
                        list.ForEach(x => sw.WriteLine(x));
                }
                else
                {
                    if (Setting2.BackgroundImage == toggleOn)
                    {
                        f.AutoCleaner();
                        using (StreamWriter sw = File.CreateText(Methods.ini))
                            sw.WriteLine($"AutoCleanerValue={Numeric1.Value}");
                    }
                }
            };
            Setting3.Click += (s, e) =>
            {
                Setting3.BackgroundImage = Setting3.BackgroundImage == toggleOn ? toggleOff : toggleOn;
                if (File.Exists(Methods.ini))
                {
                    List<string> list = File.ReadAllLines(Methods.ini).ToList().Where(x => !x.Contains("StartupCleaner=")).ToList();
                    if (Setting3.BackgroundImage == toggleOn)
                        list.Add("StartupCleaner=true");
                    using (StreamWriter sw = File.CreateText(Methods.ini))
                        list.ForEach(x => sw.WriteLine(x));
                }
                else
                {
                    if (Setting3.BackgroundImage == toggleOn)
                        using (StreamWriter sw = File.CreateText(Methods.ini))
                            sw.WriteLine("StartupCleaner=true");
                }
            };
            Setting4.Click += (s, e) =>
            {
                Setting4.BackgroundImage = Setting4.BackgroundImage == toggleOn ? toggleOff : toggleOn;
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run"))
                {
                    if (Setting4.BackgroundImage == toggleOn)
                        key.SetValue("Compact RAM Cleaner", $"\"{Path.GetDirectoryName(Application.ExecutablePath)}\\Compact RAM Cleaner.exe\" silent");
                    else
                        try { key.DeleteValue("Compact RAM Cleaner"); } catch { };
                }
            };
            Setting5.Click += (s, e) =>
            {
                Setting5.BackgroundImage = Setting5.BackgroundImage == toggleOn ? toggleOff : toggleOn;
                if (File.Exists(Methods.ini))
                {
                    List<string> list = File.ReadAllLines(Methods.ini).ToList().Where(x => !x.Contains("NotifyDisable=")).ToList();
                    if (Setting5.BackgroundImage == toggleOn)
                        list.Add("NotifyDisable=true");
                    using (StreamWriter sw = File.CreateText(Methods.ini))
                        list.ForEach(x => sw.WriteLine(x));
                }
                else
                {
                    if (Setting5.BackgroundImage == toggleOn)
                        using (StreamWriter sw = File.CreateText(Methods.ini))
                            sw.WriteLine("NotifyDisable=true");
                }
            };
        }

        public void SwitchTheme(Panel panel)
        {
            panel.BorderStyle = BorderStyle.Fixed3D;
            foreach (Panel p in TabPanel2.Controls.OfType<Panel>().Where(x => x.Name.Contains("TrayColor") && !x.Name.Contains(panel.Name)))
                p.BorderStyle = BorderStyle.None;
            if (!panel.Name.Contains("TrayColor0"))
            {
                checkBox1.Checked = false;
                ColorsPanel.Visible = false;
                TrayColor0.BorderStyle = BorderStyle.None;
            }
        }

        void ChangeLang()
        {
            if (radioButton2.Checked)
            {
                Lang.ru = false;
                Methods.type[0] = "B";
                Methods.type[1] = "KB";
                Methods.type[2] = "MB";
                Methods.type[3] = "GB";
                f.LabelRam.Location = new Point(105, f.LabelRam.Location.Y);
                f.LabelPageFile.Location = new Point(87, f.LabelPageFile.Location.Y);
                Numeric1.Location = new Point(178, Numeric1.Location.Y);
                Panel1.Location = new Point(206, Panel1.Location.Y);
                label1.Location = new Point(116, label1.Location.Y);
            }
            else
            {
                Lang.ru = true;
                Methods.type[0] = "Б";
                Methods.type[1] = "КБ";
                Methods.type[2] = "МБ";
                Methods.type[3] = "ГБ";
                f.LabelRam.Location = new Point(54, f.LabelRam.Location.Y);
                f.LabelPageFile.Location = new Point(71, f.LabelPageFile.Location.Y);
                Numeric1.Location = new Point(214, Numeric1.Location.Y);
                Panel1.Location = new Point(242, Panel1.Location.Y);
                label1.Location = new Point(121, label1.Location.Y);
            }
            f.LabelMon2.Text = $"{Methods.GetSize(f.TotalRam())}";
            f.LabelRam.Text = Lang.X("Оперативная память");
            f.label4.Text = Lang.X("Занято:");
            f.label5.Text = Lang.X("Всего памяти:");
            f.label6.Text = Lang.X("Свободной памяти:");
            f.LabelPageFile.Text = Lang.X("Файл подкачки");
            f.label9.Text = Lang.X("Занято:");
            f.label8.Text = Lang.X("Выделено памяти:");
            f.label7.Text = Lang.X("Свободной памяти:");
            f.Button1.Text = Lang.X("Очистить");
            f.CacheCheck.Text = Lang.X("+ кэш");

            LabelControl1.Text = Lang.X("Настройки");
            LabelControl2.Text = Lang.X("Трей");
            LabelControl3.Text = Lang.X("Язык");
            LabelControl4.Text = Lang.X("О программе");

            SettingLabel1.Text = Lang.X("Проверять обновления при запуске");
            SettingLabel2.Text = Lang.X("Автоочистка при достижении (%)");
            SettingLabel3.Text = Lang.X("Запускать очистку при запуске");
            SettingLabel4.Text = Lang.X("Запускать при загрузке ОС");
            SettingLabel5.Text = Lang.X("Отключить уведомление");

            label1.Text = Lang.X("Тема");
            checkBox1.Text = Lang.X("Свой стиль");

            label7.Text = Lang.X("небольшая");
            label6.Text = Lang.X("программа для очистки ОЗУ");
            linkLabel2.Text = $"{Lang.X("Версия")}: {Application.ProductVersion.Remove(Application.ProductVersion.Length - 2)}";
            linkLabel3.Text = Lang.X("Проверить обновления");

            f.Context1.Items[0].Text = Lang.X("Очистить ОЗУ");
            f.Context1.Items[1].Text = Lang.X("Очистить ОЗУ + кэш");
            f.Context1.Items[2].Text = Lang.X("Диспетчер задач");
            f.Context1.Items[3].Text = Lang.X("Выход");
        }
    }
}
