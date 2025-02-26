using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using System.Globalization;
using System.Linq;

namespace diplom
{
    public class Statistic : Form
    {
        private string filePath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\timerAmounts.json";

        // Метод для зчитування актуальних даних із JSON-файлу
        public List<TimerData> LoadTimerData()
        {
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<TimerData>>(jsonContent) ?? new List<TimerData>();
            }
            return new List<TimerData>();
        }

        // Метод для визначення діапазону попереднього тижня
        public (DateTime startOfWeek, DateTime endOfWeek) GetPreviousWeekRange()
        {
            DateTime today = DateTime.Today;
            int daysSinceMonday = ((int)today.DayOfWeek + 6) % 7; // Визначаємо відстань від поточного дня до понеділка
            DateTime lastMonday = today.AddDays(-daysSinceMonday - 7); // Минулого понеділка
            DateTime lastSunday = lastMonday.AddDays(6); // Минулої неділі
            return (lastMonday, lastSunday);
        }

        // Метод для визначення діапазону поточного тижня
        public (DateTime startOfWeek, DateTime endOfWeek) GetCurrentWeekRange()
        {
            DateTime today = DateTime.Today;
            int daysSinceMonday = ((int)today.DayOfWeek + 6) % 7; // Визначаємо відстань від поточного дня до понеділка
            DateTime currentMonday = today.AddDays(-daysSinceMonday); // Поточний понеділок
            DateTime currentSunday = currentMonday.AddDays(6); // Поточна неділя
            return (currentMonday, currentSunday);
        }

        // Метод для фільтрації даних для поточного тижня
        public List<TimerData> FilterDataForCurrentWeek(List<TimerData> timerData)
        {
            timerData = LoadTimerData();
            var (startOfWeek, endOfWeek) = GetCurrentWeekRange();
            return timerData.Where(data =>
            {
                DateTime date = DateTime.ParseExact(data.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                return date >= startOfWeek && date <= endOfWeek;
            }).ToList();
        }

        // Метод для фільтрації даних для попереднього тижня
        public List<TimerData> FilterDataForPreviousWeek(List<TimerData> timerData)
        {
            timerData = LoadTimerData();
            var (startOfWeek, endOfWeek) = GetPreviousWeekRange();
            return timerData.Where(data =>
            {
                DateTime date = DateTime.ParseExact(data.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                return date >= startOfWeek && date <= endOfWeek;
            }).ToList();
        }

        // Метод для заповнення пропущених днів
        public List<TimerData> FillMissingDays(List<TimerData> timerData, DateTime startOfWeek, DateTime endOfWeek)
        {
            var allDays = Enumerable.Range(0, (endOfWeek - startOfWeek).Days + 1)
                                     .Select(offset => startOfWeek.AddDays(offset))
                                     .ToList();

            var dataDict = timerData.ToDictionary(
                data => DateTime.ParseExact(data.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                data => data.Time);

            return allDays.Select(day => new TimerData
            {
                Date = day.ToString("dd.MM.yyyy"),
                Time = dataDict.ContainsKey(day) ? dataDict[day] : "00:00:00"
            }).ToList();
        }

        // Метод для отримання точок X (дата)
        public List<DateTime> GetXPoints(List<TimerData> timerData)
        {
            var xPoints = new List<DateTime>();
            foreach (var data in timerData)
            {
                DateTime date = DateTime.ParseExact(data.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                xPoints.Add(date);
            }
            return xPoints;
        }

        // Метод для отримання точок Y (час у секундах)
        public List<int> GetYPoints(List<TimerData> timerData)
        {
            var yPoints = new List<int>();
            foreach (var data in timerData)
            {
                TimeSpan timeSpan = TimeSpan.Parse(data.Time);
                int totalSeconds = (int)timeSpan.TotalSeconds;
                yPoints.Add(totalSeconds);
            }
            return yPoints;
        }

        public (DateTime startOfLastMonth, DateTime endOfLastMonth) GetLastMonthRange()
        {
            DateTime today = DateTime.Today;
            DateTime firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1);
            DateTime lastDayOfLastMonth = firstDayOfCurrentMonth.AddDays(-1);
            DateTime firstDayOfLastMonth = new DateTime(lastDayOfLastMonth.Year, lastDayOfLastMonth.Month, 1);
            return (firstDayOfLastMonth, lastDayOfLastMonth);
        }

        public List<TimerData> FilterDataForLastMonth(List<TimerData> timerData)
        {
            var (startOfLastMonth, endOfLastMonth) = GetLastMonthRange();
            return timerData.Where(data =>
            {
                DateTime date = DateTime.ParseExact(data.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                return date >= startOfLastMonth && date <= endOfLastMonth;
            }).ToList();
        }

        public List<TimerData> FillMissingDaysForLastMonth(List<TimerData> timerData)
        {
            var (startOfLastMonth, endOfLastMonth) = GetLastMonthRange();
            return FillMissingDays(timerData, startOfLastMonth, endOfLastMonth);
        }

        // Метод для обчислення загального часу
        public string CalculateTotalTime(List<TimerData> timerData)
        {
            // Підрахунок загальної кількості секунд
            int totalSeconds = timerData.Sum(data =>
            {
                TimeSpan timeSpan = TimeSpan.Parse(data.Time);
                return (int)timeSpan.TotalSeconds;
            });

            // Перетворення секунд у години, хвилини, секунди
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            // Форматований рядок
            return $"{hours} год. {minutes} хв. {seconds} сек.";
        }

        // Метод для групування даних за місяцями
        public Dictionary<int, List<TimerData>> GetDataByMonths(List<TimerData> timerData, int year)
        {
            var groupedData = timerData
                .Where(data =>
                {
                    DateTime date = DateTime.ParseExact(data.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    return date.Year == year; // Фільтруємо лише дані за вказаний рік
        })
                .GroupBy(data =>
                {
                    DateTime date = DateTime.ParseExact(data.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    return date.Month; // Групуємо за місяцем
        })
                .ToDictionary(group => group.Key, group => group.ToList());

            // Перевірка на відсутні місяці та їх заповнення
            for (int month = 1; month <= 12; month++)
            {
                if (!groupedData.ContainsKey(month))
                {
                    groupedData[month] = new List<TimerData>();
                }
            }

            return groupedData;
        }

        // Метод для заповнення пропущених днів у всіх місяцях року
        public Dictionary<int, List<TimerData>> FillMissingDaysForYear(List<TimerData> timerData, int year)
        {
            var dataByMonths = GetDataByMonths(timerData, year);

            for (int month = 1; month <= 12; month++)
            {
                DateTime startOfMonth = new DateTime(year, month, 1);
                DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                dataByMonths[month] = FillMissingDays(dataByMonths[month], startOfMonth, endOfMonth);
            }

            return dataByMonths;
        }

        // Метод для отримання даних за конкретний місяць і рік
        public List<TimerData> GetDataForSpecificMonth(List<TimerData> timerData, int year, int month)
        {
            return timerData.Where(data =>
            {
                // Конвертація дати зі строки у формат DateTime
                DateTime date = DateTime.ParseExact(data.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                // Перевірка на відповідність року та місяцю
                return date.Year == year && date.Month == month;
            }).ToList();
        }
    }
}
