namespace Compact_RAM_Cleaner
{
    public class Popup
    {
        public static bool ShowCleaningResult = true;

        public static void Show(string text)
        {
            new Notify(text).Show();
        }

        public static void Show(double memoryReleased)
        {
            if (ShowCleaningResult)
                new Notify(memoryReleased).Show();
        }
    }
}
