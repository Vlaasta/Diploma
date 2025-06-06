using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms; 
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; 
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading;

namespace diplom
{
    public static class JsonProcessing
    {
        private static string filePath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\MainInfo\timerAmounts.json"; //Шлях до JSON-файлу з результатами роботи таймеру
        static string fileSecondPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\MainInfo\projects.json"; //Шлях до JSON-файлу з проєктами
        private static string fileThirdPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\MainInfo\settings.json"; //Шлях до JSON-файлу з налаштуванням програми
        public static string filePath2 = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserActivity\browserUrls.json"; //Шлях до JSON-файлу з веб-сторінками
        public static string filePath3 = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\unmatchedUrls.json"; //Шлях до JSON-файлу з неробочими сторінками

        private static readonly ConcurrentQueue<List<TimerData>> saveQueue = new ConcurrentQueue<List<TimerData>>();
        private static bool isSaving = false;
        private static readonly object saveLock = new object();

        public static List<UrlData> todayUrls = new List<UrlData>();
        public static List<Project> todayProjects = new List<Project>();
        public static List<string> sessionLog = new List<string>();

        public static void EnqueueSaveData(List<TimerData> data)
        {
            saveQueue.Enqueue(data);
            ProcessQueue();
        }

        private static void ProcessQueue()
        {
            lock (saveLock)
            {
                if (isSaving) return;

                isSaving = true;

                while (saveQueue.TryDequeue(out List<TimerData> data))
                {
                    bool saved = false;
                    int attempts = 0;

                    while (!saved && attempts < 5)
                    {
                        try
                        {
                            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                            File.WriteAllText(filePath, json);
                            saved = true;
                        }
                        catch (IOException)
                        {
                            attempts++;
                            Thread.Sleep(100); 
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error saving data: " + ex.Message);
                            break;
                        }
                    }
                }

                isSaving = false;
            }
        }

        private static string FindFile(string fileName)
        {
            var result = new List<string>();

            Parallel.ForEach(DriveInfo.GetDrives(), drive =>
            {
                if (drive.IsReady)
                {
                    try
                    {
                        var files = Directory.EnumerateFiles(drive.RootDirectory.FullName, fileName, SearchOption.AllDirectories);
                        foreach (var file in files)
                        {
                            result.Add(file);
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                }
            });

            return result.FirstOrDefault();
        }

        public static List<Project> LoadProjects()
        {
            if (!File.Exists(fileSecondPath))
                throw new FileNotFoundException($"Файл {fileSecondPath} не знайдено.");

            string jsonContent = File.ReadAllText(fileSecondPath);
            var projects = JsonConvert.DeserializeObject<List<Project>>(jsonContent) ?? new List<Project>();

            bool changesMade = false;

            for (int i = 0; i < projects.Count; i++)
            {
                if (!File.Exists(projects[i].Path))
                {
                    // Пошук файлу по всьому комп'ютеру
                    string newPath = FindFile(projects[i].Name);

                    if (!string.IsNullOrEmpty(newPath))
                    {
                        projects[i].Path = newPath; // Оновлюємо шлях
                        changesMade = true;
                    }
                    else
                    {
                        // Якщо файл не знайдено – видаляємо його зі списку
                        projects.RemoveAt(i);
                        i--; // Щоб не пропустити наступний елемент
                        changesMade = true;
                    }
                }
            }

            // Якщо були зміни – зберігаємо оновлений список
            if (changesMade)
                SaveProjects(projects);

            return projects;
        }

        public static void SaveProjects(List<Project> projects)
        {
            var json = JsonConvert.SerializeObject(projects, Formatting.Indented);
            File.WriteAllText(fileSecondPath, json);
        }

        public static List<TimerData> LoadTimerData()
        {
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<TimerData>>(jsonContent) ?? new List<TimerData>();
            }
            return new List<TimerData>();
        }

        public static List<UrlData> LoadUrlData()
        {
            if (File.Exists(filePath2))
            {
                string jsonContent = File.ReadAllText(filePath2);
                return JsonConvert.DeserializeObject<List<UrlData>>(jsonContent) ?? new List<UrlData>();
            }
            return new List<UrlData>();
        }

        public static void SaveData(List<TimerData> data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static void SaveUrlToJson(string jsonBody)
        {
            var data = JsonConvert.DeserializeObject<UrlData>(jsonBody);

            //Не записуємо, якщо TimeSpent == 0
            /*if (data.TimeSpent == 0)
                return;*/

            TimeZoneInfo ukrainianZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
            data.Timestamp = TimeZoneInfo.ConvertTimeFromUtc(data.Timestamp, ukrainianZone);

            List<UrlData> urlList;
            if (File.Exists(filePath2))
            {
                var existingJson = File.ReadAllText(filePath2);
                urlList = JsonConvert.DeserializeObject<List<UrlData>>(existingJson) ?? new List<UrlData>();
            }
            else
            {
                urlList = new List<UrlData>();
            }

            urlList.Add(data);
            File.WriteAllText(filePath2, JsonConvert.SerializeObject(urlList, Formatting.Indented));
        }

        public static void SaveCurrentDayTime(TimeSpan elapsed)
        {
            var data = LoadTimerData();
            string today = DateTime.Now.ToString("dd.MM.yyyy");

            var todayData = data.Find(d => d.Date == today);

            if (todayData == null)
            {
                todayData = new TimerData { Date = today, Time = elapsed.ToString(@"hh\:mm\:ss") };
                data.Add(todayData);
            }
            else
            {
                todayData.Time = elapsed.ToString(@"hh\:mm\:ss");
            }

            EnqueueSaveData(data);
        }

        public static void SaveSessionStart()
        {
            var data = LoadTimerData();
            string today = DateTime.Now.ToString("dd.MM.yyyy");

            var todayData = data.Find(d => d.Date == today)
                            ?? new TimerData { Date = today };

            var last = todayData.Sessions.LastOrDefault();
            if (last != null && last.Stop == null)
            {
                return;
            }

            todayData.Sessions.Add(new Session
            {
                Start = DateTime.Now.ToString("HH:mm:ss"),
                Stop = null
            });

            if (!data.Contains(todayData))
                data.Add(todayData);

            EnqueueSaveData(data);
        }

        public static void SaveSessionStop()
        {
            var data = LoadTimerData();
            string today = DateTime.Now.ToString("dd.MM.yyyy");
            var todayData = data.Find(d => d.Date == today);
            if (todayData == null)
            {
                return;
            }

            var lastSession = todayData.Sessions.LastOrDefault(s => s.Stop == null);
            if (lastSession == null)
            {
                return;
            }

            lastSession.Stop = DateTime.Now.ToString("HH:mm:ss");

            SaveData(data);

        }

        public static void AddProject(string filePath)
        {
            var projects = LoadProjects();

            projects.Add(new Project
            {
                Path = filePath,
                Name = Path.GetFileName(filePath),
                Type = Path.GetExtension(filePath)
            });
            SaveProjects(projects);
        }

        public static void SaveSettings(Form1 form1)
        {
            string json = JsonConvert.SerializeObject(Form1.settings, Formatting.Indented);
            File.WriteAllText(fileThirdPath, json);
        }

        public static bool WasFileModifiedToday(string filePath)
        {
            if (File.Exists(filePath))
            {
                DateTime lastWriteDate = File.GetLastWriteTime(filePath).Date;
                return lastWriteDate == DateTime.Today;
            }

            return false;
        }

        public static bool WasProjectAnalyzedToday(string projectName, string analysisFilePath)
        {
            if (!File.Exists(analysisFilePath))
                return false;

            var json = File.ReadAllText(analysisFilePath);
            var records = JsonConvert.DeserializeObject<JArray>(json);

            string today = DateTime.Now.ToString("dd.MM.yyyy");

            foreach (var record in records)
            {
                string name = record["Project"]?.ToString();
                string date = record["AnalysisDate"]?.ToString();

                if (name == projectName && date == today)
                    return true;
            }

            return false;
        }

        public static bool WasUrlAnalyzedToday(string url, string analysisFilePath)
        {
            if (!File.Exists(analysisFilePath))
                return false;

            var json = File.ReadAllText(analysisFilePath).Trim();

            if (string.IsNullOrEmpty(json))
                return false;

            JArray records;

            try
            {
                records = JsonConvert.DeserializeObject<JArray>(json);
            }
            catch
            {
                Console.WriteLine("Некоректний JSON у файлі аналізу URL.");
                return false;
            }

            if (records == null)
                return false;

            string today = DateTime.Now.ToString("dd.MM.yyyy");

            foreach (var record in records)
            {
                string recordUrl = record["Url"]?.ToString();
                string analysisDate = record["AnalysisDate"]?.ToString();

                Console.WriteLine($"Перевіряємо URL '{url}' проти запису: Url='{recordUrl}', AnalysisDate='{analysisDate}'");

                if (recordUrl == url && analysisDate == today)
                {
                    Console.WriteLine($"URL '{url}' вже проаналізовано сьогодні.");
                    return true;
                }
            }

            return false;
        }

        public static void SaveUrlListToJson(List<UrlData> urlList)
        {
            var json = JsonConvert.SerializeObject(urlList, Formatting.Indented);
            File.WriteAllText(filePath2, json);
        }

        public static bool IfWasModifiedToday()
        {
            var urlDataList = JsonProcessing.LoadUrlData();

            DateTime today = DateTime.Today;

            todayUrls = urlDataList
                .Where(url => url.Timestamp.Date == today)
                .ToList();

            var projectsList = JsonProcessing.LoadProjects();
            todayProjects = projectsList
                .Where(project =>
                    File.Exists(project.Path) &&
                    File.GetLastWriteTime(project.Path).Date == today)
                .ToList();

            bool result = todayUrls.Any() && todayProjects.Any();
            return result;
        }

        public static void FilterUrlsBySimilarity(string comparisonResultsPath, List<UrlData> allUrls, string matchedUrlsPath, string unmatchedUrlsPath)
        {
            var fileContent = File.ReadAllText(comparisonResultsPath);

            var results = JsonConvert.DeserializeObject<List<SimilarityResult>>(fileContent);

            if (results == null)
            {
                return;
            }

            var matchedUrlsSet = results
                .Where(r => r.Similarity != null && r.Similarity.Contains("Схожість виявлено"))
                .Select(r => r.Url)
                .Distinct()
                .ToHashSet();

            var matchedUrls = allUrls.Where(u => matchedUrlsSet.Contains(u.Url)).ToList();
            var unmatchedUrls = allUrls.Where(u => !matchedUrlsSet.Contains(u.Url)).ToList();

            File.WriteAllText(matchedUrlsPath, JsonConvert.SerializeObject(matchedUrls, Formatting.Indented));
            File.WriteAllText(unmatchedUrlsPath, JsonConvert.SerializeObject(unmatchedUrls, Formatting.Indented));
        }

        public static void LoadSettings()
        {
            if (File.Exists(fileThirdPath))
            {
                string json = File.ReadAllText(fileThirdPath); 
                                                                
                Form1.settings = JsonConvert.DeserializeObject<DataSettings>(json) ?? new DataSettings();

                if(Form1.settings.InactivityAmount == 5)
                {
                    Form1.nonActiveTime = 5;
                    Form1.CheckBox5Active = true;
                    Form1.CheckBox6Active = false;
                    Form1.CheckBox7Active = false;
                }
                if (Form1.settings.InactivityAmount == 10)
                {
                    Form1.nonActiveTime = 10;
                    Form1.CheckBox6Active = true;
                    Form1.CheckBox7Active = false;
                    Form1.CheckBox5Active = false;
                }
                if (Form1.settings.InactivityAmount == 15)
                {
                    Form1.nonActiveTime = 15;
                    Form1.CheckBox7Active = true;
                    Form1.CheckBox6Active = false;
                    Form1.CheckBox5Active = false;
                }
                if (Form1.settings.ColorTheme == "dark")
                {
                    Form1.CheckBox1Active = true;
                    Form1.CheckBox2Active = false;
                }
                if (Form1.settings.ColorTheme == "light")
                {
                    Form1.CheckBox1Active = false;
                    Form1.CheckBox2Active = true;
                }
                if (Form1.settings.Autostart == true)
                {
                    Form1.CheckBox3Active = true;
                }
                if (Form1.settings.Autostart == false)
                {
                    Form1.CheckBox3Active = false;
                    Form1.autoStart = false;
                }
                if (Form1.settings.NotificatonOnOff == true)
                {
                    Form1.notificationsOnOff = true;
                    Form1.autoStart = true;
                }
                if (Form1.settings.NotificatonOnOff == false)
                {
                    Form1.notificationsOnOff = false;
                    Form1.CheckBox4Active = false;
                }
                Notifications.NotificationsEnabled = Form1.notificationsOnOff;
                Form1.AutostartEnabled = Form1.autoStart;
                if (Form1.settings.TextBoxValue != 0)
                {
                    Form1.nonActiveTime = Form1.settings.TextBoxValue;
                    Form1.CheckBox6Active = false;
                    Form1.CheckBox7Active = false;
                    Form1.CheckBox5Active = false;
                }
            }
        }
    }
}