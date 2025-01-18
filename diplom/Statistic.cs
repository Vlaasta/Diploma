using System.Drawing; // Простір імен для Color
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Globalization;
using OxyPlot.Axes;
using System.Linq;

namespace diplom
{
    public class Statistic : Form
    {

       // string jsonContent = File.ReadAllText(@"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\timerAmounts.json");
       public List<TimerData> timerData = JsonSerializer.Deserialize<List<TimerData>>(File.ReadAllText(@"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\timerAmounts.json"));

        public void test()
        {
            string jsonContent = File.ReadAllText(@"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\timerAmounts.json");
            List<TimerData> timerData = JsonSerializer.Deserialize<List<TimerData>>(jsonContent);

            // Створення PlotView для відображення графіка
            var plotView = new PlotView
            {
                Dock = DockStyle.None, // Вимикаємо автоматичне заповнення форми
                Size = new Size(600, 400), // Встановлюємо розміри графіка (ширина - 600, висота - 400)
                Location = new Point(300, 60) // Встановлюємо місце розташування (відстань від лівого верхнього кута форми)

            };

            // Створення моделі графіка
            var plotModel = new PlotModel
            {
                Title = "Статистика за останній тиждень",
                TitleColor = OxyColor.FromRgb(169, 169, 169), // Колір заголовку графіка
                PlotAreaBorderColor = OxyColor.FromRgb(2, 14, 25) // Колір межі (білий або інший, щоб вона була непомітною)
            };

            // Створення серії для лінійного графіка
            var lineSeries = new LineSeries
            {
                Title = "Час (секунди)",
                MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(169, 169, 169) // Колір лінії (сірий)
            };

            // Додаємо точки на графік
            var labels = new List<string>();
            foreach (var data in timerData)
            {
                DateTime date = DateTime.ParseExact(data.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                TimeSpan timeSpan = TimeSpan.Parse(data.Time);
                int totalSeconds = (int)timeSpan.TotalSeconds;

                lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), totalSeconds));
                labels.Add(date.ToString("dd.MM.yyyy"));
            }

            // Додаємо серію до моделі
            plotModel.Series.Add(lineSeries);

            // Налаштовуємо осі
            var axisX = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Дата",
                TitleColor = OxyColor.FromRgb(169, 169, 169),
                TextColor = OxyColor.FromRgb(169, 169, 169),
                StringFormat = "dd.MM.yyyy",  // Формат дати
                MajorGridlineColor = OxyColor.FromRgb(169, 169, 169),  // Колір сітки
                MinorGridlineColor = OxyColor.FromRgb(169, 169, 169),
                IsZoomEnabled = false,  // Вимкнути масштабування по осі X
                AxislineColor = OxyColor.FromArgb(0, 0, 0, 0),  // Приховуємо лінію осі X
                TicklineColor = OxyColor.FromArgb(0, 0, 0, 0),  // Приховуємо позначки на осі X
                IsAxisVisible = false // Приховуємо саму вісь X
            };

            var axisY = new LinearAxis
            {
                Title = "Секунди",
                TitleColor = OxyColor.FromRgb(169, 169, 169),
                TextColor = OxyColor.FromRgb(169, 169, 169),
                MajorGridlineColor = OxyColor.FromRgb(169, 169, 169),
                MinorGridlineColor = OxyColor.FromRgb(169, 169, 169),
                IsZoomEnabled = false,  // Вимкнути масштабування по осі X
                AxislineColor = OxyColor.FromArgb(0, 0, 0, 0),  // Приховуємо лінію осі X
                TicklineColor = OxyColor.FromArgb(0, 0, 0, 0),  // Приховуємо позначки на осі X
                IsAxisVisible = false // Приховуємо саму вісь X
            };

            // Додаємо осі до графіка
            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            // Призначаємо модель графіку компоненту PlotView
            plotView.Model = plotModel;

            // Додаємо PlotView на форму
            this.Controls.Add(plotView);
            MessageBox.Show("Биба на месте");
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

        // Метод для фільтрації даних для попереднього тижня
        public List<TimerData> FilterDataForPreviousWeek(List<TimerData> timerData)
        {
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
    }
}
