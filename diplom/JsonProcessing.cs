using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms; 
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;

namespace diplom
{
    public static class JsonProcessing
    {
        private static string filePath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\MainInfo\timerAmounts.json"; // Шлях до JSON-файлу з результатами таймеру
        static string fileSecondPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\MainInfo\projects.json"; // Шлях до JSON-файлу з проектами
        private static string fileThirdPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\MainInfo\settings.json"; // Шлях до JSON-файлу з настройками
       // private static string fileFourthPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserAnalysis\test.json"; // Шлях до JSON-файлу з запитами
        public static string filePath2 = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserActivity\browserUrls.json"; //Шлях до JSON-файлу з веб-сторінками

        private static string FindFile(string fileName)
        {
            var result = new List<string>();

            // Використовуємо паралельний пошук на всіх дисках
            Parallel.ForEach(DriveInfo.GetDrives(), drive =>
            {
                if (drive.IsReady) // Переконуємось, що диск доступний
                {
                    try
                    {
                        Console.WriteLine($"Шукаємо на диску: {drive.Name}"); // Додано діагностичний вивід
                        var files = Directory.EnumerateFiles(drive.RootDirectory.FullName, fileName, SearchOption.AllDirectories);
                        foreach (var file in files)
                        {
                            Console.WriteLine($"Знайдено файл: {file}"); // Виводимо знайдений файл
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

            // Якщо були знайдені файли, повертаємо перший з них
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

        public static string SaveResultToFile(string path, object data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(path, json);
                return $"Обробка завершена. Результати збережено в {path}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка збереження файлу:\n{ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return $"Помилка збереження: {ex.Message}";
            }
        }


        // Зчитування даних з файлу
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

        // Запис даних у файл
        public static void SaveData(List<TimerData> data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

          // Збереження часу поточного дня
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

            SaveData(data);
        }

        public static void SaveUrlToJson(string jsonBody)
        {
            var data = JsonConvert.DeserializeObject<UrlData>(jsonBody);

            // Конвертуємо UTC-дату у локальний час України
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


        public static void SaveSessionStart()
        {
            var data = LoadTimerData();
            string today = DateTime.Now.ToString("dd.MM.yyyy");

            var todayData = data.Find(d => d.Date == today)
                            ?? new TimerData { Date = today };

            // Якщо вже є відкрита сесія (Stop == null) — нічого не робимо
            var last = todayData.Sessions.LastOrDefault();
            if (last != null && last.Stop == null)
            {
                return;
            }

            // Інакше — додаємо нову
            todayData.Sessions.Add(new Session
            {
                Start = DateTime.Now.ToString("HH:mm:ss"),
                Stop = null
            });

            if (!data.Contains(todayData))
                data.Add(todayData);

            SaveData(data);
        }


        public static void SaveSessionStop()
        {
            var data = LoadTimerData();
            string today = DateTime.Now.ToString("dd.MM.yyyy");
            var todayData = data.Find(d => d.Date == today);
            if (todayData == null) return;

            // Закриваємо останню відкриту сесію
            var lastSession = todayData.Sessions.LastOrDefault(s => s.Stop == null);
            if (lastSession != null)
            {
                lastSession.Stop = DateTime.Now.ToString("HH:mm:ss");
                SaveData(data);
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