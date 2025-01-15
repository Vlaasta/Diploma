using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using LiveCharts; // Для роботи з SeriesCollection
using LiveCharts.Wpf; // Для осей та налаштувань діаграми
using System.Linq;


namespace diplom
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // 1. Зчитування даних із JSON-файлу
            string jsonContent = File.ReadAllText(@"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\timerAmounts.json");
            List<TimerData> timerData = JsonSerializer.Deserialize<List<TimerData>>(jsonContent);

            // 2. Підготовка даних для діаграми
            var values = new ChartValues<double>(); // Дозволяє десяткові значення
            var labels = new List<string>();

            // Список для збереження секунд
            var totalSecondsList = new List<int>();

            // Конвертація всього часу у секунди
            foreach (var data in timerData)
            {
                TimeSpan timeSpan = TimeSpan.Parse(data.Time);
                int totalSeconds = (int)timeSpan.TotalSeconds;
                totalSecondsList.Add(totalSeconds);
            }

            // 3. Автоматичне визначення одиниці вимірювання
            string unit;   // Одиниця вимірювання
            double scale;  // Масштаб для конвертації

            int maxSeconds = totalSecondsList.Max();

            if (maxSeconds >= 3600) // Якщо більше 1 години
            {
                unit = "Години";
                scale = 3600.0; // Конвертація секунд у години
            }
            else if (maxSeconds >= 60) // Якщо більше 1 хвилини
            {
                unit = "Хвилини";
                scale = 60.0; // Конвертація секунд у хвилини
            }
            else
            {
                unit = "Секунди";
                scale = 1.0; // Без конвертації
            }

            // 4. Конвертація значень та додавання міток
            foreach (var data in timerData)
            {
                TimeSpan timeSpan = TimeSpan.Parse(data.Time);
                double scaledValue = timeSpan.TotalSeconds / scale; // Конвертуємо значення
                values.Add(scaledValue);
                labels.Add(data.Date);
            }

            // 5. Побудова діаграми
            var cartesianChart = new LiveCharts.WinForms.CartesianChart
            {
                Dock = DockStyle.Fill
            };

            cartesianChart.Series = new SeriesCollection
            {
                new ColumnSeries
            {
                Title = $"Час ({unit})", // Динамічний заголовок
                Values = values
            }
            };

            cartesianChart.AxisX.Add(new Axis
            {
                Title = "Дата",
                Labels = labels
            });

            cartesianChart.AxisY.Add(new Axis
            {
                Title = unit
            });

            // 6. Додавання діаграми до форми
            Controls.Clear(); // Очищення попередніх елементів
            Controls.Add(cartesianChart);
        }
    }
}
