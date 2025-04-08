using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace diplom
{
    internal class OpenAIClient
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://api.openai.com/v1/chat/completions";

        public OpenAIClient(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<string> GetChatGptResponse(string prompt)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[] { new { role = "user", content = prompt } },
                max_tokens = 500
            };

            var jsonRequest = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(ApiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                return $"Помилка API: {response.StatusCode}";
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            dynamic responseObject = JsonConvert.DeserializeObject(jsonResponse);
            return responseObject.choices[0].message.content.ToString();
        }
    }
}




