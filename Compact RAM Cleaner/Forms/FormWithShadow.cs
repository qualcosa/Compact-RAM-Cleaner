using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Compact_RAM_Cleaner
{
    public class FormWithShadow : Form
    {
        [DllImport("dwmapi.dll")] public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [DllImport("dwmapi.dll")] public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [DllImport("dwmapi.dll")] public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        public struct MARGINS { public int leftWidth; public int rightWidth; public int topHeight; public int bottomHeight; }
        readonly bool _aeroEnabled;

        public FormWithShadow() => _aeroEnabled = CheckAeroEnabled();

        bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return enabled == 1;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x00A3)
            {
                m.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 0x0085:
                    if (_aeroEnabled)
                    {
                        var v = 2; DwmSetWindowAttribute(Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        };
                        DwmExtendFrameIntoClientArea(Handle, ref margins);
                    }
                    break;
                case 0x0083:
                    m.Result = (IntPtr)0; break;
                case 0x84 when (int)m.Result == 0x1: m.Result = (IntPtr)0x2; break;
                default: break;
            }
        }
    }
}
