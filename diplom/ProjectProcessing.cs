/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;
using System.Windows.Forms; // Додано для використання MessageBox

namespace diplom
{
    public class ProjectProcessor
    {
        private string projectsJsonPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\projects.json";
        private string timerJsonPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\timerAmounts.json";
        private readonly HandButton handButton;

        public ProjectProcessor(HandButton handButton)
        {
            this.handButton = handButton;
        }

        // Метод для перевірки відкритих проектів
        public void StartTimerForOpenedProjects()
        {
            var projectPaths = LoadProjectPaths();  // Завантажуємо шляхи проектів
            var openedFiles = GetOpenedFiles();  // Перевіряємо відкриті процеси

            foreach (var projectPath in projectPaths)
            {
                if (openedFiles.Contains(projectPath))
                {
                    DateTime currentDate = DateTime.Now.Date;
                    TimeSpan accumulatedTime = LoadTimeFromJson(currentDate);  // Завантажуємо час для сьогоднішньої дати
                    handButton.StartWithTime(accumulatedTime);  // Стартуємо таймер із наявного часу
                    break; // Як тільки знайшли відкритий проект, запускаємо таймер
                }
            }
        }

        // Завантажуємо шляхи проектів із JSON
        private string[] LoadProjectPaths()
        {
            try
            {
                var jsonContent = File.ReadAllText(projectsJsonPath);
                var projects = JsonSerializer.Deserialize<List<Project>>(jsonContent);
                return projects?.Select(p => p.Path).ToArray() ?? Array.Empty<string>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при читанні проектів: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Array.Empty<string>();
            }
        }

        // Перевіряємо відкриті процеси на наявність відповідних файлів
        private string[] GetOpenedFiles()
        {
            var processes = Process.GetProcesses();
            var openedFiles = processes
                .Where(p => p.MainWindowTitle.Length > 0)  // Перевіряємо, чи є відкрите вікно
                .Select(p => p.MainWindowTitle)  // Отримуємо назви відкритих файлів
                .ToArray();
            return openedFiles;
        }

        // Завантажуємо час для конкретної дати з JSON
        private TimeSpan LoadTimeFromJson(DateTime date)
        {
            try
            {
                string jsonContent = File.ReadAllText(timerJsonPath);
                var timerData = JsonSerializer.Deserialize<List<TimerData>>(jsonContent);
                var projectTimer = timerData?.FirstOrDefault(t => t.Date == date.ToString("dd.MM.yyyy"));
                return projectTimer != null && TimeSpan.TryParse(projectTimer.Time, out var time)
                    ? time
                    : TimeSpan.Zero;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні часу: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return TimeSpan.Zero;
            }
        }

        // Записуємо час в JSON для конкретної дати
        public void SaveTimeToJson(DateTime date)
        {
            try
            {
                var timerDataList = LoadTimerDataFromJson();

                var existingData = timerDataList.FirstOrDefault(t => t.Date == date.ToString("dd.MM.yyyy"));
                if (existingData != null)
                {
                    existingData.Time = handButton.GetAccumulatedTime().ToString(@"hh\:mm\:ss");
                }
                else
                {
                    timerDataList.Add(new TimerData
                    {
                        Date = date.ToString("dd.MM.yyyy"),
                        Time = handButton.GetAccumulatedTime().ToString(@"hh\:mm\:ss")
                    });
                }

                string jsonContent = JsonSerializer.Serialize(timerDataList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(timerJsonPath, jsonContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка запису в JSON: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Завантажуємо всі таймери з JSON
        private List<TimerData> LoadTimerDataFromJson()
        {
            try
            {
                string jsonContent = File.ReadAllText(timerJsonPath);
                return JsonSerializer.Deserialize<List<TimerData>>(jsonContent) ?? new List<TimerData>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні даних таймеру: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<TimerData>();
            }
        }
    }
}*/


