using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using LiveCharts; // Для роботи з SeriesCollection
using LiveCharts.Wpf; // Для осей та налаштувань діаграми

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
            var values = new ChartValues<int>();
            var labels = new List<string>();

            foreach (var data in timerData)
            {
                TimeSpan timeSpan = TimeSpan.Parse(data.Time);
                int totalSeconds = (int)timeSpan.TotalSeconds;

                values.Add(totalSeconds);
                labels.Add(data.Date);
            }

            // 3. Використання конкретної версії CartesianChart
            var cartesianChart = new LiveCharts.WinForms.CartesianChart
            {
                Dock = DockStyle.Fill
            };

            cartesianChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Час (секунди)",
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
                Title = "Секунди"
            });

            // 4. Додавання діаграми до форми
            Controls.Add(cartesianChart);
        }
    }
}
