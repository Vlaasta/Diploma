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

        public Form1()
        {
            InitializeComponentMainMenu();
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

            var notification = new Notifications();
            notification.ShowNotification(); // Викликає метод

            loadValues();
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
            var projectsNames = new List<Label> { label3, label4, label5};
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


        private void button7_Click(object sender, EventArgs e)
        {
            // Очищення всіх елементів управління з форми
            this.Controls.Clear();
            InitializeComponentMain();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string projectName = label5.Text;
            var result = MessageBox.Show($"Ви дійсно хочете видалити проєкт \"{projectName}\"?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeleteProject(projectName);
                loadValues();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string projectName = label4.Text;
            var result = MessageBox.Show($"Ви дійсно хочете видалити проєкт \"{projectName}\"?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeleteProject(projectName);
                loadValues();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string projectName = label3.Text;
            var result = MessageBox.Show($"Ви дійсно хочете видалити проєкт \"{projectName}\"?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DeleteProject(projectName);
                loadValues();
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
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

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
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponentMainMenu();
        }

       /* private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(848, 708);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }*/
    }
}

