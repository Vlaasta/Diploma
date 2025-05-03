using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace diplom
{
    public class CohereClient
    {
        private readonly HttpClient _client;
        private const string Url = "https://api.cohere.com/v2/chat";  // Cohere Chat endpoint

        private const int MaxRequestsPerDay = 1;
        private int _requestsSentToday = 0;
        private DateTime _lastRequestDate = DateTime.MinValue;

        public CohereClient(string apiKey)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private bool CanSendRequest()
        {
            if (_lastRequestDate.Date != DateTime.Today)
            {
                _lastRequestDate = DateTime.Today;
                _requestsSentToday = 0;
            }
            return _requestsSentToday < MaxRequestsPerDay;
        }

        private List<Project> LoadProjects(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Project>>(json);
        }

        public string ExtractTextFromDocx(string filePath)
        {
            var text = new StringBuilder();
            try
            {
                using (var wordDoc = WordprocessingDocument.Open(filePath, false))
                {
                    var body = wordDoc.MainDocumentPart.Document.Body;
                    foreach (var para in body.Elements<Paragraph>())
                    {
                        foreach (var run in para.Elements<Run>())
                            foreach (var txt in run.Elements<Text>())
                                text.Append(txt.Text);
                        text.AppendLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при читанні {filePath}: {ex.Message}");
            }
            return text.ToString();
        }

        public string AnalyzeFiles(string projectsJsonPath, string outputJsonPath)
        {
            const string testJsonPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserAnalysis\test.json";
            // 1) Очистимо файл з запитами на початку кожного запуску
           // File.WriteAllText(testJsonPath, string.Empty);

            try
            {
                if (!CanSendRequest())
                {
                    Console.WriteLine("Ліміт запитів на сьогодні перевищено.");
                    return "Ліміт запитів на сьогодні перевищено.";
                }

                var projects = LoadProjects(projectsJsonPath);
                var allResponses = new List<object>();

                foreach (var project in projects)
                {
                    if (!File.Exists(project.Path))
                    {
                        Console.WriteLine($"Файл не знайдено: {project.Path}");
                        allResponses.Add(new { Project = project.Name, Error = "Файл не знайдено" });
                        continue;
                    }

                    var fileText = ExtractTextFromDocx(project.Path);
                    if (string.IsNullOrWhiteSpace(fileText))
                    {
                        allResponses.Add(new { Project = project.Name, Error = "Файл порожній або нечитабельний" });
                        continue;
                    }

                    // Формуємо одне велике повідомлення
                    var userMessage = $"Аналіз проєкту: {Path.GetFileName(project.Path)}\n\n{fileText}";

                    var payload = new
                    {
                        model = "command-r-plus-08-2024",
                        messages = new[]
                        {
                            new { role = "user", content = userMessage }
                        }
                    };

                    // Серіалізуємо payload
                    var jsonPayload = JsonSerializer.Serialize(payload);

                    // 2) Записуємо запит у test.json
                    var logEntry = new
                    {
                        Time = DateTime.Now.ToString("o"),
                        Project = project.Name,
                        Payload = JsonDocument.Parse(jsonPayload).RootElement
                    };
                    File.AppendAllText(
                        testJsonPath,
                        JsonSerializer.Serialize(logEntry, new JsonSerializerOptions { WriteIndented = true })
                        + Environment.NewLine
                    );

                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    var response = _client.PostAsync(Url, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine($"Response for {project.Name}: {responseJson}");

                        allResponses.Add(new { Project = project.Name, Response = responseJson });
                        _requestsSentToday++;
                    }
                    else
                    {
                        var error = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine($"Помилка для {project.Name}: {response.StatusCode} - {error}");
                        allResponses.Add(new { Project = project.Name, Error = $"Помилка: {response.StatusCode} - {error}" });
                        break;
                    }
                }

                // 3) Записуємо всі зібрані відповіді у outputJsonPath
                var resultJson = JsonSerializer.Serialize(allResponses, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(outputJsonPath, resultJson);

                return $"Обробка завершена. Результати збережено в {outputJsonPath}";
            }
            catch (Exception ex)
            {
                return $"Загальна помилка: {ex.Message}";
            }
        }

        public string AnalyzeBrowserUrls(string browserUrlsJsonPath, string outputJsonPath)
        {
            const string testJsonPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserAnalysis\test.json";
            try
            {
                if (!CanSendRequest())
                {
                    Console.WriteLine("Ліміт запитів на сьогодні перевищено.");
                    return "Ліміт запитів на сьогодні перевищено.";
                }

                // 1) Зчитуємо і десеріалізуємо усі записи
                var allTextEntries = JsonSerializer.Deserialize<List<UrlData>>(
                    File.ReadAllText(browserUrlsJsonPath)
                );

                var allResponses = new List<object>();

                foreach (var entry in allTextEntries)
                {
                    // Пропускаємо, якщо текст порожній
                    if (string.IsNullOrWhiteSpace(entry.Text))
                    {
                        allResponses.Add(new { Url = entry.Url, Error = "Text is empty" });
                        continue;
                    }

                    // 2) Формуємо повідомлення для AI
                    var userMessage = new StringBuilder()
                        .AppendLine($"Аналіз веб-сторінки")
                        .AppendLine($"URL: {entry.Url}")
                        .AppendLine($"Заголовок: {entry.PageTitle}")
                        .AppendLine()
                        .AppendLine(entry.Text)
                        .ToString();

                    var payload = new
                    {
                        model = "command-r-plus-08-2024",
                        messages = new[]
                        {
                    new { role = "user", content = userMessage }
                }
                    };
                    var jsonPayload = JsonSerializer.Serialize(payload);

                    // 3) Лог запису запиту
                    var logEntry = new
                    {
                        Time = DateTime.Now.ToString("o"),
                        Url = entry.Url,
                        Payload = JsonDocument.Parse(jsonPayload).RootElement
                    };
                    File.AppendAllText(
                        testJsonPath,
                        JsonSerializer.Serialize(logEntry, new JsonSerializerOptions { WriteIndented = true })
                        + Environment.NewLine
                    );

                    // 4) Надсилаємо запит
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    var response = _client.PostAsync(Url, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine($"Response for {entry.Url}: {responseJson}");
                        allResponses.Add(new { Url = entry.Url, Response = responseJson });
                        _requestsSentToday++;
                    }
                    else
                    {
                        var error = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine($"Помилка для {entry.Url}: {response.StatusCode} - {error}");
                        allResponses.Add(new { Url = entry.Url, Error = $"{response.StatusCode} - {error}" });
                        break;  // або continue, залежно від вашої логіки
                    }
                }

                // 5) Запис результатів у файл
                var resultJson = JsonSerializer.Serialize(allResponses, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(outputJsonPath, resultJson);

                return $"Обробка завершена. Результати збережено в {outputJsonPath}";
            }
            catch (Exception ex)
            {
                return $"Загальна помилка: {ex.Message}";
            }
        }

        public string CompareProjectWebpageSimilarities(string projectsAnalysisPath, string webpagesAnalysisPath, string outputJsonPath)
        {
            const string testJsonPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserAnalysis\test.json";

            if (!CanSendRequest())
                return "Ліміт запитів на сьогодні перевищено.";

            // 1) десеріалізуємо обидва файли
            var projects = JsonSerializer.Deserialize<List<ProjectAnalysis>>(
                File.ReadAllText(projectsAnalysisPath)
            );
            var pages = JsonSerializer.Deserialize<List<UrlAnalysis>>(
                File.ReadAllText(webpagesAnalysisPath)
            );

            var results = new List<SimilarityResult>();

            foreach (var proj in projects)
            {
                foreach (var page in pages)
                {
                    // Формуємо текст запиту
                    var userMessage = new StringBuilder()
                        .AppendLine("Порівняй тематику двох текстів і дай відповідь лише “Схожість виявлено” або “Схожість не виявлено”.")
                        .AppendLine()
                        .AppendLine("Текст проекту:")
                        .AppendLine(proj.Response)
                        .AppendLine()
                        .AppendLine("Текст веб-сторінки:")
                        .AppendLine(page.Response)
                        .ToString();

                    var payload = new
                    {
                        model = "command-r-plus-08-2024",
                        messages = new[]
                        {
                    new { role = "user", content = userMessage }
                }
                    };
                    var jsonPayload = JsonSerializer.Serialize(payload);

                    // Лог запиту
                    var logEntry = new
                    {
                        Time = DateTime.Now.ToString("o"),
                        Project = proj.Project,
                        Url = page.Url,
                        Payload = JsonDocument.Parse(jsonPayload).RootElement
                    };
                    File.AppendAllText(
                        testJsonPath,
                        JsonSerializer.Serialize(logEntry, new JsonSerializerOptions { WriteIndented = true })
                        + Environment.NewLine
                    );

                    // 2) Надсилаємо до AI
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    var resp = _client.PostAsync(Url, content).Result;

                    string similarity;
                    if (resp.IsSuccessStatusCode)
                    {
                        var body = resp.Content.ReadAsStringAsync().Result.Trim();
                        // Відповідь очікуємо коротку, типу "Схожість виявлено"
                        similarity = body.Replace("\"", "");
                        _requestsSentToday++;
                    }
                    else
                    {
                        similarity = $"Error {resp.StatusCode}";
                    }

                    // 3) Зберігаємо результат
                    results.Add(new SimilarityResult
                    {
                        Project = proj.Project,
                        Url = page.Url,
                        Similarity = similarity
                    });

                    // Якщо хочете не перевищити ліміт — можна break тут
                }
            }

            // 4) Записуємо всі поєднання у вихідний файл
            var outJson = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJsonPath, outJson);

            return $"Порівняння завершено. Результати в {outputJsonPath}";
        }

    }
}






