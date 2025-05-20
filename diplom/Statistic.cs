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

        public List<T> FilterDataForDateRange<T>(List<T> dataList, DateTime startDate, DateTime endDate, Func<T, DateTime> getDate)
        {
            return dataList.Where(item =>
            {
                var date = getDate(item);
                return date >= startDate && date <= endDate;
            }).ToList();
        }

        // Метод для фільтрації даних для поточного тижня
        public List<T> FilterDataForCurrentWeek<T>(List<T> dataList, Func<T, DateTime> getDate)
        {
            var (startOfWeek, endOfWeek) = GetCurrentWeekRange();
            return FilterDataForDateRange(dataList, startOfWeek, endOfWeek, getDate);
        }

        public List<T> FilterDataForPreviousWeek<T>(List<T> dataList, Func<T, DateTime> getDate)
        {
            var (startOfWeek, endOfWeek) = GetPreviousWeekRange();
            return FilterDataForDateRange(dataList, startOfWeek, endOfWeek, getDate);
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

        public List<T> FilterDataForLastMonth<T>(List<T> dataList, Func<T, DateTime> getDate)
        {
            var (startOfLastMonth, endOfLastMonth) = GetLastMonthRange();
            return dataList.Where(item =>
            {
                DateTime date = getDate(item);
                return date >= startOfLastMonth && date <= endOfLastMonth;
            }).ToList();
        }

        public List<TimerData> FillMissingDaysForLastMonth(List<TimerData> timerData)
        {
            var (startOfLastMonth, endOfLastMonth) = GetLastMonthRange();
            return FillMissingDays(timerData, startOfLastMonth, endOfLastMonth);
        }

        public List<TimerData> FillMissingHours(List<TimerData> data)
        {
            var groupedByHour = data
                .GroupBy(d => TimeSpan.Parse(d.Time).Hours)
                .ToDictionary(g => g.Key, g => g.First());

            var today = DateTime.Today;
            var filled = new List<TimerData>();

            for (int hour = 0; hour < 24; hour++)
            {
                if (groupedByHour.ContainsKey(hour))
                {
                    filled.Add(groupedByHour[hour]);
                }
                else
                {
                    filled.Add(new TimerData
                    {
                        Date = today.ToString("dd.MM.yyyy"),
                        Time = TimeSpan.FromHours(hour).ToString(@"hh\:mm\:ss")
                    });
                }
            }

            return filled;
        }

        // Метод для обчислення загального часу
        public string CalculateTotalTime<T>(List<T> data, string timePropertyName)
        {
            int totalSeconds = data.Sum(item =>
            {
                var timeProperty = typeof(T).GetProperty(timePropertyName);
                if (timeProperty == null)
                {
                    throw new ArgumentException($"Тип {typeof(T).Name} не має властивості {timePropertyName}");
                }

                var value = timeProperty.GetValue(item);

                if (value == null)
                    return 0;

                if (value is int seconds)
                {
                    return seconds;
                }
                else if (value is string timeString)
                {
                    if (TimeSpan.TryParse(timeString, out TimeSpan timeSpan))
                    {
                        return (int)timeSpan.TotalSeconds;
                    }
                }

                return 0;
            });

            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int secondsLeft = totalSeconds % 60;

            return $"{hours} год. {minutes} хв. {secondsLeft} сек.";
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

        public List<UrlData> LoadBrowserDataForCurrentWeek(string path)
        {
            var result = new List<UrlData>();

            if (!File.Exists(path))
                return result;

            var (startOfWeek, endOfWeek) = GetCurrentWeekRange();

            try
            {
                // Зчитуємо весь вміст JSON файлу
                var jsonContent = File.ReadAllText(path);

                // Десеріалізуємо в список UrlData
                var urlDataList = JsonSerializer.Deserialize<List<UrlData>>(jsonContent);

                if (urlDataList != null)
                {
                    foreach (var entry in urlDataList)
                    {
                        if (entry == null || entry.Timestamp == DateTime.MinValue)
                            continue;

                        // Переводимо Timestamp у локальний час
                        var localTimestamp = entry.Timestamp.ToLocalTime();

                        // Фільтруємо за поточним тижнем
                        if (localTimestamp >= startOfWeek && localTimestamp <= endOfWeek)
                        {
                            result.Add(entry);
                        }
                    }
                }
            }
            catch (JsonException ex)
            {
                // Логування або інша обробка помилки
                Console.WriteLine($"Error deserializing JSON file: {path}. Exception: {ex.Message}");
            }

            return result;
        }

        public (DateTime StartOfWeek, DateTime EndOfWeek) GetWeekRangeWithOffset(int offset)
        {
            var baseDate = DateTime.Today.AddDays(offset * 7);

            // Якщо тиждень починається з понеділка:
            int daysFromMonday = ((int)baseDate.DayOfWeek + 6) % 7;
            var startOfWeek = baseDate.AddDays(-daysFromMonday).Date;
            var endOfWeek = startOfWeek.AddDays(6);

            return (startOfWeek, endOfWeek);
        }


        public (DateTime start, DateTime end) GetMonthRangeWithOffset(int offset)
        {
            DateTime today = DateTime.Today;
            DateTime targetMonth = new DateTime(today.Year, today.Month, 1).AddMonths(offset);
            DateTime start = targetMonth;
            DateTime end = targetMonth.AddMonths(1).AddDays(-1);
            return (start, end);
        }


        public List<TimerData> ProcessCombinedData()
        {
            var urlData = JsonProcessing.LoadUrlData(); // Дані з браузера
            var timerData = JsonProcessing.LoadTimerData(); // Дані з трекера

            // Перетворення в один формат
            var browserConverted = urlData.Select(d => new
            {
                Date = d.Timestamp.Date,
                Seconds = d.TimeSpent
            });

            var trackerConverted = timerData.Select(d =>
            {
                DateTime parsedDate;
                var date = DateTime.TryParseExact(d.Date?.Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate)
                    ? parsedDate
                    : DateTime.MinValue;

                int seconds = TimeSpan.TryParse(d.Time?.Trim(), out var ts) ? (int)ts.TotalSeconds : 0;

                return new
                {
                    Date = date,
                    Seconds = seconds
                };
            });

            return browserConverted
                .Concat(trackerConverted)
                .Where(x => x.Date != DateTime.MinValue) // Відкидаємо некоректні
                .GroupBy(x => x.Date)
                .Select(g => new TimerData
                {
                    Date = g.Key.ToString("dd.MM.yyyy"),
                    Time = TimeSpan.FromSeconds(g.Sum(x => x.Seconds)).ToString()
                })
                .ToList();
        }


        public List<TimerData> ProcessProjectData()
        {
            var freshData = JsonProcessing.LoadTimerData(); // Завантаження JSON-даних проєктів

            return freshData;
        }


        public List<TimerData> ProcessBrowserData()
        {
            var freshData = JsonProcessing.LoadUrlData(); // Завантаження JSON-даних з браузера

            return freshData
                .GroupBy(item => item.Timestamp.Date)
                .Select(g => new TimerData
                {
                    Date = g.Key.ToString("dd.MM.yyyy"),
                    Time = TimeSpan.FromSeconds(g.Sum(item => item.TimeSpent)).ToString()
                })
                .ToList();
        }

        public string HumanizeSeconds(double totalSeconds)
        {
            var ts = TimeSpan.FromSeconds(totalSeconds);
            return $"{ts.Hours} годин {ts.Minutes} хвилин {ts.Seconds} секунд";
        }
    }
}
