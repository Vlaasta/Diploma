using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class OpenAIClient
{
    private static readonly HttpClient client = new HttpClient();
    private const string apiKey = "your-openai-api-key"; // Встав свій ключ API

    public async Task<string> GetGPT3ResponseAsync(string prompt)
    {
        var url = "https://api.openai.com/v1/completions";

        var requestBody = new
        {
            model = "text-davinci-003", // або будь-яка інша модель GPT-3
            prompt = prompt,
            max_tokens = 100
        };

        var requestContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        var response = await client.PostAsync(url, requestContent);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Парсимо відповідь
        dynamic responseJson = JsonConvert.DeserializeObject(responseContent);
        return responseJson.choices[0].text.ToString();
    }
}

