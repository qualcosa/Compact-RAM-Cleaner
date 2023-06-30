using Microsoft.VisualBasic.Devices;
using System;
using System.Management;

namespace Compact_RAM_Cleaner
{
    public static class Memory
    {
        static readonly ComputerInfo _computerInfo = new ComputerInfo();
        public static ulong TotalPhysicalMemory => _computerInfo.TotalPhysicalMemory;
        public static ulong AvailablePhysicalMemory => _computerInfo.AvailablePhysicalMemory;

        public static double GetPageFileMaxSize()
        {
            try
            {
                double size = 0;
                using (var query = new ManagementObjectSearcher("SELECT MaximumSize FROM Win32_PageFile"))
                {
                    foreach (var obj in query.Get())
                        size = (uint)obj.GetPropertyValue("MaximumSize");
                    return Math.Round(size / 1024, 1);
                }
            }

            catch
            {
                return 0;
            }
        }

        public static double GetPageFileUsage()
        {
            try
            {
                double size = 0;
                using (var query = new ManagementObjectSearcher("SELECT CurrentUsage FROM Win32_PageFileUsage"))
                {
                    foreach (var obj in query.Get())
                        size = (uint)obj.GetPropertyValue("CurrentUsage");
                    return Math.Round(size / 1024, 1);
                }
            }

            catch
            {
                return 0;
            }
        }
    }
}
