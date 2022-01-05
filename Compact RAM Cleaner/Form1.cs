using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compact_RAM_Cleaner
{
    public partial class Form1 : Shadows
    {
        #region Paint
        public Pen pen = new Pen(Color.FromArgb(160, 160, 160), 2);
        void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.FromArgb(48, 49, 54), 2), 0, Height, Width, Height);
            Pen pen = new Pen(SystemColors.ControlDark, 2);
            e.Graphics.DrawLine(pen, 12, LabelRam.Location.Y + 10, Width - 12, LabelRam.Location.Y + 10);
            e.Graphics.DrawLine(pen, 12, LabelPageFile.Location.Y + 10, Width - 12, LabelPageFile.Location.Y + 10);
        }
        void ClosePanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawLine(pen, 4, 4, ClosePanel.Width - 4, ClosePanel.Height - 4);
            e.Graphics.DrawLine(pen, 4, ClosePanel.Height - 4, ClosePanel.Width - 4, 4);
        }
        void SettingsPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(pen, 4, 5, ClosePanel.Width - 2, 5);
            e.Graphics.DrawLine(pen, 4, 10, ClosePanel.Width - 2, 10);
            e.Graphics.DrawLine(pen, 4, 15, ClosePanel.Width - 2, 15);
        }
        void Minimize_Paint(object sender, PaintEventArgs e) => e.Graphics.DrawLine(pen, 4, 9, Minimize.Width - 4, 9);
        #endregion

        [DllImport("user32.dll", CharSet = CharSet.Auto)] extern static bool DestroyIcon(IntPtr handle);
        [DllImport("psapi.dll")] static extern int EmptyWorkingSet([In] IntPtr obj0);
        [DllImport("advapi32.dll", SetLastError = true)] internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);
        [DllImport("advapi32.dll", SetLastError = true)] internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);
        [DllImport("ntdll.dll")] static extern UInt32 NtSetSystemInformation(int InfoClass, IntPtr Info, int Length);
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct SYSTEM_CACHE_INFORMATION
        {
            public long CurrentSize;
            public long PeakSize;
            public long PageFaultCount;
            public long MinimumWorkingSet;
            public long MaximumWorkingSet;
            public long Unused1;
            public long Unused2;
            public long Unused3;
            public long Unused4;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)] internal struct TokPriv1Luid { public int Count; public long Luid; public int Attr; }
        void ClearCache()
        {
            try
            {
                if (SetIncreasePrivilege("SeIncreaseQuotaPrivilege"))
                {
                    SYSTEM_CACHE_INFORMATION sc = new SYSTEM_CACHE_INFORMATION
                    { MinimumWorkingSet = Methods.is64Bit ? -1L : uint.MaxValue, MaximumWorkingSet = Methods.is64Bit ? -1L : uint.MaxValue };
                    int sys = Marshal.SizeOf(sc);
                    GCHandle gcHandle = GCHandle.Alloc(sc, GCHandleType.Pinned);
                    uint num = NtSetSystemInformation(0x0015, gcHandle.AddrOfPinnedObject(), sys);
                    gcHandle.Free();
                }

                if (SetIncreasePrivilege("SeProfileSingleProcessPrivilege"))
                {
                    int sys = Marshal.SizeOf(4);
                    GCHandle gcHandle = GCHandle.Alloc(4, GCHandleType.Pinned);
                    uint num = NtSetSystemInformation(0x0050, gcHandle.AddrOfPinnedObject(), sys);
                    gcHandle.Free();
                }
            }
            catch { }
        }
        bool SetIncreasePrivilege(string privilegeName)
        {
            using (WindowsIdentity current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges))
            {
                TokPriv1Luid tokPriv1Luid;
                tokPriv1Luid.Count = 1;
                tokPriv1Luid.Luid = 0L;
                tokPriv1Luid.Attr = 2;
                if (!LookupPrivilegeValue(null, privilegeName, ref tokPriv1Luid.Luid)) return false;
                return AdjustTokenPrivileges(current.Token, false, ref tokPriv1Luid, 0, IntPtr.Zero, IntPtr.Zero);
            }
        }
        public ulong TotalRam() => new ComputerInfo().TotalPhysicalMemory;
        public ulong FreeRam() => new ComputerInfo().AvailablePhysicalMemory;

        public Form1()
        {
            InitializeComponent();
            string exeName = Assembly.GetEntryAssembly().Location;
            if (!exeName.EndsWith("Compact RAM Cleaner.exe"))
            {
                if (File.Exists($"{Methods.currentPath}\\Compact RAM Cleaner.exe")) File.Delete($"{Methods.currentPath}\\Compact RAM Cleaner.exe");
                Process.Start(new ProcessStartInfo { FileName = "cmd", Arguments = $"/c taskkill /f /im \"Compact RAM Cleaner.exe\" & ren \"{exeName}\" \"Compact RAM Cleaner.exe\" & start \"\" \"{Methods.currentPath}\\Compact RAM Cleaner.exe\"", WindowStyle = ProcessWindowStyle.Hidden });
                Environment.Exit(0);
            }
            p = new Settings(this);
            new List<Control> { p.Setting1, p.Setting2, p.Setting3, p.Setting4, p.Setting5 }.ForEach(x => { x.BackgroundImage = p.toggleOff; });
            Resize += async (s, e) =>
            {
                if (WindowState == FormWindowState.Minimized)
                    Hide();
                else
                {
                    if (isPageFileEnabled)
                        PageFileInfo();
                    for (Opacity = 0; Opacity < 1; Opacity += .2) await Task.Delay(10);
                }
            };
            NotifyIcon1.MouseClick += (s, e) => { if (e.Button == MouseButtons.Left) { Show(); WindowState = FormWindowState.Normal; } else if (e.Button == MouseButtons.Middle) Ram(false); };
            Button1.Click += (s, e) => Ram(false);
            Menu1.Click += (s, e) => Ram(false);
            Menu2.Click += (s, e) => Methods.Exit(this);
            Menu3.Click += (s, e) => { Ram(false); if (!CacheCheck.Checked) ClearCache(); };
            ClosePanel.Click += (s, e) => Methods.Exit(this);
            SettingsPanel.Click += (s, e) => p.ShowDialog();
            Minimize.Click += async (s, e) => { for (Opacity = 1; Opacity > .0; Opacity -= .2) await Task.Delay(10); WindowState = FormWindowState.Minimized; };
            CacheCheck.CheckedChanged += (s, e) =>
            {
                if (File.Exists(Methods.ini))
                {
                    List<string> list = File.ReadAllLines(Methods.ini).ToList().Where(x => !x.Contains("ClearCache=")).ToList();
                    if (CacheCheck.Checked)
                        list.Add("ClearCache=true");
                    using (StreamWriter sw = File.CreateText(Methods.ini))
                        list.ForEach(x => sw.WriteLine(x));
                }
                else
                {
                    if (CacheCheck.Checked)
                    {
                        using (StreamWriter sw = File.CreateText(Methods.ini))
                            sw.WriteLine("ClearCache=true");
                    }
                }
            };
            new List<Control> { TitlePanel, LabelMon1, LabelMon2, LabelMon3, LabelMon4, LabelMon5, LabelMon6, label4, label5, label6, LabelPageFile, LabelRam, AppName, label7, label8, label9 }.ForEach(x =>
            {
                x.MouseDown += (s, a) => { x.Capture = false; Capture = false; Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero); base.WndProc(ref m); };
            });
            Tray();
            LabelMon2.Text = $"{Methods.GetSize(TotalRam())}";
            foreach (var arg in Environment.GetCommandLineArgs()) Methods.silent = arg.EndsWith("silent");
            if (File.Exists(Methods.ini))
            {
                File.ReadAllLines(Methods.ini).ToList().ForEach(x =>
                {
                    if (x.Contains("Language=en")) p.radioButton2.Checked = true;
                    if (x.Contains("AutoUpdate=true")) { p.Setting1.BackgroundImage = p.toggleOn; if (Methods.UpdateCheck()) Methods.UpdateNow(); }
                    else if (x.Contains("AutoCleanerValue="))
                    {
                        p.Setting2.BackgroundImage = p.toggleOn;
                        p.Numeric1.Value = Convert.ToDecimal(x.Remove(0, 17)) <= 95 ? Convert.ToDecimal(x.Remove(0, 17)) : 80;
                        p.Numeric1.Enabled = false;
                        AutoCleaner();
                    }
                    else if (x.Contains("ClearCache=true")) CacheCheck.Checked = true;
                    else if (x.Contains("StartupCleaner=true")) { p.Setting3.BackgroundImage = p.toggleOn; Ram(false); }
                    else if (x.Contains("NotifyDisable=true")) p.Setting5.BackgroundImage = p.toggleOn;
                });
            }
            else { if (!CultureInfo.CurrentUICulture.ToString().Contains("ru-RU")) p.radioButton2.Checked = true; }
            if (Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run")?.GetValue("Compact RAM Cleaner") != null)
                p.Setting4.BackgroundImage = p.toggleOn;
        }
        async void Form1_Load(object sender, EventArgs e)
        {
            if (!Methods.silent)
            {
                WindowState = FormWindowState.Normal;
                for (Opacity = 0; Opacity < 1; Opacity += .1) await Task.Delay(10);
            }
            else
            {
                Hide();
                Opacity = 1;
            }
        }

        ulong num;
        ulong ram;
        readonly Settings p;
        bool isPageFileEnabled = true;
        async void PageFileInfo()
        {
            try
            {
                string max = "";
                foreach (var mo in new ManagementObjectSearcher("select * from Win32_PageFile").Get())
                    max = $"{mo["MaximumSize"]}";
                LabelMon5.Text = $"{(Convert.ToInt32(max) >= 1024 ? $"{Math.Round(Convert.ToDouble(max) / 1024, 1)} {Methods.type[3]}" : $"{max} {Methods.type[2]}")}";
                while (WindowState == FormWindowState.Normal)
                {
                    string free = "";
                    foreach (var mo in new ManagementObjectSearcher("select * from Win32_PageFileUsage").Get())
                        free = $"{Convert.ToInt32(max) - Convert.ToInt32($"{mo["CurrentUsage"]}")}";
                    LabelMon6.Text = $"{(Convert.ToInt32(free) >= 1024 ? $"{Math.Round(Convert.ToDouble(free) / 1024, 1)} {Methods.type[3]}" : $"{free} {Methods.type[2]}")}";
                    LabelMon4.Text = $"{(Convert.ToInt32(max) - Convert.ToInt32(free)) * 100 / Convert.ToInt32(max)}%";
                    await Task.Delay(1000);
                }
                return;
            }
            catch { isPageFileEnabled = false; LabelMon4.Text = "—"; LabelMon5.Text = "—"; LabelMon6.Text = "—"; }
        }
        async public void AutoCleaner()
        {
            while (p.Setting2.BackgroundImage == p.toggleOn)
            {
                if (num >= p.Numeric1.Value)
                    Ram(true);
                await Task.Delay(30000);
            }
        }
        public void Ram(bool auto)
        {
            if (!auto) ram = FreeRam();
            Process.GetProcesses().ToList().ForEach(x => { try { EmptyWorkingSet(x.Handle); } catch { } });
            if (CacheCheck.Checked) ClearCache();
            if (!auto && p.Setting5.BackgroundImage == p.toggleOff) Popup.Show($"{Lang.X("Освободилось")} {Methods.GetSize(FreeRam() - ram)}");
        }
        async void Tray()
        {
            while (true)
            {
                num = (TotalRam() - FreeRam()) * 100 / TotalRam();
                LabelMon1.Text = $"{num}%";
                LabelMon3.Text = $"{Methods.GetSize(FreeRam())}";
                Bitmap b = new Bitmap(16, 16);
                Graphics g = Graphics.FromImage(b);
                g.FillRectangle(new LinearGradientBrush(new Rectangle(1, 1, 15, 15), Color.FromArgb(30, 255, 0), Color.FromArgb(200, 0, 0), 270F)
                {
                    InterpolationColors = new ColorBlend(3)
                    {
                        Colors = new Color[3] { Color.FromArgb(30, 255, 0), Color.FromArgb(255, 246, 0), Color.FromArgb(200, 0, 0) },
                        Positions = new float[3] { 0.0f, 0.3f, 1f }
                    }
                }, 0, 15 - 15 * (int)num / 100, 15, 15);
                g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
                g.DrawString(num.ToString(), Methods.f, new SolidBrush(Color.Black), 0, 2);
                g.DrawString(num.ToString(), Methods.f, new SolidBrush(Color.White), 0, 1);
                Icon i = Icon.FromHandle(b.GetHicon());
                NotifyIcon1.Icon = i;
                DestroyIcon(i.Handle);
                await Task.Delay(1000);
            }
        }
    }
}
