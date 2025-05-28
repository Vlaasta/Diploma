using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using System.Linq;


namespace diplom
{
    public partial class Form1 : Form
    {
        private HandButton handButton;
        public static Form1 Instance { get; private set; }
        public Func<string> GetLabel1Text { get; set; }

        private DateTime lastUpdated = DateTime.MinValue;
        private readonly object lockObject = new object();

        private Statistic statistic = new Statistic();
        private string textBoxText = null;

        public static bool CheckBox1Active;
        public static bool CheckBox2Active;
        public static bool CheckBox3Active;
        public static bool CheckBox4Active;
        public static bool CheckBox5Active;
        public static bool CheckBox6Active;
        public static bool CheckBox7Active;
        private Panel activePanel; 

        private string typeOfStatictics = "за проєктами";

        private int currentOffset = 0;
        private const int visibleItemsCount = 8; // label3–9 + label11 = 8

        private bool isPersonChoose;

        private ToolTip toolTip;

        TimeSpan lastElapsedTime = LoadLastElapsedTimeFromFile();

        private ViewMode currentViewMode = ViewMode.Day;

        public enum ViewMode {Month, Week, Day}

        private int currentMonthOffset = 0;
        private int currentWeekOffset = 0;
        private int currentDayOffset = 0;


        public static DataSettings settings;
        public static int nonActiveTime;
        public static string themeColor;
        public static bool autoStart;
        public static bool notificationsOnOff;
        public static int TextBoxValue;

        public Form1()
        {
            InitializeComponentMainMenu();
            // this.button3.Visible = false;
            this.MaximumSize = this.Size;  // Фіксувати максимальний розмір форми
            this.MinimumSize = this.Size;  // Фіксувати мінімальний розмір форми

            toolTip = new ToolTip();

            label2.Text = lastElapsedTime.ToString(@"hh\:mm\:ss");

            Instance = this;

            GetLabel1Text = () => label2.Text;
            ActivityMonitoring.GetLabel1TextDelegate = this.GetLabel1Text;

            handButton = new HandButton();
            handButton.OnTimeUpdated += HandButton_OnTimeUpdated;

            settings = new DataSettings();

           /* label3.MouseEnter += Label1_MouseEnter;
            label4.MouseEnter += Label1_MouseEnter;
            label5.MouseEnter += Label1_MouseEnter;
            label6.MouseEnter += Label1_MouseEnter;
            label7.MouseEnter += Label1_MouseEnter;
            label8.MouseEnter += Label1_MouseEnter;

            label3.MouseLeave += Label1_MouseLeave;
            label4.MouseLeave += Label1_MouseLeave;
            label5.MouseLeave += Label1_MouseLeave;
            label6.MouseLeave += Label1_MouseLeave;
            label7.MouseLeave += Label1_MouseLeave;
            label8.MouseLeave += Label1_MouseLeave;*/

            isPersonChoose = false;

            GetTimeAmount();

            JsonProcessing.LoadSettings();
        }

        private void GetTimeAmount()
        {
            var timerDataList = JsonProcessing.LoadTimerData();

            // Отримуємо поточну дату
            string todayDate = DateTime.Now.ToString("dd.MM.yyyy");

            // Шукаємо запис для поточної дати
            var entry = timerDataList.FirstOrDefault(data => data.Date == todayDate);

            if (entry != null)
            {
                // Якщо знайдено, оновлюємо час на label2
                label2.Text = TimeSpan.Parse(entry.Time).ToString(@"hh\:mm\:ss");
            }
            else
            {
                // Якщо запису для поточної дати немає
                label2.Text = "00:00:00";
            }
        }

        public static TimeSpan LoadLastElapsedTimeFromFile()
        {
            try
            {
                var records = JsonProcessing.LoadTimerData(); 

                string currentDate = DateTime.Now.ToString("dd.MM.yyyy");

                var todayRecord = records.FirstOrDefault(r => r.Date == currentDate);

                if (todayRecord != null &&
                    TimeSpan.TryParseExact(todayRecord.Time, @"hh\:mm\:ss", null, out TimeSpan lastElapsedTime))
                {
                    return lastElapsedTime;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні часу: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return TimeSpan.Zero;
        }

        public void HandButton_OnTimeUpdated(TimeSpan elapsed)
        {
            string currentTime = elapsed.ToString(@"hh\:mm\:ss");

            if (DateTime.Now - lastUpdated > TimeSpan.FromSeconds(1))
            {
                lock (lockObject)
                {
                    // Оновлюємо UI елемент
                    if (label2.InvokeRequired)
                    {
                        label2.Invoke(new Action(() => label2.Text = currentTime));
                    }
                    else
                    {
                        label2.Text = currentTime;
                    }

                    try
                    {
                        JsonProcessing.SaveSessionStart(); 
                        JsonProcessing.SaveCurrentDayTime(elapsed);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Помилка при збереженні часу в JSON: {ex.Message}");
                    }

                    lastUpdated = DateTime.Now;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e) //графік поточного тижня
        {
            this.Controls.Clear();
            InitializeComponentMain();
            StatisticsMainMenu();
            //ProperColorTheme();
            SetActivePanel(panel2);
            CurrentDay();
        }

        private void CurrentDay()
        {           
            RemoveChart();

            DateTime selectedDate = DateTime.Today.AddDays(currentDayOffset);

            switch (typeOfStatictics)
            {
                case "за проєктами":
                    {
                        var _tmpData = JsonProcessing.LoadTimerData();
                        string _tmpDateKey = selectedDate.ToString("dd.MM.yyyy");
                        var _tmpRecord = _tmpData.FirstOrDefault(d => d.Date == _tmpDateKey);
                        var _tmpSessions = _tmpRecord?.Sessions ?? new List<Session>();

                        buildDailyChart<Session>(_tmpSessions, selectedDate, session =>
                        {
                            if (DateTime.TryParseExact(session.Start, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var startTime)
                                && DateTime.TryParseExact(session.Stop, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var endTime))
                            {
                                var start = selectedDate.Date.Add(startTime.TimeOfDay);
                                var stop = selectedDate.Date.Add(endTime.TimeOfDay);
                                if (stop < start) stop = stop.AddDays(1); // нічний перехід
                                return (start, stop);
                            }
                            return null;
                        });

                        if (_tmpRecord != null
                            && TimeSpan.TryParseExact(_tmpRecord.Time, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out var _tmpTotal))
                        {
                            label9.Text = $"Усього витрачено на роботу: {_tmpTotal.Hours} год. {_tmpTotal.Minutes} хв. {_tmpTotal.Seconds} сек.";
                        }
                        else
                        {
                            label9.Text = "Усього витрачено на роботу: 0 год. 0 хв. 0 сек.";
                        }

                        label10.Text = $"Статистика за {selectedDate:dd.MM.yyyy}";
                        break;
                    }

                case "за браузером":
                    {
                        var _tmpData = JsonProcessing.LoadUrlData();

                        // Фільтруємо записи лише на поточний день
                        var _tmpDayData = _tmpData
                            .Where(d => d.Timestamp.Date == selectedDate.Date)
                            .ToList();

                        // Побудова графіка
                        buildDailyChart<UrlData>(_tmpDayData, selectedDate, record =>
                        {
                            var start = record.Timestamp;
                            var stop = start.Add(TimeSpan.FromSeconds(record.TimeSpent));
                            return (start, stop);
                        });

                        // Обчислення загального часу
                        string _tmpTotalTime = statistic.CalculateTotalTime<UrlData>(
                            _tmpDayData,
                            nameof(UrlData.TimeSpent)
                        );

                        // Оновлення підписів
                        label9.Text = $"Усього витрачено на роботу: {_tmpTotalTime}";
                        label10.Text = $"Статистика за {selectedDate:dd.MM.yyyy}";

                        break;
                    }

                case "за браузером та проєктами":
                    // 1) Обрана дата
                    var selectedDatee = DateTime.Today.AddDays(currentDayOffset);
                    var dateKeyy = selectedDate.ToString("dd.MM.yyyy");

                    // 2) Дані по проєктах:
                    var allTimerData = JsonProcessing.LoadTimerData();
                    var recProj = allTimerData.FirstOrDefault(d => d.Date == dateKeyy);
                    var sessionss = recProj?.Sessions ?? new List<Session>();

                    // 3) Дані по браузеру:
                    var allUrlData = JsonProcessing.LoadUrlData();
                    var dayUrlData = allUrlData.Where(u => u.Timestamp.Date == selectedDate.Date).ToList();

                    // 4) Розраховуємо хвилини по годинах 0..23 для кожного джерела
                    var intervals = Enumerable.Range(0, 96); // 96 інтервалів по 15 хвилин

                    var projMinutes = intervals
                        .Select(i =>
                        {
                            double sec = 0;
                            var segStart = selectedDatee.Date.AddMinutes(i * 15);
                            var segEnd = segStart.AddMinutes(15);

                            foreach (var s in sessionss)
                            {
                                if (!DateTime.TryParseExact(s.Start, "HH:mm:ss", null, DateTimeStyles.None, out var t1)) continue;
                                if (!DateTime.TryParseExact(s.Stop, "HH:mm:ss", null, DateTimeStyles.None, out var t2)) continue;

                                var dt1 = selectedDatee.Date.Add(t1.TimeOfDay);
                                var dt2 = selectedDatee.Date.Add(t2.TimeOfDay);
                                if (dt2 < dt1) dt2 = dt2.AddDays(1);

                                var a = dt1 > segStart ? dt1 : segStart;
                                var b = dt2 < segEnd ? dt2 : segEnd;

                                if (b > a) sec += (b - a).TotalSeconds;
                            }

                            return sec / 60.0; // хвилини
    })
                        .ToList();

                    var browserMinutes = intervals
                        .Select(i =>
                        {
                            int hour = (i * 15) / 60;
                            int minute = (i * 15) % 60;
                            var segStart = selectedDatee.Date.AddHours(hour).AddMinutes(minute);
                            var segEnd = segStart.AddMinutes(15);

                            double totalSec = 0;
                            foreach (var u in dayUrlData)
                            {
                                var timestamp = u.Timestamp;
                                if (timestamp >= segStart && timestamp < segEnd)
                                {
                                    totalSec += u.TimeSpent;
                                }
                            }
                            return totalSec / 60.0;
                        })
                        .ToList();

                    BuildMergedPlotModel(projMinutes, browserMinutes, selectedDatee);
                    break;

                default:
                    break;
            }
        }

        private void CurrentWeek()
        {
            RemoveChart();

            // Отримання діапазону тижня з урахуванням зсуву
            var weekRange = statistic.GetWeekRangeWithOffset(currentWeekOffset);
            var startOfWeek = weekRange.StartOfWeek;
            var endOfWeek = weekRange.EndOfWeek;

            // Обробка даних згідно типу статистики
            List<TimerData> processedData;
            switch (typeOfStatictics)
            {
                case "за проєктами":
                    processedData = statistic.ProcessProjectData();
                    break;
                case "за браузером":
                    processedData = statistic.ProcessBrowserData();
                    break;
                case "за браузером та проєктами":
                    processedData = statistic.ProcessCombinedData();
                    break;
                default:
                    processedData = new List<TimerData>();
                    break;
            }

            var filteredData = statistic.FilterDataForDateRange(
                processedData, startOfWeek, endOfWeek,
                item =>
                {
                    DateTime parsedDate;
                    return DateTime.TryParse(item.Date, out parsedDate) ? parsedDate : DateTime.MinValue;
                }
            );
            var filledData = statistic.FillMissingDays(filteredData, startOfWeek, endOfWeek);

            var xPoints = new List<DateTime>();
            var yPoints = new List<int>();

            foreach (var d in filledData)
            {
                try
                {
                    var date = DateTime.ParseExact(d.Date.Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    var time = TimeSpan.Parse(d.Time.Trim());

                    xPoints.Add(date);
                    yPoints.Add((int)time.TotalSeconds);
                }
                catch {  }
            }

            buildChart(xPoints, yPoints);

            string totalTime = statistic.CalculateTotalTime(filledData, "Time");
            label9.Text = "Усього витрачено на роботу: " + totalTime;
            label10.Text = $"Статистика за {startOfWeek:dd.MM.yyyy} - {endOfWeek:dd.MM.yyyy}";
        }

        private void CurrentMonth()
        {
            RemoveChart();

            List<TimerData> processedData;

            switch (typeOfStatictics)
            {
                case "за проєктами":
                    processedData = statistic.ProcessProjectData();
                    break;
                case "за браузером":
                    processedData = statistic.ProcessBrowserData();
                    break;
                case "за браузером та проєктами":
                    processedData = statistic.ProcessCombinedData();
                    break;
                default:
                    processedData = new List<TimerData>();
                    break;
            }

            var (startOfMonth, endOfMonth) = statistic.GetMonthRangeWithOffset(currentMonthOffset);

            List<TimerData> filledData = statistic.FillMissingDays(processedData, startOfMonth, endOfMonth);

            var xPoints = statistic.GetXPoints(filledData);
            var yPoints = statistic.GetYPoints(filledData);

            buildChart(xPoints, yPoints);

            string totalTime = statistic.CalculateTotalTime(filledData, "Time");
            label9.Text = "Усього витрачено на роботу: " + totalTime;
            var ukrCulture = new System.Globalization.CultureInfo("uk-UA");

            switch (typeOfStatictics)
            {
                case "за проєктами":
                case "за браузером":
                    label10.Text = "Статистика за " + startOfMonth.ToString("MMMM yyyy", ukrCulture);
                    break;

                case "за браузером та проєктами":
                    label10.Text = "Статистика за " + startOfMonth.ToString("MMMM yyyy", ukrCulture) + " (обʼєднана)";
                    break;
            }
        }

        private void button11_Click(object sender, EventArgs e) //-1 в статистиці
        {
            switch (currentViewMode)
            {
                case ViewMode.Month:
                    currentMonthOffset--;
                    CurrentMonth();
                    break;
                case ViewMode.Week:
                    currentWeekOffset--;
                    CurrentWeek();
                    break;
                case ViewMode.Day:
                    currentDayOffset--;
                    CurrentDay();
                    break;
            }
        }

        private void button12_Click(object sender, EventArgs e) //+1 в статистиці
        {
            switch (currentViewMode)
            {
                case ViewMode.Month:
                    currentMonthOffset++;
                    CurrentMonth();
                    break;
                case ViewMode.Week:
                    currentWeekOffset++;
                    CurrentWeek();
                    break;
                case ViewMode.Day:
                    currentDayOffset++;
                    CurrentDay();
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All Files (*.*)|*.*";
                openFileDialog.Title = "Виберіть проект для додавання";
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;

                    try
                    {
                        JsonProcessing.AddProject(selectedFilePath);
                        MessageBox.Show($"Файл успішно додано: {selectedFilePath}", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PopulateProjects();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        } //вибрати проект

        private void button2_Click(object sender, EventArgs e)
        {
            if (!handButton.IsRunning)
            {
                JsonProcessing.SaveSessionStart();

                if (TimeSpan.TryParseExact(label2.Text, @"hh\:mm\:ss", null, out TimeSpan lastElapsed))
                {
                    handButton.StartWithTime(lastElapsed); // Виклик правильного методу
                }
                else
                {
                    handButton.StartWithTime(TimeSpan.Zero); // Починаємо з 0, якщо час некоректний
                }

                button2.Text = "Зупинити таймер";
            }
            else
            {
                handButton.Pause();

                try
                {
                    var accumulatedTime = handButton.GetAccumulatedTime();
                    JsonProcessing.SaveSessionStop();
                    JsonProcessing.SaveCurrentDayTime(accumulatedTime); // Використання GetAccumulatedTime
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка збереження часу: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                button2.Text = "Запустити таймер";
            }
        } //запустити таймер

        private void button8_Click(object sender, EventArgs e) //головне меню на панелі
        {
            this.Controls.Clear();
            InitializeComponentMainMenu();
            SetActivePanel(panel6);
            GetTimeAmount();
            PopulateProjects(); 
        }

        public static int GetNonActiveTime()
        {
            return nonActiveTime;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();

            BrowserInfo();
            label10.Text = "Статистичні дані про активність в браузері";

            LoadBrowserDataIntoLabels();
        }

        private void LoadBrowserDataIntoLabels()
        {
           /* string filePath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserActivity\browserUrls.json";
            if (!File.Exists(filePath)) return;

            string json = File.ReadAllText(filePath);
            List<UrlData> records = JsonConvert.DeserializeObject<List<UrlData>>(json);

            // Обрізаємо записи згідно поточного offset
            var visibleRecords = records.Skip(currentOffset).Take(visibleItemsCount).ToList();

            // Масиви лейблів
            Label[] urlLabels = { label3, label4, label5, label6, label7, label8, label9, label11 };
            Label[] titleLabels = { label12, label13, label14, label15, label16, label17, label18, label19 };

            for (int i = 0; i < urlLabels.Length; i++)
            {
                if (i < visibleRecords.Count)
                {
                    urlLabels[i].Text = visibleRecords[i].Url;
                    titleLabels[i].Text = visibleRecords[i].PageTitle;
                }
                else
                {
                    urlLabels[i].Text = "";
                    titleLabels[i].Text = "";
                }
            }

            // Кнопка "вниз" зникає, якщо останній елемент на екрані — останній у файлі
            button39.Visible = currentOffset + visibleItemsCount < records.Count;

            // Кнопка "вгору" зникає, якщо ми на початку
            button38.Visible = currentOffset > 0;*/
        }

        private void DeleteRecordAndRefreshLabels(int indexOnPage)
        {
            string filePath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserActivity\browserUrls.json";

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл не знайдено.");
                return;
            }

            string json = File.ReadAllText(filePath);
            List<UrlData> records = JsonConvert.DeserializeObject<List<UrlData>>(json);

            int actualIndex = currentOffset + indexOnPage; // ВАЖЛИВО: зсув

            if (actualIndex < 0 || actualIndex >= records.Count)
            {
                MessageBox.Show("Недійсний індекс.");
                return;
            }

            records.RemoveAt(actualIndex);

            // Якщо після видалення ми вийшли за межу, зменшуємо offset
            if (currentOffset > 0 && currentOffset >= records.Count - visibleItemsCount + 1)
            {
                currentOffset--;
            }

            string updatedJson = JsonConvert.SerializeObject(records, Formatting.Indented);
            File.WriteAllText(filePath, updatedJson);

            LoadBrowserDataIntoLabels();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            DeleteRecordAndRefreshLabels(0);
        }
        private void button31_Click(object sender, EventArgs e)
        {
            DeleteRecordAndRefreshLabels(1);
        }
        private void button32_Click(object sender, EventArgs e)
        {
            DeleteRecordAndRefreshLabels(2);
        }
        private void button33_Click(object sender, EventArgs e)
        {
            DeleteRecordAndRefreshLabels(3);
        }
        private void button34_Click(object sender, EventArgs e)
        {
            DeleteRecordAndRefreshLabels(4);
        }
        private void button35_Click(object sender, EventArgs e)
        {
            DeleteRecordAndRefreshLabels(5);
        }
        private void button36_Click(object sender, EventArgs e)
        {
            DeleteRecordAndRefreshLabels(6);
        }
        private void button37_Click(object sender, EventArgs e)
        {
            DeleteRecordAndRefreshLabels(7);
        }
        private void button38_Click(object sender, EventArgs e)
        {
            if (currentOffset > 0)
            {
                currentOffset--;
                LoadBrowserDataIntoLabels();
            }
        }
        private void button39_Click(object sender, EventArgs e)
        {
            string filePath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserActivity\browserUrls.json";
            if (!File.Exists(filePath)) return;

            string json = File.ReadAllText(filePath);
            List<UrlData> records = JsonConvert.DeserializeObject<List<UrlData>>(json);

            if (currentOffset + visibleItemsCount < records.Count)
            {
                currentOffset++;
                LoadBrowserDataIntoLabels();
            }
        }
        private void button13_Click(object sender, EventArgs e) //дані в браузері
        {
            this.Controls.Clear();
            InitializeComponentMain();
            BrowserInfo();
            LoadBrowserDataIntoLabels();
            ExitButton();
            label10.Text = "Дані активності в браузері"; 
        }

        private void button14_Click(object sender, EventArgs e) //вихід з даних в браузері
        {
            this.Controls.Clear();
            InitializeComponentMain();
            StatisticsMainMenu();
            CurrentDay(); 
        }

        private void button27_Click(object sender, EventArgs e) //налаштування на панелі
        {
            this.Controls.Clear();
            InitializeComponentMain();
            SetActivePanel(panel4);
            SettingsMenu();
            textBox1.KeyPress += textBox1_KeyPress;
        }

        private void button28_Click(object sender, EventArgs e) //про програму
        {
            this.Controls.Clear();
            InitializeComponentMain();
            SetActivePanel(panel3);
            AboutProgram();
        }

        private void Label1_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                toolTip.SetToolTip(label, label.Text);
            }
        }

        private void Label1_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                toolTip.Hide(label);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Блокуємо введення, якщо це не цифра
            }
        }

        public static void UpdateInactivityAmount(Form1 form1, int newInactivityAmount)
        {
            Form1.settings.InactivityAmount = newInactivityAmount;
            JsonProcessing.SaveSettings(form1); 
        }

        public static void UpdateColorTheme(Form1 form1, string newColorTheme)
        {
            Form1.settings.ColorTheme = newColorTheme;
            JsonProcessing.SaveSettings(form1);
        }

        public static void UpdateAutostart(Form1 form1, bool newAutostart)
        {
            Form1.settings.Autostart = newAutostart;
            JsonProcessing.SaveSettings(form1);
        }

        public static void UpdateNotificaton(Form1 form1, bool newNotificatonOnOff)
        {
            Form1.settings.NotificatonOnOff = newNotificatonOnOff;
            JsonProcessing.SaveSettings(form1);
        }

        public static void UpdateTextBoxValue(Form1 form1, int newTextBoxValue)
        {
            Form1.settings.TextBoxValue = newTextBoxValue;
            JsonProcessing.SaveSettings(form1);
        }
    }
}

