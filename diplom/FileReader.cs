using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace diplom
{
    internal class FileReader
    {
        private const string projectFilePath = @"E:\\4 KURS\\Диплом\\DiplomaRepo\\Diploma\\data\\BrowserAnalysis\\projectsAnalysis.json";
        private const string webPageFilePath = @"E:\\4 KURS\\Диплом\\DiplomaRepo\\Diploma\\data\\BrowserAnalysis\\webpagesAnalysis.json";

        public string ReadTextFile(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                return $"Помилка зчитування файлу: {ex.Message}";
            }
        }

        public string ProcessFile(string filePath)
        {
            string content = ReadTextFile(filePath);
            string analysis = SummarizeText(content);

            // Зберігаємо аналіз у файл
            SaveProjectAnalysis(filePath, analysis);
            return analysis;
        }

        public string SummarizeText(string text, int sentenceCount = 3)
        {
            text = StripHtmlTags(text);
            string[] sentences = SplitIntoSentences(text);
            Dictionary<string, int> wordFrequencies = GetWordFrequencies(text);
            Dictionary<string, double> sentenceScores = ScoreSentences(sentences, wordFrequencies);

            return string.Join(" ", sentenceScores
                .OrderByDescending(kv => kv.Value)
                .Take(sentenceCount)
                .Select(kv => kv.Key));
        }

        public void SaveProjectAnalysis(string filePath, string analysisResult)
        {
            var analysis = new ProjectAnalysiscs
            {
                FileName = Path.GetFileName(filePath),
                LastModified = File.GetLastWriteTime(filePath).ToString("yyyy-MM-dd HH:mm:ss"),
                AnalysisResult = analysisResult,
                Keywords = GetKeywords(analysisResult)
            };

            var analysisList = LoadAnalysis(projectFilePath);
            analysisList.Add(analysis);

            // Зберігаємо в JSON
            File.WriteAllText(projectFilePath, JsonConvert.SerializeObject(analysisList, Formatting.Indented));
        }

        public void SaveWebPageAnalysis(string url, string analysisResult)
        {
            var analysis = new WebPageAnalysis
            {
                Url = url,
                AnalysisResult = analysisResult,
                Keywords = GetKeywords(analysisResult)
            };

            var analysisList = LoadWebPageAnalysis(webPageFilePath);
            analysisList.Add(analysis);

            // Зберігаємо в JSON
            File.WriteAllText(webPageFilePath, JsonConvert.SerializeObject(analysisList, Formatting.Indented));
        }

        private List<ProjectAnalysiscs> LoadAnalysis(string path)
        {
            if (File.Exists(path))
            {
                var jsonData = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<List<ProjectAnalysiscs>>(jsonData) ?? new List<ProjectAnalysiscs>();
            }
            return new List<ProjectAnalysiscs>();
        }

        private List<WebPageAnalysis> LoadWebPageAnalysis(string path)
        {
            if (File.Exists(path))
            {
                var jsonData = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<List<WebPageAnalysis>>(jsonData) ?? new List<WebPageAnalysis>();
            }
            return new List<WebPageAnalysis>();
        }

        private List<string> GetKeywords(string text)
        {
            // Просто приклад: ви можете використовувати більш складну логіку для витягування ключових слів
            var words = text.Split(new[] { ' ', '\n', '\r', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Distinct().Take(5).ToList(); // Повертаємо перші 5 унікальних слів
        }

        private string StripHtmlTags(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        private string[] SplitIntoSentences(string text)
        {
            return Regex.Split(text, @"(?<=[.!?])\s+");
        }

        private Dictionary<string, int> GetWordFrequencies(string text)
        {
            Dictionary<string, int> wordCounts = new Dictionary<string, int>();
            string[] words = text.ToLower()
                .Split(new[] { ' ', '\n', '\r', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                if (!wordCounts.ContainsKey(word))
                    wordCounts[word] = 0;
                wordCounts[word]++;
            }

            return wordCounts;
        }

        private Dictionary<string, double> ScoreSentences(string[] sentences, Dictionary<string, int> wordFrequencies)
        {
            Dictionary<string, double> sentenceScores = new Dictionary<string, double>();

            foreach (string sentence in sentences)
            {
                string[] words = sentence.ToLower()
                    .Split(new[] { ' ', '\n', '\r', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

                double score = words.Sum(word => wordFrequencies.ContainsKey(word) ? wordFrequencies[word] : 0);
                sentenceScores[sentence] = score;
            }

            return sentenceScores;
        }
    }
}
