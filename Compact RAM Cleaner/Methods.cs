using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compact_RAM_Cleaner
{
    public static class Methods
    {
        public static string currentPath = Path.GetDirectoryName(Application.ExecutablePath);
        public static string ini = $"{currentPath}\\Compact RAM Cleaner.ini";
        public static bool is64Bit = Environment.Is64BitOperatingSystem;
        public static bool silent;
        public static Font f = new Font("Tahoma", 8F);
        public static string[] type = { "Б", "КБ", "МБ", "ГБ" };
        public static string GetSize(double countBytes)
        {
            double bytes = Math.Abs(countBytes);
            int place = (int)Math.Floor(Math.Log(bytes, 1024));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return $"{Math.Sign(countBytes) * num} {type[place]}";
        }
        async public static void Exit(Form f)
        {
            for (f.Opacity = 1; f.Opacity > .0; f.Opacity -= .1) await Task.Delay(10);
            f.Close();
        }
        public static bool UpdateCheck()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string info = wc.DownloadString("https://raw.githubusercontent.com/qualcosa/Compact-RAM-Cleaner/master/Compact%20RAM%20Cleaner/Properties/AssemblyInfo.cs");
                    Match m = Regex.Match(info, @"AssemblyFileVersion\(""(.*?)""\)\]");
                    return Convert.ToInt32(Application.ProductVersion.Replace(".", "")) < Convert.ToInt32(m.Groups[1].Value.Replace(".", ""));
                }
            }
            catch { return false; }
        }
        public static void UpdateNow()
        {
            if (MessageBox.Show(Lang.X("Доступно обновление.\nПерезапустить программу для обновления?"), "Compact RAM Cleaner", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (File.Exists($"{Path.GetTempPath()}\\Compact.RAM.Cleaner.exe")) File.Delete($"{Path.GetTempPath()}\\Compact.RAM.Cleaner.exe");
                    string update = $"{Path.GetTempPath()}\\Compact.RAM.Cleaner.exe";
                    using (WebClient wc = new WebClient())
                        wc.DownloadFile("https://github.com/qualcosa/Compact-RAM-Cleaner/releases/latest/download/Compact.RAM.Cleaner.exe", update);
                    Process.Start(new ProcessStartInfo { FileName = "cmd", Arguments = $"/c taskkill /f /im \"Compact RAM Cleaner.exe\" & del \"{currentPath}\\Compact RAM Cleaner.exe\" & move \"{update}\" \"{currentPath}\" & start \"\" \"{currentPath}\\Compact.RAM.Cleaner.exe\"", WindowStyle = ProcessWindowStyle.Hidden });
                    Environment.Exit(0);
                }
                catch { Popup.Show(Lang.X("Не удалось скачать обновление.\nПроверьте подключение к интернету")); }
            }
        }
    }
}
