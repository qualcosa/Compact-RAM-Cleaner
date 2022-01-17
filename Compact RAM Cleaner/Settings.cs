using Compact_RAM_Cleaner.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
            e.Graphics.DrawRectangle(new Pen(SystemColors.ControlDarkDark, 1), label5.Location.X - 6, label5.Location.Y + 8, Width - 24, radioButton1.Location.X + 16);
        }
        void ClosePanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawLine(f.pen, 6, 6, ClosePanel.Width - 6, ClosePanel.Height - 6);
            e.Graphics.DrawLine(f.pen, 6, ClosePanel.Height - 6, ClosePanel.Width - 6, 6);
        }
        public Settings(Form1 form1)
        {
            f = form1;
            InitializeComponent();
            ClosePanel.Click += (s, e) => Methods.Exit(this);
            new List<Control> { TitlePanel, AppName }.ForEach(x =>
            {
                x.MouseDown += (s, a) => { x.Capture = false; Capture = false; Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero); base.WndProc(ref m); };
            });
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
                Numeric1.Location = new Point(174, Numeric1.Location.Y);
                Panel1.Location = new Point(202, Panel1.Location.Y);
                f.LabelMon2.Text = $"{Methods.GetSize(f.TotalRam())}";
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
                Numeric1.Location = new Point(212, Numeric1.Location.Y);
                Panel1.Location = new Point(240, Panel1.Location.Y);
                f.LabelMon2.Text = $"{Methods.GetSize(f.TotalRam())}";
            }
            AppName.Text = Lang.X("Настройки");
            label5.Text = Lang.X("Язык");
            f.LabelRam.Text = Lang.X("Оперативная память");
            f.label4.Text = Lang.X("Занято:");
            f.label5.Text = Lang.X("Всего памяти:");
            f.label6.Text = Lang.X("Свободной памяти:");
            f.label9.Text = Lang.X("Занято:");
            f.label8.Text = Lang.X("Выделено памяти:");
            f.label7.Text = Lang.X("Свободной памяти:");
            f.LabelPageFile.Text = Lang.X("Файл подкачки");
            f.Button1.Text = Lang.X("Очистить");
            f.CacheCheck.Text = Lang.X("+ кэш");
            SettingLabel1.Text = Lang.X("Проверять обновления при запуске");
            SettingLabel2.Text = Lang.X("Автоочистка при достижении (%)");
            SettingLabel3.Text = Lang.X("Запускать очистку при запуске");
            SettingLabel4.Text = Lang.X("Запускать при загрузке ОС");
            SettingLabel5.Text = Lang.X("Отключить уведомление");
            f.Context1.Items[0].Text = Lang.X("Очистить ОЗУ");
            f.Context1.Items[1].Text = Lang.X("Очистить ОЗУ + кэш");
            f.Context1.Items[2].Text = Lang.X("Выход");
        }
    }
}
