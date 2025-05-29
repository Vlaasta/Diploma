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
        private static string filePath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\MainInfo\timerAmounts.json"; // Шлях до JSON-файлу з результатами таймеру
        static string fileSecondPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\MainInfo\projects.json"; // Шлях до JSON-файлу з проектами
        private static string fileThirdPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\MainInfo\settings.json"; // Шлях до JSON-файлу з настройками
        public static string filePath2 = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserActivity\browserUrls.json"; //Шлях до JSON-файлу з веб-сторінками
        public static string filePath3 = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\unmatchedUrls.json";

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
                        Console.WriteLine($"Шукаємо на диску: {drive.Name}");
                        var files = Directory.EnumerateFiles(drive.RootDirectory.FullName, fileName, SearchOption.AllDirectories);
                        foreach (var file in files)
                        {
                            Console.WriteLine($"Знайдено файл: {file}");
                            result.Add(file);
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine($"Доступ заборонено до диску: {drive.Name}");
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine($"Помилка при доступі до диску {drive.Name}: {ex.Message}");
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

        // Запис проектів у файл
        public static void SaveProjects(List<Project> projects)
        {
            var json = JsonConvert.SerializeObject(projects, Formatting.Indented);
            File.WriteAllText(fileSecondPath, json);
        }

        // Метод для додавання нового проекту через файловий провідник
        public static void AddProjectThroughFileDialog()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Фільтр файлів для пошуку проектів
                openFileDialog.Filter = "Microsoft Visual Studio Files (*.sln;*.csproj)|*.sln;*.csproj|Photoshop Files (*.psd)|*.psd|All Files (*.*)|*.*";
                openFileDialog.Title = "Виберіть проект для додавання";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;
                    string projectName = Path.GetFileNameWithoutExtension(fileName);

                    // Зчитуємо існуючі проекти
                    var projects = LoadProjects();

                    // Додаємо новий проект, якщо його ще немає
                    if (!projects.Exists(p => p.Path == fileName))
                    {
                        projects.Add(new Project { Name = projectName, Path = fileName });
                        SaveProjects(projects);

                        MessageBox.Show("Проект успішно додано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Цей проект вже доданий.", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
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
            if (todayData == null) return;

            var lastSession = todayData.Sessions.LastOrDefault(s => s.Stop == null);
            if (lastSession != null)
            {
                lastSession.Stop = DateTime.Now.ToString("HH:mm:ss");
                EnqueueSaveData(data);
            }
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
            foreach (var url in urlDataList)
            {
                Console.WriteLine($"URL: {url.Url} | Timestamp (UTC): {url.Timestamp} | Timestamp (Local): {url.Timestamp.ToLocalTime()} | Date (Local): {url.Timestamp.ToLocalTime().Date}");
            }

            DateTime today = DateTime.Today;

            todayUrls = urlDataList
                .Where(url => url.Timestamp.Date == today)
                .ToList();

            Console.WriteLine($"URL-дані на сьогодні ({today:yyyy-MM-dd}): {todayUrls.Count} записів");

            var projectsList = JsonProcessing.LoadProjects();
            todayProjects = projectsList
                .Where(project =>
                    File.Exists(project.Path) &&
                    File.GetLastWriteTime(project.Path).Date == today)
                .ToList();

            Console.WriteLine($"Проєкти на сьогодні ({today:yyyy-MM-dd}): {todayProjects.Count} записів");

            bool result = todayUrls.Any() && todayProjects.Any();
            Console.WriteLine($"Результат перевірки умов виконання: {result}");

            return result;
        }

        public static void FilterUrlsBySimilarity(
      string comparisonResultsPath,
      List<UrlData> allUrls,
      string matchedUrlsPath,
      string unmatchedUrlsPath
  )
        {
            Console.WriteLine($"Зчитування файлу: {comparisonResultsPath}");

            var fileContent = File.ReadAllText(comparisonResultsPath);
            Console.WriteLine("Зміст файлу:");
            Console.WriteLine(fileContent);

            var results = JsonConvert.DeserializeObject<List<SimilarityResult>>(fileContent);

            if (results == null)
            {
                Console.WriteLine("Не вдалося десеріалізувати results — null.");
                return;
            }

            Console.WriteLine($"Усього записів у results: {results.Count}");

            var matchedUrlsSet = results
                .Where(r => r.Similarity != null && r.Similarity.Contains("Схожість виявлено"))
                .Select(r => r.Url)
                .Distinct()
                .ToHashSet();

            Console.WriteLine("URL-и з \"Схожість виявлено\":");
            foreach (var url in matchedUrlsSet)
            {
                Console.WriteLine($"  ✅ {url}");
            }

            Console.WriteLine($"Усього вхідних URL-ів (allUrls): {allUrls.Count}");

            var matchedUrls = allUrls.Where(u => matchedUrlsSet.Contains(u.Url)).ToList();
            var unmatchedUrls = allUrls.Where(u => !matchedUrlsSet.Contains(u.Url)).ToList();

            Console.WriteLine($"URL-ів з подібністю знайдено: {matchedUrls.Count}");
            Console.WriteLine($"URL-ів без подібності: {unmatchedUrls.Count}");

            Console.WriteLine("Збереження matchedUrls у файл:");
            Console.WriteLine(matchedUrlsPath);
            File.WriteAllText(matchedUrlsPath, JsonConvert.SerializeObject(matchedUrls, Formatting.Indented));

            Console.WriteLine("Збереження unmatchedUrls у файл:");
            Console.WriteLine(unmatchedUrlsPath);
            File.WriteAllText(unmatchedUrlsPath, JsonConvert.SerializeObject(unmatchedUrls, Formatting.Indented));

            Console.WriteLine("Завершено.");
        }

        public static void LoadSettings()
        {
            if (File.Exists(fileThirdPath))
            {
                string json = File.ReadAllText(fileThirdPath); // Читаємо файл в рядок
                                                                // Десеріалізуємо JSON в об'єкт типу DataSettings
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
                if (Form1.settings.NotificatonOnOff == true)
                {
                    Form1.notificationsOnOff = true;
                    Form1.CheckBox4Active = true;
                    // Form1.NotificationsOn();
                }
                if (Form1.settings.NotificatonOnOff == false)
                {
                    Form1.notificationsOnOff = false;
                    Form1.CheckBox4Active = false;
                }
                Notifications.NotificationsEnabled = Form1.notificationsOnOff;
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