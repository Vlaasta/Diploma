using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms; // Для OpenFileDialog
using Newtonsoft.Json;

namespace diplom
{
    public static class JsonProcessing
    {
        private static string filePath = @"E:\4 KURS\Диплом\diplom\data\timerAmounts.json"; // Шлях до JSON-файлу з статистикою
        private static string fileSecondPath = @"E:\4 KURS\Диплом\diplom\data\projects.json"; // Шлях до JSON-файлу з проектами

        // Зчитування проектів з файлу
        public static List<Project> LoadProjects()
        {
            if (!File.Exists(fileSecondPath))
            {
                return new List<Project>();
            }

            var json = File.ReadAllText(fileSecondPath);
            return JsonConvert.DeserializeObject<List<Project>>(json) ?? new List<Project>();
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

        // Зчитування даних з файлу
        public static List<TimerData> LoadData()
        {
            if (!File.Exists(filePath))
            {
                return new List<TimerData>();
            }

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<TimerData>>(json) ?? new List<TimerData>();
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
            var data = LoadData();
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

        public static void AddProject(string filePath)
        {
            // Зчитуємо поточний список проєктів
            var projects = LoadProjects();

            // Додаємо новий файл як проєкт
            projects.Add(new Project
            {
                Path = filePath,
                Name = Path.GetFileName(filePath),
                Type = Path.GetExtension(filePath)
            });

            // Зберігаємо оновлений список проєктів
            SaveProjects(projects);
        }
    }
}
