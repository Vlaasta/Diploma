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
        public static bool AutostartEnabled { get; set; } = false;

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
            //ListInstalledFonts();


            GetTimeAmount();

            JsonProcessing.LoadSettings();
        }

        private void GetTimeAmount()
        {
            var timerDataList = JsonProcessing.LoadTimerData();

            string todayDate = DateTime.Now.ToString("dd.MM.yyyy");

            var entry = timerDataList.FirstOrDefault(data => data.Date == todayDate);

            if (entry != null)
            {
                label2.Text = TimeSpan.Parse(entry.Time).ToString(@"hh\:mm\:ss");
            }
            else
            {
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
                        // 1. Завантажуємо сесії з JSON
                        var TimerData = JsonProcessing.LoadTimerData();
                        string dateKey = selectedDate.ToString("dd.MM.yyyy");
                        var reccProj = TimerData.FirstOrDefault(d => d.Date == dateKey);
                        var sessionsList = reccProj?.Sessions ?? new List<Session>();

                        // 2. Перетворюємо кожен Session у (DateTime Start, DateTime Stop)
                        //    і одразу викликаємо новий buildDailyChartWithDateTime
                        buildDailyChartWithDateTime<Session>(
                            sessionsList,
                            selectedDate,
                            session =>
                            {
                                if (DateTime.TryParseExact(session.Start, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var t1)
                                    && DateTime.TryParseExact(session.Stop, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var t2))
                                {
                                    var start = selectedDate.Date.Add(t1.TimeOfDay);
                                    var stop = selectedDate.Date.Add(t2.TimeOfDay);
                                    if (stop < start)
                                        stop = stop.AddDays(1); // перехід через північ
                    return (start, stop);
                                }
                                return null;
                            }
                        );

                        // 3. Оновлюємо підписи label9 і label10 так само, як у вас було:
                        if (reccProj != null
                            && TimeSpan.TryParseExact(reccProj.Time, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out var totalProj))
                        {
                            label9.Text = $"Усього витрачено на роботу: {totalProj.Hours} год. {totalProj.Minutes} хв. {totalProj.Seconds} сек.";
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
                        // 1. Завантажуємо всю UrlData та фільтруємо по дню
                        var LUrlData = JsonProcessing.LoadUrlData();
                        var dailyUrlData = LUrlData
                            .Where(u => u.Timestamp.Date == selectedDate.Date)
                            .ToList();

                        // 2. Викликаємо buildDailyChartWithDateTime<UrlData>, передаючи кожен UrlData у інтервал (Start, Stop)
                        buildDailyChartWithDateTime<UrlData>(
                            dailyUrlData,
                            selectedDate,
                            record =>
                            {
                                var start = record.Timestamp;
                                var stop = record.Timestamp.AddSeconds(record.TimeSpent);
                // обрізаємо до межі доби, якщо потрібно
                if (start < selectedDate.Date)
                                    start = selectedDate.Date;
                                if (stop > selectedDate.Date.AddDays(1))
                                    stop = selectedDate.Date.AddDays(1);
                                return (start, stop);
                            }
                        );

                        // 3. Рахуємо загальний час у браузері за допомогою вашого statistic.CalculateTotalTime
                        string totalBrowserTime = statistic.CalculateTotalTime<UrlData>(
                            dailyUrlData,
                            nameof(UrlData.TimeSpent)
                        );

                        label9.Text = $"Усього витрачено на роботу: {totalBrowserTime}";
                        label10.Text = $"Статистика за {selectedDate:dd.MM.yyyy}";
                        break;
                    }

                case "за браузером та проєктами":
                    var selectedDatee = DateTime.Today.AddDays(currentDayOffset);
                    var dateKeyy = selectedDatee.ToString("dd.MM.yyyy");

                    var allTimerData = JsonProcessing.LoadTimerData();
                    var recProj = allTimerData.FirstOrDefault(d => d.Date == dateKeyy);
                    var sessionss = recProj?.Sessions ?? new List<Session>();

                    var projectIntervals = sessionss
                        .Select(s =>
                        {
                            if (!DateTime.TryParseExact(s.Start, "HH:mm:ss", null, DateTimeStyles.None, out var t1))
                                return (Start: DateTime.MinValue, Stop: DateTime.MinValue);
                            if (!DateTime.TryParseExact(s.Stop, "HH:mm:ss", null, DateTimeStyles.None, out var t2))
                                return (Start: DateTime.MinValue, Stop: DateTime.MinValue);

                            var st = selectedDatee.Date.Add(t1.TimeOfDay);
                            var sp = selectedDatee.Date.Add(t2.TimeOfDay);
                            if (sp < st) sp = sp.AddDays(1);
                            return (Start: st, Stop: sp);
                        })
                        .Where(x => x.Start != DateTime.MinValue)
                        .ToList();

                    // 2) Завантажуємо браузерні записи та перетворюємо в інтервали (DateTime,DateTime)
                    var allUrlData = JsonProcessing.LoadUrlData();
                    var dayUrlData = allUrlData
                        .Where(u => u.Timestamp.Date == selectedDatee.Date)
                        .ToList();

                    var browserIntervals = dayUrlData
                        .Select(u =>
                        {
                            var stBr = u.Timestamp;
                            var spBr = u.Timestamp.AddSeconds(u.TimeSpent);
                            if (stBr < selectedDatee.Date)
                                stBr = selectedDatee.Date;
                            if (spBr > selectedDatee.Date.AddDays(1))
                                spBr = selectedDatee.Date.AddDays(1);
                            return (Start: stBr, Stop: spBr);
                        })
                        .Where(x => x.Stop > x.Start)
                        .ToList();

                    // 3) Поєднуємо обидва списки інтервалів
                    var allIntervals = new List<(DateTime Start, DateTime Stop)>();
                    allIntervals.AddRange(projectIntervals);
                    allIntervals.AddRange(browserIntervals);

                    // 4) Викликаємо buildDailyChartWithDateTime, передаючи єдину колекцію інтервалів
                    buildDailyChartWithDateTime<(DateTime Start, DateTime Stop)>(
                        allIntervals,
                        selectedDatee,
                        interval => interval  // просто віддає кортеж, адже в allIntervals він уже (Start, Stop)
                    );
                    break;

                default:
                    break;
            }
        }

        private void BuildMergedLineChart(DateTime selectedDate,
                                  List<(DateTime Start, DateTime Stop)> allIntervals)
        {
            // Використовуємо той самий buildDailyChart, тільки з типовим T = (DateTime, DateTime).
            // Для цього передамо адаптер convertToInterval, який віддасть кортеж (Start, Stop).
            buildDailyChart<(DateTime Start, DateTime Stop)>(
                allIntervals,
                selectedDate,
                interval => (interval.Start, interval.Stop)  // просто повертаємо те, що нам дали
            );
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
                openFileDialog.Title = "Виберіть проєкт для додавання";
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
        
        private void button13_Click(object sender, EventArgs e) //дані в браузері
        {
            this.Controls.Clear();
            InitializeComponentMain();
            BrowserInfo();
            ExitButton();
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

        public void EnableAutoStart()
        {
            if (!AutostartEnabled)
                DisableAutoStart();

            string startupFolder = Environment.GetFolderPath(
                Environment.SpecialFolder.Startup);
            string shortcutPath = Path.Combine(startupFolder, "MyApp.lnk");

            var wshType = Type.GetTypeFromProgID("WScript.Shell");
            dynamic wsh = Activator.CreateInstance(wshType);
            dynamic shortcut = wsh.CreateShortcut(shortcutPath);

            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.WorkingDirectory = Application.StartupPath;
            shortcut.Description = "Auto-start MyApp";
            shortcut.Save();
        }

        public void DisableAutoStart()
        {
            string startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            string shortcutPath = Path.Combine(startupFolder, "MyApp.lnk");

            try
            {
                if (File.Exists(shortcutPath))
                    File.Delete(shortcutPath);
            }
            catch (Exception ex)
            {
                /*MessageBox.Show(
                    $"Не вдалося вимкнути автозапуск: {ex.Message}",
                    "DisableAutoStart",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );*/
            }
        }
    }
}

