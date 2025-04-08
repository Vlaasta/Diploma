using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;

namespace diplom
{
    public static class BrowserMonitor
    {
        private static string currentActiveBrowserPage = null;
        private static Stopwatch browserStopwatch = new Stopwatch();
        private static readonly string BrowserLogPath = @"E:\\4 KURS\\Диплом\\DiplomaRepo\\Diploma\\data\\browserActivity.json";
        private static readonly string ProjectsPath = @"E:\\4 KURS\\Диплом\\DiplomaRepo\\Diploma\\data\\projects.json";

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        public static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, buff, nChars) > 0)
            {
                return buff.ToString();
            }

            return "Без назви";
        }

        public static bool IsBrowserActive()
        {
            string[] browsers = { "Chrome", "Edge", "Firefox", "Opera", "Brave" };
            string activeWindowTitle = GetActiveWindowTitle();

            return browsers.Any(browser => activeWindowTitle.Contains(browser));
        }

        public static void CheckActiveWindow()
        {
            string activeWindowTitle = GetActiveWindowTitle();

            if (IsBrowserActive())
            {
                if (currentActiveBrowserPage != activeWindowTitle)
                {
                    // Якщо попередня сторінка була і таймер працював, збережемо дані
                    if (browserStopwatch.IsRunning)
                    {
                        browserStopwatch.Stop();
                        SaveBrowserActivity(currentActiveBrowserPage, browserStopwatch.Elapsed, "Browser");
                    }

                    // Перезапуск лічильника для нової сторінки
                    browserStopwatch.Reset();
                    browserStopwatch.Start();
                    currentActiveBrowserPage = activeWindowTitle;

                    // Викликаємо методи з BrowserHistoryReader для отримання історії
                    BrowserHistoryReader.GetChromeHistory();  // Отримуємо історію Chrome
                   // BrowserHistoryReader.GetOperaHistory();  // Отримуємо історію Opera
                }
            }
            else
            {
                // Якщо браузер більше не активний, зберігаємо активність
                if (browserStopwatch.IsRunning)
                {
                    browserStopwatch.Stop();
                    SaveBrowserActivity(currentActiveBrowserPage, browserStopwatch.Elapsed, "Browser");
                    currentActiveBrowserPage = null;
                }
            }
        }

        private static void SaveBrowserActivity(string pageTitle, TimeSpan timeSpent, string category)
        {
            List<BrowserActivityLog> logs = new List<BrowserActivityLog>();

            if (File.Exists(BrowserLogPath))
            {
                try
                {
                    string json = File.ReadAllText(BrowserLogPath);
                    logs = JsonConvert.DeserializeObject<List<BrowserActivityLog>>(json) ?? new List<BrowserActivityLog>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка читання файлу: {ex.Message}");
                }
            }

            logs.Add(new BrowserActivityLog
            {
                PageTitle = pageTitle,
                TimeSpent = timeSpent.ToString(@"hh\:mm\:ss"),
                Category = category,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });

            try
            {
                string updatedJson = JsonConvert.SerializeObject(logs, Formatting.Indented);
                File.WriteAllText(BrowserLogPath, updatedJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка запису у файл: {ex.Message}");
            }
        }
    }
}







