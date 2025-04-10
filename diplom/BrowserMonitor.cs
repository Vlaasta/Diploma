using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;
using System.Globalization;


namespace diplom
{
    public static class BrowserMonitor
    {
        private static string currentActiveBrowserPage = null;
        private static Stopwatch browserStopwatch = new Stopwatch();
        private static readonly string BrowserLogPath = @"E:\\4 KURS\\Диплом\\DiplomaRepo\\Diploma\\data\\browserActivity.json";
        private static readonly string UrlsPath = @"E:\\4 KURS\\Диплом\\DiplomaRepo\\Diploma\\data\\browserUrls.json";


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
                       // SaveBrowserActivity(currentActiveBrowserPage, browserStopwatch.Elapsed, "Browser");
                    }

                    // Перезапуск лічильника для нової сторінки
                    browserStopwatch.Reset();
                    browserStopwatch.Start();
                    currentActiveBrowserPage = activeWindowTitle;
                }
            }
            else
            {
                // Якщо браузер більше не активний, зберігаємо активність
                if (browserStopwatch.IsRunning)
                {
                    browserStopwatch.Stop();
                    //SaveBrowserActivity(currentActiveBrowserPage, browserStopwatch.Elapsed, "Browser");
                    currentActiveBrowserPage = null;
                }
            }
        }

       /* private static void SaveBrowserActivity(string pageTitle, TimeSpan timeSpent, string category)
        {
            //string url = SaveUrlController.LastReceivedUrl; // отримаємо останній URL з оперативної памʼяті

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
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
               // Url = url // отут додається
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

            MatchClosestUrls(BrowserLogPath, UrlsPath);
            Console.WriteLine("Готово! URL-адреси оновлено.");
        }

        public static void MatchClosestUrls(string activityPath, string urlPath)
        {
            string activityJson = File.ReadAllText(activityPath);
            string urlJson = File.ReadAllText(urlPath);

            var activityLogs = JsonConvert.DeserializeObject<List<BrowserActivityLog>>(activityJson);
            var urlDataList = JsonConvert.DeserializeObject<List<UrlData>>(urlJson);

            foreach (var log in activityLogs)
            {
                if (!DateTime.TryParseExact(log.Timestamp, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime logTime))
                    continue;

                UrlData closestUrl = null;
                TimeSpan minDiff = TimeSpan.MaxValue;

                foreach (var urlEntry in urlDataList)
                {
                    if (!DateTime.TryParse(urlEntry.Timestamp, null, DateTimeStyles.AdjustToUniversal, out DateTime urlTimeUtc))
                        continue;

                    DateTime urlTimeLocal = urlTimeUtc.ToLocalTime(); // Приводимо до локального часу

                    var diff = (logTime - urlTimeLocal).Duration(); // Абсолютна різниця

                    if (diff < minDiff)
                    {
                        minDiff = diff;
                        closestUrl = urlEntry;
                    }
                }

                if (closestUrl != null)
                {
                    log.Url = closestUrl.Url; // Перезаписуємо, навіть якщо щось було
                }
            }

            string updatedJson = JsonConvert.SerializeObject(activityLogs, Formatting.Indented);
            File.WriteAllText(activityPath, updatedJson);
        }*/

    }
}







