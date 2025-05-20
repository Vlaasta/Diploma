using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 
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
        private static readonly string lastRunFilePath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\lastRun.txt";

        private static List<UrlData> todayUrls = new List<UrlData>();
        private static List<Project> todayProjects = new List<Project>();
        public static List<string> sessionLog = new List<string>();

        public CohereClient(string apiKey)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static bool CanSendRequest()
        {
            try
            {
                DateTime lastRun = File.Exists(lastRunFilePath)
                    ? DateTime.Parse(File.ReadAllText(lastRunFilePath))
                    : DateTime.MinValue;
                DateTime today = DateTime.Today;

                Console.WriteLine($"LastRun: {lastRun:yyyy-MM-dd}, Today: {today:yyyy-MM-dd}");

                if (lastRun < today && УмовиВиконання())
                {
                    RunDailyTask(); // запускаємо тільки один раз
                    File.WriteAllText(lastRunFilePath, today.ToString("yyyy-MM-dd")); // зберігаємо дату
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка в CanSendRequest:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return false;
        }

        private string ExtractTextFromDocx(string filePath)
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

        private bool SendMessageToAI(string userMessage, out string aiResponse)
        {
            aiResponse = string.Empty;

            try
            {
                var payload = new
                {
                    model = "command-r-plus-08-2024",
                    messages = new[]
                    { new { role = "user", content = userMessage } }
                };

                var jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = _client.PostAsync(Url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    aiResponse = response.Content.ReadAsStringAsync().Result;
                    return true;
                }
                else
                {
                    var error = response.Content.ReadAsStringAsync().Result;
                    aiResponse = $"{response.StatusCode} - {error}";
                    return false;
                }
            }
            catch (Exception ex)
            {
                aiResponse = $"Exception: {ex.Message}";
                return false;
            }
        }

        private string AnalyzeFiles(string outputJsonPath)
        {
            try
            {
                var projects = JsonProcessing.LoadProjects();
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

                    var userMessage = $"Аналіз проєкту: {Path.GetFileName(project.Path)}\n\n{fileText}";

                    var success = SendMessageToAI(userMessage, out string aiResponse);

                    if (success)
                    {
                        Console.WriteLine($"Response for {project.Name}: {aiResponse}");
                        allResponses.Add(new { Project = project.Name, Response = aiResponse });
                        _requestsSentToday++;
                    }
                    else
                    {
                        Console.WriteLine($"Помилка при обробці проєкту {project.Name}: {aiResponse}");
                        allResponses.Add(new { Project = project.Name, Error = aiResponse });
                        break;
                    }
                }

                var resultJson = JsonSerializer.Serialize(allResponses, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(outputJsonPath, resultJson);

                return $"Обробка завершена. Результати збережено в {outputJsonPath}";
            }
            catch (Exception ex)
            {
                return $"Загальна помилка: {ex.Message}";
            }
        }

        private string AnalyzeBrowserUrls(string outputJsonPath)
        {
            try
            {
                string jsonContent = File.ReadAllText(JsonProcessing.filePath2);
                var allTextEntries = JsonSerializer.Deserialize<List<UrlData>>(jsonContent);
                var allResponses = new List<object>();

                foreach (var entry in allTextEntries)
                {
                    if (string.IsNullOrWhiteSpace(entry.Text))
                    {
                        allResponses.Add(new { Url = entry.Url, Error = "Text is empty" });
                        continue;
                    }

                    var userMessage = $"Аналіз веб-сторінки: {Path.GetFileName(entry.Url)}\n\n{entry.Text}";
                    var responseSuccess = SendMessageToAI(userMessage, out string aiResponse);

                    if (responseSuccess)
                    {
                        Console.WriteLine($"Response for {entry.Url}: {aiResponse}");
                        allResponses.Add(new { Url = entry.Url, Response = aiResponse });
                        _requestsSentToday++;
                    }
                    else
                    {
                        Console.WriteLine($"Помилка для {entry.Url}: {aiResponse}");
                        allResponses.Add(new { Url = entry.Url, Error = aiResponse });
                        continue; 
                    }
                }

                var resultJson = JsonSerializer.Serialize(allResponses, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(outputJsonPath, resultJson);

                return $"Обробка завершена. Результати збережено в {outputJsonPath}";
            }
            catch (Exception ex)
            {
                return $"Загальна помилка: {ex.Message}";
            }
        }

        private string CompareProjectWebpageSimilarities(string projectsAnalysisPath, string webpagesAnalysisPath, string outputJsonPath)
        {
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
                    string userMessage = "Порівняй тематику двох текстів і дай відповідь лише “Схожість виявлено” або “Схожість не виявлено”.\n\n" + "Текст проекту:\n" + proj.Response + "\n\n" + "Текст веб-сторінки:\n" + page.Response;

                    bool success = SendMessageToAI(userMessage, out string aiResponse);

                    string similarity = success ? aiResponse.Trim().Replace("\"", "") : $"Error";

                    if (success)
                        _requestsSentToday++;

                    results.Add(new SimilarityResult
                    {
                        Project = proj.Project,
                        Url = page.Url,
                        Similarity = similarity
                    });
                }
            }

            var outJson = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputJsonPath, outJson);

            return $"Порівняння завершено. Результати в {outputJsonPath}";
        }

        private static bool УмовиВиконання()
        {
            DateTime today = DateTime.Today;

            // Завантажуємо URL-дані
            var urlDataList = JsonProcessing.LoadUrlData();
            todayUrls = urlDataList
                .Where(url => url.Timestamp.Date == today)
                .ToList();

            Console.WriteLine($"URL-дані на сьогодні ({today:yyyy-MM-dd}): {todayUrls.Count} записів");

            // Завантажуємо проєкти
            var projectsList = JsonProcessing.LoadProjects();
            todayProjects = projectsList
                .Where(project =>
                    File.Exists(project.Path) &&
                    File.GetLastWriteTime(project.Path).Date == today)
                .ToList();

            Console.WriteLine($"Проєкти на сьогодні ({today:yyyy-MM-dd}): {todayProjects.Count} записів");

            bool result = todayUrls.Any() || todayProjects.Any();
            Console.WriteLine($"Результат перевірки умов виконання: {result}");

            return result;
        }


        public static void RunDailyTask()
        {
            try
            {
                string apiKey = "Palmi92v7dC5q2FIMoVG4PX3GtkIa5dQZJzHc9zZ";

                string outputProjectsJson = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserAnalysis\projectsAnalysis.json";
                string outputUrlsJsonPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserAnalysis\webpagesAnalysis.json";

                string projectsAnalysisPath = outputProjectsJson;
                string webpagesAnalysisPath = outputUrlsJsonPath;
                string outputJsonPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserAnalysis\analysisResults.json";

                Console.WriteLine("Ініціалізація клієнта...");
                CohereClient deepSeekClient = new CohereClient(apiKey);

                if (!JsonProcessing.WasFileModifiedToday(@"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserAnalysis\projectsAnalysis.json"))
                {
                    Console.WriteLine("Аналіз проєктів...");
                    string projectAnalysisResult = deepSeekClient.AnalyzeFiles(outputProjectsJson);
                    Console.WriteLine("Результат аналізу проєктів:");
                    Console.WriteLine(projectAnalysisResult);
                }

                if (!JsonProcessing.WasFileModifiedToday(@"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserAnalysis\webpagesAnalysis.json"))
                {
                    Console.WriteLine("Аналіз вебсторінок...");
                    string urlAnalysisResult = deepSeekClient.AnalyzeBrowserUrls(outputUrlsJsonPath);
                    Console.WriteLine("Результат аналізу вебсторінок:");
                    Console.WriteLine(urlAnalysisResult);
                }
                if (!JsonProcessing.WasFileModifiedToday(@"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserAnalysis\analysisResults.json"))
                {
                    Console.WriteLine("Порівняння схожості між проєктами та сторінками...");
                    string resultMessage = deepSeekClient.CompareProjectWebpageSimilarities(
                        projectsAnalysisPath, webpagesAnalysisPath, outputJsonPath);
                    Console.WriteLine("Результат порівняння:");
                    Console.WriteLine(resultMessage);
                }
            
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка під час виконання RunDailyTask:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Внутрішня помилка:");
                    Console.WriteLine(ex.InnerException.Message);
                    Console.WriteLine(ex.InnerException.StackTrace);
                }
            }
        }

    }
}






