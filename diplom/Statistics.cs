using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;

namespace diplom
{
    internal class Statistics
    {
        static void Draw()
        {
            // 1. Зчитування даних із JSON-файлу
            string jsonContent = File.ReadAllText("timerAmounts.json");
            List<TimerData> timerData = JsonSerializer.Deserialize<List<TimerData>>(jsonContent);

            // 2. Перетворення часу у секунди
            List<int> seconds = new List<int>();
            List<string> dates = new List<string>();

            foreach (var data in timerData)
            {
                TimeSpan timeSpan = TimeSpan.Parse(data.Time);
                seconds.Add((int)timeSpan.TotalSeconds);
                dates.Add(data.Date);
            }

            // 3. Налаштування стовпчастої діаграми
            var cartesianChart = new LiveCharts.WinForms.CartesianChart
            {
                Dock = DockStyle.Fill,
                Series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Час активності (секунди)",
                        Values = new ChartValues<int>(seconds)
                    }
                },
                AxisX = new AxesCollection
                {
                    new Axis
                    {
                        Title = "Дата",
                        Labels = dates
                    }
                },
                AxisY = new AxesCollection
                {
                    new Axis
                    {
                        Title = "Секунди",
                        LabelFormatter = value => value.ToString("N0")
                    }
                }
            };

            // 4. Відображення діаграми у формі
            var form = new Form
            {
                Text = "Час активності по днях",
                Width = 800,
                Height = 600
            };

            form.Controls.Add(cartesianChart);
            Application.Run(form);
        }
    }
}
