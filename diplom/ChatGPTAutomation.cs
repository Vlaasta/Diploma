using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace diplom
{
    class ChatGptAutomation
    {
        private OpenAIClient _chatGptClient;
        private const string analysisFilePath = @"E:\\4 KURS\\Диплом\\DiplomaRepo\\Diploma\\data\\BrowserAnalysis\\analysisResults.json";

        public ChatGptAutomation(string apiKey)
        {
            _chatGptClient = new OpenAIClient(apiKey);
        }

        public async Task<string> AnalyzeProject(string filePath)
        {
            string content = File.ReadAllText(filePath, Encoding.UTF8);
            string analysis = await _chatGptClient.GetChatGptResponse($"Аналізуй наступний проєкт: {content}");
            SaveAnalysis("Project", filePath, analysis);
            return analysis;
        }

        public async Task<string> AnalyzeWebPage(string url, string pageContent)
        {
            string analysis = await _chatGptClient.GetChatGptResponse($"Аналізуй наступну веб-сторінку: {pageContent}");
            SaveAnalysis("WebPage", url, analysis);
            return analysis;
        }

        public async Task<string> CompareAnalysis()
        {
            var analysisList = LoadAnalysis();
            var projectAnalysis = analysisList.FirstOrDefault(a => a.Type == "Project")?.ResultAnalysis;
            var webPageAnalysis = analysisList.FirstOrDefault(a => a.Type == "WebPage")?.ResultAnalysis;

            if (projectAnalysis == null || webPageAnalysis == null)
                return "Недостатньо даних для порівняння.";

            return await _chatGptClient.GetChatGptResponse($"Порівняй два аналізи:\nПроєкт: {projectAnalysis}\nВеб-сторінка: {webPageAnalysis}\nЧи пов'язані ці тексти?");
        }

        private void SaveAnalysis(string type, string source, string result)
        {
            var analysisList = LoadAnalysis();
            analysisList.Add(new AnalysisResult
            {
                Type = type,
                Source = source,
                ResultAnalysis = result,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });
            File.WriteAllText(analysisFilePath, JsonConvert.SerializeObject(analysisList, Formatting.Indented));
        }

        private List<AnalysisResult> LoadAnalysis()
        {
            if (File.Exists(analysisFilePath))
            {
                var jsonData = File.ReadAllText(analysisFilePath);
                return JsonConvert.DeserializeObject<List<AnalysisResult>>(jsonData) ?? new List<AnalysisResult>();
            }
            return new List<AnalysisResult>();
        }
    }


    internal class AnalysisResult
    {
        public string Type { get; set; } // "Project" або "WebPage"
        public string Source { get; set; } // Файл або URL
        public string ResultAnalysis { get; set; }
        public string Timestamp { get; set; }
    }
}




