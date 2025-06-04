using System.Drawing; // Простір імен для Color
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot.Axes;
using System.Linq;
using OxyPlot.Annotations;
using System.Globalization;
using System.Drawing.Text;

namespace diplom
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>

        private readonly ToolTip _customToolTip = new ToolTip() { ShowAlways = true };

        private bool _showDateInTooltip = false;
        private HashSet<int> selectedRows = new HashSet<int>();

        private List<UrlData> allUrls;
        private List<DateTime> availableDates;
        private int currentIndex;

        private const int rowH = 37;
        private const int spacing = 6;
        private readonly ToolTip _labelToolTip = new ToolTip { ShowAlways = true };

        private double _currentScaleFactor = 1.0;
        private string _currentUnitLabel = "хв.";

        private void InitializeComponentMain()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button27 = new System.Windows.Forms.Button();
            this.button28 = new System.Windows.Forms.Button();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();

            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();

            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            this.BackColor = Color.FromArgb(2, 14, 25);

            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.pictureBox10);

            this.panel1.BackColor = Color.FromArgb(4, 26, 44);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 733);

            this.panel3.Controls.Add(this.button28);
            this.panel3.Location = new System.Drawing.Point(0, 251);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(225, 46);
            this.panel3.TabIndex = 23;

            this.pictureBox10.Image = isDarkTheme ? Properties.Resources.Logos4 : Properties.Resources.LightLogo;
            this.ConfigurePictureBox(pictureBox10, new Point(23, 15), new Size(170, 100));
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox10.TabStop = false;
            this.pictureBox10.Visible = true;

            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.ForeColor = isDarkTheme ? Color.FromArgb(186, 192, 196) : Color.FromArgb(0, 0, 0);
            this.button7.Location = new System.Drawing.Point(-8, 0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(200, 45);
            this.button7.Text = "Статистика";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.Font = new Font("Segoe UI", 12);

            this.panel2.Controls.Add(this.button7);
            this.panel2.Location = new System.Drawing.Point(0, 150);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(225, 46);

            this.button27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button27.ForeColor = isDarkTheme ? Color.FromArgb(186, 192, 196) : Color.FromArgb(0, 0, 0);
            this.button27.Location = new System.Drawing.Point(-8, 0);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(200, 45);
            this.button27.Text = "Налаштування";
            this.button27.UseVisualStyleBackColor = true;
            this.button27.Click += new System.EventHandler(this.button27_Click);
            this.button27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button27.FlatAppearance.BorderSize = 0;
            this.button27.Font = new Font("Segoe UI", 12);

            this.panel6.Controls.Add(this.button8);
            this.panel6.Location = new System.Drawing.Point(0, 100);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(225, 46);

            this.button28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button28.ForeColor = isDarkTheme ? Color.FromArgb(186, 192, 196) : Color.FromArgb(0, 0, 0);
            this.button28.Location = new System.Drawing.Point(-8, 0);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(200, 45);
            this.button28.Text = "Про програму";
            this.button28.UseVisualStyleBackColor = true;
            this.button28.Click += new System.EventHandler(this.button28_Click);
            this.button28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button28.FlatAppearance.BorderSize = 0;
            this.button28.Font = new Font("Segoe UI", 12);

            this.panel4.Controls.Add(this.button27);
            this.panel4.Location = new System.Drawing.Point(0, 200);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(225, 46);
            this.panel4.TabIndex = 24;

            this.button8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button8.BackgroundImage")));
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.ForeColor = isDarkTheme ? Color.FromArgb(186, 192, 196) : Color.FromArgb(0, 0, 0);
            this.button8.Location = new System.Drawing.Point(-8, 0);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(200, 45);
            this.button8.Text = "Головне меню";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.Font = new Font("Segoe UI", 12);

            if (CheckBox2Active == true || Form1.settings.ColorTheme == "light")
            {
                this.BackColor = Color.FromArgb(255, 255, 255);
                this.panel1.BackColor = Color.FromArgb(249, 249, 249);
                this.button7.ForeColor = Color.FromArgb(0, 0, 0);
                this.button8.ForeColor = Color.FromArgb(0, 0, 0);
                this.button27.ForeColor = Color.FromArgb(0, 0, 0);
                this.button28.ForeColor = Color.FromArgb(0, 0, 0);
            }

            if (CheckBox1Active == true || Form1.settings.ColorTheme == "dark")
            {
                this.BackColor = Color.FromArgb(2, 14, 25);
                this.panel1.BackColor = Color.FromArgb(4, 26, 44);
            }

            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.Controls.Add(this.panel1);
  
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void SetActivePanel(Panel panel)
        {
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            if (activePanel != null)
            {
                activePanel.BackColor = !isDarkTheme ? Color.FromArgb(249, 249, 249) : Color.FromArgb(4, 26, 44);
            }

            panel.BackColor = isDarkTheme ? Color.FromArgb(30, 60, 90) : Color.FromArgb(235, 235, 235); 
            activePanel = panel;
        }

        private void InitializeComponentMainMenu()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            InitializeComponentMain();
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button(); 
            this.button10 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.SuspendLayout();

            this.button1 = CreateButton("button1", "Додати проєкт", new Point(280, 327), new Size(515, 37), this.button1_Click);
            this.button2 = CreateButton("button2", "Запустити таймер", new Point(438, 250), new Size(200, 38), this.button2_Click);

            this.pictureBox1.Image = isDarkTheme ? Properties.Resources.ClockForDarkTheme : Properties.Resources.ClockForLightTheme;
            this.ConfigurePictureBox(pictureBox1, new Point(456, 58), new Size(164, 164));
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabStop = false;

            var commonBackColor = Color.FromArgb(6, 40, 68);
            this.ConfigurePictureBox(pictureBox2, new Point(280, 370), new Size(255, 37), backgroundImage: (Image)resources.GetObject("pictureBox2.BackgroundImage")); //назва
            this.ConfigurePictureBox(pictureBox9, new Point(540, 370), new Size(255, 37), backgroundImage: (Image)resources.GetObject("pictureBox2.BackgroundImage")); //шлях

            this.label2 = ConfigureLabel(new Point(498, 123), new Size(100, 36), "00:00:00");
            label2.BackColor = Color.FromArgb(186, 192, 196);
            label2.ForeColor = Color.FromArgb(0, 0, 0);

            panel2 = new Panel
            {
                Name = "panel2",
                Location = new Point(280, 413),       
                Size = new Size(540, 168),      
                BackColor = isDarkTheme ? Color.FromArgb(2, 14, 25) : Color.FromArgb(255, 255, 255), 
                AutoScroll = true
            };

            this.Controls.Add(panel2);

            this.label1 = ConfigureLabel(new Point(380, 370), new Size(65, 35), "Назва");
            this.label9 = ConfigureLabel(new Point(640, 370), new Size(65, 35), "Шлях");

            this.label2.Font = new Font("Microsoft Sans Serif", 14);

            this.button3 = CreateButton("buttonDelete", "Видалити", new Point(694, 586), new Size(100, 25), this.buttonDelete_Click);
            this.button3.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(900,650);

            PopulateProjects();

            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void PopulateProjects()
        {
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            panel2.Controls.Clear();
            selectedRows.Clear();
            button3.Visible = false;

            var projects = JsonProcessing.LoadProjects();
            int rowH = 37, spacing = 6;
            int nameX = 0, pathX = 260;
            int y = 0;

            for (int i = 0; i < projects.Count; i++, y += rowH + spacing)
            {
                var lblName = ConfigureLabel3(new Point(nameX, y), new Size(255, rowH), "lblName" + i);
                lblName.Text = projects[i].Name;
                lblName.Tag = i;
                lblName.Click += Label_Click;
                lblName.Text = TrimWithEllipsis(projects[i].Name, lblName.Font, lblName.Width);

                var lblPath = ConfigureLabel3(new Point(pathX, y), new Size(255, rowH), "lblPath" + i);
                lblPath.Text = projects[i].Path;
                lblPath.Tag = i;
                lblPath.Click += Label_Click;
                lblPath.Text = TrimWithEllipsis(projects[i].Path, lblName.Font, lblName.Width);

                panel2.Controls.Add(lblName);
                panel2.Controls.Add(lblPath);

                _labelToolTip.SetToolTip(lblName, projects[i].Name);
                _labelToolTip.SetToolTip(lblPath, projects[i].Path);
            }
        }

        private void Label_Click(object sender, EventArgs e)
        {
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            var lbl = sender as Label;
            int rowIndex = (int)lbl.Tag;

            // Тoggle у HashSet: якщо був – видаляємо, інакше додаємо
            if (!selectedRows.Remove(rowIndex))
                selectedRows.Add(rowIndex);

            // Перефарбовуємо обидва Label-і цього рядка
            bool isSelected = selectedRows.Contains(rowIndex);
            Color bg = isSelected
                ? isDarkTheme ? Color.FromArgb(30, 60, 90) : Color.FromArgb(145, 150, 153)   // ваш колір “відзначено”
                : isDarkTheme ? Color.FromArgb(6, 40, 68) : Color.FromArgb(182, 192, 196);            // колір “звичайний”

            foreach (Control c in panel2.Controls)
                if (c is Label l && (int)l.Tag == rowIndex)
                    l.BackColor = bg;

            // Показуємо кнопку видалення, якщо є хоч один вибраний
            button3.Visible = selectedRows.Count > 0;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var projects = JsonProcessing.LoadProjects();

            // Видаляємо вибрані з кінця, щоб індекси не зіпсувались
            foreach (int idx in selectedRows.OrderByDescending(i => i))
                projects.RemoveAt(idx);

            JsonProcessing.SaveProjects(projects);

            // Перебудовуємо UI
            PopulateProjects();
        }

        private void buttonDelete2_Click(object sender, EventArgs e)
        {
            if (selectedRows.Count == 0)
                return; // нічого не видаляти

            // Запитуємо підтвердження у користувача
            /*var result = MessageBox.Show(
                $"Ви дійсно хочете видалити {selectedRows.Count} запис(и)?",
                "Підтвердження видалення",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return; */

            // 1) Збираємо список URLData, які зараз показані на поточній сторінці (за date)
            var date = availableDates[currentIndex];
            var todaysUrls = allUrls
                .Where(u => u.Timestamp.Date == date)
                .ToList();

            // 2) Визначаємо, які саме об’єкти потрібно видалити
            var toDelete = selectedRows
                .Select(idx => todaysUrls[idx])
                .ToList();

            // 3) Видаляємо їх з головного списку
            foreach (var u in toDelete)
                allUrls.Remove(u);

            // 4) Записуємо оновлений список у файл
            JsonProcessing.SaveUrlListToJson(allUrls);

            // 5) Оновлюємо список дат на випадок, якщо остання дата стала пустою
            availableDates = allUrls
                .Select(u => u.Timestamp.Date)
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            if (availableDates.Count == 0)
            {
                // Якщо більше немає даних — очищаємо вивід
                panel2.Controls.Clear();
                label10.Text = "Немає даних";
                button11.Visible = button12.Visible = false;
                button3.Visible = false;
                return;
            }

            // 6) Підганяємо currentIndex, щоб він був у межах нового списку дат
            currentIndex = Math.Min(currentIndex, availableDates.Count - 1);

            // 7) Очищаємо виділення й оновлюємо вивід
            selectedRows.Clear();
            button3.Visible = false;
            UpdateDateView();
        }

        private Label ConfigureLabel3(Point location, Size size, string name)
        {
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            var lbl = new Label
            {
                Name = name,
                Location = location,
                Size = size,
                ForeColor = isDarkTheme ? Color.FromArgb(186, 192, 196) : Color.FromArgb(0, 0, 0),
                BackColor = isDarkTheme ? Color.FromArgb(6, 40, 68) : Color.FromArgb(235, 235, 235),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft, 
                Font = new Font("Segoe UI", 12),
                AutoEllipsis = true
            };

            lbl.Tag = location.Y;
            return lbl;
        }

        string TrimWithEllipsis(string txt, Font f, int maxWidth)
        {
            const string ell = "...";
            if (TextRenderer.MeasureText(txt, f).Width <= maxWidth)
                return txt;

            int lo = 0, hi = txt.Length;
            while (lo < hi)
            {
                int mid = (lo + hi) / 2;
                var candidate = txt.Substring(0, mid) + ell;
                if (TextRenderer.MeasureText(candidate, f).Width <= maxWidth)
                    lo = mid + 1;
                else
                    hi = mid;
            }
            return txt.Substring(0, lo - 1) + ell;
        }

        private System.Windows.Forms.Label ConfigureLabel(Point location, Size size, string text, Image image = null)
        {
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            var label = new System.Windows.Forms.Label();
            label.Location = location;
            label.Size = size;
            label.Text = text;
            if (image != null)
            {
                label.Image = image;
            }
            label.BackColor = isDarkTheme ? Color.FromArgb(6, 40, 68) : Color.FromArgb(249, 249, 249);
            if (label != label2)
            {
                label.ForeColor = isDarkTheme ? Color.FromArgb(186, 192, 196) : Color.FromArgb(0, 0, 0);
            }
            label.Font = new Font("Segoe UI", 12);
            label.AutoEllipsis = true;  // Додає "..." якщо текст не влізе
            label.TextAlign = ContentAlignment.MiddleLeft;
            this.Controls.Add(label);
            return label;
        }

        private void ConfigurePictureBox(PictureBox pictureBox, Point location, Size size, Image backgroundImage = null)
        {
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            pictureBox.Location = location;
            pictureBox.Size = size;
            if (backgroundImage != null)
            {
                pictureBox.BackgroundImage = backgroundImage ;
            }
            pictureBox.BackColor = isDarkTheme ? Color.FromArgb(6, 40, 68) : Color.FromArgb(249, 249, 249);
            pictureBox.TabStop = false;
        }

        private PlotView CreatePlotView()
        {
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            var plotView = new PlotView
            {
                Dock = DockStyle.None,
                Size = new Size(600, 400),
                Location = new Point(230, 150)
            };

            var plotModel = new PlotModel
            {
                PlotAreaBorderColor = isDarkTheme ? OxyColor.FromRgb(2, 14, 25) : OxyColor.FromRgb(212, 220, 225),
                PlotAreaBorderThickness = new OxyThickness(1)
            };

            plotView.Model = plotModel;

            return plotView;
        }

        private void StyleAxis(Axis axis)
        {
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            var mainColor = OxyColor.FromRgb(159, 183, 213);
            var minorBaseColor = isDarkTheme ? OxyColor.FromRgb(81, 99, 119) : OxyColor.FromRgb(82, 82, 82);
            var minorTransparent = isDarkTheme
                ? OxyColor.FromAColor(80, minorBaseColor)   
                : OxyColor.FromAColor(50, minorBaseColor); 

            axis.TitleColor = isDarkTheme ? mainColor : OxyColor.FromRgb(82, 82, 82);
            axis.TextColor = axis.TitleColor;
            axis.TicklineColor = axis.TitleColor;
            axis.AxislineColor = axis.TitleColor;
            var majorTransparent = isDarkTheme
                 ? OxyColor.FromAColor(120, mainColor) // 120 ≈ 47% прозорості
                 : OxyColor.FromAColor(100, OxyColor.FromRgb(82, 82, 82)); // трохи менше прозоре у світлій темі

            axis.MajorGridlineColor = majorTransparent;
            axis.MinorGridlineColor = minorTransparent;
            axis.MajorGridlineThickness = 1;
            axis.MinorGridlineThickness = 0.5;
            axis.MajorGridlineStyle = LineStyle.Solid;
            axis.MinorGridlineStyle = LineStyle.Dot;
            axis.IsZoomEnabled = false; 
        }

        private string FormatTimeForY(int totalSeconds)
        {
            if (totalSeconds < 60)
                return $"{totalSeconds} сек.";

            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;

            if (hours > 0)
            {
                return minutes > 0
                    ? $"{hours} год. {minutes} хв."
                    : $"{hours} год.";
            }
            return $"{minutes} хв.";
        }

        private void buildChart(List<DateTime> xPoints, List<int> yPoints)
        {
            var plotView = CreatePlotView();
            var plotModel = plotView.Model;

            var lineSeries = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                Color = Form1.settings.ColorTheme == "dark" ? OxyColor.FromRgb(159, 183, 213) : OxyColor.FromRgb(82, 82, 82),
                TrackerFormatString = "\u200B"
            };

            for (int i = 0; i < xPoints.Count; i++)
            {
                double dateAsDouble = DateTimeAxis.ToDouble(xPoints[i]);
                lineSeries.Points.Add(new DataPoint(dateAsDouble, yPoints[i]));
            }

            plotModel.Series.Add(lineSeries);

            var ctrl = plotView.Controller ?? plotView.ActualController;

            ctrl.UnbindMouseDown(
                OxyPlot.OxyMouseButton.Left,
                OxyPlot.OxyModifierKeys.None,
                clickCount: 1
            );

            plotView.Controller = ctrl;

            var yMin = yPoints.Min();
            var yMax = yPoints.Max();
            double range = yMax - yMin;
            double majorStep = Math.Ceiling(range / 5.0 / 100.0) * 100;
            if (majorStep == 0) majorStep = 10;
            double minorStep = majorStep / 5.0;

            var axisY = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Час",
                Minimum = yMin - 2,
                Maximum = yMax + 2,
                MajorStep = majorStep,
                MinorStep = minorStep,
                LabelFormatter = value => FormatTimeForY((int)Math.Round(value))
            };

            axisY.LabelFormatter = value =>
            {
                int totalSeconds = (int)Math.Round(value);
                if (totalSeconds < 60)
                    return $"{totalSeconds} сек.";
                int hours = totalSeconds / 3600;
                int minutes = (totalSeconds % 3600) / 60;

                if (hours > 0)
                    return minutes > 0
                        ? $"{hours} год. {minutes} хв."
                        : $"{hours} год.";
                return $"{minutes} хв.";
            };

            var minDate = xPoints.Min();
            var maxDate = xPoints.Max();

            var minimumX = DateTimeAxis.ToDouble(minDate.AddHours(-2));
            var maximumX = DateTimeAxis.ToDouble(maxDate.AddHours(+2));

            var totalDays = maximumX - minimumX;
            double step;
            if (totalDays <= 10)
                step = 1;
            else if (totalDays <= 30)
                step = 2;
            else if (totalDays <= 90)
                step = 7;
            else if (totalDays <= 365)
                step = 30;
            else
                step = 90;

            var axisX = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Дата",
                Minimum = minimumX,
                Maximum = maximumX,
                StringFormat = "dd.MM",
                MajorStep = step,
                Angle = 45,
                AxisTitleDistance = 10
            };

            _showDateInTooltip = true;
            plotView.MouseDown += PlotView_MouseDown;
            plotView.MouseUp += PlotView_MouseUp;

            this.Controls.Add(plotView);

            StyleAxis(axisX);
            StyleAxis(axisY);
            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            this.label9 = CreateMainLabel("label9", "label9", 545, 550, new Size(530, 50));
            plotView.Model = plotModel;
            this.Controls.Add(plotView);
        }

        private void PlotView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (!(sender is OxyPlot.WindowsForms.PlotView view))
                return;
            var model = view.Model;
            if (model == null)
                return;

            _customToolTip.Hide(view);

            var series = model.Series.OfType<LineSeries>().FirstOrDefault();
            if (series == null)
                return;

            var sp = new ScreenPoint(e.X, e.Y);
            var nearest = series.GetNearestPoint(sp, interpolate: false);
            if (nearest == null)
                return;
            if (nearest.Position.DistanceTo(sp) >= 10)
                return;

            var dp = nearest.DataPoint;
            var dt = DateTimeAxis.ToDateTime(dp.X, TimeSpan.FromSeconds(1));

            double rawMinutes = dp.Y / _currentScaleFactor;

            string text = GetToolTipText(dt, rawMinutes, _showDateInTooltip);

            _customToolTip.Show(text, view, e.X + 10, e.Y + 10);
        }

        private void PlotView_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is OxyPlot.WindowsForms.PlotView view)
                _customToolTip.Hide(view);
        }

        private string FormatHoursMinutes(double totalMinutes)
        {
            if (totalMinutes < 1.0)
            {
                int totalSeconds = (int)Math.Round(totalMinutes * 60.0);

                if (totalSeconds <= 0)
                    totalSeconds = 1;
                return $"{totalSeconds} с";
            }

            int wholeMinutes = (int)Math.Round(totalMinutes);
            int hours = wholeMinutes / 60;
            int minutes = wholeMinutes % 60;

            if (hours > 0)
            {
                return $"{hours} год. {minutes:D2} хв.";
            }
            else
            {
                return $"{minutes} хв.";
            }
        }

        private string GetToolTipText(DateTime dt, double rawMinutes, bool showDate)
        {
            string timePart = $"Час: {FormatHoursMinutes(rawMinutes)}";
            if (!showDate)
                return timePart;

            string datePart = $"Дата: {dt:dd.MM.yyyy}";
            return $"{datePart}\n{timePart}";
        }

        private void buildDailyChart<T>(List<T> dataList, DateTime selectedDate, Func<T, (DateTime Start, DateTime Stop)?> convertToInterval)
        {
            DateTime today = selectedDate.Date;

            // 1) Збираємо інтервали
            var parsed = dataList
                .Select(convertToInterval)
                .Where(i => i.HasValue && i.Value.Start != DateTime.MinValue)
                .Select(i => i.Value)
                .ToList();

            // 2) Підраховуємо хвилини по годинах
            double[] minutesPerHour = new double[24];
            foreach (var interval in parsed)
            {
                DateTime start = interval.Start;
                DateTime stop = interval.Stop;

                if (stop < today || start >= today.AddDays(1))
                    continue;
                if (start < today)
                    start = today;
                if (stop > today.AddDays(1))
                    stop = today.AddDays(1);

                while (start < stop)
                {
                    int hour = start.Hour;
                    DateTime nextHour = new DateTime(start.Year, start.Month, start.Day, hour, 0, 0).AddHours(1);
                    DateTime segmentEnd = (stop < nextHour) ? stop : nextHour;

                    double minutes = (segmentEnd - start).TotalMinutes;
                    minutesPerHour[hour] += minutes;

                    start = segmentEnd;
                }
            }

            // 3) Знаходимо максимум (у хвилинах) для вибору одиниць Y
            double rawMax = minutesPerHour.Max();

            // 4) Вибираємо масштаб (сек/хв/год) і unitLabel
            double scaleFactor;
            string unitLabel;

            if (rawMax <= 0.0)
            {
                scaleFactor = 1.0;   // показувати в «хв.» навіть якщо усе нульове
                unitLabel = "хв.";
            }
            else if (rawMax < 1.0)
            {
                scaleFactor = 60.0;  // показувати в «сек.»
                unitLabel = "с";
            }
            else if (rawMax < 60.0)
            {
                scaleFactor = 1.0;   // показувати в «хв.»
                unitLabel = "хв.";
            }
            else
            {
                scaleFactor = 1.0 / 60.0;  // показувати в «год.»
                unitLabel = "год.";
            }

            _currentScaleFactor = scaleFactor;
            _currentUnitLabel = unitLabel;

            // 5) Обчислюємо граничні значення для Y
            double maxInUnit = rawMax * scaleFactor;
            // Якщо всі дані нульові → мінімальна одиниця = 1,
            // щоб потім при відображенні осі Y точка y=0 не «зникала» під дном.
            double maxY = (maxInUnit <= 0) ? 1.0 : maxInUnit;
            double majorY = Math.Max(1.0, Math.Ceiling(maxY / 10.0));
            double minorY = majorY / 2.0;
            double axisMaxY = maxY * 1.05;

            // 6) Створюємо PlotView / PlotModel
            var plotView = CreatePlotView();
            var plotModel = plotView.Model;

            // 7) Створюємо LineSeries (лінія + маркери)
            var series = new LineSeries
            {
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2,
                Color = Form1.settings.ColorTheme == "dark"
                            ? OxyColor.FromRgb(159, 183, 213)
                            : OxyColor.FromRgb(82, 82, 82),
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,   // зменшені кружечки
                MarkerFill = Form1.settings.ColorTheme == "dark"
                                 ? OxyColor.FromRgb(159, 183, 213)
                                 : OxyColor.FromRgb(82, 82, 82),
                TrackerFormatString = $"Година: {{2:0}}\nЧас: {{4:0.##}} {unitLabel}"
            };

            // 8) Додаємо точки у серію – по одній на годину
            for (int h = 0; h < 24; h++)
            {
                double yValue = minutesPerHour[h] * scaleFactor;
                double xValue = h;
                series.Points.Add(new DataPoint(xValue, yValue));
            }
            plotModel.Series.Add(series);

            // 9) Налаштовуємо X-вісь (підписи кожні 2 години, дрібні лінії щогодини)
            var axisX = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Година",
                Minimum = -0.5,
                Maximum = 23.5,
                MajorStep = 2,  // підписи (00:00, 02:00, 04:00 …)
                MinorStep = 1,  // дрібні вертикальні лінії щогодини
                LabelFormatter = v =>
                {
                    if (v < 0 || v > 23) return "";
                    int iv = (int)Math.Round(v);
                    if (Math.Abs(v - iv) > 0.001) return "";
                    return TimeSpan.FromHours(iv).ToString(@"hh\:mm");
                },
                Angle = -45,
            };
            StyleAxis(axisX);
            plotModel.Axes.Add(axisX);

            // 10) Налаштовуємо Y-вісь (рідша сітка по majorY)
            var axisY = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Час",
                // Тепер нижня межа динамічно: якщо нема даних — трохи від’ємна,
                // інакше завжди нуль:
                Minimum = (rawMax <= 0) ? -1 * majorY : 0,
                Maximum = axisMaxY,
                MajorStep = majorY * 2,
                MinorStep = majorY,

                LabelFormatter = value =>
                {
                    if (unitLabel == "с")
                    {
                        int sec = (int)Math.Round(value);
                        return $"{sec} с";
                    }
                    else if (unitLabel == "хв.")
                    {
                        int min = (int)Math.Round(value);
                        return $"{min} хв.";
                    }
                    else // "год."
                    {
                        if (Math.Abs(value - Math.Round(value)) < 0.0001)
                            return $"{(int)Math.Round(value)} год";
                        else
                            return $"{value:0.#} год";
                    }
                }
            };
            StyleAxis(axisY);
            plotModel.Axes.Add(axisY);

            // 11) Якщо немає жодної хвилини (rawMax == 0), додаємо явну лінію Y = 0
            if (rawMax <= 0.0)
            {
                plotModel.Annotations.Add(new LineAnnotation
                {
                    Type = LineAnnotationType.Horizontal,
                    Y = 0,
                    Color = Form1.settings.ColorTheme == "dark"
                                           ? OxyColor.FromRgb(159, 183, 213)
                                           : OxyColor.FromRgb(82, 82, 82),
                    StrokeThickness = 2,
                    LineStyle = LineStyle.Solid
                });
            }

            // 12) Вимикаємо пан/зум по лівому кліку
            var ctrl = plotView.Controller ?? plotView.ActualController;
            ctrl.UnbindMouseDown(
                OxyPlot.OxyMouseButton.Left,
                OxyPlot.OxyModifierKeys.None,
                clickCount: 1
            );
            plotView.Controller = ctrl;

            // 13) Підписи “Загальний час” і “Статистика за …”
            this.label9 = CreateMainLabel("label9", "label9", 545, 550, new Size(530, 50));
            double totalSeconds = parsed.Sum(iv => (iv.Stop - iv.Start).TotalSeconds);
            label9.Text = $"Загальний час: {statistic.HumanizeSeconds(totalSeconds)}";
            label10.Text = $"Статистика за {selectedDate:dd.MM.yyyy}";

            // 14) Додаємо PlotView на форму
            plotView.Model = plotModel;
            this.Controls.Add(plotView);

            // 15) Підключаємо тултіп-обробники
            _showDateInTooltip = false;
            plotView.MouseDown += PlotView_MouseDown;
            plotView.MouseUp += PlotView_MouseUp;
        }

        private void RemoveChart()
        {
            var plotView = this.Controls.OfType<PlotView>().FirstOrDefault();
            if (plotView != null)
            {
                this.Controls.Remove(plotView); // Видалення PlotView з форми
                plotView.Dispose(); // Звільнення ресурсів
            }

            if (this.label9 != null)
            {
                this.Controls.Remove(this.label9);
                this.label9.Dispose();
                this.label9 = null; // Звільнення ресурсів
            }
        }

        private void StatisticsMainMenu()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));

            ComboBox comboBox = new ComboBox();
            comboBox.BackColor = Color.FromArgb(186, 192, 196);
            comboBox.Items.Add("За день");
            comboBox.Items.Add("За тиждень");
            comboBox.Items.Add("За місяць");
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Location = new Point(245, 100);
            comboBox.Size = new Size(150, 30);
            comboBox.ItemHeight = 30; 
            comboBox.Font = new Font("Microsoft Sans Serif", 12);

            ComboBox secondcomboBox = new ComboBox();
            secondcomboBox.BackColor = Color.FromArgb(186, 192, 196);
            secondcomboBox.Items.Add("За проєктами");
            secondcomboBox.Items.Add("За браузером");
            secondcomboBox.Items.Add("Об'єднана");
            secondcomboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            secondcomboBox.Location = new Point(400, 100);
            secondcomboBox.Size = new Size(150, 30);
            secondcomboBox.ItemHeight = 30;
            secondcomboBox.Font = new Font("Microsoft Sans Serif", 12);
            secondcomboBox.ItemHeight = 30;

            comboBox.SelectedItem = "За день";
            secondcomboBox.SelectedItem = "За проєктами";

            this.Controls.Add(comboBox);
            this.Controls.Add(secondcomboBox);

            comboBox.SelectionChangeCommitted += (s, e) =>
             {
                 string selected = comboBox.SelectedItem.ToString();

                 switch (selected)
                 {
                     case "За місяць":
                         currentViewMode = ViewMode.Month;
                         currentMonthOffset = 0;
                         CurrentMonth();
                         break;
                     case "За тиждень":
                         currentViewMode = ViewMode.Week;
                         currentWeekOffset = 0;
                         CurrentWeek();
                         break;
                     case "За день":
                         currentViewMode = ViewMode.Day;
                         currentDayOffset = 0;
                         CurrentDay();
                         break;
                 }

             };

            secondcomboBox.SelectionChangeCommitted += (s, e) =>
            {
                switch (secondcomboBox.SelectedItem.ToString())
                {
                    case "За проєктами":
                        typeOfStatictics = "за проєктами";
                        break;
                    case "Об'єднана":
                        typeOfStatictics = "за браузером та проєктами";
                        break;
                    case "За браузером":
                        typeOfStatictics = "за браузером";
                        break;
                }

                switch (currentViewMode)
                {
                    case ViewMode.Day: CurrentDay(); break;
                    case ViewMode.Week: CurrentWeek(); break;
                    case ViewMode.Month: CurrentMonth(); break;
                }
            };

            this.button11 = CreateButton("button11", "<", new Point(218, 550), new Size(41, 41), this.button11_Click);
            this.button12 = CreateButton("button12", ">", new Point(820, 550), new Size(41, 41), this.button12_Click);
            this.button13 = CreateButton("button13", "Продивитися дані", new Point(647, 100), new Size(150, 30), this.button13_Click);
            this.label10 = CreateMainLabel("label10", "label10", 545, 30, new Size(530, 50));

            if (CheckBox2Active == true)
            {
                this.label10.ForeColor = Color.FromArgb(82, 82, 82);
                this.label10.BackColor = Color.FromArgb(212, 220, 225);
            }


            if (CheckBox1Active == true)
            {
                this.label10.ForeColor = Color.FromArgb(186, 192, 196);
                this.label10.BackColor = Color.FromArgb(2, 14, 25);
            }

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ExitButton()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));

            this.button14 = CreateButton("button14", "Повернутися назад", new Point(420, 550), new Size(240, 41), this.button14_Click);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button CreateButton(string name, string text, Point location, Size size, EventHandler clickHandler)
        {
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            var button = new System.Windows.Forms.Button
            {
                Name = name,
                Text = text,
                Location = location,
                Size = size,
                ForeColor = isDarkTheme ? Color.FromArgb(186, 192, 196) : Color.FromArgb(0, 0, 0),
                BackColor = isDarkTheme ? Color.FromArgb(6, 40, 68) : Color.FromArgb(235, 235, 235),
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                UseVisualStyleBackColor = true,
            };
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI", 12); 
            button.Click += clickHandler;
            this.Controls.Add(button);
            return button;
        }

        private System.Windows.Forms.Label CreateMainLabel(string name, string text, int centerX, int y, Size size)
        {
            // Обчислюємо X так, щоб центр мітки збігався з centerX
            int labelX = centerX - size.Width / 2;
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            var label = new Label
            {
                Name = name,
                Text = text,
                Location = new Point(labelX, y),
                Size = size,
                Font = new Font("Segoe UI", 16),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = isDarkTheme ? Color.FromArgb(186, 192, 196) : Color.FromArgb(0, 0, 0),
                BackColor = isDarkTheme ? Color.FromArgb(2, 14, 25) : Color.FromArgb(255, 255, 255)
            };
            this.Controls.Add(label);

            return label; 
        }

        private Label CreateSettingsLabel(Point location, string name, string text, int fontSize = 12, bool isTitle = false)
        {
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            var label = new Label
            {
                AutoSize = true,
                ForeColor = isDarkTheme ? Color.FromArgb(186, 192, 196) : Color.FromArgb(0, 0, 0),
                BackColor = isDarkTheme ? Color.FromArgb(2, 14, 25) : Color.FromArgb(212, 220, 225),
                Location = location,
                Name = name,
                Font = new Font("Segoe UI", 12),
                Size = new Size(180, 180),
                Text = text,
                TextAlign = ContentAlignment.MiddleLeft
            };

            this.Controls.Add(label);
            return label;
        }

        private CheckBox CreateCheckBox(Point location, string name, string text)
        {
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            var checkBox = new CheckBox
            {
                AutoSize = true,
                ForeColor = isDarkTheme ? Color.FromArgb(186, 192, 196) : Color.FromArgb(0, 0, 0),
                Location = location,
                Name = name,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(180, 180),
                Text = text,
                BackColor = Color.Transparent
            };
            
            this.Controls.Add(checkBox);
            return checkBox;
        }

        #endregion

        private void SettingsMenu()
        {
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label11 = CreateSettingsLabel(new Point(250, 100), "label11", "Колір теми:");
            label12 = CreateSettingsLabel(new Point(250, 350), "label12", "Автоматично вимикати таймер за неактивності протягом:");
            label13 = CreateSettingsLabel(new Point(250, 450), "label13", "Якщо вас не задовільнило жодне значення, введіть власне в наступне поле:");

            label13.Size = new Size(400, 300);
            label10 = CreateMainLabel("label10", "Налаштування роботи програми", 550, 30, new Size(500, 50));

            label11.BackColor = Color.Transparent;
            label12.BackColor = Color.Transparent;
            label13.BackColor = Color.Transparent;

            checkBox1 = CreateCheckBox(new Point(250, 150), "checkBox1", "Темна тема");
            checkBox2 = CreateCheckBox(new Point(450, 150), "checkBox2", "Світла тема");
            checkBox3 = CreateCheckBox(new Point(250, 200), "checkBox3", "Автозапуск програми");
            checkBox4 = CreateCheckBox(new Point(250, 250), "checkBox4", "Дозволити програмі надсилати сповіщення");
            checkBox5 = CreateCheckBox(new Point(250, 400), "checkBox5", "5 хв");
            checkBox6 = CreateCheckBox(new Point(450, 400), "checkBox6", "10 хв");
            checkBox7 = CreateCheckBox(new Point(650, 400), "checkBox7", "15 хв");
            checkBox8 = CreateCheckBox(new Point(250, 300), "checkBox8", "Відстежувати активність в браузері");

           // this.Paint += Form1_Paint;

            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox1.Location = new Point(250, 500); 
            this.textBox1.AutoSize = false;
            this.textBox1.Size = new Size(150, 20);
            this.textBox1.BackColor = isDarkTheme ? Color.FromArgb(6, 40, 68) : Color.FromArgb(235, 235, 235); 
            this.textBox1.ForeColor = isDarkTheme ? Color.FromArgb(186, 192, 196) : Color.FromArgb(0, 0, 0);
            this.textBox1.Font = new Font("Segoe UI", 12); 
            this.textBox1.Text = "Введіть число";
            this.textBox1.BorderStyle = BorderStyle.None;
            this.Controls.Add(textBox1);

            JsonProcessing.LoadSettings();

            if (isDarkTheme)
            {
                checkBox1.Checked = true;
                checkBox2.Checked = false;
            }
            else
            {
                checkBox2.Checked = true;
                checkBox1.Checked = false;
            }

            checkBox7.CheckedChanged += (sender, e) =>
            {
                if (checkBox7.Checked &&
                (string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Введіть число" || textBoxText == null))
                {
                    checkBox5.Checked = false;
                    checkBox6.Checked = false;
                    CheckBox7Active = true;

                    nonActiveTime = 15;

                    UpdateInactivityAmount(this, nonActiveTime);
                }
            };

            if (CheckBox7Active == true)
            {
                checkBox7.Checked = true;
            }

            checkBox6.CheckedChanged += (sender, e) =>
            {
                if (checkBox6.Checked  &&
                (string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Введіть число" || textBoxText == null))
                {
                    checkBox5.Checked = false;
                    checkBox7.Checked = false;
                    CheckBox6Active = true;

                    nonActiveTime = 10;
                    UpdateInactivityAmount(this, nonActiveTime);
                }
            };

            if (CheckBox6Active == true)
            {
                checkBox6.Checked = true;
            }

            checkBox5.CheckedChanged += (sender, e) =>
            {
                if (checkBox5.Checked &&
                (string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Введіть число" || textBoxText == null))
                {
                    checkBox6.Checked = false;
                    checkBox7.Checked = false;
                    CheckBox5Active = true;

                    nonActiveTime = 5;
                    UpdateInactivityAmount(this, nonActiveTime);
                }
            };

            if (CheckBox5Active == true)
            {
                checkBox5.Checked = true;
            }

            checkBox1.CheckedChanged += (sender, e) =>
            {
                if (checkBox1.Checked || Form1.settings.ColorTheme == "dark")
                {
                    checkBox2.Checked = false;

                    CheckBox1Active = true;
                    CheckBox2Active = false;

                    themeColor = "dark";

                    UpdateColorTheme(this, themeColor);

                    BackColor = Color.FromArgb(2, 14, 25);
                    panel1.BackColor = Color.FromArgb(4, 26, 44);
                    button7.ForeColor = Color.FromArgb(186, 192, 196);
                    button8.ForeColor = Color.FromArgb(186, 192, 196);
                    button27.ForeColor = Color.FromArgb(186, 192, 196);
                    button28.ForeColor = Color.FromArgb(186, 192, 196);

                    checkBox1.BackColor = Color.FromArgb(2, 14, 25);
                    checkBox2.BackColor = Color.FromArgb(2, 14, 25);
                    checkBox3.BackColor = Color.FromArgb(2, 14, 25);
                    checkBox4.BackColor = Color.FromArgb(2, 14, 25);
                    checkBox5.BackColor = Color.FromArgb(2, 14, 25);
                    checkBox6.BackColor = Color.FromArgb(2, 14, 25);
                    checkBox7.BackColor = Color.FromArgb(2, 14, 25);
                    checkBox8.BackColor = Color.FromArgb(2, 14, 25);

                    checkBox1.ForeColor = Color.FromArgb(186, 192, 196);
                    checkBox2.ForeColor = Color.FromArgb(186, 192, 196);
                    checkBox3.ForeColor = Color.FromArgb(186, 192, 196);
                    checkBox4.ForeColor = Color.FromArgb(186, 192, 196);
                    checkBox5.ForeColor = Color.FromArgb(186, 192, 196);
                    checkBox6.ForeColor = Color.FromArgb(186, 192, 196);
                    checkBox7.ForeColor = Color.FromArgb(186, 192, 196);
                    checkBox8.ForeColor = Color.FromArgb(186, 192, 196);

                    label11.ForeColor = Color.FromArgb(186, 192, 196);
                    label12.ForeColor = Color.FromArgb(186, 192, 196);
                    label13.ForeColor = Color.FromArgb(186, 192, 196);
                    label10.ForeColor = Color.FromArgb(186, 192, 196);

                    label11.BackColor = Color.FromArgb(2, 14, 25);
                    label12.BackColor = Color.FromArgb(2, 14, 25);
                    label13.BackColor = Color.FromArgb(2, 14, 25);
                    label10.BackColor = Color.FromArgb(2, 14, 25);

                    textBox1.BackColor = Color.FromArgb(6, 40, 68);// Розмір поля
                    textBox1.ForeColor = Color.FromArgb(186, 192, 196);

                    button3.BackColor = Color.FromArgb(2, 14, 25);
                    pictureBox2.BackColor = Color.FromArgb(2, 14, 25);

                    SetActivePanel(panel4);

                }
                if (!checkBox1.Checked && !checkBox2.Checked)
                {
                    checkBox1.Checked = true; 
                }
            };

                checkBox2.CheckedChanged += (sender, e) =>
            {
                if (checkBox2.Checked || Form1.settings.ColorTheme == "light")
                {
                    checkBox1.Checked = false;

                    CheckBox2Active = true;
                    CheckBox1Active = false;

                    themeColor = "light";

                    UpdateColorTheme(this, themeColor);

                    BackColor = Color.FromArgb(212, 220, 225);
                    panel1.BackColor = Color.FromArgb(171, 176, 180);
                    button7.ForeColor = Color.FromArgb(82, 82, 82);
                    button8.ForeColor = Color.FromArgb(82, 82, 82);
                    button27.ForeColor = Color.FromArgb(82, 82, 82);
                    button28.ForeColor = Color.FromArgb(82, 82, 82);

                    checkBox1.ForeColor = Color.FromArgb(82, 82, 82);
                    checkBox2.ForeColor = Color.FromArgb(82, 82, 82);
                    checkBox3.ForeColor = Color.FromArgb(82, 82, 82);
                    checkBox4.ForeColor = Color.FromArgb(82, 82, 82);
                    checkBox5.ForeColor = Color.FromArgb(82, 82, 82);
                    checkBox6.ForeColor = Color.FromArgb(82, 82, 82);
                    checkBox7.ForeColor = Color.FromArgb(82, 82, 82);
                    checkBox8.ForeColor = Color.FromArgb(82, 82, 82);

                    checkBox1.BackColor = Color.FromArgb(212, 220, 225);
                    checkBox2.BackColor = Color.FromArgb(212, 220, 225);
                    checkBox3.BackColor = Color.FromArgb(212, 220, 225);
                    checkBox4.BackColor = Color.FromArgb(212, 220, 225);
                    checkBox5.BackColor = Color.FromArgb(212, 220, 225);
                    checkBox6.BackColor = Color.FromArgb(212, 220, 225);
                    checkBox7.BackColor = Color.FromArgb(212, 220, 225);
                    checkBox8.BackColor = Color.FromArgb(212, 220, 225);

                    label11.BackColor = Color.FromArgb(212, 220, 225);
                    label12.BackColor = Color.FromArgb(212, 220, 225);
                    label13.BackColor = Color.FromArgb(212, 220, 225);
                    label10.BackColor = Color.FromArgb(212, 220, 225);

                    label11.ForeColor = Color.FromArgb(82, 82, 82);
                    label12.ForeColor = Color.FromArgb(82, 82, 82);
                    label13.ForeColor = Color.FromArgb(82, 82, 82);
                    label10.ForeColor = Color.FromArgb(82, 82, 82);

                    textBox1.BackColor = Color.FromArgb(171, 176, 180);
                    textBox1.ForeColor = Color.FromArgb(82, 82, 82);

                    SetActivePanel(panel4);
                }
                if (!checkBox2.Checked && !checkBox1.Checked)
                {
                    checkBox2.Checked = true; 
                }
            };

            checkBox3.CheckedChanged += (sender, e) =>
            {
                if (checkBox3.Checked)
                {
                    CheckBox3Active = true;

                    autoStart = true;

                    UpdateAutostart(this, autoStart);
                }

            };

            if (CheckBox3Active == true)
            {
                checkBox3.Checked = true;
            }

            checkBox4.CheckedChanged += (sender, e) =>
            {
                if (checkBox4.Checked)
                {
                    CheckBox4Active = true;
                    notificationsOnOff = true;
                    UpdateNotificaton(this, notificationsOnOff);
                }
                else
                {
                    CheckBox4Active = false;
                    notificationsOnOff = false;
                    UpdateNotificaton(this, notificationsOnOff);
                }
            };

            if (CheckBox4Active == true)
            {
                checkBox4.Checked = true;
            }

             if (Form1.settings.TextBoxValue == 0)
             {
                 textBox1.Text = "Введіть число";
             }
             else if (Form1.settings.TextBoxValue != 0)
             {
                 textBox1.Text = Convert.ToString(Form1.settings.TextBoxValue);
                 nonActiveTime = Form1.settings.TextBoxValue;
                 CheckBox5Active = false;
                 checkBox5.Checked = false;
                 CheckBox6Active = false;
                 checkBox6.Checked = false;
                 CheckBox7Active = false;
                 checkBox7.Checked = false;
             }

            textBox1.TextChanged += (sender, e) =>
            {
                if (!(string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Введіть число") || Form1.settings.TextBoxValue != 0)
                {
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
                }

                if (string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Введіть число" || Form1.settings.TextBoxValue == 0)
                {
                    textBoxText = "Введіть число";
                    TextBoxValue = 0;

                    UpdateTextBoxValue(this, TextBoxValue);

                    CheckBox5Active = true;
                    checkBox5.Checked = true;
                    nonActiveTime = 5;
                }
            };

            textBox1.Enter += (sender, e) =>
            {
                if (textBox1.Text == "Введіть число")
                {
                    textBox1.Text = "";
                }
            };

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private RichTextBox CreateRichTextLabel(Point location, string name, string text, Size size)
        {
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            // Встановлюємо кольори
            Color backColor = isDarkTheme ? Color.FromArgb(2, 14, 25) : Color.FromArgb(212, 220, 225);
            Color foreColor = isDarkTheme ? Color.FromArgb(186, 192, 196) : Color.FromArgb(82, 82, 82);

            // Створюємо сам RichTextBox
            var richText = new RichTextBox
            {
                Name = name,
                Location = location,
                Size = size,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = backColor,
                ForeColor = foreColor,
                Font = new Font("Segoe UI", 12),
                TabStop = false,
                Cursor = Cursors.Default,
                EnableAutoDragDrop = false,
                DetectUrls = false,
                ScrollBars = RichTextBoxScrollBars.None
            };

            // Підготувати RTF-текст з justify + 1.5 міжрядковим інтервалом
            string rtfColor = $@"\red{foreColor.R}\green{foreColor.G}\blue{foreColor.B}";
            // Заміна "\n" на "\par " для коректного переходу на новий абзац
            string rtfText = text.Replace("\n", @"\par ");

            // \sl360 -> 360 twips міжрядкового інтервалу = ~1.5 для 12pt (240twips)
            // \slmult1 -> застосувати цей інтервал точно
            richText.Rtf =
                @"{\rtf1\ansi\deff0" +
                $@"{{\colortbl ;{rtfColor};}}" +
                @"{\fonttbl{\f0 Segoe UI;}}" +
                // \fs24 = 12pt, \sl360\slmult1 = інтервал 1.5, \cf1 = колір, \qj = justify
                @"\fs24\sl360\slmult1\cf1\qj " +
                rtfText +
                "}";

            // Заборонити зміну масштабу колесиком миші
            richText.MouseWheel += (s, e) => ((HandledMouseEventArgs)e).Handled = true;

            this.Controls.Add(richText);
            return richText;
        }

        private void SetJustifiedText(RichTextBox rtb, string text, Color foreColor)
        {
            // Заміна "\n" на "\par " для коректних абзаців
            string rtfText = text.Replace("\n", @"\par ");

            string rtfColor = $@"\red{foreColor.R}\green{foreColor.G}\blue{foreColor.B}";
            string rtf =
                @"{\rtf1\ansi\deff0" +
                $@"{{\colortbl ;{rtfColor};}}" +
                @"{\fonttbl{\f0 Segoe UI;}}" +
                // Додаємо \sl360\slmult1 для 1.5‐інтервалу
                @"\fs24\sl360\slmult1\cf1\qj " +
                rtfText +
                "}";

            rtb.Rtf = rtf;
        }

        private void AboutProgram()
        {
            CreateMainLabel("label10", "Про програму", 550, 30, new Size(500, 50));

            ComboBox comboBox = new ComboBox();
            comboBox.BackColor = Color.FromArgb(186, 192, 196);
            comboBox.Items.Add("Як скористатися автоматизованим трекінгом?");
            comboBox.Items.Add("Чи є можливість ручного трекінгу?");
            comboBox.Items.Add("Як працює інтелектуальний аналіз?");
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Location = new Point(250, 250);
            comboBox.Size = new Size(450, 30);
            comboBox.ItemHeight = 30;
            comboBox.Font = new Font("Microsoft Sans Serif", 12);

            comboBox.SelectedIndex = 0;

            this.Controls.Add(comboBox);

            CreateRichTextLabel(new Point(250, 100), "label11", "Даний додаток призначений для автоматизованого тайм-трекінгу. Програма дозволяє користувачу зосередитися на роботі, автоматично фіксуючи робочу активність без потреби вручну запускати або зупиняти таймер.", new Size(600, 90));

            RichTextBox label12 = CreateRichTextLabel(
                new Point(250, 300),
                "label12",
                "Додайте у систему Ваші робочі файли. Для таких файлів буде фіксуватися час, який ви в них проводите. " +
                "За відсутності активності у файлі протягом певного часу — таймер зупиняється. " +
                "Ви також можете додатково підключити функцію інтелектуального трекінгу, яка доступна у вікні налаштування програми. " +
                "Результати виводитимуться на головному меню на основному лічильнику та відкриті до перегляду у вікні статистики.",
                new Size(600, 150)
            );

            RichTextBox label14 = CreateRichTextLabel(new Point(250, 220), "label13", "Найпоширеніші запитання та відповіді до них: ", new Size(600, 30));

            comboBox.SelectionChangeCommitted += (sender, e) =>
            {
                string selected = comboBox.SelectedItem.ToString();
                bool isDarkTheme = Form1.settings.ColorTheme == "dark";
                Color fore = isDarkTheme
                              ? Color.FromArgb(186, 192, 196)
                              : Color.FromArgb(82, 82, 82);

                switch (selected)
                {
                    case "Як скористатися автоматизованим трекінгом?":
                        SetJustifiedText(label12,
                            "Додайте у систему Ваші робочі файли. Для таких файлів буде фіксуватися час, який ви в них проводите. " +
                            "За відсутності активності у файлі протягом певного часу — таймер зупиняється. " +
                            "Ви також можете додатково підключити функцію інтелектуального трекінгу, яка доступна у вікні налаштування програми. " +
                            "Результати виводитимуться на головному меню на основному лічильнику та відкриті до перегляду у вікні статистики.",
                            fore);
                        break;

                    case "Чи є можливість ручного трекінгу?":
                        SetJustifiedText(label12,
                            "Так. Натисніть кнопку «Запустити таймер» у головному меню програми. " +
                            "Щоб зупинити трекінг, скористайтесь кнопкою «Зупинити таймер». " +
                            "Результати зберігатимуться в системі та будуть доступні для перегляду у вікні статистики.",
                            fore);
                        break;

                    case "Як працює інтелектуальний аналіз?":
                        SetJustifiedText(label12,
                            "Додаток відстежує вашу активність у браузері та порівнює її з файлами, доданими в програму. " +
                            "Якщо буде виявлено схожість, активність класифікується як робоча й додається до статистики.",
                            fore);
                        break;
                }
            };

            /* CreateRichTextLabel(new Point(250, 400), "label15",
                 "Виникли додаткові запитання чи труднощі? Пишіть лист на пошту - 7411641@stud.kai.edu.ua ",
                 new Size(600, 100));*/

            

            this.label13 = CreateMainLabel("label15", "Виникли додаткові запитання чи труднощі? Пишіть лист на пошту - 7411641@stud.kai.edu.ua ", 550, 500, new Size(700, 100));
            label13.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            label13.BackColor = Color.Transparent;


            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private readonly List<Rectangle> _blueRects = new List<Rectangle>
        {
            new Rectangle(250, 525, 600, 50)
        };

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            using (var brush = new SolidBrush(isDarkTheme ? Color.FromArgb(6, 40, 68) : Color.FromArgb(249, 249, 249)))
            {
                foreach (var r in _blueRects)
                    e.Graphics.FillRectangle(brush, r);
            }
        }

        private void RemoveBlueRectangles()
        {
            _blueRects.Clear();

            this.Invalidate();
        }

        private void InitBrowserInfo()
        {
            // 1) Завантажуємо один раз
            allUrls = JsonProcessing.LoadUrlData();

            // 2) Формуємо список унікальних дат
            availableDates = allUrls
                .Select(u => u.Timestamp.Date)
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            if (availableDates.Count == 0)
                return; // нічого не показувати

            // 3) За замовчуванням – остання дата
            currentIndex = availableDates.Count - 1;

            // 4) Прив’язуємо обробники
            button11.Click += ButtonPrevDate_Click;
            button12.Click += ButtonNextDate_Click;

            // 5) Першочергове наповнення
            UpdateDateView();
        }

        private void UpdateDateView()
        {
            var date = availableDates[currentIndex];

            label10.Text = "Робочі вебсторінки за " + date.ToString("dd.MM.yyyy");

            // Кнопки «‹» та «›» видно лише коли є відповідна дата
            button11.Visible = (currentIndex > 0);
            button12.Visible = (currentIndex < availableDates.Count - 1);

            // Очищаємо і наповнюємо панель
            panel2.Controls.Clear();
            selectedRows.Clear();
            button3.Visible = false;

            // Фільтруємо записи саме за поточною датою
            var todaysUrls = allUrls
                .Where(u => u.Timestamp.Date == date)
                .ToList();
            PopulateUrls(todaysUrls);
        }

        private void ButtonPrevDate_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                UpdateDateView();
            }
        }

        private void ButtonNextDate_Click(object sender, EventArgs e)
        {
            if (currentIndex < availableDates.Count - 1)
            {
                currentIndex++;
                UpdateDateView();
            }
        }

        private void PopulateUrls(List<UrlData> urls)
        {
            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            int rowH = 37, spacing = 5;
            int nameX = 0, pathX = 260;
            int y = 0;

            for (int i = 0; i < urls.Count; i++, y += rowH + spacing)
            {
                var lblUrl = ConfigureLabel3(
                    new Point(nameX, y),
                    new Size(255, rowH),
                    "lblUrl" + i);
                lblUrl.Text = urls[i].Url;
                lblUrl.Tag = i;
                lblUrl.ForeColor = isDarkTheme ? Color.FromArgb(186, 192, 196) : Color.FromArgb(82, 82, 82);
                lblUrl.BackColor = isDarkTheme ? Color.FromArgb(6, 40, 68) : Color.FromArgb(182, 192, 196);
                lblUrl.Click += Label_Click;
                lblUrl.Text = TrimWithEllipsis(urls[i].Url, lblUrl.Font, lblUrl.Width);

                var lblPageTitle = ConfigureLabel3(
                    new Point(pathX, y),
                    new Size(255, rowH),
                    "lblPageTitle" + i);
                lblPageTitle.Text = urls[i].PageTitle;
                lblPageTitle.Tag = i;
                lblPageTitle.ForeColor = isDarkTheme ? Color.FromArgb(186, 192, 196) : Color.FromArgb(82, 82, 82);
                lblPageTitle.BackColor = isDarkTheme ? Color.FromArgb(6, 40, 68) : Color.FromArgb(182, 192, 196);
                lblPageTitle.Click += Label_Click;
                lblPageTitle.Text = TrimWithEllipsis(urls[i].PageTitle, lblPageTitle.Font, lblPageTitle.Width);

                panel2.Controls.Add(lblUrl);
                panel2.Controls.Add(lblPageTitle);

                _labelToolTip.SetToolTip(lblUrl, urls[i].Url);
                _labelToolTip.SetToolTip(lblPageTitle, urls[i].PageTitle);
            }
        }

        private void BrowserInfo()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));

            bool isDarkTheme = Form1.settings.ColorTheme == "dark";

            this.panel2 = new System.Windows.Forms.Panel();

            this.label10 = CreateMainLabel("label10", "label10", 545, 30, new Size(530, 50));
            this.label10.ForeColor = isDarkTheme ? Color.FromArgb(186, 192, 196) : Color.FromArgb(82, 82, 82);

            this.panel2 = new Panel
            {
                Name = "panel2",
                Location = new Point(270, 100),
                Size = new Size(550, 377),
                BackColor = isDarkTheme ? Color.FromArgb(2, 14, 25) : Color.FromArgb(212, 220, 225),
                AutoScroll = true
            };
            this.Controls.Add(panel2);

            this.button3 = CreateButton("buttonDelete", "Видалити", new Point(685, 482), new Size(100, 25), this.buttonDelete2_Click);
            this.button3.Font = new Font("Segoe UI", 9);
            this.button11 = CreateButton("buttonPrevDate", "<", new Point(218, 550), new Size(41, 41), this.ButtonPrevDate_Click);
            this.button12 = CreateButton("buttonNextDate", ">", new Point(820, 550), new Size(41, 41), this.ButtonNextDate_Click);

            InitBrowserInfo();

        }

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.PictureBox pictureBox10;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;

        private System.Windows.Forms.Button button27;
        private System.Windows.Forms.Button button28;


        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;

        public System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.CheckBox checkBox2;
        public System.Windows.Forms.CheckBox checkBox3;
        public System.Windows.Forms.CheckBox checkBox4;
        public System.Windows.Forms.CheckBox checkBox5;
        public System.Windows.Forms.CheckBox checkBox6;
        public System.Windows.Forms.CheckBox checkBox7;
        public System.Windows.Forms.CheckBox checkBox8;

        private System.Windows.Forms.TextBox textBox1;

        

    }

}

