using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Compact_RAM_Cleaner
{
    public static class Paths
    {
        public static string ApplicationExe = Assembly.GetEntryAssembly().Location;
        public static string ApplicationDirectory = Path.GetDirectoryName(Application.ExecutablePath);
        public static string IniFile = $"{ApplicationDirectory}\\Compact RAM Cleaner.ini";
    }
}
