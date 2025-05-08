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
        private bool isPreviousWeek = true;
        private string textBoxText = null;

        public static bool CheckBox1Active;
        public static bool CheckBox2Active;
        public static bool CheckBox3Active;
        public static bool CheckBox4Active;
        public static bool CheckBox5Active;
        public static bool CheckBox6Active;
        public static bool CheckBox7Active;

        private bool isBrowserStats = false; // Початково: показує браузерну статистику

        private string typeOfStatictics = "за проєктами";

        private int currentOffset = 0;
        private const int visibleItemsCount = 8; // label3–9 + label11 = 8

        private bool isPersonChoose;

        private ToolTip toolTip;

        // Завантажуємо час з файлу при запуску програми
        TimeSpan lastElapsedTime = LoadLastElapsedTimeFromFile();

        private ViewMode currentViewMode = ViewMode.Day;

        public enum ViewMode
        {
            Month,
            Week,
            Day
        }

        private int currentMonthOffset = 0;
        private int currentWeekOffset = 0;
        private int currentDayOffset = 0;


        public static DataSettings settings;
        // public int nonActiveTime = 5;
        public static int nonActiveTime;
        public static string themeColor;
        public static bool autoStart;
        public static bool notificationsOnOff;
        public static int TextBoxValue;

       // private List<TimerData> timerData;

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
            Program.GetLabel1TextDelegate = this.GetLabel1Text;

            //timerData = new TimerData(); 

            handButton = new HandButton();
            handButton.OnTimeUpdated += HandButton_OnTimeUpdated;

            settings = new DataSettings();

            loadValues();

            label3.MouseEnter += Label1_MouseEnter;
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
            label8.MouseLeave += Label1_MouseLeave;

            isPersonChoose = false;

            LabelsToShow();
            ButtonsToShow();

            JsonProcessing.LoadSettings();

        }

        private void GetTimeAmount()
        {
            string json = File.ReadAllText(@"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\MainInfo\timerAmounts.json");
            var timerDataList = JsonConvert.DeserializeObject<List<TimerData>>(json);

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
                // Шлях до файлу
                string filePath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\timerAmounts.json";

                // Перевіряємо, чи існує файл
                if (File.Exists(filePath))
                {
                    // Зчитуємо вміст файлу
                    string json = File.ReadAllText(filePath);

                    // Десеріалізація JSON в список об'єктів
                    var records = JsonConvert.DeserializeObject<List<TimerData>>(json);

                    // Отримуємо поточну дату в форматі, що зберігається в JSON
                    string currentDate = DateTime.Now.ToString("dd.MM.yyyy");

                    // Шукаємо запис за сьогоднішньою датою
                    var todayRecord = records.FirstOrDefault(r => r.Date == currentDate);

                    if (todayRecord != null && TimeSpan.TryParseExact(todayRecord.Time, @"hh\:mm\:ss", null, out TimeSpan lastElapsedTime))
                    {
                        return lastElapsedTime;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні часу: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Якщо не вдалося, повертаємо 0
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

        private void DeleteProject(string projectName)
        {
            var projects = JsonProcessing.LoadProjects();
            var projectToRemove = projects.Find(p => p.Name == projectName);

            if (projectToRemove != null)
            {
                projects.Remove(projectToRemove);
                JsonProcessing.SaveProjects(projects);
            }
        }

        private void loadValues()
        {
            // Завантажуємо проєкти через існуючий метод
            var projects = JsonProcessing.LoadProjects();

            // Список ваших Label (вказуєте всі необхідні)
            var projectsNames = new List<Label> { label3, label4, label5 };
            var projectsPath = new List<Label> { label6, label7, label8 };

            // Прив'язка значень Name до Label
            for (int i = 0; i < projectsNames.Count && i < projects.Count; i++)
            {
                projectsNames[i].Text = projects[i].Name;
            }

            // Прив'язка значень Name до Label
            for (int i = 0; i < projectsPath.Count && i < projects.Count; i++)
            {
                projectsPath[i].Text = projects[i].Path;
            }
        }

        private void button7_Click(object sender, EventArgs e) //графік поточного тижня
        {
            // Очищення всіх елементів управління з форми
            this.Controls.Clear();
            InitializeComponentMain();
            StatisticsMainMenu();

            CurrentDay();
        }

        public (DateTime StartOfWeek, DateTime EndOfWeek) GetWeekRangeWithOffset(int offset)
        {
            DateTime today = DateTime.Today.AddDays(offset * 7); // зміщення на тиждень вперед/назад
            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfWeek = today.AddDays(-1 * diff).Date;
            DateTime endOfWeek = startOfWeek.AddDays(6);
            return (startOfWeek, endOfWeek);
        }

        private void CurrentDay()
        {           
            RemoveChart();

            DateTime selectedDate = DateTime.Today.AddDays(currentDayOffset);

            switch (typeOfStatictics)
            {
                case "за проєктами":

                     var timerData = JsonProcessing.LoadData();

                     string dateKey = selectedDate.ToString("dd.MM.yyyy");

                     // 3) Шукаємо записи саме для цієї дати
                     var todayRecord = timerData
                         .FirstOrDefault(d => d.Date == dateKey);

                     // 4) Якщо знайдено — беремо його Sessions, інакше порожній список
                     var sessions = todayRecord?.Sessions
                                    ?? new List<Session>();

                     // 5) Нарешті — будуємо графік
                     buildDailyChart(sessions, selectedDate);

                     // ---- Ось додайте це: в label9 показати todayRecord.Time ----
                     label9.Text = $"Усього витрачено на роботу: {todayRecord.Time}";
                     label10.Text = $"Статистика за день {selectedDate:dd.MM.yyyy}";
                    // MessageBox.Show("LOX");
                    
                    break;

                case "за браузером":

                    var data = statistic.LoadUrlData();

                    // Малюємо графік саме за selectedDate
                    buildDailyChart(data, selectedDate);

                    // Фільтруємо для підрахунку суми
                    var dayData = data
                        .Where(d => d.Timestamp.Date == selectedDate.Date)
                        .ToList();

                    // Підрахунок загального часу
                    string totalTime = statistic.CalculateTotalTime<UrlData>(
                        dayData,
                        nameof(UrlData.TimeSpent)
                    );
                    label9.Text = $"Усього витрачено на роботу: {totalTime}";
                    label10.Text = $"Статистика за день {selectedDate:dd.MM.yyyy}";
                    break;

                case "за браузером та проєктами":
                    // 1) Підготуйте дати й ключ
                    DateTime selDate = DateTime.Today.AddDays(currentDayOffset);
                    string dateKeyy = selDate.ToString("dd.MM.yyyy");

                    // 2) Завантажте дані проєктів і відфільтруйте по даті
                    var allTimerData = JsonProcessing.LoadData();
                    var recProj = allTimerData.FirstOrDefault(d => d.Date == dateKeyy);
                    var sessionss = recProj?.Sessions ?? new List<Session>();

                    // 3) Завантажте URL-дані і відфільтруйте по даті
                    var allUrlData = statistic.LoadUrlData();
                    var dayUrlData = allUrlData
                        .Where(d => d.Timestamp.Date == selDate.Date)
                        .ToList();

                    // 4) Агрегуйте обидві колекції в списки "хвилин по годинах"
                    var hours = Enumerable.Range(0, 24).ToList();

                    var projMinutes = hours.Select(h =>
                    {
                        double sec = 0;
                        foreach (var s in sessionss)
                        {
                            if (!DateTime.TryParseExact(s.Start, "HH:mm:ss", CultureInfo.InvariantCulture,
                                                        DateTimeStyles.None, out var t1)) continue;
                            if (!DateTime.TryParseExact(s.Stop, "HH:mm:ss", CultureInfo.InvariantCulture,
                                                        DateTimeStyles.None, out var t2)) continue;
                            var dt1 = selDate.Date.Add(t1.TimeOfDay);
                            var dt2 = selDate.Date.Add(t2.TimeOfDay);
                            if (dt2 < dt1) dt2 = dt2.AddDays(1);

                            var periodStart = selDate.Date.AddHours(h);
                            var periodEnd = selDate.Date.AddHours(h + 1);
                            var a = dt1 > periodStart ? dt1 : periodStart;
                            var b = dt2 < periodEnd ? dt2 : periodEnd;
                            if (b > a) sec += (b - a).TotalSeconds;
                        }
                        return sec / 60.0;
                    }).ToList();

                    var browserMinutes = hours.Select(h =>
                        dayUrlData.Where(d => d.Timestamp.Hour == h)
                                  .Sum(d => d.TimeSpent)
                        / 60.0
                    ).ToList();

                    // 5) Викликаєте свій метод:
                    BuildCombinedPlot(projMinutes, browserMinutes, selDate);
                    break;

                default:
                    // якщо раптом typeOfStatictics некоректний — покажіть якийсь дефолт
                    break;
            }
        }

        private string HumanizeSeconds(double totalSeconds)
        {
            var ts = TimeSpan.FromSeconds(totalSeconds);
            return $"{ts.Hours} годин {ts.Minutes} хвилин {ts.Seconds} секунд";
        }

        private void CurrentWeek()
        {
            RemoveChart();

            // Отримання діапазону тижня з урахуванням зсуву
            var weekRange = GetWeekRangeWithOffset(currentWeekOffset);
            var startOfWeek = weekRange.StartOfWeek;
            var endOfWeek = weekRange.EndOfWeek;

            // Обробка даних згідно типу статистики
            List<TimerData> processedData;
            switch (typeOfStatictics)
            {
                case "за проєктами":
                    processedData = ProcessProjectData();
                    break;
                case "за браузером":
                    processedData = ProcessBrowserData();
                    break;
                case "за браузером та проєктами":
                    processedData = ProcessCombinedData();
                    break;
                default:
                    processedData = new List<TimerData>();
                    break;
            }

            // Фільтрація і заповнення пропущених днів
            var filteredData = statistic.FilterDataForDateRange(
                processedData, startOfWeek, endOfWeek,
                item => DateTime.ParseExact(item.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture)
            );
            var filledData = statistic.FillMissingDays(filteredData, startOfWeek, endOfWeek);

            // Побудова графіка
            var xPoints = filledData
                .Select(d => DateTime.ParseExact(d.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture))
                .ToList();

            var yPoints = filledData
                .Select(d => (int)TimeSpan.Parse(d.Time).TotalSeconds)
                .ToList();

            buildChart(xPoints, yPoints);

            // Загальний час
            string totalTime = statistic.CalculateTotalTime(filledData, "Time");
            label9.Text = "Усього витрачено на роботу: " + totalTime;
            label10.Text = $"Статистика за тиждень {startOfWeek:dd.MM.yyyy} - {endOfWeek:dd.MM.yyyy}";
        }


        private void CurrentMonth()
        {
            RemoveChart();

            List<TimerData> processedData;

            switch (typeOfStatictics)
            {
                case "за проєктами":
                    processedData = ProcessProjectData();
                    break;
                case "за браузером":
                    processedData = ProcessBrowserData();
                    break;
                case "за браузером та проєктами":
                    processedData = ProcessCombinedData();
                    break;
                default:
                    processedData = new List<TimerData>();
                    break;
            }

            var (startOfMonth, endOfMonth) = GetMonthRangeWithOffset(currentMonthOffset);

            List<TimerData> filledData = statistic.FillMissingDays(processedData, startOfMonth, endOfMonth);

            var xPoints = statistic.GetXPoints(filledData);
            var yPoints = statistic.GetYPoints(filledData);
            buildChart(xPoints, yPoints);

            string totalTime = statistic.CalculateTotalTime(filledData, "Time");
            label9.Text = "Усього витрачено на роботу: " + totalTime;

            switch (typeOfStatictics)
            {
                case "за проєктами":
                case "за браузером":
                    label10.Text = $"Статистика за {startOfMonth:MMMM yyyy}";
                    break;

                case "за браузером та проєктами":
                    label10.Text = $"Статистика за {startOfMonth:MMMM yyyy} (обʼєднана)";
                    break;
            }
        }//графік для попереднього тижня

        private List<TimerData> ProcessProjectData()
        {
            var freshData = statistic.LoadTimerData(); // Завантаження JSON-даних проєктів
            return statistic.FilterDataForLastMonth(freshData,
                item => DateTime.ParseExact(item.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture)); // Фільтрація по даті
        }

        private List<TimerData> ProcessBrowserData()
        {
            var freshData = statistic.LoadUrlData(); // Завантаження JSON-даних з браузера
            var result = statistic.FilterDataForLastMonth(freshData, item => item.Timestamp.Date); // Фільтрація

            return result
                .GroupBy(item => item.Timestamp.Date) // Групування по днях
                .Select(g => new TimerData
                {
                    Date = g.Key.ToString("dd.MM.yyyy"), // Форматування дати
            Time = TimeSpan.FromSeconds(g.Sum(item => item.TimeSpent)).ToString() // Підрахунок часу
        })
                .ToList();
        }

        private List<TimerData> ProcessCombinedData()
        {
            var urlData = statistic.LoadUrlData(); // Дані з браузера
            var timerData = statistic.LoadTimerData(); // Дані з трекера

            var (start, end) = statistic.GetLastMonthRange(); // Діапазон

            var filteredUrl = statistic.FilterDataForDateRange(urlData, start, end, item => item.Timestamp.Date);
            var filteredTimer = statistic.FilterDataForDateRange(timerData, start, end,
                item => DateTime.ParseExact(item.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture));

            return filteredUrl
                .Select(d => new { Date = d.Timestamp.Date, Seconds = d.TimeSpent }) // Перетворення у спільний формат
                .Concat(filteredTimer.Select(d => new
                {
                    Date = DateTime.ParseExact(d.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    Seconds = (int)TimeSpan.Parse(d.Time).TotalSeconds
                }))
                .GroupBy(x => x.Date) // Групування по днях
                .Select(g => new TimerData
                {
                    Date = g.Key.ToString("dd.MM.yyyy"),
                    Time = TimeSpan.FromSeconds(g.Sum(x => x.Seconds)).ToString()
                })
                .ToList();
        }

        private (DateTime start, DateTime end) GetMonthRangeWithOffset(int offset)
        {
            DateTime today = DateTime.Today;
            DateTime targetMonth = new DateTime(today.Year, today.Month, 1).AddMonths(offset);
            DateTime start = targetMonth;
            DateTime end = targetMonth.AddMonths(1).AddDays(-1);
            return (start, end);
        }

        private void button11_Click(object sender, EventArgs e)
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

        private void button12_Click(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
        {
            string projectName = label5.Text;
            var result = MessageBox.Show($"Ви дійсно хочете видалити проєкт \"{projectName}\"?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeleteProject(projectName);
                loadValues();
                LabelsToShow();
                ButtonsToShow();
            }
        } //видалити проект

        private void button5_Click(object sender, EventArgs e)
        {
            string projectName = label4.Text;
            var result = MessageBox.Show($"Ви дійсно хочете видалити проєкт \"{projectName}\"?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeleteProject(projectName);
                loadValues();
                LabelsToShow();
                ButtonsToShow();

            }
        } //видалити проект

        private void button4_Click(object sender, EventArgs e)
        {
            string projectName = label3.Text;
            var result = MessageBox.Show($"Ви дійсно хочете видалити проєкт \"{projectName}\"?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeleteProject(projectName);
                loadValues();
                LabelsToShow();
                ButtonsToShow();
            }
        } //видалити проект

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
                        LabelsToShow();
                        loadValues();
                        ButtonsToShow();
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
            this.button3.Visible = false;
            GetTimeAmount();
            loadValues();
            LabelsToShow();
            ButtonsToShow();

           // MessageBox.Show(Convert.ToString(isPersonChoose));
        }

        private void button3_Click(object sender, EventArgs e)//кнопка вверх
        {
            // Завантажуємо проєкти
            var projects = JsonProcessing.LoadProjects();
            this.button10.Visible = true;

            string firstProjectName = GetFirstProjectName();
            //MessageBox.Show($"Перше значення Name: {firstProjectName}");

            // Поточні значення Label
            string currentLabel3 = label3.Text;
            string currentLabel4 = label4.Text;
            string currentLabel5 = label5.Text;

            // Знаходимо індекс для label3 у списку проектів
            int currentIndexForLabel3 = projects.FindIndex(p => p.Name == currentLabel3);

            // Отримуємо попереднє значення для label3
            string previousLabel3 = currentIndexForLabel3 > 0
                ? projects[currentIndexForLabel3 - 1].Name // Попереднє значення
                : projects.LastOrDefault()?.Name ?? "Немає даних"; // Повертаємося на останній елемент, якщо це перший елемент

            // Оновлюємо значення Label
            label3.Text = previousLabel3;  // label3 отримує попереднє значення
            label4.Text = currentLabel3;   // label4 отримує значення label3
            label5.Text = currentLabel4;   // label5 отримує значення label4

            // Поточні значення Label
            string currentLabel6 = label6.Text;
            string currentLabel7 = label7.Text;
            string currentLabel8 = label8.Text;

            // Знаходимо індекс для label3 у списку проектів
            int currentIndexForLabel6 = projects.FindIndex(p => p.Path == currentLabel6);

            // Отримуємо попереднє значення для label3
            string previousLabel6 = currentIndexForLabel6 > 0
                ? projects[currentIndexForLabel6 - 1].Path // Попереднє значення
                : projects.LastOrDefault()?.Path ?? "Немає даних"; // Повертаємося на останній елемент, якщо це перший елемент

            // Оновлюємо значення Label
            label6.Text = previousLabel6;  // label3 отримує попереднє значення
            label7.Text = currentLabel6;   // label4 отримує значення label3
            label8.Text = currentLabel7;   // label5 отримує значення label4

            // Якщо label5 набуває значення останнього елемента
            if (label3.Text == firstProjectName)
            {
                this.button3.Visible = false; // Приховуємо кнопку
                this.button10.Visible = true;
            }

            /* var labelsToCheck = new List<Label> { label3, label4, label5, label6, label7 };

             // Перевірка кожного Label на кількість символів
             foreach (var label in labelsToCheck)
             {
                 if (label.Text.Length > 41)
                 {
                     label.Text = label.Text.Substring(0, 41);
                 }
             }*/

            LabelsToShow();
            // ButtonsToShow();
        }

        private void button10_Click(object sender, EventArgs e) //кнопка вниз
        {
            // Завантажуємо проєкти
            var projects = JsonProcessing.LoadProjects();
            string lastProjectName = GetLastProjectName();
            button3.Visible = true;
            //MessageBox.Show(lastProjectName);

            // Поточні значення Label
            string currentLabel3 = label3.Text;
            string currentLabel4 = label4.Text;
            string currentLabel5 = label5.Text;

            // Знаходимо індекс для label5 у списку проектів
            int currentIndex = projects.FindIndex(p => p.Name == currentLabel5);

            // Отримуємо наступне значення для label5
            string nextLabel5 = currentIndex != -1 && currentIndex + 1 < projects.Count
                ? projects[currentIndex + 1].Name // Наступне значення
                : projects.FirstOrDefault()?.Name ?? "Немає даних"; // Повертаємося на початок, якщо це останній елемент

            // Оновлюємо значення Label
            label3.Text = currentLabel4; // label3 отримує значення label4
            label4.Text = currentLabel5; // label4 отримує значення label5
            label5.Text = nextLabel5;    // label5 отримує наступне значення

            // Якщо label5 набуває значення останнього елемента
            if (label5.Text == lastProjectName)
            {
                this.button10.Visible = false; // Приховуємо кнопку
                this.button3.Visible = true;

            }

            // Поточні значення Label
            string currentLabel6 = label6.Text;
            string currentLabel7 = label7.Text;
            string currentLabel8 = label8.Text;

            // Знаходимо індекс для label3 у списку проектів
            int currentIndex2 = projects.FindIndex(p => p.Path == currentLabel8);

            // Отримуємо попереднє значення для label3
            string nextLabel8 = currentIndex2 != -1 && currentIndex2 + 1 < projects.Count
                ? projects[currentIndex2 + 1].Path // Попереднє значення
                : projects.FirstOrDefault()?.Path ?? "Немає даних"; // Повертаємося на останній елемент, якщо це перший елемент

            // Оновлюємо значення Label
            label6.Text = currentLabel7;  // label3 отримує попереднє значення
            label7.Text = currentLabel8;   // label4 отримує значення label3
            label8.Text = nextLabel8;   // label5 отримує значення label4
                                        //MessageBox.Show($"Привіт");

            /* var labelsToCheck = new List<Label> { label3, label4, label5, label6, label7 };

             // Перевірка кожного Label на кількість символів
             foreach (var label in labelsToCheck)
             {
                 if (label.Text.Length > 41)
                 {
                     label.Text = label.Text.Substring(0, 41);
                 }
             }*/

            LabelsToShow();
            // ButtonsToShow();
        }

        private string GetLastProjectName()
        {
            // Завантажуємо проєкти з JSON
            var projects = JsonProcessing.LoadProjects();

            // Перевіряємо, чи список не порожній
            if (projects.Count > 0)
            {
                // Повертаємо Name останнього елемента
                return projects.Last().Name;
            }

            // Якщо список порожній, повертаємо повідомлення або порожній рядок
            return "Немає даних";
        }

        private string GetFirstProjectName()
        {
            // Завантажуємо проєкти з JSON
            var projects = JsonProcessing.LoadProjects();

            // Перевіряємо, чи список не порожній
            if (projects.Count > 0)
            {
                // Повертаємо Name останнього елемента
                return projects.First().Name;
            }

            // Якщо список порожній, повертаємо повідомлення або порожній рядок
            return "Немає даних";


        }

        public static int GetNonActiveTime()
        {
            return nonActiveTime;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();
            AnnualStatistics();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();

            BuildMonthlyChart(2024, 10);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();

            BuildMonthlyChart(2024, 11);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();

            BuildMonthlyChart(2024, 12);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();

            BuildMonthlyChart(2024, 7);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();

            BuildMonthlyChart(2024, 8);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();

            BuildMonthlyChart(2024, 9);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();

            BuildMonthlyChart(2024, 4);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();

            BuildMonthlyChart(2024, 5);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();

            BuildMonthlyChart(2024, 6);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();

            BuildMonthlyChart(2024, 1);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();

            BuildMonthlyChart(2024, 2);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();

            BuildMonthlyChart(2024, 3);
            ExitButton();
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
            string filePath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserActivity\browserUrls.json";
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
            button38.Visible = currentOffset > 0;
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
        private void button40_Click(object sender, EventArgs e)
        {
            // Очищення та ініціалізація
            this.Controls.Clear();
            InitializeComponentMain();
            StatisticsMainMenu();

            if (isBrowserStats)
            {
                typeOfStatictics = "за браузером";

                string filePath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\BrowserActivity\browserUrls.json";
                var data = statistic.LoadBrowserDataForCurrentWeek(filePath);

                var timerDataList = data
                    .GroupBy(d => d.Timestamp.Date)
                    .Select(g => new TimerData
                    {
                        Date = g.Key.ToString("dd.MM.yyyy"),
                        Time = TimeSpan.FromSeconds(g.Sum(d => d.TimeSpent)).ToString()
                    })
                    .ToList();

                var (startOfWeek, endOfWeek) = statistic.GetCurrentWeekRange();

                var filteredData = statistic.FilterDataForDateRange(
                    timerDataList, startOfWeek, endOfWeek,
                    item => DateTime.ParseExact(item.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture)
                );

                var filledData = statistic.FillMissingDays(filteredData, startOfWeek, endOfWeek);

                var xPoints = filledData
                    .Select(d => DateTime.ParseExact(d.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture))
                    .ToList();

                var yPoints = filledData
                    .Select(d => (int)TimeSpan.Parse(d.Time).TotalSeconds)
                    .ToList();

                buildChart(xPoints, yPoints);

                string totalTime = statistic.CalculateTotalTime(data, "TimeSpent");
                label9.Text = "Усього витрачено на роботу: " + totalTime;
                label10.Text = "Статистика за поточний тиждень";
            }
            else
            {
                typeOfStatictics = "за проєктами";

                var freshData = statistic.LoadTimerData();

                var filteredData = statistic.FilterDataForCurrentWeek(
                    freshData,
                    item => DateTime.ParseExact(item.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture)
                );

                var (startOfWeek, endOfWeek) = statistic.GetCurrentWeekRange();
                var filledData = statistic.FillMissingDays(filteredData, startOfWeek, endOfWeek);

                var xPoints = filledData
                    .Select(item => DateTime.ParseExact(item.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture))
                    .ToList();

                var yPoints = filledData
                    .Select(item => (int)TimeSpan.Parse(item.Time).TotalSeconds)
                    .ToList();

                buildChart(xPoints, yPoints);

                string totalTime = statistic.CalculateTotalTime(filledData, "Time");
                label9.Text = "Усього витрачено на роботу: " + totalTime;
                label10.Text = "Статистика за останній тиждень";
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();
            StatisticsMainMenu();

            typeOfStatictics = "за браузером та проєктами";

            // 1. Завантаження даних
            var urlData = statistic.LoadUrlData();
            var timerData = statistic.LoadTimerData();

            // 2. Діапазон поточного тижня
            var (startOfWeek, endOfWeek) = statistic.GetCurrentWeekRange();
            Console.WriteLine($"Період: {startOfWeek:dd.MM.yyyy} — {endOfWeek:dd.MM.yyyy}");

            // 3. Фільтрація
            var filteredUrlData = statistic.FilterDataForDateRange(
                urlData,
                startOfWeek,
                endOfWeek,
                item => item.Timestamp.Date
            );

            var filteredTimerData = statistic.FilterDataForDateRange(
                timerData,
                startOfWeek,
                endOfWeek,
                item => DateTime.ParseExact(item.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture)
            );

            // 4. Обʼєднання по датах
            var combined = filteredUrlData
                .Select(d => new
                {
                    Date = d.Timestamp.Date,
                    Seconds = d.TimeSpent
                })
                .Concat(filteredTimerData.Select(d => new
                {
                    Date = DateTime.ParseExact(d.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    Seconds = (int)TimeSpan.Parse(d.Time).TotalSeconds
                }))
                .GroupBy(x => x.Date)
                .Select(g => new TimerData
                {
                    Date = g.Key.ToString("dd.MM.yyyy"),
                    Time = TimeSpan.FromSeconds(g.Sum(x => x.Seconds)).ToString()
                })
                .ToList();

            // 5. Заповнення пропущених днів поточного тижня
            var filledData = statistic.FillMissingDays(combined, startOfWeek, endOfWeek);

            // 6. Побудова графіка
            var xPoints = statistic.GetXPoints(filledData);
            var yPoints = statistic.GetYPoints(filledData);
            buildChart(xPoints, yPoints);

            // 7. Вивід статистики
            string totalTime = statistic.CalculateTotalTime(filledData, "Time");
            label9.Text = "Усього витрачено на роботу:  " + totalTime;
            label10.Text = "Статистика за поточний тиждень (обʼєднана)";

        }


        public void BuildMonthlyChart(int year, int month)
        {
            var freshData = statistic.LoadTimerData(); // Завантажуємо актуальні дані з JSON
            // Фільтрація даних за конкретний місяць
            var filteredData = statistic.GetDataForSpecificMonth(freshData, year, month);

            // Визначення початку і кінця місяця
            var startOfMonth = new DateTime(year, month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            // Заповнення пропущених днів
            var filledData = statistic.FillMissingDays(filteredData, startOfMonth, endOfMonth);

            // Отримання точок X і Y для графіка
            var xPoints = statistic.GetXPoints(filledData);
            var yPoints = statistic.GetYPoints(filledData);

            // Побудова графіка
            buildChart(xPoints, yPoints);

            // Обчислення загального часу
            string totalTime = statistic.CalculateTotalTime(filteredData, "Time");

            // Виведення тексту в Label
            label9.Text = $"Усього витрачено на роботу: {totalTime}";
            ExitButton();
        }

        private void button26_Click(object sender, EventArgs e) //статистика на панелі
        {
            if (isPersonChoose == true)
            {
                /*checkBox5.CheckedChanged -= ValidateTextBox;
                checkBox6.CheckedChanged -= ValidateTextBox;
                checkBox7.CheckedChanged -= ValidateTextBox;
                textBox1.Leave -= ValidateTextBox;*/
            }

            this.Controls.Clear();
            InitializeComponentMain();
            AnnualStatistics();
        }

        private void button27_Click(object sender, EventArgs e) //налаштування на панелі
        {
            this.Controls.Clear();
            InitializeComponentMain();
            SettingsMenu();

           /* if (isPersonChoose == true)
            {
                CheckBox5Active = false;
                checkBox5.Checked = false;

                // MessageBox.Show("DA");
            }
            else if (isPersonChoose == false)
            {
                CheckBox5Active = true;
                checkBox5.Checked = true;
            }*/

          /*  if (textBoxText != null)
            {
                nonActiveTime = Convert.ToInt32(textBoxText);
                CheckBox5Active = false;
                checkBox5.Checked = false;
                CheckBox6Active = false;
                checkBox6.Checked = false;
                CheckBox7Active = false;
                checkBox7.Checked = false;
            }*/

           /* checkBox5.CheckedChanged += ValidateTextBox;
            checkBox6.CheckedChanged += ValidateTextBox;
            checkBox7.CheckedChanged += ValidateTextBox;
            textBox1.Leave += ValidateTextBox;*/

            // MessageBox.Show(isPersonChoose.ToString());

            // textBox1.Text = Convert.ToString(nonActiveTime);

            textBox1.KeyPress += textBox1_KeyPress;
            textBox1.TextChanged += textBox1_TextChanged;

            //MessageBox.Show(Convert.ToString(nonActiveTime));
        }

        private void button28_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();
            AboutProgram();
        }

        public static void NotificationsOn()
        {
            if (notificationsOnOff)
            {
                var notification = new Notifications();
            }
            //notification.ShowNotification(); // Викликає метод

            //  MessageBox.Show("Fllf");
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

        private void LabelsToShow()
        {
            // var projects = JsonProcessing.LoadProjects();

            if (JsonProcessing.LoadProjects().Count == 0)
            {
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;
                pictureBox8.Visible = false;

                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;

                button6.Visible = false;
                button10.Visible = false;
                button3.Visible = false;
                button5.Visible = false;
                button4.Visible = false;
            }
            else if (JsonProcessing.LoadProjects().Count == 1)
            {
                pictureBox3.Visible = true;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = true;
                pictureBox7.Visible = false;
                pictureBox8.Visible = false;

                label3.Visible = true;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = true;
                label7.Visible = false;
                label8.Visible = false;

                button6.Visible = false;
                button10.Visible = false;
                button3.Visible = false;
                button5.Visible = false;
                button4.Visible = true;
                button5.Visible = false;

            }
            else if (JsonProcessing.LoadProjects().Count == 2)
            {
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;
                pictureBox5.Visible = false;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = false;

                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = false;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = false;

                button6.Visible = false;
                button10.Visible = false;
                button3.Visible = false;
                button4.Visible = true;
                button5.Visible = true;
            }
            else if (JsonProcessing.LoadProjects().Count >= 3)
            {
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;

                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;

                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
            }
        }

        private void ButtonsToShow()
        {
            string firstProjectName = GetFirstProjectName();
            string lastProjectName = GetLastProjectName();

            if (label3.Text == firstProjectName)
            {
                this.button3.Visible = false;
            }

            if (label5.Text == lastProjectName)
            {
                this.button10.Visible = false;
            }

            if (label5.Text != lastProjectName && JsonProcessing.LoadProjects().Count >= 3)
            {
                this.button10.Visible = true;
            }

            if (label3.Text != firstProjectName && JsonProcessing.LoadProjects().Count >= 3)
            {
                this.button3.Visible = true;
            }
        }

         private void textBox1_TextChanged(object sender, EventArgs e)
         {
             if (!(string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Введіть лише число за допомогою цифр") || Form1.settings.TextBoxValue != 0)
             {
                //textBox1.Text = Convert.ToString(Form1.settings.TextBoxValue);

                textBoxText = textBox1.Text;

                CheckBox5Active = false;
                checkBox5.Checked = false;
                CheckBox6Active = false;
                checkBox6.Checked = false;
                CheckBox7Active = false;
                checkBox7.Checked = false;

                if (int.TryParse(textBoxText, out int result))
                {
                    nonActiveTime = result;
                    UpdateTextBoxValue(this, nonActiveTime);
                }
                // MessageBox.Show(Convert.ToString(nonActiveTime));
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Введіть лише число за допомогою цифр" || Form1.settings.TextBoxValue == 0)
           // if (string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Введіть лише число за допомогою цифр")
            {
                textBoxText = "Введіть лише число за допомогою цифр";
                TextBoxValue = 0;

                UpdateTextBoxValue(this, TextBoxValue);

                CheckBox5Active = true;
                checkBox5.Checked = true;
                nonActiveTime = 5;
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
            // Оновлюємо властивість в об'єкті settings
            Form1.settings.InactivityAmount = newInactivityAmount;

            // Тепер зберігаємо оновлені налаштування у файл
            JsonProcessing.SaveSettings(form1); 
        }

        public static void UpdateColorTheme(Form1 form1, string newColorTheme)
        {
            // Оновлюємо властивість в об'єкті settings
            Form1.settings.ColorTheme = newColorTheme;

            // Тепер зберігаємо оновлені налаштування у файл
            JsonProcessing.SaveSettings(form1);
        }

        public static void UpdateAutostart(Form1 form1, bool newAutostart)
        {
            // Оновлюємо властивість в об'єкті settings
            Form1.settings.Autostart = newAutostart;

            // Тепер зберігаємо оновлені налаштування у файл
            JsonProcessing.SaveSettings(form1);
        }

        public static void UpdateNotificaton(Form1 form1, bool newNotificatonOnOff)
        {
            // Оновлюємо властивість в об'єкті settings
            Form1.settings.NotificatonOnOff = newNotificatonOnOff;

            // Тепер зберігаємо оновлені налаштування у файл
            JsonProcessing.SaveSettings(form1);
            MessageBox.Show("UpdateNotificaton викликано!");
        }

        public static void UpdateTextBoxValue(Form1 form1, int newTextBoxValue)
        {
            // Оновлюємо властивість в об'єкті settings
            Form1.settings.TextBoxValue = newTextBoxValue;

            // Тепер зберігаємо оновлені налаштування у файл
            JsonProcessing.SaveSettings(form1);
        }
    }
}

