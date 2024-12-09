using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace diplom
{
    public class FileProcessor
    {
        private readonly string projectsFilePath;

        public FileProcessor(string projectsFilePath)
        {
            this.projectsFilePath = projectsFilePath;
        }

        // Зчитування проектів з JSON файлу
        public List<Project> ReadProjectsFromJson()
        {
            try
            {
                string jsonContent = File.ReadAllText(projectsFilePath);
                return JsonSerializer.Deserialize<List<Project>>(jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при читанні з файлу: {ex.Message}");
                return null;
            }
        }

        // Запис часу в файл JSON
        public void WriteTimerToJson(TimeSpan accumulatedTime)
        {
            var jsonObject = new { ElapsedTime = accumulatedTime.ToString(@"hh\:mm\:ss") };
            try
            {
                string jsonContent = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("timerAmounts.json", jsonContent);
                Console.WriteLine("Час успішно записано в файл timerAmounts.json");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при записі в файл: {ex.Message}");
            }
        }
    }
}
