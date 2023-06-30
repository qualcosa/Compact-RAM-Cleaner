using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using static Compact_RAM_Cleaner.Memory;

namespace Compact_RAM_Cleaner
{
    public static class Cleaner
    {
        [DllImport("psapi.dll")] static extern int EmptyWorkingSet([In] IntPtr obj0);
        [DllImport("advapi32.dll", SetLastError = true)] internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);
        [DllImport("advapi32.dll", SetLastError = true)] internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);
        [DllImport("ntdll.dll")] static extern uint NtSetSystemInformation(int InfoClass, IntPtr Info, int Length);
        [StructLayout(LayoutKind.Sequential, Pack = 1)] internal struct TokPriv1Luid { public int Count; public long Luid; public int Attr; }
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

        static readonly bool _is64Bit = Environment.Is64BitOperatingSystem;

        static bool _duringCleaning;
        static bool _duringAutoCleaning;
        static int _autoCleanerValue;

        public static async void EnableAutoCleaner(int value)
        {
            _autoCleanerValue = value;
            if (_duringAutoCleaning) return;
            _duringAutoCleaning = true;

            while (_duringAutoCleaning)
            {
                var used = (int)((TotalPhysicalMemory - AvailablePhysicalMemory) * 100 / TotalPhysicalMemory);

                if (used >= _autoCleanerValue)
                    Clear();

                await Task.Delay(30000);
            }
        }

        public static void DisableAutoCleaner()
        {
            _duringAutoCleaning = false;
        }

        public static async void ClearRAM()
        {
            if (_duringCleaning) return;
            _duringCleaning = true;

            var before = AvailablePhysicalMemory;

            Clear();

            if (Popup.ShowCleaningResult)
                Popup.Show(AvailablePhysicalMemory - before);

            await Task.Delay(2000);
            _duringCleaning = false;
        }

        static void Clear()
        {
            var processes = Process.GetProcesses();
            for (int i = 0; i < processes.Length; i++)
            {
                try { EmptyWorkingSet(processes[i].Handle); }
                catch { }
            }
        }

        public static void ClearCache()
        {
            try
            {
                if (SetIncreasePrivilege("SeIncreaseQuotaPrivilege"))
                {
                    var sc = new SYSTEM_CACHE_INFORMATION { MinimumWorkingSet = _is64Bit ? -1L : uint.MaxValue, MaximumWorkingSet = _is64Bit ? -1L : uint.MaxValue };
                    var sys = Marshal.SizeOf(sc);
                    var gcHandle = GCHandle.Alloc(sc, GCHandleType.Pinned);
                    var num = NtSetSystemInformation(0x0015, gcHandle.AddrOfPinnedObject(), sys);
                    gcHandle.Free();
                }

                if (SetIncreasePrivilege("SeProfileSingleProcessPrivilege"))
                {
                    var sys = Marshal.SizeOf(4);
                    var gcHandle = GCHandle.Alloc(4, GCHandleType.Pinned);
                    var num = NtSetSystemInformation(0x0050, gcHandle.AddrOfPinnedObject(), sys);
                    gcHandle.Free();
                }
            }
            catch { }
        }

        static bool SetIncreasePrivilege(string privilegeName)
        {
            using (var current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges))
            {
                TokPriv1Luid tokPriv1Luid;
                tokPriv1Luid.Count = 1;
                tokPriv1Luid.Luid = 0L;
                tokPriv1Luid.Attr = 2;
                if (!LookupPrivilegeValue(null, privilegeName, ref tokPriv1Luid.Luid)) return false;
                return AdjustTokenPrivileges(current.Token, false, ref tokPriv1Luid, 0, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
