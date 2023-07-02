using System.Collections.Generic;

namespace Compact_RAM_Cleaner
{
    public class Translations
    {
        static Language _language;
        public static Language Language
        {
            get => _language;
            set
            {
                _language = value;
                Helpers.DataType[0] = GetString("B");
                Helpers.DataType[1] = GetString("KB");
                Helpers.DataType[2] = GetString("MB");
                Helpers.DataType[3] = GetString("GB");
            }
        }

        public static string GetString(string value)
        {
            switch (Language)
            {
                case Language.Russian:
                    return _russian.TryGetValue(value, out var ru) ? ru : "";
                case Language.Ukrainian:
                    return _ukrainian.TryGetValue(value, out var ua) ? ua : "";
                default:
                    return _english.TryGetValue(value, out var en) ? en : "";
            }
        }

        static readonly Dictionary<string, string> _english = new Dictionary<string, string>()
        {
            ["B"] = "B",
            ["KB"] = "KB",
            ["MB"] = "MB",
            ["GB"] = "GB",

            ["PhysicalMemory"] = "Physical memory",
            ["PageFile"] = "Page file",
            ["Clear"] = "Clear",
            ["RAM"] = "RAM",
            ["RAMAndCache"] = "RAM + Cache",

            ["Tray1"] = "Clear RAM",
            ["Tray2"] = "Clear RAM + Cache",
            ["Tray3"] = "Task manager",
            ["Tray4"] = "Exit",



            ["TabSelect1"] = "Settings",
            ["TabSelect2"] = "Tray icon",
            ["TabSelect3"] = "About",
            ["ResetSettings"] = "Reset settings",

            ["GeneralSettings"] = "General",
            ["AutoUpdate"] = "Automatically check for updates",
            ["Autorun"] = "Run when my computer starts",
            ["AutoClear"] = "Auto clear when reached (%)",
            ["ShowCleaningResults"] = "Show cleaning results",
            ["StartMinimized"] = "Start minimized to tray",
            ["Language"] = "Language",

            ["TextColor"] = "Text color",
            ["TextShadow"] = "Text shadow",
            ["MiddleMouseClick"] = "Middle mouse button click",
            ["ClearRAM"] = "Clear RAM",
            ["OpenTaskManager"] = "Open task manager",
            ["Style"] = "Style",
            ["Custom"] = "Custom",



            ["AboutString1"] = "small program",
            ["AboutString2"] = "for cleaning RAM",
            ["Version"] = "Version",
            ["CheckUpdates"] = "Check for updates",



            ["LatestVersion"] = "You have the latest version",
            ["NoNetworkAccess"] = "No network access",
            ["FailedToCheckForUpdates"] = "Failed to check for updates",
            ["FailedToDownload"] = "Failed to download update",
            ["MemoryReleased"] = "Memory released:",
            ["UpdateAvailable"] = "An update is available.\nDo you want to restart the program to update?",
        };

        static readonly Dictionary<string, string> _russian = new Dictionary<string, string>()
        {
            ["B"] = "Б",
            ["KB"] = "КБ",
            ["MB"] = "МБ",
            ["GB"] = "ГБ",

            ["PhysicalMemory"] = "Физическая память",
            ["PageFile"] = "Файл подкачки",
            ["Clear"] = "Очистить",
            ["RAM"] = "ОЗУ",
            ["RAMAndCache"] = "ОЗУ + кэш",

            ["Tray1"] = "Очистить ОЗУ",
            ["Tray2"] = "Очистить ОЗУ + кэш",
            ["Tray3"] = "Диспетчер задач",
            ["Tray4"] = "Выход",



            ["TabSelect1"] = "Настройки",
            ["TabSelect2"] = "Трей",
            ["TabSelect3"] = "О программе",
            ["ResetSettings"] = "Сбросить настройки",

            ["GeneralSettings"] = "Основные настройки",
            ["AutoUpdate"] = "Автоматически проверять обновления",
            ["Autorun"] = "Запускать при запуске компьютера",
            ["AutoClear"] = "Автоочистка при достижении (%)",
            ["ShowCleaningResults"] = "Показывать результаты очистки",
            ["StartMinimized"] = "Запускать свёрнутым в трей",
            ["Language"] = "Язык",

            ["TextColor"] = "Цвет текста",
            ["TextShadow"] = "Тень текста",
            ["MiddleMouseClick"] = "Клик средней кнопки мыши",
            ["ClearRAM"] = "Очистить ОЗУ",
            ["OpenTaskManager"] = "Открыть диспетчер задач",
            ["Style"] = "Стиль",
            ["Custom"] = "Пользовательский",



            ["AboutString1"] = "небольшая программа",
            ["AboutString2"] = "для очистки ОЗУ",
            ["Version"] = "Версия",
            ["CheckUpdates"] = "Проверить обновления",



            ["LatestVersion"] = "У вас актуальная версия",
            ["NoNetworkAccess"] = "Нет доступа к сети",
            ["FailedToCheckForUpdates"] = "Не удалось проверить наличие обновлений",
            ["FailedToDownload"] = "Не удалось скачать обновление",
            ["MemoryReleased"] = "Освобождено памяти:",
            ["UpdateAvailable"] = "Доступно обновление.\nПерезапустить программу для обновления?",
        };

        static readonly Dictionary<string, string> _ukrainian = new Dictionary<string, string>()
        {
            ["B"] = "Б",
            ["KB"] = "КБ",
            ["MB"] = "МБ",
            ["GB"] = "ГБ",

            ["PhysicalMemory"] = "Фізична пам'ять",
            ["PageFile"] = "Файл підкачки",
            ["Clear"] = "Очистити",
            ["RAM"] = "ОЗУ",
            ["RAMAndCache"] = "ОЗУ + кеш",

            ["Tray1"] = "Очистити ОЗУ",
            ["Tray2"] = "Очистити ОЗУ + кеш",
            ["Tray3"] = "Диспетчер завдань",
            ["Tray4"] = "Вихід",



            ["TabSelect1"] = "Налаштування",
            ["TabSelect2"] = "Трей",
            ["TabSelect3"] = "Про програму",
            ["ResetSettings"] = "Скинути налаштування",

            ["GeneralSettings"] = "Основні налаштування",
            ["AutoUpdate"] = "Автоматично перевіряти оновлення",
            ["Autorun"] = "Запускати під час запуску комп'ютера",
            ["AutoClear"] = "Автоочищення при досягненні (%)",
            ["ShowCleaningResults"] = "Показувати результати очищення",
            ["StartMinimized"] = "Запускати згорнутим у трей",
            ["Language"] = "Мова",

            ["TextColor"] = "Колір тексту",
            ["TextShadow"] = "Тінь тексту",
            ["MiddleMouseClick"] = "Клік середньої кнопки миші",
            ["ClearRAM"] = "Очистити ОЗУ",
            ["OpenTaskManager"] = "Відкрити диспетчер завдань",
            ["Style"] = "Стиль",
            ["Custom"] = "Користувацький",



            ["AboutString1"] = "невелика програма",
            ["AboutString2"] = "для очищення ОЗУ",
            ["Version"] = "Версія",
            ["CheckUpdates"] = "Перевірити оновлення",



            ["LatestVersion"] = "У вас актуальна версія",
            ["NoNetworkAccess"] = "Немає доступу до мережі",
            ["FailedToCheckForUpdates"] = "Не вдалося перевірити наявність оновлень",
            ["FailedToDownload"] = "Не вдалося завантажити оновлення",
            ["MemoryReleased"] = "Звільнено пам'яті:",
            ["UpdateAvailable"] = "Доступно оновлення.\nПерезапустити програму для оновлення?",
        };
    }
}
