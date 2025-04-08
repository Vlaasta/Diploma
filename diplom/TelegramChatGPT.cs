using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace diplom
{
    class TelegramChatGPT
    {
        public static async Task lalala()
        {
            string apiKey = "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"; // Встав свій API-ключ
            string url = "https://api.openai.com/v1/chat/completions";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "gpt-3.5-turbo", // Оновлена модель
                    messages = new[]
                    {
                        new { role = "system", content = "Ти асистент, який аналізує текст." },
                        new { role = "user", content = "Привіт, проаналізуй цей текст: ..." }
                    },
                    max_tokens = 150
                };

                string jsonRequest = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Відповідь від ChatGPT: ");
                Console.WriteLine(responseBody);
            }
        }
    }
}


