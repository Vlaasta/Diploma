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

        private void InitializeComponentMain()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button27 = new System.Windows.Forms.Button();
            this.button28 = new System.Windows.Forms.Button();

            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();

            this.BackColor = Color.FromArgb(2, 14, 25);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel6);
            // this.panel1.Controls.Add(this.panel2);
            this.panel1.BackColor = Color.FromArgb(4, 26, 44);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 733);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button7);
            this.panel3.Location = new System.Drawing.Point(0, 251);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(225, 46);
            this.panel3.TabIndex = 23;
            // 
            // button7
            // 
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button7.Location = new System.Drawing.Point(-8, 0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(200, 45);
            this.button7.TabIndex = 21;
            this.button7.Text = "Статистика";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.FlatAppearance.BorderSize = 0;
            //
            //
            //
             this.panel2.Controls.Add(this.button27);
             this.panel2.Location = new System.Drawing.Point(0, 150);
             this.panel2.Name = "panel2";
             this.panel2.Size = new System.Drawing.Size(225, 46);
            // this.panel2.TabIndex = 31;
            //
            //
            //
            this.button27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button27.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button27.Location = new System.Drawing.Point(-8, 0);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(200, 45);
            // this.button27.TabIndex = 21;
            this.button27.Text = "Налаштування";
            this.button27.UseVisualStyleBackColor = true;
            this.button27.Click += new System.EventHandler(this.button27_Click);
            this.button27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button27.FlatAppearance.BorderSize = 0;

            this.panel6.Controls.Add(this.button28);
            this.panel6.Location = new System.Drawing.Point(0, 100);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(225, 46);
            //this.panel6.TabIndex = 31;


            this.button28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button28.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button28.Location = new System.Drawing.Point(-8, 0);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(200, 45);
            // this.button27.TabIndex = 21;
            this.button28.Text = "Про програму";
            this.button28.UseVisualStyleBackColor = true;
            this.button28.Click += new System.EventHandler(this.button28_Click);
            this.button28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button28.FlatAppearance.BorderSize = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button8);
            this.panel4.Location = new System.Drawing.Point(0, 200);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(225, 46);
            this.panel4.TabIndex = 24;
            // 
            // button8
            // 
            this.button8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button8.BackgroundImage")));
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button8.Location = new System.Drawing.Point(-8, 0);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(200, 45);
            this.button8.TabIndex = 22;
            this.button8.Text = "Головне меню";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.FlatAppearance.BorderSize = 0;

            if (CheckBox2Active == true || Form1.settings.ColorTheme == "light")
            {
                this.BackColor = Color.FromArgb(212, 220, 225);
                this.panel1.BackColor = Color.FromArgb(171, 176, 180);
                this.button7.ForeColor = Color.FromArgb(82, 82, 82);
                this.button8.ForeColor = Color.FromArgb(82, 82, 82);
                this.button27.ForeColor = Color.FromArgb(82, 82, 82);

                this.button28.ForeColor = Color.FromArgb(82, 82, 82);

            }

            if (CheckBox1Active == true || Form1.settings.ColorTheme == "dark")
            {
                this.BackColor = Color.FromArgb(2, 14, 25);

                this.panel1.BackColor = Color.FromArgb(4, 26, 44);

                this.button7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button27.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            }
            // 
            // Form1
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            //this.ClientSize = new System.Drawing.Size(1044, 734);
            //this.Size = new Size(800, 600);
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

        private void InitializeComponentMainMenu()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            InitializeComponentMain();
            // this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button(); //кнопка "вверх"
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

            // this.panel2.SuspendLayout();
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

            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Location = new System.Drawing.Point(280, 327);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(515, 37);
            this.button1.TabIndex = 2;
            this.button1.Text = "Додати проєкт";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.BackColor = Color.FromArgb(6, 40, 68);
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.FlatAppearance.BorderSize = 0;

            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button2.Location = new System.Drawing.Point(438, 250);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 38);
            this.button2.TabIndex = 20;
            this.button2.Text = "Запустити таймер";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.BackColor = Color.FromArgb(6, 40, 68);
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.FlatAppearance.BorderSize = 0;

            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button3.Location = new System.Drawing.Point(224, 413);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(50, 37);
            this.button3.Text = "Вверх";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.BackColor = Color.FromArgb(6, 40, 68);

            this.button4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button4.Location = new System.Drawing.Point(801, 413);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(50, 37);
            this.button4.TabIndex = 22;
            this.button4.Text = "-";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            this.button4.BackColor = Color.FromArgb(6, 40, 68);
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.FlatAppearance.BorderSize = 0;

            this.button5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button5.Location = new System.Drawing.Point(801, 456);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(50, 37);
            this.button5.TabIndex = 23;
            this.button5.Text = "-";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.BackColor = Color.FromArgb(6, 40, 68);
            this.button5.Click += new System.EventHandler(this.button5_Click);
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.FlatAppearance.BorderSize = 0;

            this.button6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button6.Location = new System.Drawing.Point(801, 499);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(50, 37);
            this.button6.TabIndex = 24;
            this.button6.Text = "-";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.BackColor = Color.FromArgb(6, 40, 68);
            this.button6.Click += new System.EventHandler(this.button6_Click);
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.FlatAppearance.BorderSize = 0;

            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button10.Location = new System.Drawing.Point(224, 499);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(50, 37);
            this.button10.Text = "Вниз";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            this.button10.BackColor = Color.FromArgb(6, 40, 68);

            // Налаштування для pictureBox1
            this.pictureBox1.Image = Properties.Resources.ClockForDarkTheme;
            this.ConfigurePictureBox(pictureBox1, new Point(456, 58), new Size(164, 163), backColor: Color.Transparent);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabStop = false;

            // Налаштування для pictureBox2 - pictureBox5 (з однаковим стилем)
            var commonBackColor = Color.FromArgb(6, 40, 68);
            this.ConfigurePictureBox(pictureBox2, new Point(280, 370), new Size(255, 37), backgroundImage: (Image)resources.GetObject("pictureBox2.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox3, new Point(280, 413), new Size(255, 37), backgroundImage: (Image)resources.GetObject("pictureBox3.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox4, new Point(280, 456), new Size(255, 37), backgroundImage: (Image)resources.GetObject("pictureBox4.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox5, new Point(280, 499), new Size(255, 37), backgroundImage: (Image)resources.GetObject("pictureBox5.BackgroundImage"), backColor: commonBackColor);

            // Налаштування для pictureBox6 - pictureBox8 (з однаковим стилем)
            this.ConfigurePictureBox(pictureBox6, new Point(540, 413), new Size(255, 37), backgroundImage: (Image)resources.GetObject("pictureBox6.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox7, new Point(540, 456), new Size(255, 37), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox8, new Point(540, 499), new Size(255, 37), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox9, new Point(540, 370), new Size(255, 37), backgroundImage: (Image)resources.GetObject("pictureBox2.BackgroundImage"), backColor: commonBackColor);

            this.label2 = ConfigureLabel(new Point(500, 133), new Size(65, 26), "00:00:00", backColor: Color.FromArgb(186, 192, 196));
            this.label2.ForeColor = Color.FromArgb(0, 0, 0);
            this.label1 = ConfigureLabel(new Point(380, 380), new Size(65, 16), "Назва", backColor: Color.FromArgb(6, 40, 68));
            this.label9 = ConfigureLabel(new Point(640, 380), new Size(65, 16), "Шлях", backColor: Color.FromArgb(6, 40, 68));
            this.label7 = ConfigureLabel(new Point(540, 456), new Size(pictureBox7.Width, pictureBox7.Height), "label7", backColor: Color.FromArgb(6, 40, 68));
            this.label3 = ConfigureLabel(new Point(280, 413), new Size(pictureBox3.Width, pictureBox3.Height), "label3", image: (Image)resources.GetObject("label3.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label4 = ConfigureLabel(new Point(280, 456), new Size(pictureBox4.Width, pictureBox4.Height), "label4", image: (Image)resources.GetObject("label4.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label5 = ConfigureLabel(new Point(280, 499), new Size(pictureBox5.Width, pictureBox5.Height), "label5", image: (Image)resources.GetObject("label5.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label6 = ConfigureLabel(new Point(540, 413), new Size(pictureBox6.Width, pictureBox6.Height), "label6", image: (Image)resources.GetObject("label6.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label8 = ConfigureLabel(new Point(540, 499), new Size(pictureBox8.Width, pictureBox8.Height), "label8", image: (Image)resources.GetObject("label8.Image"), backColor: Color.FromArgb(6, 40, 68));

            if (CheckBox2Active == true || Form1.settings.ColorTheme == "light")
            {
                this.button10.BackColor = Color.FromArgb(182, 192, 196);
                this.button10.ForeColor = Color.FromArgb(82, 82, 82);

                for (int i = 1; i <= 6; i++)
                {
                    Control button = this.Controls["button" + i];
                    button.BackColor = Color.FromArgb(182, 192, 196);
                    button.ForeColor = Color.FromArgb(82, 82, 82);
                }

                pictureBox1.Image = Properties.Resources.ClockForLightTheme;

                for (int i = 2; i <= 8; i++)
                {
                    Control pictureBox = this.Controls["pictureBox" + i];
                    pictureBox.BackColor = Color.FromArgb(182, 192, 196);
                }

                for (int i = 1; i <= 8; i++)
                {
                    Control label = this.Controls["label" + i];
                    label.BackColor = Color.FromArgb(182, 192, 196);
                    label.ForeColor = Color.FromArgb(82, 82, 82);
                }
            }

            if (CheckBox1Active == true || Form1.settings.ColorTheme == "dark")
            {
                this.button10.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button10.BackColor = Color.FromArgb(6, 40, 68);

                for (int i = 1; i <= 6; i++)
                {
                    Control button = this.Controls["button" + i];
                    if (button != null)
                    {
                        button.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                        button.BackColor = Color.FromArgb(6, 40, 68);
                    }
                }

                this.pictureBox1.Image = Properties.Resources.ClockForDarkTheme;

                for (int i = 1; i <= 8; i++)
                {
                    Control pictureBox = this.Controls["pictureBox" + i];
                    if (pictureBox != null)
                    {
                        pictureBox.BackColor = Color.FromArgb(6, 40, 68);
                    }
                }

                if (label1 != null)
                {
                    label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                    label1.BackColor = Color.FromArgb(6, 40, 68);
                }

                if (label2 != null)
                {
                    label2.BackColor = Color.FromArgb(186, 192, 196);
                }

                for (int i = 3; i <= 8; i++)
                {
                    Control label = this.Controls["label" + i];
                    if (label != null)
                    {
                        label.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                        label.BackColor = Color.FromArgb(6, 40, 68);
                    }
                }

            }

           /* ComboBox comboBox = new ComboBox();
            comboBox.Items.Add("За день");
            comboBox.Items.Add("За тиждень");
            comboBox.Items.Add("За місяць");
            comboBox.Items.Add("За рік");
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // Встановлюємо координати: X = 50, Y = 100
            comboBox.Location = new Point(600, 600);
            comboBox.Size = new Size(150, 30);
           // comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox.ItemHeight = 30; // Висота кожного пункту
            comboBox.Font = new Font("Segoe UI", 12);

            comboBox.SelectionChangeCommitted += (s, e) =>
            {
                string selected = comboBox.SelectedItem.ToString();

                if (selected == "За тиждень")
                {
                    MessageBox.Show("Ви вибрали особливу опцію!");
                }
                else
                {
                    MessageBox.Show("Вибрано: " + selected);
                }
            };

            this.Controls.Add(comboBox);*/

            // 
            // Form1
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(900,650);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button10);
           
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
           // this.panel2.ResumeLayout(false);
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

        private System.Windows.Forms.Label ConfigureLabel(Point location, Size size, string text, Image image = null, Color? backColor = null, Color? foreColor = null)
        {
            var label = new System.Windows.Forms.Label();
            label.Location = location;
            label.Size = size;
            label.Text = text;
            if (image != null)
            {
                label.Image = image;
            }
            label.BackColor = backColor ?? Color.Transparent;
            if (label != label2)
            {
                label.ForeColor = foreColor ?? System.Drawing.SystemColors.ActiveCaption;
            }
            label.AutoEllipsis = true;  // Додає "..." якщо текст не влізе
            label.TextAlign = ContentAlignment.MiddleLeft;
            this.Controls.Add(label);
            return label;
        }

        private void ConfigurePictureBox(PictureBox pictureBox, Point location, Size size, Image backgroundImage = null, Color? backColor = null)
        {
            pictureBox.Location = location;
            pictureBox.Size = size;
            if (backgroundImage != null)
            {
                pictureBox.BackgroundImage = backgroundImage;
            }
            pictureBox.BackColor = backColor ?? Color.Transparent;
            pictureBox.TabStop = false;
        }

        private void buildChart(List<DateTime> xPoints, List<int> yPoints)
        {
            var plotView = new PlotView
            {
                Dock = DockStyle.None,
                Size = new Size(600, 400),
                Location = new Point(225, 150)
            };

            var plotModel = new PlotModel
            {
                PlotAreaBorderColor = OxyColor.FromRgb(2, 14, 25),
                PlotAreaBorderThickness = new OxyThickness(1)
            };

            var lineSeries = new LineSeries
            {
                Title = "Час (секунди)",
                MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(159, 183, 213)
            };

            for (int i = 0; i < xPoints.Count; i++)
            {
                // Перетворення DateTime в double
                var dateAsDouble = DateTimeAxis.ToDouble(xPoints[i]);
                lineSeries.Points.Add(new DataPoint(dateAsDouble, yPoints[i]));
            }

            plotModel.Series.Add(lineSeries);

            // Ось X
            var axisX = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Дата",
                TitleColor = OxyColor.FromRgb(159, 183, 213),
                TextColor = OxyColor.FromRgb(159, 183, 213),
                StringFormat = "dd.MM.yyyy",
                MajorGridlineColor = OxyColor.FromRgb(159, 183, 213),
                MinorGridlineColor = OxyColor.FromRgb(159, 183, 213),
                MajorGridlineThickness = 1,  // Товщина основної сітки
                MinorGridlineThickness = 0.5, // Товщина допоміжної сітки
                IsZoomEnabled = false,
                AxislineColor = OxyColor.FromRgb(159, 183, 213),
                TicklineColor = OxyColor.FromRgb(159, 183, 213),
                IsAxisVisible = true,
                AxisTitleDistance = 10 // Відстань підпису від осі Y (можна змінити за потреби)
            };

            // Ось Y
            var axisY = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Секунди",
                TitleColor = OxyColor.FromRgb(159, 183, 213),
                TextColor = OxyColor.FromRgb(159, 183, 213),
                MajorGridlineColor = OxyColor.FromRgb(159, 183, 213),
                MinorGridlineColor = OxyColor.FromRgb(81, 99, 119),
                MajorGridlineThickness = 1,
                MinorGridlineThickness = 0.5,
                MajorStep = 100, // Крок основних ліній
                MinorStep = 20, // Крок допоміжних ліній
                IsZoomEnabled = false,
                AxislineColor = OxyColor.FromRgb(159, 183, 213),
                TicklineColor = OxyColor.FromRgb(159, 183, 213),
            };

            var MinimumY = yPoints.Min() - 2;  // Мінімум на основі даних
            var MaximumY = yPoints.Max() + 2;  // Максимум на основі даних

            var MinimumX = DateTimeAxis.ToDouble(xPoints.Min().AddDays(-2)); // Мінімальна дата на основі даних
            var MaximumX = DateTimeAxis.ToDouble(xPoints.Max().AddDays(+2)); // Максимальна дата на основі даних

            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            // Побудова сітки окремо (не прив'язано до основних серій)
            for (double x = MinimumX; x <= MaximumX; x += (MaximumX - MinimumX) / 10)
            {
                var gridLine = new LineAnnotation
                {
                    Type = LineAnnotationType.Vertical,
                    X = x,
                    Color = OxyColor.FromRgb(81, 99, 119), // Колір сітки
                    LineStyle = LineStyle.Solid,
                    StrokeThickness = 1.5  // Товща лінія для кращої видимості
                };
                plotModel.Annotations.Add(gridLine);
            }

            // Додавання власної сітки
            for (double y = MinimumY; y <= MaximumY; y += axisY.MinorStep) // Використовуємо MinorStep для допоміжних ліній
            {
                var gridLine = new LineAnnotation
                {
                    Type = LineAnnotationType.Horizontal,
                    Y = y,
                    Color = OxyColor.FromRgb(81, 99, 119), // Колір сітки
                    LineStyle = y % axisY.MajorStep == 0 ? LineStyle.Solid : LineStyle.Dot, // Основна — суцільна, допоміжна — пунктирна
                    StrokeThickness = y % axisY.MajorStep == 0 ? 1.5 : 0.5
                };
                plotModel.Annotations.Add(gridLine);
            }

            plotView.Model = plotModel;

            this.Controls.Add(plotView);

            this.label9 = new System.Windows.Forms.Label();
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Location = new System.Drawing.Point(290, 570);
            this.label9.Name = "label9";
            this.label9.Font = new Font("Arial", 16, FontStyle.Regular); // Arial, 16 пт, курсив
            this.label9.Size = new System.Drawing.Size(180, 180);
            this.label9.BackColor = Color.FromArgb(2, 14, 25);
            this.label9.Text = "label9";
            this.Controls.Add(this.label9);

            if (CheckBox1Active == true)
            {
                this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.label9.BackColor = Color.FromArgb(2, 14, 25);
                plotModel.PlotAreaBorderColor = OxyColor.FromRgb(2, 14, 25);
                lineSeries.Color = OxyColor.FromRgb(159, 183, 213);

                axisX.TitleColor = OxyColor.FromRgb(159, 183, 213);
                axisX.TextColor = OxyColor.FromRgb(159, 183, 213);
                axisX.MajorGridlineColor = OxyColor.FromRgb(159, 183, 213);
                axisX.MinorGridlineColor = OxyColor.FromRgb(159, 183, 213);
                axisX.AxislineColor = OxyColor.FromRgb(159, 183, 213);
                axisX.TicklineColor = OxyColor.FromRgb(159, 183, 213);

                axisY.TitleColor = OxyColor.FromRgb(159, 183, 213);
                axisY.TextColor = OxyColor.FromRgb(159, 183, 213);
                axisY.MajorGridlineColor = OxyColor.FromRgb(159, 183, 213);
                axisY.MinorGridlineColor = OxyColor.FromRgb(159, 183, 213);
                axisY.AxislineColor = OxyColor.FromRgb(159, 183, 213);
                axisY.TicklineColor = OxyColor.FromRgb(159, 183, 213);
            }

            if (CheckBox2Active == true)
            {
                this.label9.ForeColor = Color.FromArgb(82, 82, 82);
                this.label9.BackColor = Color.FromArgb(212, 220, 225);

                plotModel.PlotAreaBorderColor = OxyColor.FromRgb(212, 220, 225);
                lineSeries.Color = OxyColor.FromRgb(82, 82, 82);

                axisX.TitleColor = OxyColor.FromRgb(82, 82, 82);
                axisX.TextColor = OxyColor.FromRgb(82, 82, 82);
                axisX.MajorGridlineColor = OxyColor.FromRgb(82, 82, 82);
                axisX.MinorGridlineColor = OxyColor.FromRgb(82, 82, 82);
                axisX.AxislineColor = OxyColor.FromRgb(82, 82, 82);
                axisX.TicklineColor = OxyColor.FromRgb(82, 82, 82);

                axisY.TitleColor = OxyColor.FromRgb(82, 82, 82);
                axisY.TextColor = OxyColor.FromRgb(82, 82, 82);
                axisY.MajorGridlineColor = OxyColor.FromRgb(82, 82, 82);
                axisY.MinorGridlineColor = OxyColor.FromRgb(82, 82, 82);
                axisY.AxislineColor = OxyColor.FromRgb(82, 82, 82);
                axisY.TicklineColor = OxyColor.FromRgb(82, 82, 82);
            }
        }

        private void buildChart2(List<DateTime> xPoints, List<int> yPoints)
        {
            var plotView = new PlotView
            {
                Dock = DockStyle.None,
                Size = new Size(600, 400),
                Location = new Point(225, 150)
            };

            var plotModel = new PlotModel
            {
                PlotAreaBorderColor = OxyColor.FromRgb(2, 14, 25),
                PlotAreaBorderThickness = new OxyThickness(1)
            };

            var lineSeries = new LineSeries
            {
                Title = "Час (секунди)",
                MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(159, 183, 213)
            };

            for (int i = 0; i < xPoints.Count; i++)
            {
                // Перетворення DateTime в double
                var dateAsDouble = DateTimeAxis.ToDouble(xPoints[i]);
                lineSeries.Points.Add(new DataPoint(dateAsDouble, yPoints[i]));
            }

            plotModel.Series.Add(lineSeries);

            // Ось X
            var axisX = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Час доби",
                StringFormat = "HH:mm",                    // години:хвилини
                IntervalType = DateTimeIntervalType.Hours,  // основні мітки через годину
                MajorStep = 1,                           // 1 година
                MinorIntervalType = DateTimeIntervalType.Minutes,
                MinorStep = 15,                          // кожні 15 хв
                IsZoomEnabled = false,
                // …інші налаштування кольорів та сітки з вашого прикладу…
            };

            // Ось Y
            var axisY = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Секунди",
                TitleColor = OxyColor.FromRgb(159, 183, 213),
                TextColor = OxyColor.FromRgb(159, 183, 213),
                MajorGridlineColor = OxyColor.FromRgb(159, 183, 213),
                MinorGridlineColor = OxyColor.FromRgb(81, 99, 119),
                MajorGridlineThickness = 1,
                MinorGridlineThickness = 0.5,
                MajorStep = 100, // Крок основних ліній
                MinorStep = 20, // Крок допоміжних ліній
                IsZoomEnabled = false,
                AxislineColor = OxyColor.FromRgb(159, 183, 213),
                TicklineColor = OxyColor.FromRgb(159, 183, 213),
            };

            var MinimumY = yPoints.Min() - 2;  // Мінімум на основі даних
            var MaximumY = yPoints.Max() + 2;  // Максимум на основі даних

            var MinimumX = DateTimeAxis.ToDouble(xPoints.Min().AddDays(-2)); // Мінімальна дата на основі даних
            var MaximumX = DateTimeAxis.ToDouble(xPoints.Max().AddDays(+2)); // Максимальна дата на основі даних

            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            // Побудова сітки окремо (не прив'язано до основних серій)
            for (double x = MinimumX; x <= MaximumX; x += (MaximumX - MinimumX) / 10)
            {
                var gridLine = new LineAnnotation
                {
                    Type = LineAnnotationType.Vertical,
                    X = x,
                    Color = OxyColor.FromRgb(81, 99, 119), // Колір сітки
                    LineStyle = LineStyle.Solid,
                    StrokeThickness = 1.5  // Товща лінія для кращої видимості
                };
                plotModel.Annotations.Add(gridLine);
            }

            // Додавання власної сітки
            for (double y = MinimumY; y <= MaximumY; y += axisY.MinorStep) // Використовуємо MinorStep для допоміжних ліній
            {
                var gridLine = new LineAnnotation
                {
                    Type = LineAnnotationType.Horizontal,
                    Y = y,
                    Color = OxyColor.FromRgb(81, 99, 119), // Колір сітки
                    LineStyle = y % axisY.MajorStep == 0 ? LineStyle.Solid : LineStyle.Dot, // Основна — суцільна, допоміжна — пунктирна
                    StrokeThickness = y % axisY.MajorStep == 0 ? 1.5 : 0.5
                };
                plotModel.Annotations.Add(gridLine);
            }

            plotView.Model = plotModel;

            this.Controls.Add(plotView);

            this.label9 = new System.Windows.Forms.Label();
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Location = new System.Drawing.Point(290, 570);
            this.label9.Name = "label9";
            this.label9.Font = new Font("Arial", 16, FontStyle.Regular); // Arial, 16 пт, курсив
            this.label9.Size = new System.Drawing.Size(180, 180);
            this.label9.BackColor = Color.FromArgb(2, 14, 25);
            this.label9.Text = "label9";
            this.Controls.Add(this.label9);

            if (CheckBox1Active == true)
            {
                this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.label9.BackColor = Color.FromArgb(2, 14, 25);
                plotModel.PlotAreaBorderColor = OxyColor.FromRgb(2, 14, 25);
                lineSeries.Color = OxyColor.FromRgb(159, 183, 213);

                axisX.TitleColor = OxyColor.FromRgb(159, 183, 213);
                axisX.TextColor = OxyColor.FromRgb(159, 183, 213);
                axisX.MajorGridlineColor = OxyColor.FromRgb(159, 183, 213);
                axisX.MinorGridlineColor = OxyColor.FromRgb(159, 183, 213);
                axisX.AxislineColor = OxyColor.FromRgb(159, 183, 213);
                axisX.TicklineColor = OxyColor.FromRgb(159, 183, 213);

                axisY.TitleColor = OxyColor.FromRgb(159, 183, 213);
                axisY.TextColor = OxyColor.FromRgb(159, 183, 213);
                axisY.MajorGridlineColor = OxyColor.FromRgb(159, 183, 213);
                axisY.MinorGridlineColor = OxyColor.FromRgb(159, 183, 213);
                axisY.AxislineColor = OxyColor.FromRgb(159, 183, 213);
                axisY.TicklineColor = OxyColor.FromRgb(159, 183, 213);
            }

            if (CheckBox2Active == true)
            {
                this.label9.ForeColor = Color.FromArgb(82, 82, 82);
                this.label9.BackColor = Color.FromArgb(212, 220, 225);

                plotModel.PlotAreaBorderColor = OxyColor.FromRgb(212, 220, 225);
                lineSeries.Color = OxyColor.FromRgb(82, 82, 82);

                axisX.TitleColor = OxyColor.FromRgb(82, 82, 82);
                axisX.TextColor = OxyColor.FromRgb(82, 82, 82);
                axisX.MajorGridlineColor = OxyColor.FromRgb(82, 82, 82);
                axisX.MinorGridlineColor = OxyColor.FromRgb(82, 82, 82);
                axisX.AxislineColor = OxyColor.FromRgb(82, 82, 82);
                axisX.TicklineColor = OxyColor.FromRgb(82, 82, 82);

                axisY.TitleColor = OxyColor.FromRgb(82, 82, 82);
                axisY.TextColor = OxyColor.FromRgb(82, 82, 82);
                axisY.MajorGridlineColor = OxyColor.FromRgb(82, 82, 82);
                axisY.MinorGridlineColor = OxyColor.FromRgb(82, 82, 82);
                axisY.AxislineColor = OxyColor.FromRgb(82, 82, 82);
                axisY.TicklineColor = OxyColor.FromRgb(82, 82, 82);
            }
        }
        private void buildDailyChart(List<UrlData> urlDataList, DateTime selectedDate)
        {
            // 1) Фільтрація за сьогоднішньою датою
            var today = DateTime.Today; // наприклад, 07.05.2025
            var todayData = urlDataList
               .Where(d => d.Timestamp.Date == selectedDate.Date)
               .ToList();

            // 2) Агрегуємо по годинах 0..23: сумуємо TimeSpent і переводимо в хвилини
            var hoursOfDay = Enumerable.Range(0, 24).ToList();
            var spentMinutes = hoursOfDay
                .Select(h => todayData
                                .Where(d => d.Timestamp.Hour == h)
                                .Sum(d => d.TimeSpent)
                            / 60.0)  // секунди → хвилини
                .ToList();

            // 3) Підготувати PlotView і PlotModel
            var plotView = new PlotView
            {
                Dock = DockStyle.None,
                Size = new Size(600, 400),
                Location = new Point(225, 150)
            };

            var plotModel = new PlotModel
            {
                PlotAreaBorderColor = OxyColor.FromRgb(2, 14, 25),
                PlotAreaBorderThickness = new OxyThickness(1)
            };

            // 4) Серія: X = година доби, Y = хвилини
            var lineSeries = new LineSeries
            {
                Title = "Час проведений (хвилини)",
                MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(159, 183, 213)
            };
            for (int i = 0; i < hoursOfDay.Count; i++)
            {
                lineSeries.Points.Add(new DataPoint(hoursOfDay[i], spentMinutes[i]));
            }
            plotModel.Series.Add(lineSeries);

            // 5) Ось X — години доби
            var axisX = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Година доби",
                TitleColor = OxyColor.FromRgb(159, 183, 213),
                TextColor = OxyColor.FromRgb(159, 183, 213),
                MajorGridlineColor = OxyColor.FromRgb(159, 183, 213),
                MinorGridlineColor = OxyColor.FromRgb(159, 183, 213),
                MajorGridlineThickness = 1,
                MinorGridlineThickness = 0.5,
                MajorStep = 1,
                MinorStep = 1,
                Minimum = -0.5,
                Maximum = 23.5,
                IsZoomEnabled = false,
                AxislineColor = OxyColor.FromRgb(159, 183, 213),
                TicklineColor = OxyColor.FromRgb(159, 183, 213)
            };

            // 6) Ось Y — хвилини
            double maxY = Math.Ceiling(spentMinutes.Max() + 1);
            // Встановлюємо кроки: приблизно 10 основних ліній
            double majorStep = Math.Ceiling(maxY / 10.0);
            if (majorStep < 1) majorStep = 1;
            double minorStep = majorStep / 2.0;

            var axisY = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Час проведений (хвилини)",
                TitleColor = OxyColor.FromRgb(159, 183, 213),
                TextColor = OxyColor.FromRgb(159, 183, 213),
                MajorGridlineColor = OxyColor.FromRgb(159, 183, 213),
                MinorGridlineColor = OxyColor.FromRgb(81, 99, 119),
                MajorGridlineThickness = 1,
                MinorGridlineThickness = 0.5,
                MajorStep = majorStep,
                MinorStep = minorStep,
                Minimum = 0,
                Maximum = maxY,
                IsZoomEnabled = false,
                AxislineColor = OxyColor.FromRgb(159, 183, 213),
                TicklineColor = OxyColor.FromRgb(159, 183, 213)
            };

            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            // 7) Додаткова сітка
            // Вертикальні лінії за годинами
            foreach (var h in hoursOfDay)
            {
                plotModel.Annotations.Add(new LineAnnotation
                {
                    Type = LineAnnotationType.Vertical,
                    X = h,
                    Color = OxyColor.FromRgb(81, 99, 119),
                    LineStyle = LineStyle.Solid,
                    StrokeThickness = 1.5
                });
            }
            // Горизонтальні лінії за хвилинами
            for (double y = 0; y <= maxY; y += minorStep)
            {
                plotModel.Annotations.Add(new LineAnnotation
                {
                    Type = LineAnnotationType.Horizontal,
                    Y = y,
                    Color = OxyColor.FromRgb(81, 99, 119),
                    LineStyle = (y % majorStep == 0) ? LineStyle.Solid : LineStyle.Dot,
                    StrokeThickness = (y % majorStep == 0) ? 1.5 : 0.5
                });
            }

            // 8) Додаємо на форму
            plotView.Model = plotModel;
            this.Controls.Add(plotView);

            this.label9 = new System.Windows.Forms.Label();
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Location = new System.Drawing.Point(290, 570);
            this.label9.Name = "label9";
            this.label9.Font = new Font("Arial", 16, FontStyle.Regular); // Arial, 16 пт, курсив
            this.label9.Size = new System.Drawing.Size(180, 180);
            this.label9.BackColor = Color.FromArgb(2, 14, 25);
            this.label9.Text = "label9";
            this.Controls.Add(this.label9);
        }

        private void buildDailyChart(List<Session> sessions, DateTime selectedDate)
        {
            // Парсимо рядки Start/Stop у DateTime
            var today = selectedDate.Date;
            var parsed = sessions
                .Select(s =>
                {
                    if (!DateTime.TryParseExact(s.Start, "HH:mm:ss", CultureInfo.InvariantCulture,
                                                DateTimeStyles.None, out var t1)) return (Start: DateTime.MinValue, Stop: DateTime.MinValue);
                    if (!DateTime.TryParseExact(s.Stop, "HH:mm:ss", CultureInfo.InvariantCulture,
                                                DateTimeStyles.None, out var t2)) return (Start: DateTime.MinValue, Stop: DateTime.MinValue);
                    var dt1 = today.Add(t1.TimeOfDay);
                    var dt2 = today.Add(t2.TimeOfDay);
                    if (dt2 < dt1) dt2 = dt2.AddDays(1);  // через північ
            return (Start: dt1, Stop: dt2);
                })
                .Where(p => p.Start != DateTime.MinValue)
                .OrderBy(p => p.Start)
                .ToList();

            // Підготувати PlotView / PlotModel
            var plotView = new PlotView
            {
                Dock = DockStyle.None,
                Size = new Size(600, 400),
                Location = new Point(225, 150)
            };
            var plotModel = new PlotModel
            {
                PlotAreaBorderColor = OxyColor.FromRgb(2, 14, 25),
                PlotAreaBorderThickness = new OxyThickness(1)
            };

            // Серія «зубчастого» графіку (X ‒ години як число, Y ‒ хвилини)
            var series = new LineSeries
            {
                Title = "Час роботи (хв)",
                MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(159, 183, 213),
                StrokeThickness = 2
            };

            // Починаємо у 00:00 з нуля
            series.Points.Add(new DataPoint(0, 0));

            foreach (var seg in parsed)
            {
                double startH = (seg.Start - today).TotalHours;
                double stopH = (seg.Stop - today).TotalHours;
                double elapsedMin = (seg.Stop - seg.Start).TotalSeconds / 60.0;

                // 0 у момент старту
                series.Points.Add(new DataPoint(startH, 0));
                // пікова точка
                series.Points.Add(new DataPoint(stopH, elapsedMin));
                // і відразу спад до нуля
                series.Points.Add(new DataPoint(stopH, 0));
            }

            // Останній нуль ― до кінця доби
            series.Points.Add(new DataPoint(23.5, 0));

            plotModel.Series.Add(series);

            // Ось X — лінійна від −0.5 до 23.5, формат ‒ hh:mm
            var axisX = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Година доби",
                Minimum = -0.5,
                Maximum = 23.5,
                MajorStep = 1,
                MinorStep = 1,
                IsZoomEnabled = false,
                LabelFormatter = v => TimeSpan.FromHours(v).ToString(@"hh\:mm"),
                Angle = -45,
                TitleColor = OxyColor.FromRgb(159, 183, 213),
                TextColor = OxyColor.FromRgb(159, 183, 213),
                MajorGridlineColor = OxyColor.FromRgb(159, 183, 213),
                MinorGridlineColor = OxyColor.FromRgb(159, 183, 213),
                AxislineColor = OxyColor.FromRgb(159, 183, 213),
                TicklineColor = OxyColor.FromRgb(159, 183, 213)
            };
            plotModel.Axes.Add(axisX);

            // Ось Y — від 0 до максимуму + запас
            double maxMin = series.Points.Max(p => p.Y);
            double maxY = Math.Ceiling(maxMin + 1);
            double majorY = Math.Max(1, Math.Ceiling(maxY / 10.0));
            double minorY = majorY / 2.0;

            var axisY = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Час роботи (хв)",
                Minimum = -1,
                Maximum = maxY,
                MajorStep = majorY,
                MinorStep = minorY,
                IsZoomEnabled = false,
                TitleColor = OxyColor.FromRgb(159, 183, 213),
                TextColor = OxyColor.FromRgb(159, 183, 213),
                MajorGridlineColor = OxyColor.FromRgb(159, 183, 213),
                MinorGridlineColor = OxyColor.FromRgb(81, 99, 119),
                AxislineColor = OxyColor.FromRgb(159, 183, 213),
                TicklineColor = OxyColor.FromRgb(159, 183, 213)
            };
            plotModel.Axes.Add(axisY);

            // Додаткова сітка: вертикальні лінії на кожну годину
            for (int h = 0; h < 24; h++)
            {
                plotModel.Annotations.Add(new LineAnnotation
                {
                    Type = LineAnnotationType.Vertical,
                    X = h,
                    Color = OxyColor.FromRgb(81, 99, 119),
                    StrokeThickness = 1.5,
                    LineStyle = LineStyle.Solid
                });
            }
            // Горизонтальні лінії за дрібним кроком
            for (double y = 0; y <= maxY; y += minorY)
            {
                plotModel.Annotations.Add(new LineAnnotation
                {
                    Type = LineAnnotationType.Horizontal,
                    Y = y,
                    Color = OxyColor.FromRgb(81, 99, 119),
                    StrokeThickness = (y % majorY == 0) ? 1.5 : 0.5,
                    LineStyle = (y % majorY == 0) ? LineStyle.Solid : LineStyle.Dot
                });
            }

            // Додаємо PlotView на форму
            plotView.Model = plotModel;
            this.Controls.Add(plotView);

            // Ваша label9
            this.label9 = new System.Windows.Forms.Label();
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Location = new System.Drawing.Point(290, 570);
            this.label9.Name = "label9";
            this.label9.Font = new Font("Arial", 16, FontStyle.Regular); // Arial, 16 пт, курсив
            this.label9.Size = new System.Drawing.Size(180, 180);
            this.label9.BackColor = Color.FromArgb(2, 14, 25);
            this.label9.Text = "label9";
            this.Controls.Add(this.label9);
        }

        private void BuildCombinedPlot(
            List<double> projMinutes,
            List<double> browserMinutes,
            DateTime selectedDate)
        {
            // Список годин 0..23
            var hours = Enumerable.Range(0, 24).ToList();

            // 1) Налаштовуємо модель
            var model = new PlotModel
            {
                PlotAreaBorderColor = OxyColor.FromRgb(2, 14, 25),
                PlotAreaBorderThickness = new OxyThickness(1)
            };

            // 2) Серія «Проєкти»
            var seriesProj = new LineSeries
            {
                Title = "Проєкти",
                MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(159, 183, 213),
                StrokeThickness = 2
            };
            for (int i = 0; i < 24; i++)
                seriesProj.Points.Add(new DataPoint(hours[i], projMinutes[i]));
            model.Series.Add(seriesProj);

            // 3) Серія «Браузер»
            var seriesBrowser = new LineSeries
            {
                Title = "Браузер",
                MarkerType = MarkerType.Square,
                Color = OxyColor.FromRgb(213, 159, 159),
                StrokeThickness = 2
            };
            for (int i = 0; i < 24; i++)
                seriesBrowser.Points.Add(new DataPoint(hours[i], browserMinutes[i]));
            model.Series.Add(seriesBrowser);

            // 4) Ось X — години доби
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Година доби",
                Minimum = -0.5,
                Maximum = 23.5,
                MajorStep = 1,
                MinorStep = 1,
                LabelFormatter = v => TimeSpan.FromHours(v).ToString(@"hh\:mm"),
                Angle = -45,
                TitleColor = OxyColor.FromRgb(159, 183, 213),
                TextColor = OxyColor.FromRgb(159, 183, 213),
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,
                MajorGridlineColor = OxyColor.FromRgb(81, 99, 119),
                IsZoomEnabled = false
            });

            // 5) Ось Y — хвилини
            double maxVal = Math.Max(projMinutes.Max(), browserMinutes.Max());
            double maxY = Math.Ceiling(maxVal + 1);
            double stepY = Math.Max(1, Math.Ceiling(maxY / 10.0));
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Хвилини",
                Minimum = 0,
                Maximum = maxY,
                MajorStep = stepY,
                MinorStep = stepY / 2,
                TitleColor = OxyColor.FromRgb(159, 183, 213),
                TextColor = OxyColor.FromRgb(159, 183, 213),
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColor.FromRgb(81, 99, 119),
                MinorGridlineColor = OxyColor.FromRgb(81, 99, 119),
                IsZoomEnabled = false
            });

            // 6) Додаємо PlotView на форму
            var plotView = new PlotView
            {
                Dock = DockStyle.None,
                Size = new Size(600, 400),
                Location = new Point(225, 150),
                Model = model
            };
            this.Controls.Add(plotView);

            // 7) Оновлюємо label9 і label10
            // Переводимо назад у секунди та форматуємо "X годин Y хвилин Z секунд"
            double totalProjSeconds = projMinutes.Sum() * 60;
            double totalBrowserSeconds = browserMinutes.Sum() * 60;
            label9.Text = $"Проєкти: {HumanizeSeconds(totalProjSeconds)}\n" +
                           $"Браузер: {HumanizeSeconds(totalBrowserSeconds)}";
            label10.Text = $"Статистика за {selectedDate:dd.MM.yyyy}";
        }

        private void RemoveChart()
        {
            // Перевірка, чи існує контрол PlotView
            var plotView = this.Controls.OfType<PlotView>().FirstOrDefault();
            if (plotView != null)
            {
                this.Controls.Remove(plotView); // Видалення PlotView з форми
                plotView.Dispose(); // Звільнення ресурсів
            }

            // Перевірка, чи існує label9 (якщо ви хочете видалити його також)
            if (this.label9 != null)
            {
                this.Controls.Remove(this.label9); // Видалення label9 з форми
                this.label9.Dispose(); // Звільнення ресурсів
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
            comboBox.Items.Add("За рік");
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Location = new Point(245, 100);
            comboBox.Size = new Size(150, 30);
            comboBox.ItemHeight = 30; 
            comboBox.Font = new Font("Segoe UI", 12);

            ComboBox secondcomboBox = new ComboBox();
            secondcomboBox.BackColor = Color.FromArgb(186, 192, 196);
            secondcomboBox.Items.Add("За проєктами");
            secondcomboBox.Items.Add("За активністю в браузері");
            secondcomboBox.Items.Add("За проєктами + активності в браузері");
            secondcomboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            secondcomboBox.Location = new Point(406, 100);
            secondcomboBox.Size = new Size(150, 30);
            secondcomboBox.ItemHeight = 30; 
            secondcomboBox.Font = new Font("Segoe UI", 12);
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
                    case "За проєктами + активності в браузері":
                        typeOfStatictics = "за браузером та проєктами";
                        break;
                    case "За активністю в браузері":
                        typeOfStatictics = "за браузером";
                       // MessageBox.Show(typeOfStatictics);
                        break;
                }

                switch (currentViewMode)
                {
                    case ViewMode.Day: CurrentDay(); break;
                    case ViewMode.Week: CurrentWeek(); break;
                    case ViewMode.Month: CurrentMonth(); break;
                }
            };

            this.button11 = CreateButton("button11", "<", new Point(200, 200), new Size(41, 41), this.button11_Click);
            this.button12 = CreateButton("button12", ">", new Point(850, 200), new Size(41, 41), this.button12_Click);


             /*this.button13 = CreateButton("button13", "Статистика за останній рік", new Point(770, 600), new Size(240, 41), this.button13_Click);
             this.button29 = CreateButton("button29", "Дані активності в браузері", new Point(240, 670), new Size(240, 41), this.button29_Click);
             this.button40 = CreateButton("button40", "Статистика активності в браузері", new Point(505, 670), new Size(240, 41), this.button40_Click);
             button40.Text = isBrowserStats
               ? "Статистика за проєктами"
               : "Статистика активності в браузері";*/
            // this.button41 = CreateButton("button41", "Статистика з урахуванням активності в браузері", new Point(770, 670), new Size(240, 41), this.button41_Click);

            this.label10 = CreateMainLabel("label10", "label10", new Point(450, 50), new Size(180, 180), new Font("Arial", 16, FontStyle.Regular));

           // CurrentDay();

            if (CheckBox2Active == true)
            {
               /* this.button11.ForeColor = Color.FromArgb(82, 82, 82);
                this.button11.BackColor = Color.FromArgb(171, 176, 180);

                this.button12.ForeColor = Color.FromArgb(82, 82, 82);
                this.button12.BackColor = Color.FromArgb(171, 176, 180);

                this.button13.ForeColor = Color.FromArgb(82, 82, 82);
                this.button13.BackColor = Color.FromArgb(171, 176, 180);

                this.button29.ForeColor = Color.FromArgb(82, 82, 82);
                this.button29.BackColor = Color.FromArgb(171, 176, 180);*/

                this.label10.ForeColor = Color.FromArgb(82, 82, 82);
                this.label10.BackColor = Color.FromArgb(212, 220, 225);
            }


            if (CheckBox1Active == true)
            {
                /*this.button11.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button11.BackColor = Color.FromArgb(6, 40, 68);

                this.button12.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button12.BackColor = Color.FromArgb(6, 40, 68);

                this.button13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button13.BackColor = Color.FromArgb(6, 40, 68);

                this.button29.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button29.BackColor = Color.FromArgb(6, 40, 68);*/

                this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.label10.BackColor = Color.FromArgb(2, 14, 25);
            }


            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button CreateButton(string name, string text, Point location, Size size, EventHandler clickHandler)
        {
            var button = new System.Windows.Forms.Button
            {
                Name = name,
                Text = text,
                Location = location,
                Size = size,
                ForeColor = System.Drawing.SystemColors.ActiveCaption,
                BackColor = System.Drawing.Color.FromArgb(6, 40, 68),
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                UseVisualStyleBackColor = true
            };
            button.FlatAppearance.BorderSize = 0;
            button.Click += clickHandler;
            this.Controls.Add(button);
            return button;
        }

        private System.Windows.Forms.Label CreateMainLabel(string name, string text, Point location, Size size, Font font)
        {
            var label = new System.Windows.Forms.Label();
            label.AutoSize = true;
            label.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            label.Location = location;
            label.Name = name;
            label.Font = font;
            label.Size = size;
            label.BackColor = System.Drawing.Color.FromArgb(2, 14, 25);
            label.Text = text;
            this.Controls.Add(label);
            return label;
        }

        private void AnnualStatistics()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));

            this.button14 = CreateButton("button14", "Статистика за Жовтень", new Point(240, 500), new Size(240, 41), this.button14_Click);
            this.button15 = CreateButton("button15", "Статистика за Листопад", new Point(505, 500), new Size(240, 41), this.button15_Click);
            this.button16 = CreateButton("button16", "Статистика за Грудень", new Point(770, 500), new Size(240, 41), this.button16_Click);
            this.button17 = CreateButton("button17", "Статистика за Липень", new Point(240, 400), new Size(240, 41), this.button17_Click);
            this.button18 = CreateButton("button18", "Статистика за Серпень", new Point(505, 400), new Size(240, 41), this.button18_Click);
            this.button19 = CreateButton("button19", "Статистика за Вересень", new Point(770, 400), new Size(240, 41), this.button19_Click);
            this.button20 = CreateButton("button20", "Статистика за Квітень", new Point(240, 300), new Size(240, 41), this.button20_Click);
            this.button21 = CreateButton("button21", "Статистика за Травень", new Point(505, 300), new Size(240, 41), this.button21_Click);
            this.button22 = CreateButton("button22", "Статистика за Червень", new Point(770, 300), new Size(240, 41), this.button22_Click);
            this.button23 = CreateButton("button23", "Статистика за Січень", new Point(240, 200), new Size(240, 41), this.button23_Click);
            this.button24 = CreateButton("button24", "Статистика за Лютий", new Point(505, 200), new Size(240, 41), this.button24_Click);
            this.button25 = CreateButton("button25", "Статистика за Березень", new Point(770, 200), new Size(240, 41), this.button25_Click);

            if (CheckBox1Active == true)
            {
                this.button14.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button15.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button16.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button17.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button18.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button19.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button20.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button21.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button22.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button23.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button24.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button25.ForeColor = System.Drawing.SystemColors.ActiveCaption;

                this.button14.BackColor = Color.FromArgb(6, 40, 68);
                this.button15.BackColor = Color.FromArgb(6, 40, 68);
                this.button16.BackColor = Color.FromArgb(6, 40, 68);
                this.button17.BackColor = Color.FromArgb(6, 40, 68);
                this.button18.BackColor = Color.FromArgb(6, 40, 68);
                this.button19.BackColor = Color.FromArgb(6, 40, 68);
                this.button20.BackColor = Color.FromArgb(6, 40, 68);
                this.button21.BackColor = Color.FromArgb(6, 40, 68);
                this.button22.BackColor = Color.FromArgb(6, 40, 68);
                this.button23.BackColor = Color.FromArgb(6, 40, 68);
                this.button24.BackColor = Color.FromArgb(6, 40, 68);
                this.button25.BackColor = Color.FromArgb(6, 40, 68);
            }

            if (CheckBox2Active == true)
            {
                this.button14.ForeColor = Color.FromArgb(82, 82, 82);
                this.button15.ForeColor = Color.FromArgb(82, 82, 82);
                this.button16.ForeColor = Color.FromArgb(82, 82, 82);
                this.button17.ForeColor = Color.FromArgb(82, 82, 82);
                this.button18.ForeColor = Color.FromArgb(82, 82, 82);
                this.button19.ForeColor = Color.FromArgb(82, 82, 82);
                this.button20.ForeColor = Color.FromArgb(82, 82, 82);
                this.button21.ForeColor = Color.FromArgb(82, 82, 82);
                this.button22.ForeColor = Color.FromArgb(82, 82, 82);
                this.button23.ForeColor = Color.FromArgb(82, 82, 82);
                this.button24.ForeColor = Color.FromArgb(82, 82, 82);
                this.button25.ForeColor = Color.FromArgb(82, 82, 82);


                this.button14.BackColor = Color.FromArgb(171, 176, 180);
                this.button15.BackColor = Color.FromArgb(171, 176, 180);
                this.button16.BackColor = Color.FromArgb(171, 176, 180);
                this.button17.BackColor = Color.FromArgb(171, 176, 180);
                this.button18.BackColor = Color.FromArgb(171, 176, 180);
                this.button19.BackColor = Color.FromArgb(171, 176, 180);
                this.button20.BackColor = Color.FromArgb(171, 176, 180);
                this.button21.BackColor = Color.FromArgb(171, 176, 180);
                this.button22.BackColor = Color.FromArgb(171, 176, 180);
                this.button23.BackColor = Color.FromArgb(171, 176, 180);
                this.button24.BackColor = Color.FromArgb(171, 176, 180);
                this.button25.BackColor = Color.FromArgb(171, 176, 180);
            }

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ExitButton()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));

            this.button26 = CreateButton("button26", "Повернутися назад", new Point(505, 650), new Size(240, 41), this.button26_Click);

            this.PerformLayout();

            if (CheckBox1Active == true)
            {
                this.button26.BackColor = Color.FromArgb(6, 40, 68);
                this.button26.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            }

            if (CheckBox2Active == true)
            {
                this.button26.BackColor = Color.FromArgb(171, 176, 180);
                this.button26.ForeColor = Color.FromArgb(82, 82, 82);
            }
        }

        #endregion

        private void SettingsMenu()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));

            this.label11 = new System.Windows.Forms.Label();
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label11.Location = new System.Drawing.Point(300, 150);
            this.label11.Name = "label11";
            this.label11.Font = new Font("Arial", 10, FontStyle.Regular); // Arial, 16 пт, курсив
            this.label11.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
            this.label11.BackColor = Color.FromArgb(2, 14, 25);
            this.label11.Text = "Колір теми:";
            this.Controls.Add(this.label11);

            this.label12 = new System.Windows.Forms.Label();
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label12.Location = new System.Drawing.Point(300, 350);
            this.label12.Name = "label12";
            this.label12.Font = new Font("Arial", 10, FontStyle.Regular); // Arial, 16 пт, курсив
            this.label12.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
            this.label12.BackColor = Color.FromArgb(2, 14, 25);
            this.label12.Text = "Автоматично вимикати таймер за неактивності протягом:";
            this.Controls.Add(this.label12);

            this.label13 = new System.Windows.Forms.Label();
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label13.Location = new System.Drawing.Point(300, 450);
            this.label13.Name = "label13";
            this.label13.Font = new Font("Arial", 10, FontStyle.Regular); // Arial, 16 пт, курсив
            this.label13.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
            this.label13.BackColor = Color.FromArgb(2, 14, 25);
            this.label13.Text = "Якщо вас не задовільнило жодне значення, введіть власне значення в наступне поле:";
            this.Controls.Add(this.label13);

            this.label14 = new System.Windows.Forms.Label();
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label14.Location = new System.Drawing.Point(400, 50);
            this.label14.Name = "label14";
            this.label14.Font = new Font("Arial", 20, FontStyle.Regular); // Arial, 16 пт, курсив
            this.label14.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
            //this.label14.BackColor = Color.FromArgb(2, 14, 25);
            this.label14.Text = "Налаштування роботи програми";
            this.Controls.Add(this.label14);

            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox1.AutoSize = true;
            this.checkBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.checkBox1.Location = new System.Drawing.Point(300, 200);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Font = new Font("Arial", 10, FontStyle.Regular); // Arial, 16 пт, курсив
            this.checkBox1.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
            this.checkBox1.BackColor = Color.Transparent;
            this.checkBox1.Text = "Темна тема";
            this.Controls.Add(this.checkBox1);

            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox2.AutoSize = true;
            this.checkBox2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.checkBox2.Location = new System.Drawing.Point(600, 200);
            this.checkBox2.Name = "label13";
            this.checkBox2.Font = new Font("Arial", 10, FontStyle.Regular); // Arial, 16 пт, курсив
            this.checkBox2.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
            this.checkBox2.BackColor = Color.Transparent;
            this.checkBox2.Text = "Світла тема";
            this.Controls.Add(this.checkBox2);

            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox3.AutoSize = true;
            this.checkBox3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.checkBox3.Location = new System.Drawing.Point(300, 250);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Font = new Font("Arial", 10, FontStyle.Regular); // Arial, 16 пт, курсив
            this.checkBox3.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
            this.checkBox3.BackColor = Color.FromArgb(2, 14, 25);
            this.checkBox3.Text = "Автозапуск програми";
            this.Controls.Add(this.checkBox3);

            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox4.AutoSize = true;
            this.checkBox4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.checkBox4.Location = new System.Drawing.Point(300, 300);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Font = new Font("Arial", 10, FontStyle.Regular); // Arial, 16 пт, курсив
            this.checkBox4.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
            this.checkBox4.BackColor = Color.FromArgb(2, 14, 25);
            this.checkBox4.Text = "Дозволити програмі надсилати сповіщення";
            this.Controls.Add(this.checkBox4);

            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox5.AutoSize = true;
            this.checkBox5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.checkBox5.Location = new System.Drawing.Point(300, 400);
            this.checkBox5.Name = "CheckBox5";
            this.checkBox5.Font = new Font("Arial", 10, FontStyle.Regular); // Arial, 16 пт, курсив
            this.checkBox5.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
            this.checkBox5.BackColor = Color.FromArgb(2, 14, 25);
            this.checkBox5.Text = "5 хв";
            this.Controls.Add(this.checkBox5);

            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox6.AutoSize = true;
            this.checkBox6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.checkBox6.Location = new System.Drawing.Point(600, 400);
            this.checkBox6.Name = "CheckBox6";
            this.checkBox6.Font = new Font("Arial", 10, FontStyle.Regular); // Arial, 16 пт, курсив
            this.checkBox6.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
            this.checkBox6.BackColor = Color.FromArgb(2, 14, 25);
            this.checkBox6.Text = "10 хв";
            this.Controls.Add(this.checkBox6);

            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox7.AutoSize = true;
            this.checkBox7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.checkBox7.Location = new System.Drawing.Point(900, 400);
            this.checkBox7.Name = "CheckBox7";
            this.checkBox7.Font = new Font("Arial", 10, FontStyle.Regular); // Arial, 16 пт, курсив
            this.checkBox7.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
            this.checkBox7.BackColor = Color.FromArgb(2, 14, 25);
            this.checkBox7.Text = "15 хв";
            this.Controls.Add(this.checkBox7);

            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox1.Location = new Point(300, 500); // Розташування на формі
            this.textBox1.AutoSize = false;
            this.textBox1.Size = new Size(250, 20);
            this.textBox1.BackColor = Color.FromArgb(6, 40, 68);// Розмір поля
            this.textBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBox1.Font = new Font("Arial", 9, FontStyle.Regular);
            this.textBox1.Text = "Введіть лише число за допомогою цифр";
            this.textBox1.BorderStyle = BorderStyle.None;
            this.Controls.Add(textBox1);
            //this.textBox1.Leave += ValidateTextBox;

            ProperColorTheme();
            JsonProcessing.LoadSettings();

            // Подія для ручного зняття вибору
            checkBox7.CheckedChanged += (sender, e) =>
            {
                if (checkBox7.Checked &&
                (string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Введіть лише число за допомогою цифр" || textBoxText == null))
                {
                    checkBox5.Checked = false;
                    checkBox6.Checked = false;
                    CheckBox7Active = true;

                    nonActiveTime = 15;
                    //checkBox7.CheckedChanged += ValidateTextBox;

                    // Наприклад, коли користувач змінює стан чекбокса
                    UpdateInactivityAmount(this, nonActiveTime);

                   // MessageBox.Show("jfvnkjnjk");
                }
            };

            if (CheckBox7Active == true)
            {
                checkBox7.Checked = true;
            }

            // Подія для ручного зняття вибору
            checkBox6.CheckedChanged += (sender, e) =>
            {
                if (checkBox6.Checked  &&
                (string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Введіть лише число за допомогою цифр" || textBoxText == null))
                {
                    checkBox5.Checked = false;
                    checkBox7.Checked = false;
                    CheckBox6Active = true;

                    nonActiveTime = 10;
                    //checkBox6.CheckedChanged += ValidateTextBox;
                    UpdateInactivityAmount(this, nonActiveTime);
                }
            };

            if (CheckBox6Active == true)
            {
                checkBox6.Checked = true;
            }

            // Подія для ручного зняття вибору
            checkBox5.CheckedChanged += (sender, e) =>
            {
                if (checkBox5.Checked &&
                (string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Введіть лише число за допомогою цифр" || textBoxText == null))
                {
                    checkBox6.Checked = false;
                    checkBox7.Checked = false;
                    CheckBox5Active = true;

                    nonActiveTime = 5;
                    // checkBox7.CheckedChanged += ValidateTextBox;
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

                    ProperColorTheme();
                }
                if (!checkBox1.Checked && !checkBox2.Checked)
                {
                    checkBox1.Checked = true; // Повертаємо назад, якщо жоден інший не вибраний
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

                    ProperColorTheme();
                }
                if (!checkBox2.Checked && !checkBox1.Checked)
                {
                    checkBox2.Checked = true; // Повертаємо назад, якщо жоден інший не вибраний
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
                    NotificationsOn();
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
                 textBox1.Text = "Введіть лише число за допомогою цифр";
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


            /* if (!(string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Введіть лише число за допомогою цифр"))
             {
                 nonActiveTime = int.Parse(textBox1.Text);
             }*/

            /*if (!(checkBox5.Checked || checkBox6.Checked || checkBox7.Checked) && (string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Введіть лише число за допомогою цифр"))
            {
                MessageBox.Show("Будь ласка, виберіть значення, яке буде вважатися за мінімальну неактивність. Якщо не вибрати жодного значення, програма не зможе якісно виконувати освновну функцію",
                    "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
            }*/

            /* if (!(nonActiveTime == 5 || nonActiveTime == 10 || nonActiveTime == 15))
             {
                 textBox1.Text = Convert.ToString(nonActiveTime);
             }*/

            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        private void ProperColorTheme()
        {
            if (CheckBox1Active == true || Form1.settings.ColorTheme == "dark")
            {
                checkBox1.Checked = true;

                this.BackColor = Color.FromArgb(2, 14, 25);
                this.panel1.BackColor = Color.FromArgb(4, 26, 44);
                this.button7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button27.ForeColor = System.Drawing.SystemColors.ActiveCaption;

                this.checkBox1.BackColor = Color.FromArgb(2, 14, 25);
                this.checkBox2.BackColor = Color.FromArgb(2, 14, 25);
                this.checkBox3.BackColor = Color.FromArgb(2, 14, 25);
                this.checkBox4.BackColor = Color.FromArgb(2, 14, 25);
                this.checkBox5.BackColor = Color.FromArgb(2, 14, 25);
                this.checkBox6.BackColor = Color.FromArgb(2, 14, 25);
                this.checkBox7.BackColor = Color.FromArgb(2, 14, 25);

                this.checkBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.checkBox2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.checkBox3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.checkBox4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.checkBox5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.checkBox6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.checkBox7.ForeColor = System.Drawing.SystemColors.ActiveCaption;

                this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.label12.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.label13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.label14.ForeColor = System.Drawing.SystemColors.ActiveCaption;

                this.label11.BackColor = Color.FromArgb(2, 14, 25);
                this.label12.BackColor = Color.FromArgb(2, 14, 25);
                this.label13.BackColor = Color.FromArgb(2, 14, 25);
                this.label14.BackColor = Color.FromArgb(2, 14, 25);

                this.textBox1.BackColor = Color.FromArgb(6, 40, 68);// Розмір поля
                this.textBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;

                this.button3.BackColor = Color.FromArgb(2, 14, 25);
                this.pictureBox2.BackColor = Color.FromArgb(2, 14, 25);
            }

            if (CheckBox2Active == true || Form1.settings.ColorTheme == "light")
            {
                checkBox2.Checked = true;

                this.BackColor = Color.FromArgb(212, 220, 225);
                this.panel1.BackColor = Color.FromArgb(171, 176, 180);
                this.button7.ForeColor = Color.FromArgb(82, 82, 82);
                this.button8.ForeColor = Color.FromArgb(82, 82, 82);
                this.button27.ForeColor = Color.FromArgb(82, 82, 82);

                this.checkBox1.ForeColor = Color.FromArgb(82, 82, 82);
                this.checkBox2.ForeColor = Color.FromArgb(82, 82, 82);
                this.checkBox3.ForeColor = Color.FromArgb(82, 82, 82);
                this.checkBox4.ForeColor = Color.FromArgb(82, 82, 82);
                this.checkBox5.ForeColor = Color.FromArgb(82, 82, 82);
                this.checkBox6.ForeColor = Color.FromArgb(82, 82, 82);
                this.checkBox7.ForeColor = Color.FromArgb(82, 82, 82);

                this.checkBox1.BackColor = Color.FromArgb(212, 220, 225);
                this.checkBox2.BackColor = Color.FromArgb(212, 220, 225);
                this.checkBox3.BackColor = Color.FromArgb(212, 220, 225);
                this.checkBox4.BackColor = Color.FromArgb(212, 220, 225);
                this.checkBox5.BackColor = Color.FromArgb(212, 220, 225);
                this.checkBox6.BackColor = Color.FromArgb(212, 220, 225);
                this.checkBox7.BackColor = Color.FromArgb(212, 220, 225);

                this.label11.BackColor = Color.FromArgb(212, 220, 225);
                this.label12.BackColor = Color.FromArgb(212, 220, 225);
                this.label13.BackColor = Color.FromArgb(212, 220, 225);
                this.label14.BackColor = Color.FromArgb(212, 220, 225);

                this.label11.ForeColor = Color.FromArgb(82, 82, 82);
                this.label12.ForeColor = Color.FromArgb(82, 82, 82);
                this.label13.ForeColor = Color.FromArgb(82, 82, 82);
                this.label14.ForeColor = Color.FromArgb(82, 82, 82);

                this.textBox1.BackColor = Color.FromArgb(171, 176, 180); // Розмір поля
                this.textBox1.ForeColor = Color.FromArgb(82, 82, 82);
            }

        }

        private void AboutProgram()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));

            this.label15 = new System.Windows.Forms.Label();
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label15.Location = new System.Drawing.Point(300, 150);
            this.label15.Name = "label15";
            this.label15.Font = new Font("Arial", 10, FontStyle.Regular); // Arial, 16 пт, курсив
            this.label15.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
            this.label15.BackColor = Color.FromArgb(2, 14, 25);
            this.label15.Text = "Ця програма представляє собою кваліфікаційну роботу";
            this.Controls.Add(this.label15);

            this.label16 = new System.Windows.Forms.Label();
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label16.Location = new System.Drawing.Point(300, 200);
            this.label16.Name = "label16";
            this.label16.Font = new Font("Arial", 10, FontStyle.Regular); // Arial, 16 пт, курсив
            this.label16.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
            this.label16.BackColor = Color.FromArgb(2, 14, 25);
            this.label16.Text = "Більшість людей у сучасному світі працює дистанційно у вільному графіку, і тому багато хто не знає, скільки реально часу працює, " +
                                "оскільки робочий день не нормований. Дома людям важко залишатися повністю зосередженими на роботі і не відволікатися на сторонні " +
                                "фактори. Через це часто люди можуть перепрацьовувати. Це призводить до зниження ефективності роботи і виникненню проблем зі здоров'ям." +
                                "Ця програма створена для того, щоб вирішити цю проблему та допомогти людям отримати реальні данні про робочий час.";
            this.Controls.Add(this.label16);

           /* this.label17 = new System.Windows.Forms.Label();
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label17.Location = new System.Drawing.Point(300, 400);
            this.label17.Name = "label17";
            this.label17.Font = new Font("Arial", 10, FontStyle.Regular); // Arial, 16 пт, курсив
            this.label17.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
            this.label17.BackColor = Color.FromArgb(2, 14, 25);
            this.label17.Text = "Якщо вас не задовільнило жодне значення, введіть власне значення в наступне поле:";
            this.Controls.Add(this.label17);

            this.label18 = new System.Windows.Forms.Label();
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label18.Location = new System.Drawing.Point(400, 50);
            this.label18.Name = "label18";
            this.label18.Font = new Font("Arial", 20, FontStyle.Regular); // Arial, 16 пт, курсив
            this.label18.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
            //this.label14.BackColor = Color.FromArgb(2, 14, 25);
            this.label18.Text = "Докладні відомості про програму";
            this.Controls.Add(this.label18);*/

            ProperColorTheme();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BrowserInfo()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.pictureBox14 = new System.Windows.Forms.PictureBox();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.pictureBox16 = new System.Windows.Forms.PictureBox();
            this.pictureBox17 = new System.Windows.Forms.PictureBox();
            this.pictureBox18 = new System.Windows.Forms.PictureBox();
            // Створюємо основну мітку
            this.label10 = CreateMainLabel("label10", "label10", new Point(410, 50), new Size(180, 180), new Font("Arial", 16, FontStyle.Regular));
            var commonBackColor = Color.FromArgb(6, 40, 68);

            // Налаштовуємо PictureBox з фоновим зображенням
           // this.ConfigurePictureBox(pictureBox2, new Point(338, 394), new Size(515, 41), backgroundImage: (Image)resources.GetObject("pictureBox2.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox3, new Point(338, 441), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox3.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox4, new Point(338, 488), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox4.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox5, new Point(338, 535), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox5.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox6, new Point(338, 582), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox6.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox7, new Point(338, 629), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox3.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox8, new Point(338, 676), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox4.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox9, new Point(338, 723), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox5.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox10, new Point(338, 770), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox6.BackgroundImage"), backColor: commonBackColor);

            // Налаштовуємо Label з зображенням
            this.label3 = ConfigureLabel(new Point(360, 150), new Size(pictureBox3.Width, pictureBox3.Height), "label3", image: (Image)resources.GetObject("label3.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label4 = ConfigureLabel(new Point(360, 200), new Size(pictureBox4.Width, pictureBox4.Height), "label4", image: (Image)resources.GetObject("label4.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label5 = ConfigureLabel(new Point(360, 250), new Size(pictureBox5.Width, pictureBox5.Height), "label5", image: (Image)resources.GetObject("label5.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label6 = ConfigureLabel(new Point(360, 300), new Size(pictureBox6.Width, pictureBox6.Height), "label6", image: (Image)resources.GetObject("label6.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label7 = ConfigureLabel(new Point(360, 350), new Size(pictureBox7.Width, pictureBox7.Height), "label7", image: (Image)resources.GetObject("label7.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label8 = ConfigureLabel(new Point(360, 400), new Size(pictureBox8.Width, pictureBox8.Height), "label8", image: (Image)resources.GetObject("label8.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label9 = ConfigureLabel(new Point(360, 450), new Size(pictureBox9.Width, pictureBox9.Height), "label9", image: (Image)resources.GetObject("label9.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label11 = ConfigureLabel(new Point(360, 500), new Size(pictureBox10.Width, pictureBox10.Height), "label11", image: (Image)resources.GetObject("label11.Image"), backColor: Color.FromArgb(6, 40, 68));

            this.ConfigurePictureBox(pictureBox11, new Point(338, 441), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox11.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox12, new Point(338, 488), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox12.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox13, new Point(338, 535), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox13.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox14, new Point(338, 582), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox14.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox15, new Point(338, 629), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox15.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox16, new Point(338, 676), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox16.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox17, new Point(338, 723), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox17.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox18, new Point(338, 770), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox18.BackgroundImage"), backColor: commonBackColor);

            // Налаштовуємо Label з зображенням
            this.label12 = ConfigureLabel(new Point(630, 150), new Size(pictureBox11.Width, pictureBox11.Height), "label12", image: (Image)resources.GetObject("label12.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label13 = ConfigureLabel(new Point(630, 200), new Size(pictureBox12.Width, pictureBox12.Height), "label13", image: (Image)resources.GetObject("label13.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label14 = ConfigureLabel(new Point(630, 250), new Size(pictureBox13.Width, pictureBox13.Height), "label14", image: (Image)resources.GetObject("label14.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label15 = ConfigureLabel(new Point(630, 300), new Size(pictureBox14.Width, pictureBox14.Height), "label15", image: (Image)resources.GetObject("label15.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label16 = ConfigureLabel(new Point(630, 350), new Size(pictureBox15.Width, pictureBox15.Height), "label16", image: (Image)resources.GetObject("label16.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label17 = ConfigureLabel(new Point(630, 400), new Size(pictureBox16.Width, pictureBox16.Height), "label17", image: (Image)resources.GetObject("label17.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label18 = ConfigureLabel(new Point(630, 450), new Size(pictureBox17.Width, pictureBox17.Height), "label18", image: (Image)resources.GetObject("label18.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label19 = ConfigureLabel(new Point(630, 500), new Size(pictureBox18.Width, pictureBox18.Height), "label19", image: (Image)resources.GetObject("label19.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));

            this.button30 = CreateButton("button30", "-", new Point(900, 150), new Size(50, 41), this.button30_Click);
            this.button31 = CreateButton("button31", "-", new Point(900, 200), new Size(50, 41), this.button31_Click);
            this.button32 = CreateButton("button32", "-", new Point(900, 250), new Size(50, 41), this.button32_Click);
            this.button33 = CreateButton("button33", "-", new Point(900, 300), new Size(50, 41), this.button33_Click);
            this.button34 = CreateButton("button34", "-", new Point(900, 350), new Size(50, 41), this.button34_Click);
            this.button35 = CreateButton("button35", "-", new Point(900, 400), new Size(50, 41), this.button35_Click);
            this.button36 = CreateButton("button36", "-", new Point(900, 450), new Size(50, 41), this.button36_Click);
            this.button37 = CreateButton("button37", "-", new Point(900, 500), new Size(50, 41), this.button37_Click);

            this.button38 = CreateButton("button38", "Вверх", new Point(300, 150), new Size(50, 41), this.button38_Click);
            this.button39 = CreateButton("button39", "Вниз", new Point(300, 500), new Size(50, 41), this.button39_Click);
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

        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.PictureBox pictureBox13;
        private System.Windows.Forms.PictureBox pictureBox14;
        private System.Windows.Forms.PictureBox pictureBox15;
        private System.Windows.Forms.PictureBox pictureBox16;
        private System.Windows.Forms.PictureBox pictureBox17;
        private System.Windows.Forms.PictureBox pictureBox18;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        //налаштування програми
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        //
        //налаштування програми
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;

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
        //Для річної статистики
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Button button24;
        private System.Windows.Forms.Button button25;

        private System.Windows.Forms.Button button26;
        private System.Windows.Forms.Button button27;
        private System.Windows.Forms.Button button28;
        private System.Windows.Forms.Button button29;

        private System.Windows.Forms.Button button30;
        private System.Windows.Forms.Button button31;
        private System.Windows.Forms.Button button32;
        private System.Windows.Forms.Button button33;
        private System.Windows.Forms.Button button34;
        private System.Windows.Forms.Button button35;
        private System.Windows.Forms.Button button36;
        private System.Windows.Forms.Button button37;
        private System.Windows.Forms.Button button38;
        private System.Windows.Forms.Button button39;
        private System.Windows.Forms.Button button40;
        private System.Windows.Forms.Button button41;


        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;

        public System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.CheckBox checkBox2;
        public System.Windows.Forms.CheckBox checkBox3;
        public System.Windows.Forms.CheckBox checkBox4;
        public System.Windows.Forms.CheckBox checkBox5;
        public System.Windows.Forms.CheckBox checkBox6;
        public System.Windows.Forms.CheckBox checkBox7;


        private System.Windows.Forms.TextBox textBox1;

        private System.Windows.Forms.ComboBox secondcomboBox;


    }
}

