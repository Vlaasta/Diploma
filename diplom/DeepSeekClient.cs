using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using ClosedXML.Excel;
using UglyToad.PdfPig;

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
        public static List<string> sessionLog = new List<string>();

        static string apiKey = "Palmi92v7dC5q2FIMoVG4PX3GtkIa5dQZJzHc9zZ";

        const string outputProjectsJson = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserAnalysis\projectsAnalysis.json";
        const string outputUrlsJsonPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserAnalysis\webpagesAnalysis.json";

        public const string outputJsonPath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserAnalysis\analysisResults.json";

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

                if (lastRun < today && JsonProcessing.IfWasModifiedToday())
                {
                    RunDailyTask();
                   // JsonProcessing.FilterUrlsBySimilarity(outputJsonPath, JsonProcessing.todayUrls, JsonProcessing.filePath2, JsonProcessing.filePath3);
                    File.WriteAllText(lastRunFilePath, today.ToString("yyyy-MM-dd")); 
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

        private string ExtractTextFromFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            var text = new StringBuilder();

            string[] plainTextExtensions = { ".txt", ".log", ".md", ".json", ".xml", ".html", ".cs", ".cpp", ".js", ".ts", ".java", ".py",
                                             ".yml", ".yaml", ".ini", ".sln", ".bat", ".sh", ".csv", ".config", ".props", ".targets" };

            try
            {
                switch (extension)
                {
                    case ".docx":
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
                        return text.ToString();

                    case ".xls":
                    case ".xlsx":
                        using (var workbook = new XLWorkbook(filePath))
                        {
                            foreach (var ws in workbook.Worksheets)
                            {
                                foreach (var row in ws.RowsUsed())
                                {
                                    foreach (var cell in row.CellsUsed())
                                    {
                                        text.Append(cell.GetValue<string>() + "\t");
                                    }
                                    text.AppendLine();
                                }
                            }
                        }
                        return text.ToString();

                    case ".pdf":
                        using (var document = PdfDocument.Open(filePath)) // PdfPig
                        {
                            foreach (var page in document.GetPages())
                            {
                                text.AppendLine(page.Text);
                            }
                        }
                        return text.ToString();

                    default:
                        if (plainTextExtensions.Contains(extension))
                        {
                            return File.ReadAllText(filePath);
                        }
                        else
                        {
                            // Для невідомих типів — спроба прочитати як текст
                            return File.ReadAllText(filePath);
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при читанні {filePath}: {ex.Message}");
                return string.Empty;
            }
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

                var jsonPayload = JsonConvert.SerializeObject(payload);
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

        private string AnalyzeFiles(string outputJsonPath, List<Project> projects)
        {
            try
            {
                var allResponses = new List<object>();

                foreach (var project in projects)
                {
                    if (!File.Exists(project.Path))
                    {
                        Console.WriteLine($"Файл не знайдено: {project.Path}");
                        allResponses.Add(new { Project = project.Name, Error = "Файл не знайдено" });
                        continue;
                    }

                    var fileText = ExtractTextFromFile(project.Path);
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
                        allResponses.Add(new
                        {
                            Project = project.Name,
                            Response = aiResponse,
                            AnalysisDate = DateTime.Now.ToString("dd.MM.yyyy")
                        });
                        _requestsSentToday++;
                    }
                    else
                    {
                        Console.WriteLine($"Помилка при обробці проєкту {project.Name}: {aiResponse}");
                        allResponses.Add(new { Project = project.Name, Error = aiResponse });
                        break;
                    }
                }

                var resultJson = JsonConvert.SerializeObject(allResponses, Formatting.Indented);
                File.WriteAllText(outputJsonPath, resultJson);

                return $"Обробка завершена. Результати збережено в {outputJsonPath}";
            }
            catch (Exception ex)
            {
                return $"Загальна помилка: {ex.Message}";
            }
        }

        private string AnalyzeBrowserUrls(string outputJsonPath, List<UrlData> urls)
        {
            try
            {
                Console.WriteLine("=== ПОЧАТОК АНАЛІЗУ URL ===");
                Console.WriteLine($"📥 Шлях до JSON-файлу: {outputJsonPath}");
                Console.WriteLine($"📌 Всього URL на вхід: {urls.Count}");

                List<dynamic> allResponses;

                // Завантажуємо існуючі результати або ініціалізуємо порожній список
                if (File.Exists(outputJsonPath))
                {
                    Console.WriteLine("📂 JSON-файл існує. Завантаження вмісту...");
                    var existingContent = File.ReadAllText(outputJsonPath).Trim();

                    if (!string.IsNullOrWhiteSpace(existingContent))
                    {
                        try
                        {
                            allResponses = JsonConvert.DeserializeObject<List<dynamic>>(existingContent) ?? new List<dynamic>();
                            Console.WriteLine($"✅ Завантажено {allResponses.Count} попередніх записів.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ Помилка десеріалізації: {ex.Message}");
                            allResponses = new List<dynamic>();
                        }
                    }
                    else
                    {
                        Console.WriteLine("⚠️ Файл порожній.");
                        allResponses = new List<dynamic>();
                    }
                }
                else
                {
                    Console.WriteLine("📄 JSON-файл ще не створено.");
                    allResponses = new List<dynamic>();
                }

                // URL, які вже аналізувалися
                var analyzedUrls = new HashSet<string>(
                    allResponses
                    .Where(r => r.Response != null && r.Error == null)
                    .Select(r => (string)r.Url)
                );

                Console.WriteLine($"🧾 Вже проаналізовано URL: {analyzedUrls.Count}");

                var urlsToAnalyze = urls.Where(u => !analyzedUrls.Contains(u.Url)).ToList();
                Console.WriteLine($"🔍 Нових URL для аналізу: {urlsToAnalyze.Count}");

                // Розбиття по категоріях
                // Визначити YouTube URL серед нових URL для аналізу
                var youtubeUrls = urlsToAnalyze
                    .Where(u => u.Url.Contains("youtube.com") || u.Url.Contains("youtu.be"))
                    .ToList();

                // Всі інші URL (окрім YouTube)
                var otherUrls = urlsToAnalyze.Except(youtubeUrls).ToList();

                // Для інших URL — розбиваємо по тексту
                var urlsWithoutText = otherUrls.Where(u => string.IsNullOrWhiteSpace(u.Text)).ToList();
                var urlsWithText = otherUrls.Where(u => !string.IsNullOrWhiteSpace(u.Text)).ToList();

                Console.WriteLine($"YouTube URL (автоматичний запис): {youtubeUrls.Count}");
                Console.WriteLine($"URL без тексту: {urlsWithoutText.Count}");
                Console.WriteLine($"URL з текстом: {urlsWithText.Count}");

                // YouTube: додаємо без AI, Response = PageTitle
                foreach (var entry in youtubeUrls)
                {
                    allResponses.Add(new
                    {
                        Url = entry.Url,
                        Response = entry.PageTitle,
                        AnalysisDate = DateTime.Now.ToString("dd.MM.yyyy")
                    });

                    Console.WriteLine($"✔ YouTube URL додано без аналізу: {entry.Url} (PageTitle)");
                }

                // Інші URL без тексту — можеш або додати з PageTitle, або обробити інакше
                foreach (var entry in urlsWithoutText)
                {
                    allResponses.Add(new
                    {
                        Url = entry.Url,
                        Response = entry.PageTitle,
                        AnalysisDate = DateTime.Now.ToString("dd.MM.yyyy")
                    });

                    Console.WriteLine($"✔ URL без тексту додано без аналізу: {entry.Url} (PageTitle)");
                }

                // URL з текстом — аналіз через AI
                foreach (var entry in urlsWithText)
                {
                    var userMessage = $"Аналіз веб-сторінки: {Path.GetFileName(entry.Url)}\n\n{entry.Text}";
                    Console.WriteLine($"🤖 Аналізую текст для: {entry.Url}");
                    var responseSuccess = SendMessageToAI(userMessage, out string aiResponse);
                    Console.WriteLine($"✅ AI відповідь: {(responseSuccess ? "успішна" : "❌ помилка")}");
                    Console.WriteLine($"=== AI Output ===\n{aiResponse}\n=================");

                    if (responseSuccess)
                    {
                        allResponses.Add(new
                        {
                            Url = entry.Url,
                            Response = aiResponse,
                            AnalysisDate = DateTime.Now.ToString("dd.MM.yyyy")
                        });

                        _requestsSentToday++;
                    }
                    else
                    {
                        allResponses.Add(new { Url = entry.Url, Error = aiResponse });
                    }
                }

                // Фінальна перевірка перед записом
                Console.WriteLine($"💾 Загальна кількість записів до збереження: {allResponses.Count}");
                var resultJson = JsonConvert.SerializeObject(allResponses, Formatting.Indented);
                File.WriteAllText(outputJsonPath, resultJson);
                Console.WriteLine($"✅ Результати збережено у файл: {outputJsonPath}");

                return $"Обробка завершена. Результати збережено в {outputJsonPath}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔥 Виняток: {ex.Message}");
                return $"Загальна помилка: {ex.Message}";
            }
        }

        private string CompareProjectWebpageSimilarities(string projectsAnalysisPath, string webpagesAnalysisPath, string outputJsonPath)
        {
            try
            {
                Console.WriteLine("Читання файлів...");
                if (!File.Exists(projectsAnalysisPath) || !File.Exists(webpagesAnalysisPath))
                    return "Файл(и) не знайдено.";

                var projectsText = File.ReadAllText(projectsAnalysisPath);
                var webpagesText = File.ReadAllText(webpagesAnalysisPath);

                if (string.IsNullOrWhiteSpace(projectsText) || string.IsNullOrWhiteSpace(webpagesText))
                    return "Один із файлів порожній.";

                var projects = JsonConvert.DeserializeObject<List<ProjectAnalysis>>(projectsText);
                var pages = JsonConvert.DeserializeObject<List<UrlAnalysis>>(webpagesText);

                if (projects == null || pages == null)
                    return "Помилка десеріалізації JSON.";

                var completedPairs = new HashSet<string>();
                var results = new List<SimilarityResult>();

                if (File.Exists(outputJsonPath))
                {
                    var existingResultsText = File.ReadAllText(outputJsonPath);
                    var existingResults = JsonConvert.DeserializeObject<List<SimilarityResult>>(existingResultsText) ?? new List<SimilarityResult>();
                    foreach (var res in existingResults)
                        completedPairs.Add($"{res.Project}||{res.Url}");
                }

                foreach (var proj in projects)
                {
                    // === 1. Обробка YouTube URL ===
                    var youtubePages = pages
                        .Where(p => p.Url.Contains("youtube") && !string.IsNullOrWhiteSpace(p.Response))
                        .Where(p => !completedPairs.Contains($"{proj.Project}||{p.Url}"))
                        .ToList();

                    if (youtubePages.Any() && !string.IsNullOrWhiteSpace(proj.Response))
                    {
                        Console.WriteLine($"YouTube аналіз ({youtubePages.Count} сторінок) для {proj.Project}");

                        var sb = new StringBuilder();
                        sb.AppendLine("Проаналізуй окремо кожну пару: проєкт + YouTube сторінка. Для кожної сторінки виведи результат у форматі:");
                        sb.AppendLine("URL: [url] — [чи пов’язаний зміст із проєктом? Якщо так, коротке пояснення. Якщо ні — коротке пояснення]");
                        sb.AppendLine($"\nОсь текст проєкту:\n{proj.Response}");

                        int i = 1;
                        foreach (var page in youtubePages)
                        {
                            sb.AppendLine($"\nСторінка {i}:");
                            sb.AppendLine($"URL: {page.Url}");
                            sb.AppendLine($"Текст: {page.Response}");
                            i++;
                        }

                        bool success = SendMessageToAI(sb.ToString(), out string aiResponse);
                        if (success)
                        {
                            _requestsSentToday++;
                            var lines = aiResponse.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                            foreach (var page in youtubePages)
                            {
                                var key = $"{proj.Project}||{page.Url}";
                                var line = lines.FirstOrDefault(l => l.Contains(page.Url));
                                string result = line ?? "Не знайдено відповідь";

                                results.Add(new SimilarityResult
                                {
                                    Project = proj.Project,
                                    Url = page.Url,
                                    Similarity = result
                                });

                                completedPairs.Add(key);
                            }
                        }
                        else
                        {
                            foreach (var page in youtubePages)
                            {
                                var key = $"{proj.Project}||{page.Url}";
                                results.Add(new SimilarityResult
                                {
                                    Project = proj.Project,
                                    Url = page.Url,
                                    Similarity = "Error"
                                });
                                completedPairs.Add(key);
                            }
                        }
                    }

                    // === 2. Обробка решти сторінок (не YouTube) ===
                    foreach (var page in pages)
                    {
                        if (page.Url.Contains("youtube")) continue;
                        if (string.IsNullOrWhiteSpace(proj.Response) || string.IsNullOrWhiteSpace(page.Response))
                            continue;

                        var key = $"{proj.Project}||{page.Url}";
                        if (completedPairs.Contains(key))
                        {
                            Console.WriteLine($"Пропущено (вже оброблено): {proj.Project} + {page.Url}");
                            continue;
                        }

                        string userMessage =
                            "Ти отримуєш два фрагменти тексту. Проаналізуй, чи пов’язані вони між собою за змістом. " +
                            "Виведи стислу відповідь, чи відповідає сторінка тематиці проєкту, і чому (1-2 речення).\n\n" +
                            $"Текст проєкту:\n{proj.Response}\n\nТекст веб-сторінки:\n{page.Response}";

                        Console.WriteLine($"Надсилання запиту для: {proj.Project} + {page.Url}");

                        bool success = SendMessageToAI(userMessage, out string aiResponse);
                        string similarity = success ? aiResponse.Trim().Replace("\"", "") : "Error";

                        if (success)
                            _requestsSentToday++;

                        results.Add(new SimilarityResult
                        {
                            Project = proj.Project,
                            Url = page.Url,
                            Similarity = similarity
                        });

                        completedPairs.Add(key);
                    }
                }

                // === Запис результатів ===
                List<SimilarityResult> allResults = new List<SimilarityResult>();
                if (File.Exists(outputJsonPath))
                {
                    var existing = JsonConvert.DeserializeObject<List<SimilarityResult>>(File.ReadAllText(outputJsonPath));
                    if (existing != null)
                        allResults.AddRange(existing);
                }
                allResults.AddRange(results);

                Console.WriteLine("Запис результатів у файл...");
                File.WriteAllText(outputJsonPath, JsonConvert.SerializeObject(allResults, Formatting.Indented));

                return $"Порівняння завершено. Додано {results.Count} нових записів у {outputJsonPath}";
            }
            catch (Exception ex)
            {
                return $"Сталася помилка: {ex.Message}\n\n{ex.StackTrace}";
            }
        }



        public static void RunDailyTask()
        {
            try
            {
                string projectsAnalysisPath = outputProjectsJson;
                string webpagesAnalysisPath = outputUrlsJsonPath;

                Console.WriteLine("Ініціалізація клієнта...");
                CohereClient deepSeekClient = new CohereClient(apiKey);

                var projects = JsonProcessing.todayProjects;
                var projectsToAnalyze = new List<Project>();

                foreach (var project in projects)
                {
                    if (!JsonProcessing.WasProjectAnalyzedToday(project.Name, outputProjectsJson))
                    {
                        Console.WriteLine($"Проєкт '{project.Name}' ще не аналізувався сьогодні. Додаємо до черги.");
                        projectsToAnalyze.Add(project);
                    }
                    else
                    {
                        Console.WriteLine($"Пропускаємо вже проаналізований сьогодні проєкт: {project.Name}");
                    }
                }

                if (projectsToAnalyze.Count > 0)
                {
                    Console.WriteLine("Аналізуємо нові проєкти...");
                    string projectAnalysisResult = deepSeekClient.AnalyzeFiles(outputProjectsJson, projectsToAnalyze);
                    Console.WriteLine("Результат аналізу проєктів:");
                    Console.WriteLine(projectAnalysisResult);
                }
                else
                {
                    Console.WriteLine("Усі проєкти вже були проаналізовані сьогодні.");
                }

                var urls = JsonProcessing.todayUrls;

                Console.WriteLine($"Всього URL сьогодні: {urls.Count}");

                string urlAnalysisResult = deepSeekClient.AnalyzeBrowserUrls(outputUrlsJsonPath, urls);
                Console.WriteLine("Результат аналізу URL:");
                Console.WriteLine(urlAnalysisResult);


               /* Console.WriteLine("Порівняння схожості між проєктами та сторінками...");
                string resultMessage = deepSeekClient.CompareProjectWebpageSimilarities(projectsAnalysisPath, webpagesAnalysisPath, outputJsonPath);
                Console.WriteLine("Результат порівняння:");
                Console.WriteLine(resultMessage);*/
                
            
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






