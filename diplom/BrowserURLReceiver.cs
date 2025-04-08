using System;
using System.Data.SQLite;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diplom
{
    public class BrowserHistoryReader
    {
        private static readonly string chromeHistoryPath = @"C:\Users\<UserName>\AppData\Local\Google\Chrome\User Data\Default\History";
        private static readonly string operaHistoryPath = @"C:\Users\<UserName>\AppData\Roaming\Opera Software\Opera Stable\History";
        private static readonly string BrowserLogPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\browserActivity.json";
        private static readonly string UrlHistoryPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\urlhistory.json";

        // Отримуємо історію Chrome
        public static void GetChromeHistory()
        {
            if (File.Exists(chromeHistoryPath))
            {
                string connectionString = $"Data Source={chromeHistoryPath};Version=3;";
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT url, last_visit_time FROM urls ORDER BY last_visit_time DESC LIMIT 10";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            string url = reader["url"].ToString();
                            long lastVisitTime = Convert.ToInt64(reader["last_visit_time"]);
                            DateTime visitTime = FromUnixTime(lastVisitTime); // Перетворюємо час
                            Console.WriteLine($"URL: {url}, Visit Time: {visitTime}");

                            // Тут ви можете додати код для вимірювання часу перебування
                            TimeSpan timeSpent = new TimeSpan(0, 0, 31); // Тут приклад, треба буде вимірювати фактичний час

                            if (timeSpent.TotalSeconds > 30)
                            {
                                // Якщо час перевищує 30 секунд, записуємо URL в окремий файл
                                SaveUrlHistory(url, timeSpent);
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Chrome history file not found.");
            }
        }

        // Збереження URL, на яких користувач перебував більше 30 секунд
        public static void SaveUrlHistory(string url, TimeSpan timeSpent)
        {
            List<UrlHistoryLog> urlLogs = new List<UrlHistoryLog>();

            // Якщо файл існує, читаємо існуючі записи
            if (File.Exists(UrlHistoryPath))
            {
                try
                {
                    string json = File.ReadAllText(UrlHistoryPath);
                    urlLogs = JsonConvert.DeserializeObject<List<UrlHistoryLog>>(json) ?? new List<UrlHistoryLog>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка читання файлу: {ex.Message}");
                }
            }

            // Додаємо новий запис
            urlLogs.Add(new UrlHistoryLog
            {
                Url = url,
                TimeSpent = timeSpent.ToString(),
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });

            // Записуємо оновлені дані в файл
            try
            {
                string updatedJson = JsonConvert.SerializeObject(urlLogs, Formatting.Indented);
                File.WriteAllText(UrlHistoryPath, updatedJson);
                Console.WriteLine("URL збережено в файл.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка запису в файл: {ex.Message}");
            }
        }

        // Перетворення Unix часу в DateTime
        private static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1);
            return epoch.AddMilliseconds(unixTime).ToLocalTime();
        }

        // Структура для логів URL
        public class UrlHistoryLog
        {
            public string Url { get; set; }
            public string TimeSpent { get; set; }
            public string Timestamp { get; set; }
        }
    }
}




