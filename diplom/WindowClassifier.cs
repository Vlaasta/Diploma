using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text;

namespace diplom
{
    public class WindowClassifier
    {
        private readonly OpenAIClient _openAIClient;

        public WindowClassifier()
        {
            _openAIClient = new OpenAIClient();
        }

        // Порівнюємо вміст веб-сторінки і файлу
        public async Task<bool> CompareContentAsync(string pageContent, string projectContent)
        {
            var prompt = $"Compare the following two texts and return a similarity score between 0 and 1.\nText 1: {pageContent}\nText 2: {projectContent}";

            // Викликаємо OpenAI API для порівняння вмісту
            var similarityResult = await _openAIClient.GetGPT3ResponseAsync(prompt);

            // Перевіряємо результат (ми припускаємо, що відповідь - це число)
            if (double.TryParse(similarityResult, out double score))
            {
                return score > 0.7; // Поріг для подібності (можна змінити)
            }

            return false;
        }
    }
}

