using System.Collections.Generic;

namespace Compact_RAM_Cleaner
{
    public class Lang
    {
        public static bool ru = true;
        public static string X(string text) => ru ? text : english[text];

        public static Dictionary<string, string> english = new Dictionary<string, string>()
        {
            ["Оперативная память"] = "RAM",
            ["Занято:"] = "Usage:",
            ["Всего памяти:"] = "Total memory:",
            ["Свободной памяти:"] = "Free memory:",
            ["Файл подкачки"] = "Paging file",
            ["Выделено памяти:"] = "Allocated memory:",
            ["Очистить"] = "Clear",
            ["+ кэш"] = "+ Cached",

            ["Настройки"] = "Settings",
            ["Трей"] = "Tray",
            ["Язык"] = "Language",
            ["О программе"] = "About",

            ["Проверять обновления при запуске"] = "Check for updates at startup",
            ["Автоочистка при достижении (%)"] = "Auto purge on reaching (%)",
            ["Запускать очистку при запуске"] = "Run cleanup on startup",
            ["Запускать при загрузке ОС"] = "Start at OS boot",
            ["Отключить уведомление"] = "Disable notification",

            ["Тема"] = "Theme",
            ["Свой стиль"] = "Custom style",

            ["небольшая"] = "small",
            ["программа для очистки ОЗУ"] = "program for cleaning RAM",
            ["Версия"] = "Version",
            ["Проверить обновления"] = "Check for updates",

            ["Очистить ОЗУ"] = "Сlear RAM",
            ["Очистить ОЗУ + кэш"] = "Сlear RAM + Cached",
            ["Диспетчер задач"] = "Task Manager",
            ["Выход"] = "Exit",

            ["Доступно обновление.\nПерезапустить программу для обновления?"] = "An update is available.\nDo you want to restart the program to update?",
            ["Не удалось скачать обновление.\nПроверьте подключение к интернету"] = "Failed to download update.\nCheck internet connection",
            ["У вас актуальная версия"] = "Do you have the latest version",
            ["Освободилось"] = "Freed",
        };
    }
}
