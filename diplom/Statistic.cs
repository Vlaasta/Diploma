using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.Linq;

namespace diplom
{
    public class Statistic : Form
    {
        public List<T> FilterDataForDateRange<T>(List<T> dataList, DateTime startDate, DateTime endDate, Func<T, DateTime> getDate)
        {
            return dataList.Where(item =>
            {
                var date = getDate(item);
                return date >= startDate && date <= endDate;
            }).ToList();
        }

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

        public (DateTime StartOfWeek, DateTime EndOfWeek) GetWeekRangeWithOffset(int offset)
        {
            var baseDate = DateTime.Today.AddDays(offset * 7);
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
            var urlData = JsonProcessing.LoadUrlData(); 
            var timerData = JsonProcessing.LoadTimerData(); 

            var browserConverted = urlData.Select(d => new
            {
                Date = d.Timestamp.Date,
                Seconds = d.TimeSpent
            });

            var trackerConverted = timerData.Select(d =>
            {
                DateTime parsedDate;
                var date = DateTime.TryParseExact(d.Date?.Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate) ? parsedDate : DateTime.MinValue;

                int seconds = TimeSpan.TryParse(d.Time?.Trim(), out var ts) ? (int)ts.TotalSeconds : 0;

                return new
                {
                    Date = date,
                    Seconds = seconds
                };
            });

            return browserConverted
                .Concat(trackerConverted)
                .Where(x => x.Date != DateTime.MinValue) 
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
            var freshData = JsonProcessing.LoadTimerData(); 
            return freshData;
        }

        public List<TimerData> ProcessBrowserData()
        {
            var freshData = JsonProcessing.LoadUrlData(); 

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
