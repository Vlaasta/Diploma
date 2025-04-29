using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace diplom
{
    class DeepSeekClient
    {
        private readonly HttpClient _client;
        private readonly string _apiKey = "";
        private readonly string _url = "https://openrouter.ai/api/v1/chat/completions";

        private const int MaxRequestsPerDay = 1;  // Ліміт запитів
        private int _requestsSentToday = 0;       // Лічильник запитів на сьогодні
        private DateTime _lastRequestDate = DateTime.MinValue; // Дата останнього запиту

        public DeepSeekClient(string apiKey)
        {
            _apiKey = apiKey;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        private bool CanSendRequest()
        {
            // Якщо змінився день, скидаємо лічильник
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
            StringBuilder text = new StringBuilder();

            try
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false))
                {
                    // Отримуємо основне тіло документа
                    Body body = wordDoc.MainDocumentPart.Document.Body;

                    // Проходимо через всі параграфи в документі
                    foreach (var para in body.Elements<Paragraph>())
                    {
                        foreach (var run in para.Elements<Run>())
                        {
                            foreach (var textElement in run.Elements<Text>())
                            {
                                text.Append(textElement.Text);
                            }
                        }

                        // Додаємо розрив рядка після кожного параграфа
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
                    Console.WriteLine("Ліміт запитів на сьогодні перевищено. Спробуйте пізніше.");
                    return "Ліміт запитів на сьогодні перевищено.";
                }

                var projects = LoadProjects(projectsJsonPath);
                var allResponses = new List<object>();  // Список для збереження результатів у вигляді об'єктів

                foreach (var project in projects)
                {
                    try
                    {
                        if (!File.Exists(project.Path))
                        {
                            Console.WriteLine($"Файл не знайдено: {project.Path}");
                            allResponses.Add(new { Project = project.Name, Error = $"Файл не знайдено: {project.Path}" });
                            continue;
                        }

                        // Витягування тексту з файлу .docx
                        var fileText = ExtractTextFromDocx(project.Path);

                        if (string.IsNullOrEmpty(fileText))
                        {
                            Console.WriteLine($"Файл {project.Path} не містить тексту або не вдалося витягнути текст.");
                            allResponses.Add(new { Project = project.Name, Error = "Не вдалося витягнути текст з файлу." });
                            continue;
                        }
                        var jsonContent = new StringContent(
                            $"{{\"prompt\":\"Проаналізуйте проект: {Path.GetFileName(project.Path)}\n\n{fileText}\", \"model\":\"deepseek/deepseek-r1:free\"}}",
                            Encoding.UTF8, "application/json");

                        var request = new HttpRequestMessage(HttpMethod.Post, _url)
                        {
                            Content = jsonContent
                        };

                        // Додавання заголовка
                        request.Headers.Add("Accept", "application/json");

                        var response = _client.SendAsync(request).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var responseString = response.Content.ReadAsStringAsync().Result;
                            Console.WriteLine($"Response for project {project.Name}: {responseString}");

                            // Додаємо успішну відповідь до списку
                            allResponses.Add(new
                            {
                                Project = project.Name,
                                Response = responseString
                            });

                            _requestsSentToday++;
                        }
                        else
                        {
                            var errorResponse = response.Content.ReadAsStringAsync().Result;
                            Console.WriteLine($"Помилка обробки проекту {project.Name}: {response.StatusCode} - {errorResponse}");

                            // Додаємо помилку до списку
                            allResponses.Add(new
                            {
                                Project = project.Name,
                                Error = $"Помилка обробки проекту: {response.StatusCode} - {errorResponse}"
                            });
                        }

                        if (!CanSendRequest())
                        {
                            Console.WriteLine("Ліміт запитів на сьогодні перевищено. Спробуйте пізніше.");
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Помилка при обробці проекту {project.Name}: {ex.Message}");

                        // Додаємо помилку до списку
                        allResponses.Add(new
                        {
                            Project = project.Name,
                            Error = $"Помилка при обробці проекту: {ex.Message}"
                        });
                    }
                }

                try
                {
                    // Записуємо результат у JSON файл
                    var jsonResult = JsonSerializer.Serialize(allResponses, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(outputJsonPath, jsonResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка при записі файлу: {ex.Message}");
                    return $"Помилка при записі файлу: {ex.Message}";
                }

                return $"Обробка завершена. Результати збережено в {outputJsonPath}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Загальна помилка: {ex.Message}");
                return $"Загальна помилка: {ex.Message}";
            }
        }

    }
}

