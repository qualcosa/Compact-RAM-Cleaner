using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compact_RAM_Cleaner
{
    public static class Helpers
    {
        public static string ExeName => AppDomain.CurrentDomain.FriendlyName;

        public static string[] DataType = { "B", "KB", "MB", "GB" };
        public static string BytesToString(double bytes)
        {
            if (bytes <= 0)
                return $"0 {DataType[0]}";

            double b = Math.Abs(bytes);
            int place = (int)Math.Floor(Math.Log(b, 1024));

            if (place > DataType.Length)
                return $"0 {DataType[0]}";

            double num = Math.Round(b / Math.Pow(1024, place), 1);
            return $"{Math.Sign(bytes) * num} {DataType[place]}";
        }

        public static double BytesToGigabytes(double bytes) => Math.Round(bytes / Math.Pow(1024, 3), 1);

        public static List<T> GetAllControlsOfType<T>(Control control) where T : Control
        {
            var list = new List<T>();
            for (int i = 0; i < control.Controls.Count; i++)
            {
                if (control.Controls[i] is T t)
                    list.Add(t);
                list.AddRange(GetAllControlsOfType<T>(control.Controls[i]));
            }
            return list;
        }

        public static void Cmd(string command)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c {command}",
                WindowStyle = ProcessWindowStyle.Hidden
            });
        }

        public static async Task StartAnimation(Form form)
        {
            for (form.Opacity = 0; form.Opacity < 1; form.Opacity += 0.2)
                await Task.Delay(5);
        }

        public static async Task ExitAnimation(Form form)
        {
            for (form.Opacity = 1; form.Opacity > 0; form.Opacity -= 0.2)
                await Task.Delay(5);
        }

        public static async void ExitAnimationAndClose(Form form)
        {
            for (form.Opacity = 1; form.Opacity > 0; form.Opacity -= 0.2)
                await Task.Delay(5);
            form.Close();
        }
    }
}
