using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Compact_RAM_Cleaner
{
    public static class SaveSystem
    {
        static Dictionary<string, string> _data;

        public static string GetValue(string key) => _data.TryGetValue(key, out var value) ? value : null;
        public static bool TryGetValue(string key, out string value) => _data.TryGetValue(key, out value);

        public static bool Load()
        {
            if (File.Exists(Paths.IniFile))
            {
                try
                {
                    var data = File.ReadAllLines(Paths.IniFile).Where(x => x.Contains("="));
                    _data = data.ToDictionary(k => k.Substring(0, k.IndexOf("=")), v => v.Substring(v.IndexOf("=") + 1));
                }
                catch { }
            }

            return _data != null;
        }

        public static void Save(string key, string value)
        {
            if (File.Exists(Paths.IniFile))
            {
                var data = File.ReadAllLines(Paths.IniFile).Where(x => !x.Contains($"{key}=")).ToList();
                data.Add($"{key}={value}");
                using (var sw = File.CreateText(Paths.IniFile))
                    data.ForEach(x => sw.WriteLine(x));
            }
            else
            {
                using (var sw = File.CreateText(Paths.IniFile))
                    sw.WriteLine($"{key}={value}");
            }
        }

        public static void Delete(string key)
        {
            if (File.Exists(Paths.IniFile))
            {
                var data = File.ReadAllLines(Paths.IniFile).Where(x => !x.Contains($"{key}=")).ToList();
                using (var sw = File.CreateText(Paths.IniFile))
                    data.ForEach(x => sw.WriteLine(x));
            }
        }
    }
}
