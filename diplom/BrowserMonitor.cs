using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

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
                    if (browserStopwatch.IsRunning)
                    {
                        browserStopwatch.Stop();
                        AnalyzeAndSaveBrowserActivity(currentActiveBrowserPage, browserStopwatch.Elapsed);
                    }

                    browserStopwatch.Reset();
                    browserStopwatch.Start();
                    currentActiveBrowserPage = activeWindowTitle;
                }
            }
            else
            {
                if (browserStopwatch.IsRunning)
                {
                    browserStopwatch.Stop();
                    AnalyzeAndSaveBrowserActivity(currentActiveBrowserPage, browserStopwatch.Elapsed);
                    currentActiveBrowserPage = null;
                }
            }
        }

        private static void AnalyzeAndSaveBrowserActivity(string pageTitle, TimeSpan timeSpent)
        {
            if (string.IsNullOrEmpty(pageTitle) || timeSpent.TotalSeconds < 1) return;

            string browserContent = GetWebPageContent();
            string projectContent = GetProjectContent();

            if (AnalyzeSimilarity(browserContent, projectContent))
            {
                SaveBrowserActivity(pageTitle, timeSpent, "Робота");
            }
            else
            {
                SaveBrowserActivity(pageTitle, timeSpent, "Інше");
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

        private static string GetWebPageContent()
        {
            try
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--headless"); // Прихований режим
                options.AddArgument("--disable-gpu");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--remote-allow-origins=*");

                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true; // Приховує консольне вікно chromedriver.exe

                using (IWebDriver driver = new ChromeDriver(service, options))
                {
                    driver.Navigate().GoToUrl("https://www.google.com");
                    return driver.PageSource;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка отримання вмісту сторінки: {ex.Message}");
                return string.Empty;
            }
        }

        private static string GetProjectContent()
        {
            try
            {
                if (File.Exists(ProjectsPath))
                {
                    return File.ReadAllText(ProjectsPath);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при зчитуванні проекту: {ex.Message}");
                return string.Empty;
            }
        }

        private static bool AnalyzeSimilarity(string text1, string text2)
        {
            if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2)) return false;

            // Розбиваємо текст на слова та переводимо в нижній регістр
            HashSet<string> words1 = new HashSet<string>(text1.ToLower().Split(' '));
            HashSet<string> words2 = new HashSet<string>(text2.ToLower().Split(' '));

            // Рахуємо, скільки спільних слів
            int commonWords = words1.Intersect(words2).Count();
            int totalWords = words1.Count + words2.Count;

            // Визначаємо відсоток схожості (чим більше, тим схожіше)
            double similarity = (2.0 * commonWords) / totalWords;

            return similarity > 0.2; // Наприклад, 20% спільних слів вважаємо схожістю
        }
    }
}


