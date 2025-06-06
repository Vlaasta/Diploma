using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
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
            this.MaximumSize = this.Size; 
            this.MinimumSize = this.Size;

            toolTip = new ToolTip();

            label2.Text = lastElapsedTime.ToString(@"hh\:mm\:ss");

            Instance = this;

            GetLabel1Text = () => label2.Text;
            ActivityMonitoring.GetLabel1TextDelegate = this.GetLabel1Text;

            handButton = new HandButton();
            handButton.OnTimeUpdated += HandButton_OnTimeUpdated;

            settings = new DataSettings();

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

                        var allData = JsonProcessing.LoadTimerData();
                        string todayKey = DateTime.Now.ToString("dd.MM.yyyy");
                        var todayRecord = allData.FirstOrDefault(d => d.Date == todayKey);
                    }
                    catch (Exception ex)
                    {

                    }

                    lastUpdated = DateTime.Now;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e) //Вікно Статистика
        {
            this.Controls.Clear();
            InitializeComponentMain();
            StatisticsMainMenu();
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
                            if (DateTime.TryParseExact(
                                    session.Start,
                                    "HH:mm:ss",
                                    CultureInfo.InvariantCulture,
                                    DateTimeStyles.None,
                                    out var startTime)
                                && DateTime.TryParseExact(
                                    session.Stop ?? string.Empty,
                                    "HH:mm:ss",
                                    CultureInfo.InvariantCulture,
                                    DateTimeStyles.None,
                                    out var endTime))
                            {
                                var start = selectedDate.Date.Add(startTime.TimeOfDay);
                                var stop = selectedDate.Date.Add(endTime.TimeOfDay);

                                if (stop < start)
                                    stop = stop.AddDays(1);
                                return (start, stop);
                            }
                            return null;
                        });

                        if (_tmpRecord != null
                            && TimeSpan.TryParseExact(
                                   _tmpRecord.Time,
                                   @"hh\:mm\:ss",
                                   CultureInfo.InvariantCulture,
                                   out var _tmpTotal))
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

                        var _tmpDayData = _tmpData
                            .Where(d => d.Timestamp.Date == selectedDate.Date)
                            .ToList();

                        buildDailyChart<UrlData>(_tmpDayData, selectedDate, record =>
                        {
                            var start = record.Timestamp;
                            var stop = start.Add(TimeSpan.FromSeconds(record.TimeSpent));
                            return (start, stop);
                        });

                        string _tmpTotalTime = statistic.CalculateTotalTime<UrlData>(
                            _tmpDayData,
                            nameof(UrlData.TimeSpent)
                        );

                        label9.Text = $"Усього витрачено на роботу: {_tmpTotalTime}";
                        label10.Text = $"Статистика за {selectedDate:dd.MM.yyyy}";

                        break;
                    }

                case "за браузером та проєктами":
                    var selectedDatee = DateTime.Today.AddDays(currentDayOffset);
                    var dateKeyy = selectedDatee.ToString("dd.MM.yyyy");

                    var allTimerData = JsonProcessing.LoadTimerData();
                    var recProj = allTimerData.FirstOrDefault(d => d.Date == dateKeyy);
                    var sessionss = recProj?.Sessions ?? new List<Session>();

                    var allUrlData = JsonProcessing.LoadUrlData();
                    var dayUrlData = allUrlData
                        .Where(u => u.Timestamp.Date == selectedDatee.Date)
                        .ToList();

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

                    var allIntervals = new List<(DateTime Start, DateTime Stop)>();
                    allIntervals.AddRange(projectIntervals);
                    allIntervals.AddRange(browserIntervals);

                    BuildMergedLineChart(selectedDatee, allIntervals);

                    break;

                default:
                    break;
            }
        }

        private void BuildMergedLineChart(DateTime selectedDate, List<(DateTime Start, DateTime Stop)> allIntervals)
        {
            buildDailyChart<(DateTime Start, DateTime Stop)>(
                allIntervals,
                selectedDate,
                interval => (interval.Start, interval.Stop) 
            );
        }

        private void CurrentWeek()
        {
            RemoveChart();

            var weekRange = statistic.GetWeekRangeWithOffset(currentWeekOffset);
            var startOfWeek = weekRange.StartOfWeek;
            var endOfWeek = weekRange.EndOfWeek;

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
                    label10.Text = "Статистика за " + startOfMonth.ToString("MMMM yyyy", ukrCulture);
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
            RemoveBlueRectangles();
        }

        public static int GetNonActiveTime()
        {
            return nonActiveTime;
        }
        
        private void button13_Click(object sender, EventArgs e) //дані в браузері
        {
            this.Controls.Clear();
            InitializeComponentMain();
            SetActivePanel(panel2);
            BrowserInfo();
            ExitButton();
            RemoveBlueRectangles();
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
            RemoveBlueRectangles();
        }

        private void button28_Click(object sender, EventArgs e) //про програму
        {
            this.Controls.Clear();
            InitializeComponentMain();
            SetActivePanel(panel3);
            AboutProgram();
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Invalidate();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; //Вводити лише цифри
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

            }
        }
    }
}

