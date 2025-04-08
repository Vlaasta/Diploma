using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diplom
{
    internal class AnalysisController
    {
        private readonly FileReader _fileReader;
        private readonly ChatGptAutomation _chatGptAutomation;

        public AnalysisController(string apiKey)
        {
            _fileReader = new FileReader();
            _chatGptAutomation = new ChatGptAutomation(apiKey);
        }

        // Метод для аналізу проекту (файлу)
        public async Task AnalyzeProjectAsync(string projectFilePath)
        {
            string analysis = await _chatGptAutomation.AnalyzeProject(projectFilePath);
            Console.WriteLine("Аналіз проекту:");
            Console.WriteLine(analysis);
        }

        // Метод для аналізу веб-сторінки
        public async Task AnalyzeWebPageAsync(string url, string pageContent)
        {
            string analysis = await _chatGptAutomation.AnalyzeWebPage(url, pageContent);
            Console.WriteLine("Аналіз веб-сторінки:");
            Console.WriteLine(analysis);
        }

        // Метод для порівняння аналізу проекту та веб-сторінки
        public async Task CompareAnalysesAsync()
        {
            string comparisonResult = await _chatGptAutomation.CompareAnalysis();
            Console.WriteLine("Порівняння аналізу проекту та веб-сторінки:");
            Console.WriteLine(comparisonResult);
        }

        // Повний процес аналізу проекту та веб-сторінки
        public async Task ExecuteFullAnalysisAsync(string projectFilePath, string url, string pageContent)
        {
            // Аналіз проекту
            await AnalyzeProjectAsync(projectFilePath);
            await Task.Delay(20000);

            // Аналіз веб-сторінки
            await AnalyzeWebPageAsync(url, pageContent);
            await Task.Delay(20000);

            // Порівняння аналізів
            await CompareAnalysesAsync();
        }
    }
}
