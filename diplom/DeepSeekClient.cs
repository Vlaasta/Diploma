using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace diplom 
{
    public class PoeClient
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;
        private readonly string _url;

        private const int MaxRequestsPerDay = 5;
        private int _requestsSentToday = 0;
        private DateTime _lastRequestDate = DateTime.MinValue;

        public PoeClient(string apiKey, string botName)
        {
            _apiKey = apiKey;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("text/event-stream"));

            _url = $"https://api.poe.com/bot/{botName}";
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

        private IEnumerable<string> ChunkText(string text, int maxChars = 30000)
        {
            for (int i = 0; i < text.Length; i += maxChars)
            {
                yield return text.Substring(i, Math.Min(maxChars, text.Length - i));
            }
        }

        private HttpResponseMessage PostWithRetry(HttpContent content, int maxRetries = 3)
        {
            int attempt = 0;
            HttpResponseMessage resp = null;

            while (true)
            {
                // Створюємо HttpRequestMessage замість прямого PostAsync
                var request = new HttpRequestMessage(HttpMethod.Post, _url)
                {
                    Content = content
                };

                // Тепер можна передати HttpCompletionOption
                resp = _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;

                if (resp.IsSuccessStatusCode || attempt++ >= maxRetries)
                    return resp;

                // exponential back-off: 2s, 4s, 6s…
                Thread.Sleep(2000 * attempt);
            }
        }


        private string ReadSseContent(HttpResponseMessage response)
        {
            var sb = new StringBuilder();

            using (var stream = response.Content.ReadAsStreamAsync().Result)
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // шукаємо тільки SSE-рядки з даними
                    if (!line.StartsWith("data:"))
                        continue;

                    var json = line.Substring("data:".Length).Trim();
                    if (json == "[DONE]")
                        break;

                    // парсимо кожен чанок як JSON
                    using (var doc = JsonDocument.Parse(json))
                    {
                        var root = doc.RootElement;

                        // якщо це повідомлення про нестачу поінтів — кидаємо окремий ексепшн
                        if (root.TryGetProperty("allow_retry", out var ar)
                            && ar.GetBoolean()
                            && root.TryGetProperty("text", out var errTxt))
                        {
                            throw new InvalidOperationException(errTxt.GetString());
                        }

                        // інакше додаємо текстове поле
                        if (root.TryGetProperty("text", out var txt))
                        {
                            sb.Append(txt.GetString());
                        }
                    }
                }
            }

            return sb.ToString();
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
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false))
                {
                    Body body = wordDoc.MainDocumentPart.Document.Body;

                    foreach (var para in body.Elements<Paragraph>())
                    {
                        foreach (var run in para.Elements<Run>())
                        {
                            foreach (var textElement in run.Elements<Text>())
                            {
                                text.Append(textElement.Text);
                            }
                        }
                        text.AppendLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при витягуванні тексту з файлу: {ex.Message}");
            }

            return text.ToString();
        }

        public string AnalyzeFiles(string projectsJsonPath, string outputJsonPath)
        {
            try
            {
                if (!CanSendRequest())
                {
                    Console.WriteLine("Ліміт запитів на сьогодні перевищено.");
                    return "Ліміт запитів на сьогодні перевищено.";
                }

                var projects = LoadProjects(projectsJsonPath);
                var allResponses = new List<object>();
                const string testJsonPath = "test.json";

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

                    var projectResponses = new List<string>();
                    var header = $"Аналіз проєкту: {Path.GetFileName(project.Path)}\n\n";

                    foreach (var chunk in ChunkText(header + fileText))
                    {
                        if (!CanSendRequest())
                        {
                            Console.WriteLine("Ліміт запитів на сьогодні вичерпано під час обробки чанків.");
                            projectResponses.Add("Ліміт запитів вичерпано");
                            break;
                        }

                        var payload = new
                        {
                            version = "1.0",
                            type = "query",
                            query = new[]
                            {
                                new { role = "user", content = chunk }
                            },
                            user_id = "",
                            conversation_id = "",
                            message_id = ""
                        };

                        var jsonPayload = JsonSerializer.Serialize(payload);
                        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                        var response = PostWithRetry(content);

                        File.AppendAllText(testJsonPath,
                            JsonSerializer.Serialize(new { Project = project.Name, Prompt = chunk }) + ",\n");

                        if (response.IsSuccessStatusCode)
                        {
                            try
                            {
                                var respString = ReadSseContent(response);
                                projectResponses.Add(respString);
                                _requestsSentToday++;
                            }
                            catch (InvalidOperationException ex) when (ex.Message.Contains("You do not have enough points"))
                            {
                                // Poe повернув, що не вистачає поінтів
                                Console.WriteLine($"Poe error для {project.Name}: {ex.Message}");
                                projectResponses.Add($"Error: {ex.Message}. Будь ласка, поповніть Poe points або оновіться до Poe Pro.");
                                break; // далі з цим проєктом сенсу немає
                            }
                            catch (Exception ex)
                            {
                                // інші помилки SSE
                                Console.WriteLine($"SSE error для {project.Name}: {ex.Message}");
                                projectResponses.Add($"Помилка SSE: {ex.Message}");
                                break;
                            }
                            _requestsSentToday++;
                        }
                        else
                        {
                            var error = response.Content.ReadAsStringAsync().Result;
                            Console.WriteLine(
                                $"Помилка для проєкту {project.Name}: {response.StatusCode} - {error}");
                            projectResponses.Add($"Помилка: {response.StatusCode} - {error}");
                            break;
                        }
                    }

                    allResponses.Add(new { Project = project.Name, Responses = projectResponses });
                }

                var jsonResult = JsonSerializer.Serialize(allResponses, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(outputJsonPath, jsonResult);

                return $"Обробка завершена. Результати збережено в {outputJsonPath}";
            }
            catch (Exception ex)
            {
                return $"Загальна помилка: {ex.Message}";
            }
        }
    }
}



