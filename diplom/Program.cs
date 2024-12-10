using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace diplom
{
    internal static class Program
    {
        private static readonly string ProjectsFilePath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\projects.json";

        // Перевірка чи файл відкритий іншим процесом
        public static bool IsFileLocked(string filePath)
        {
            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return false; // Файл не заблокований
                }
            }
            catch
            {
                return true; // Файл заблокований
            }
        }

        // Завантаження проектів із JSON
        public static List<Project> LoadProjects()
        {
            if (!File.Exists(ProjectsFilePath))
            {
                Console.WriteLine("Файл projects.json не знайдено.");
                return new List<Project>();
            }

            try
            {
                var json = File.ReadAllText(ProjectsFilePath);
                return JsonConvert.DeserializeObject<List<Project>>(json) ?? new List<Project>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка читання файлу: {ex.Message}");
                return new List<Project>();
            }
        }

        // Порівняння файлів із відкритими процесами
        public static void CompareProjectsWithOpenProcesses(List<Project> projects)
        {
            var openFiles = new List<string>();

            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    string windowTitle = process.MainWindowTitle;

                    if (!string.IsNullOrEmpty(windowTitle))
                    {
                        // Зберігаємо відкриті файли, які є в заголовках вікон
                        openFiles.Add(windowTitle);
                    }
                }
                catch
                {
                    // Ігноруємо процеси, які недоступні
                }
            }

            foreach (var project in projects)
            {
                // Перевірка, чи відкритий файл
                bool isFileOpen = openFiles.Any(title =>
                    title.IndexOf(Path.GetFileNameWithoutExtension(project.Path), StringComparison.OrdinalIgnoreCase) >= 0);

                // Альтернатива: спроба відкрити файл
                isFileOpen = isFileOpen || IsFileLocked(project.Path);

                Console.WriteLine(isFileOpen
                    ? $"Файл відкритий: {project.Name}"
                    : $"Файл закритий: {project.Name}");
            }
        }

        static void Main()
        {
            var projects = LoadProjects();
            if (projects.Count == 0)
            {
                Console.WriteLine("Немає проектів для перевірки.");
                return;
            }

            CompareProjectsWithOpenProcesses(projects);
        }
    }
}













