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
            InitializeComponent();
            this.MaximumSize = this.Size;  // Фіксувати максимальний розмір форми
            this.MinimumSize = this.Size;  // Фіксувати мінімальний розмір форми

            // Завантажуємо час з файлу при запуску програми
            TimeSpan lastElapsedTime = LoadLastElapsedTimeFromFile();
            label1.Text = lastElapsedTime.ToString(@"hh\:mm\:ss");

            InitializeDataGridView();
            LoadProjectsToDataGridView();
            Instance = this;

            GetLabel1Text = () => label1.Text;
            Program.GetLabel1TextDelegate = this.GetLabel1Text;

            // Ініціалізація HandButton
            handButton = new HandButton();
            handButton.OnTimeUpdated += HandButton_OnTimeUpdated;

            dataGridView1.CellContentClick += dataGridView1_CellContentClick;

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
                    if (label1.InvokeRequired)
                    {
                        label1.Invoke(new Action(() => label1.Text = currentTime));
                    }
                    else
                    {
                        label1.Text = currentTime;
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (!handButton.IsRunning)
            {
                if (TimeSpan.TryParseExact(label1.Text, @"hh\:mm\:ss", null, out TimeSpan lastElapsed))
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
                        LoadProjectsToDataGridView();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void InitializeDataGridView()
        {
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Path", "Path");
            dataGridView1.Columns.Add("Type", "Type");

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "Delete",
                Text = "Видалити",
                UseColumnTextForButtonValue = true
            };

            dataGridView1.Columns.Add(deleteButtonColumn);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
        }

        private void LoadProjectsToDataGridView()
        {
            var projects = JsonProcessing.LoadProjects();

            dataGridView1.Rows.Clear();

            foreach (var project in projects)
            {
                dataGridView1.Rows.Add(project.Name, project.Path, project.Type);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                string projectName = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                var result = MessageBox.Show($"Ви дійсно хочете видалити проєкт \"{projectName}\"?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteProject(projectName);
                    LoadProjectsToDataGridView();
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

        private void button3_Click(object sender, EventArgs e)
        {
            Form Form2 = new Form2();
            Form2.ShowDialog();
        }
    }
}

