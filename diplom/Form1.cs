using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
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

        public bool CheckBox1Active;
        public bool CheckBox2Active;
        public bool CheckBox3Active;
        public bool CheckBox4Active;
        public bool CheckBox5Active;
        public bool CheckBox6Active;
        public bool CheckBox7Active;


        public Form1()
        {
            InitializeComponentMainMenu();
            this.button3.Visible = false;
            this.MaximumSize = this.Size;  // Фіксувати максимальний розмір форми
            this.MinimumSize = this.Size;  // Фіксувати мінімальний розмір форми

            // Завантажуємо час з файлу при запуску програми
            TimeSpan lastElapsedTime = LoadLastElapsedTimeFromFile();
            label2.Text = lastElapsedTime.ToString(@"hh\:mm\:ss");

            Instance = this;

            GetLabel1Text = () => label2.Text;
            Program.GetLabel1TextDelegate = this.GetLabel1Text;

            // Ініціалізація HandButton
            handButton = new HandButton();
            handButton.OnTimeUpdated += HandButton_OnTimeUpdated;


           // var notification = new Notifications();
            //notification.ShowNotification(); // Викликає метод

            loadValues();

            CheckBox1Active = true;
            CheckBox2Active = false;
            CheckBox3Active = false;
            CheckBox4Active = false;
            CheckBox5Active = false;
            CheckBox6Active = false;
            CheckBox7Active = false;

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

            // Фільтрація даних для поточного тижня
            var filteredData = statistic.FilterDataForCurrentWeek(statistic.timerData);

            // Заповнення пропущених днів (якщо потрібно)
            var (startOfWeek, endOfWeek) = statistic.GetCurrentWeekRange();
            var filledData = statistic.FillMissingDays(filteredData, startOfWeek, endOfWeek);

            // Отримання точок X і Y
            var xPoints = statistic.GetXPoints(filledData);
            var yPoints = statistic.GetYPoints(filledData);

            // Побудова графіка
            buildChart(xPoints, yPoints);

            // Обчислення загального часу
            string totalTime = statistic.CalculateTotalTime(filledData);

            // Виведення у Label9
            label9.Text = "Усього витрачено на роботу:  " + totalTime;
            label10.Text = "Статистика за останній тиждень";
        }

        /* public void fuck()
         {
             // Фільтрація даних для поточного тижня
             var filteredData = statistic.FilterDataForCurrentWeek(statistic.timerData);

             // Заповнення пропущених днів (якщо потрібно)
             var (startOfWeek, endOfWeek) = statistic.GetCurrentWeekRange();
             var filledData = statistic.FillMissingDays(filteredData, startOfWeek, endOfWeek);

             // Отримання точок X і Y
             var xPoints = statistic.GetXPoints(filledData);
             var yPoints = statistic.GetYPoints(filledData);

             // Побудова графіка
             buildChart(xPoints, yPoints);

             // Обчислення загального часу
             string totalTime = statistic.CalculateTotalTime(filledData);

             // Виведення у Label9
             label9.Text = "Усього витрачено на роботу:  " + totalTime;
             label10.Text = "Статистика за останній тиждень";
         }*/

        private void button11_Click(object sender, EventArgs e)
        {
            // Зберігаємо поточний стан перед очищенням
            bool currentState = isPreviousWeek;

            this.Controls.Clear();
            InitializeComponentMain();
            StatisticsMainMenu();

            // Фільтрація даних залежно від стану
            var filteredData = currentState
                ? statistic.FilterDataForPreviousWeek(statistic.timerData)
                : statistic.FilterDataForCurrentWeek(statistic.timerData);

            var (startOfWeek, endOfWeek) = currentState
                ? statistic.GetPreviousWeekRange()
                : statistic.GetCurrentWeekRange();

            var filledData = statistic.FillMissingDays(filteredData, startOfWeek, endOfWeek);

            // Побудова графіка
            var xPoints = statistic.GetXPoints(filledData);
            var yPoints = statistic.GetYPoints(filledData);
            buildChart(xPoints, yPoints);

            // Обчислення загального часу
            string totalTime = statistic.CalculateTotalTime(filledData);

            // Виведення в Label
            label9.Text = "Усього витрачено на роботу: " + totalTime;
            label10.Text = currentState
                ? "Статистика за попередній тиждень"
                : "Статистика за останній тиждень";

            // Оновлення тексту кнопки
            button11.Text = currentState
                ? "Статистика за останній тиждень"
                : "Статистика за попередній тиждень";

            // Змінюємо стан для наступного натискання
            isPreviousWeek = !currentState;
        } //кнопка попередній тиждень останній тиждень

        private void button12_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();
            StatisticsMainMenu();

            // Фільтрація даних для попереднього тижня
            var filteredData = statistic.FilterDataForLastMonth(statistic.timerData);

            // Заповнення пропущених днів (якщо потрібно)
            var (startOfLastMonth, endOfLastMonth) = statistic.GetLastMonthRange();
            var filledData = statistic.FillMissingDays(filteredData, startOfLastMonth, endOfLastMonth);

            // Отримання точок X і Y
            var xPoints = statistic.GetXPoints(filledData);
            var yPoints = statistic.GetYPoints(filledData);

            // Побудова графіка
            buildChart(xPoints, yPoints);

            string totalTime = statistic.CalculateTotalTime(filledData);

            // Виведення у Label9
            label9.Text = "Усього витрачено на роботу:  " + totalTime;
            label10.Text = "Статистика за останній місяць";

        } //графік для попереднього тижня

        private void button6_Click(object sender, EventArgs e)
        {
            string projectName = label5.Text;
            var result = MessageBox.Show($"Ви дійсно хочете видалити проєкт \"{projectName}\"?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeleteProject(projectName);
                loadValues();
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
            loadValues();
        }

        private void button3_Click(object sender, EventArgs e)
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

        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Завантажуємо проєкти
            var projects = JsonProcessing.LoadProjects();
            string lastProjectName = GetLastProjectName();
            button3.Visible = true;

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

        public void BuildMonthlyChart(int year, int month)
        {
            // Фільтрація даних за конкретний місяць
            var filteredData = statistic.GetDataForSpecificMonth(statistic.timerData, year, month);

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
            string totalTime = statistic.CalculateTotalTime(filledData);

            // Виведення тексту в Label
            label9.Text = $"Усього витрачено на роботу: {totalTime}";
            ExitButton();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();
            AnnualStatistics();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMain();
            SettingsMenu();
        }

        private void NotificationsOn()
        {
             var notification = new Notifications();
             //notification.ShowNotification(); // Викликає метод

          //  MessageBox.Show("Fllf");
        }
    }   
}

