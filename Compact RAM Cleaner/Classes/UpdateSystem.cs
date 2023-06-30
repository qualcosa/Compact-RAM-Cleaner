using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static Compact_RAM_Cleaner.Helpers;

namespace Compact_RAM_Cleaner
{
    public static class UpdateSystem
    {
        public static bool IsUpdateAvailable(bool notify)
        {
            if (!CheckInternetConnection())
            {
                if (notify)
                    Popup.Show(Translations.GetString("NoNetworkAccess"));
                return false;
            }

            try
            {
                using (var wc = new WebClient())
                {
                    string info = wc.DownloadString("https://raw.githubusercontent.com/qualcosa/Compact-RAM-Cleaner/master/Compact%20RAM%20Cleaner/Properties/AssemblyInfo.cs");
                    Match m = Regex.Match(info, @"AssemblyFileVersion\(""(.*?)""\)\]");

                    int current = Convert.ToInt32(Application.ProductVersion.Replace(".", ""));
                    int latest = Convert.ToInt32(m.Groups[1].Value.Replace(".", ""));

                    if (current >= latest && notify)
                        Popup.Show(Translations.GetString("LatestVersion"));

                    return current < latest;
                }
            }
            catch
            {
                if (notify)
                    Popup.Show(Translations.GetString("FailedToCheckForUpdates"));
                return false;
            }
        }

        public static void UpdateAndRestart()
        {
            if (MessageBox.Show(Translations.GetString("UpdateAvailable"), "Compact RAM Cleaner", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var temp = $"{Path.GetTempPath()}\\Compact RAM Cleaner.exe";

                    if (File.Exists(temp))
                        File.Delete(temp);

                    using (var wc = new WebClient())
                        wc.DownloadFile("https://github.com/qualcosa/Compact-RAM-Cleaner/releases/latest/download/Compact.RAM.Cleaner.exe", temp);

                    Cmd($"taskkill /f /im \"{ExeName}\" & del \"{Paths.ApplicationExe}\" & move \"{temp}\" \"{Paths.ApplicationDirectory}\" & \"{Paths.ApplicationDirectory}\\Compact RAM Cleaner.exe\"");
                    Environment.Exit(0);
                }

                catch
                {
                    Popup.Show(Translations.GetString("FailedToDownload"));
                }
            }
        }

        static bool CheckInternetConnection()
        {
            try
            {
                Dns.GetHostEntry("github.com");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
