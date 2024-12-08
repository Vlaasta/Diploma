using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace diplom
{
    public partial class Form1 : Form
    {
        private HandButton handButton;
        private DesignForm design;

        public Form1()
        {
            InitializeComponent();

            // Ініціалізуємо клас HandButton
            handButton = new HandButton();

            // Підписуємося на подію OnTimeUpdated
            handButton.OnTimeUpdated += HandButton_OnTimeUpdated;

            // Створюємо екземпляр класу Design
            design = new DesignForm();

            // Викликаємо метод для зробити кнопку овальною
           // design.MakeButtonOval(button1); // Тут button1 - це кнопка на вашій формі
            design.Design();

            /*Button roundButton = new Button
            {
                Text = "Round Button",
                Size = new System.Drawing.Size(150, 50),
                Location = new System.Drawing.Point(50, 50),
                BackColor = Color.MediumSlateBlue,
                ForeColor = Color.White,
                Radius = 20
            };

            // Додаємо її на форму
            this.Controls.Add(roundButton);*/
        }

        // Метод для обробки події та оновлення label1
        private void HandButton_OnTimeUpdated(TimeSpan elapsed)
        {
            // Оновлюємо label1 на основі часу таймера
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
                // Фільтр тепер дозволяє всі файли
                openFileDialog.Filter = "All Files (*.*)|*.*";
                openFileDialog.Title = "Виберіть проект для додавання";

                // Дозволяємо вибір лише існуючих файлів
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;

                    try
                    {
                        // Додаємо файл до JSON
                        JsonProcessing.AddProject(selectedFilePath);

                        // Повідомлення про успішне додавання
                        MessageBox.Show($"Файл успішно додано: {selectedFilePath}", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Обробка можливих помилок
                        MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }     
        }
    }
}

