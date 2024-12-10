using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace diplom
{
    public partial class Form1 : Form
    {
        private HandButton handButton;
        private System.Threading.Timer _timer; // Таймер перевірки стану проекту
       // private string _projectPath = @"E:\Drafts\Draft\Draft.sln"; // Шлях до проекту

        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadProjectsToDataGridView();

            // Ініціалізація таймера для перевірки проекту
           // _timer = new System.Threading.Timer(CheckIfProjectsOpen, null, 0, 5000); // Перевірка кожні 5 секунд

            // Ініціалізація HandButton
            handButton = new HandButton();
            handButton.OnTimeUpdated += HandButton_OnTimeUpdated;

            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        }

        // Метод для перевірки, чи відкритий проект
       /* private void CheckIfProjectsOpen(object state)
        {
            var projects = JsonProcessing.LoadProjects(); // Завантажуємо проекти з JSON

            foreach (var project in projects)
            {
                bool isOpen = IsProjectOpen(project.Path); // Перевіряємо шлях

                if (!isOpen)
                {
                    MessageBox.Show("Я сліпа мразь блаблабла");
                    // Додайте дію, якщо проект закритий

                }
                else
                {
                    MessageBox.Show("Боже я поняла зачем треба гітхаб я боше так не буду");
                }
            }
        }*/




        // Перевірка, чи відкритий проект
      /*  public static bool IsProjectOpen(string projectPath)
        {
            string projectName = Path.GetFileNameWithoutExtension(projectPath);

            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName.Contains("devenv") && process.MainWindowTitle.Contains(projectName))
                {
                    return true;
                }
            }

            return false;
        }
      */


        // Метод для обробки події та оновлення label1
        private void HandButton_OnTimeUpdated(TimeSpan elapsed)
        {
            string currentTime = elapsed.ToString(@"hh\:mm\:ss");
            if (label1.InvokeRequired)
            {
                label1.Invoke(new Action(() => label1.Text = currentTime));
            }
            else
            {
                label1.Text = currentTime;
            }

            // Зберігаємо поточний час в JSON файл
            JsonProcessing.SaveCurrentDayTime(elapsed);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Вмикаємо або призупиняємо таймер
            if (!handButton.IsRunning)
            {
                handButton.Start();
                MessageBox.Show("Я існую братан, це ти лох просто");
                button2.Text = "Зупинити таймер"; // Змінюємо текст кнопки
            }
            else
            {
                handButton.Pause();
                button2.Text = "Запустити таймер"; // Змінюємо текст кнопки
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

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.HeaderText = "Delete";
            deleteButtonColumn.Text = "Видалити";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
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
           /* string filePath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\projects.json"; // Задайте шлях до вашого файлу
            bool isOpen = JsonProcessing.IsProjectOpen(filePath);

            if (isOpen)
            {
                MessageBox.Show("Проект відкритий в системі.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Проект не відкритий в системі.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }*/
        }
    }
}
