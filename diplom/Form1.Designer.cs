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

            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel6);

            this.panel1.BackColor = Color.FromArgb(4, 26, 44);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 733);

            this.panel3.Controls.Add(this.button28);
            this.panel3.Location = new System.Drawing.Point(0, 251);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(225, 46);
            this.panel3.TabIndex = 23;

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

            this.panel2.Controls.Add(this.button7);
            this.panel2.Location = new System.Drawing.Point(0, 150);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(225, 46);

            this.button27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button27.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button27.Location = new System.Drawing.Point(-8, 0);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(200, 45);
            this.button27.Text = "Налаштування";
            this.button27.UseVisualStyleBackColor = true;
            this.button27.Click += new System.EventHandler(this.button27_Click);
            this.button27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button27.FlatAppearance.BorderSize = 0;

            this.panel6.Controls.Add(this.button8);
            this.panel6.Location = new System.Drawing.Point(0, 100);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(225, 46);

            this.button28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button28.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button28.Location = new System.Drawing.Point(-8, 0);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(200, 45);
            this.button28.Text = "Про програму";
            this.button28.UseVisualStyleBackColor = true;
            this.button28.Click += new System.EventHandler(this.button28_Click);
            this.button28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button28.FlatAppearance.BorderSize = 0;

            this.panel4.Controls.Add(this.button27);
            this.panel4.Location = new System.Drawing.Point(0, 200);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(225, 46);
            this.panel4.TabIndex = 24;

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
            if (activePanel != null)
            {
                activePanel.BackColor = Form1.settings.ColorTheme == "light"
                    ? Color.FromArgb(171, 176, 180)
                    : Color.FromArgb(4, 26, 44);
            }

            panel.BackColor = Color.FromArgb(30, 60, 90); // або інший колір, який тобі подобається
            activePanel = panel;
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
            this.button1.Font = new Font("Microsoft Sans Serif", 12);

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
            this.button2.Font = new Font("Microsoft Sans Serif", 12);

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
            this.button3.Font = new Font("Microsoft Sans Serif", 10);

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
            this.button4.Font = new Font("Microsoft Sans Serif", 16);

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
            this.button5.Font = new Font("Microsoft Sans Serif", 16);

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
            this.button6.Font = new Font("Microsoft Sans Serif", 16);

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
            this.button10.Font = new Font("Microsoft Sans Serif", 10);

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

            this.label2 = ConfigureLabel(new Point(507, 128), new Size(65, 26), "00:00:00", backColor: Color.FromArgb(186, 192, 196));
            this.label2.ForeColor = Color.FromArgb(0, 0, 0);

            this.label1 = ConfigureLabel(new Point(380, 380), new Size(65, 16), "Назва", backColor: Color.FromArgb(6, 40, 68));
            this.label9 = ConfigureLabel(new Point(640, 380), new Size(65, 16), "Шлях", backColor: Color.FromArgb(6, 40, 68));
            this.label7 = ConfigureLabel(new Point(540, 456), new Size(pictureBox7.Width, pictureBox7.Height), "label7", backColor: Color.FromArgb(6, 40, 68));
            this.label3 = ConfigureLabel(new Point(280, 413), new Size(pictureBox3.Width, pictureBox3.Height), "label3", image: (Image)resources.GetObject("label3.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label4 = ConfigureLabel(new Point(280, 456), new Size(pictureBox4.Width, pictureBox4.Height), "label4", image: (Image)resources.GetObject("label4.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label5 = ConfigureLabel(new Point(280, 499), new Size(pictureBox5.Width, pictureBox5.Height), "label5", image: (Image)resources.GetObject("label5.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label6 = ConfigureLabel(new Point(540, 413), new Size(pictureBox6.Width, pictureBox6.Height), "label6", image: (Image)resources.GetObject("label6.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label8 = ConfigureLabel(new Point(540, 499), new Size(pictureBox8.Width, pictureBox8.Height), "label8", image: (Image)resources.GetObject("label8.Image"), backColor: Color.FromArgb(6, 40, 68));

            this.label2.Font = new Font("Microsoft Sans Serif", 10);
            this.label1.Font = new Font("Microsoft Sans Serif", 12);
            this.label9.Font = new Font("Microsoft Sans Serif", 12);
            this.label7.Font = new Font("Microsoft Sans Serif", 9);
            this.label3.Font = new Font("Microsoft Sans Serif", 9);
            this.label4.Font = new Font("Microsoft Sans Serif", 9);
            this.label6.Font = new Font("Microsoft Sans Serif", 9);
            this.label5.Font = new Font("Microsoft Sans Serif", 9);
            this.label8.Font = new Font("Microsoft Sans Serif", 9);

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

        private PlotView CreatePlotView()
        {
            var plotView = new PlotView
            {
                Dock = DockStyle.None,
                Size = new Size(600, 400),
                Location = new Point(230, 150)
            };

            var plotModel = new PlotModel
            {
                PlotAreaBorderColor = OxyColor.FromRgb(2, 14, 25),
                PlotAreaBorderThickness = new OxyThickness(1)
            };

            plotView.Model = plotModel;

            return plotView;
        }

        private void StyleAxis(Axis axis)
        {
            var mainColor = OxyColor.FromRgb(159, 183, 213);
            var minorColor = OxyColor.FromRgb(81, 99, 119);

            axis.TitleColor = mainColor;
            axis.TextColor = mainColor;
            axis.TicklineColor = mainColor;
            axis.AxislineColor = mainColor;
            axis.MajorGridlineColor = mainColor;
            axis.MinorGridlineColor = minorColor;
            axis.MajorGridlineThickness = 1;
            axis.MinorGridlineThickness = 0.5;
            axis.IsZoomEnabled = false;
        }

        private void buildChart(List<DateTime> xPoints, List<int> yPoints)
        {
            var plotView = CreatePlotView();
            var plotModel = plotView.Model;

            var lineSeries = new LineSeries
            {
                Title = "Час роботи (сек)",
                MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(159, 183, 213)
            };

            for (int i = 0; i < xPoints.Count; i++)
            {
                double dateAsDouble = DateTimeAxis.ToDouble(xPoints[i]);
                lineSeries.Points.Add(new DataPoint(dateAsDouble, yPoints[i]));
            }

            plotModel.Series.Add(lineSeries);

            var yMin = yPoints.Min();
            var yMax = yPoints.Max();
            double range = yMax - yMin;
            double majorStep = Math.Ceiling(range / 5.0 / 100.0) * 100;
            if (majorStep == 0) majorStep = 10;
            double minorStep = majorStep / 5.0;

            var axisY = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Час роботи (сек)",
                Minimum = yMin - 2,
                Maximum = yMax + 2,
                MajorStep = majorStep,
                MinorStep = minorStep
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

            StyleAxis(axisX);
            StyleAxis(axisY);
            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            for (double y = axisY.Minimum; y <= axisY.Maximum; y += axisY.MinorStep)
            {
                var gridLine = new LineAnnotation
                {
                    Type = LineAnnotationType.Horizontal,
                    Y = y,
                    Color = OxyColor.FromRgb(81, 99, 119),
                    LineStyle = y % axisY.MajorStep == 0 ? LineStyle.Solid : LineStyle.Dot,
                    StrokeThickness = y % axisY.MajorStep == 0 ? 1.5 : 0.5
                };
                plotModel.Annotations.Add(gridLine);
            }
            this.label9 = CreateMainLabel("label9", "label9", 545, 570, new Size(530, 50), new Font("Arial", 16, FontStyle.Regular));
            plotView.Model = plotModel;
            this.Controls.Add(plotView);
        }

        private void buildDailyChart<T>(List<T> dataList, DateTime selectedDate, Func<T, (DateTime Start, DateTime Stop)?> convertToInterval)
        {
            var today = selectedDate.Date;

            var parsed = dataList
                .Select(convertToInterval)
                .Where(interval => interval.HasValue && interval.Value.Start != DateTime.MinValue)
                .Select(interval => interval.Value)
                .OrderBy(p => p.Start)
                .ToList();

            var plotView = CreatePlotView();
            var plotModel = plotView.Model;

            var series = new LineSeries
            {
                Title = "Час роботи (хв)",
                MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(159, 183, 213),
                StrokeThickness = 2
            };

            series.Points.Add(new DataPoint(0, 0));

            foreach (var seg in parsed)
            {
                double startH = (seg.Start - today).TotalHours;
                double stopH = (seg.Stop - today).TotalHours;
                double elapsedMin = (seg.Stop - seg.Start).TotalMinutes;

                series.Points.Add(new DataPoint(startH, 0));
                series.Points.Add(new DataPoint(stopH, elapsedMin));
                series.Points.Add(new DataPoint(stopH, 0));
            }

            series.Points.Add(new DataPoint(23.5, 0));

            plotModel.Series.Add(series);

            var axisX = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Година доби",
                Minimum = -0.5,
                Maximum = 23.5,
                MajorStep = 1,
                MinorStep = 1,
                LabelFormatter = v => TimeSpan.FromHours(v).ToString(@"hh\:mm"),
                Angle = -45
            };

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
                MinorStep = minorY
            };

            StyleAxis(axisX);
            StyleAxis(axisY);
            plotModel.Axes.Add(axisX); 
            plotModel.Axes.Add(axisY);

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

            this.label9 = CreateMainLabel("label9", "label9", 545, 570, new Size(530, 50), new Font("Arial", 16, FontStyle.Regular));
            plotView.Model = plotModel;
            this.Controls.Add(plotView);

        }

        private void BuildMergedPlotModel(List<double> projMinutes, List<double> browserMinutes, DateTime selectedDate)
        {
            var merged = projMinutes.Zip(browserMinutes, (p, b) => p + b).ToList();

            var model = new PlotModel
            {
                PlotAreaBorderColor = OxyColor.FromRgb(2, 14, 25),
                PlotAreaBorderThickness = new OxyThickness(1)
            };

            var series = new LineSeries
            {
                Title = "Загальний час (хв)",
                MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(159, 183, 213),
                StrokeThickness = 2,
                TrackerFormatString = "Значення: {4:0.00} хв"
            };

            for (int i = 0; i < 96; i++)
            {
                series.Points.Add(new DataPoint(i, merged[i]));
            }

            model.Series.Add(series);

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Час",
                Minimum = -0.5,
                Maximum = 95.5,
                MajorStep = 4,
                MinorStep = 1,
                LabelFormatter = v =>
                {
                    if (v < 0 || v > 95) return "";
                    var time = TimeSpan.FromMinutes(v * 15);
                    return time.ToString(@"hh\:mm");
                },
                Angle = -45,
                TitleColor = OxyColor.FromRgb(159, 183, 213),
                TextColor = OxyColor.FromRgb(159, 183, 213),
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,
                MajorGridlineColor = OxyColor.FromRgb(81, 99, 119),
                IsZoomEnabled = false
            });

            double maxY = Math.Ceiling(merged.Max() + 1);
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

            var plotView = new PlotView
            {
                Dock = DockStyle.None,
                Size = new Size(600, 400),
                Location = new Point(230, 150),
                Model = model
            };
            this.Controls.Add(plotView);
            this.label9 = CreateMainLabel("label9", "label9", 545, 570, new Size(530, 50), new Font("Arial", 16, FontStyle.Regular));
            label9.Text = $"Загальний час: {statistic.HumanizeSeconds(merged.Sum() * 60)}";
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
            comboBox.Items.Add("За рік");
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Location = new Point(245, 100);
            comboBox.Size = new Size(150, 30);
            comboBox.ItemHeight = 30; 
            comboBox.Font = new Font("Segoe UI", 12);

            ComboBox secondcomboBox = new ComboBox();
            secondcomboBox.BackColor = Color.FromArgb(186, 192, 196);
            secondcomboBox.Items.Add("За проєктами");
            secondcomboBox.Items.Add("За браузером");
            secondcomboBox.Items.Add("Об'єднана");
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

            this.button11 = CreateButton("button11", "<", new Point(218, 520), new Size(41, 41), this.button11_Click);
            this.button12 = CreateButton("button12", ">", new Point(820, 520), new Size(41, 41), this.button12_Click);
            this.button13 = CreateButton("button13", "Продивитися дані", new Point(600, 100), new Size(150, 30), this.button13_Click);
            this.label10 = CreateMainLabel("label10", "label10", 545, 30, new Size(530, 50), new Font("Arial", 16, FontStyle.Regular));

            if (CheckBox2Active == true)
            {
                this.label10.ForeColor = Color.FromArgb(82, 82, 82);
                this.label10.BackColor = Color.FromArgb(212, 220, 225);
            }


            if (CheckBox1Active == true)
            {
                this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.label10.BackColor = Color.FromArgb(2, 14, 25);
            }

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ExitButton()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));

            this.button14 = CreateButton("button14", "Повернутися назад", new Point(420, 540), new Size(240, 41), this.button14_Click);

            this.PerformLayout();

            if (CheckBox1Active == true)
            {
                this.button14.BackColor = Color.FromArgb(6, 40, 68);
                this.button14.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            }

            if (CheckBox2Active == true)
            {
                this.button14.BackColor = Color.FromArgb(171, 176, 180);
                this.button14.ForeColor = Color.FromArgb(82, 82, 82);
            }
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

        private System.Windows.Forms.Label CreateMainLabel(string name, string text, int centerX, int y, Size size, Font font)
        {
            // Обчислюємо X так, щоб центр мітки збігався з centerX
            int labelX = centerX - size.Width / 2;

            var label = new Label
            {
                Name = name,
                Text = text,
                Location = new Point(labelX, y),
                Size = size,
                Font = font,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(label);

            return label; 
        }

        private Label CreateSettingsLabel(Point location, string name, string text, int fontSize = 10, bool isTitle = false)
        {
            var label = new Label
            {
                AutoSize = true,
                ForeColor = System.Drawing.SystemColors.ActiveCaption,
                Location = location,
                Name = name,
                Font = new Font("Arial", fontSize, FontStyle.Regular),
                Size = new Size(180, 180),
                Text = text,
                TextAlign = ContentAlignment.MiddleLeft
            };
            if (!isTitle)
                label.BackColor = Color.FromArgb(2, 14, 25);

            this.Controls.Add(label);
            return label;
        }

        private CheckBox CreateCheckBox(Point location, string name, string text, bool hasDarkBackground = false)
        {
            var checkBox = new CheckBox
            {
                AutoSize = true,
                ForeColor = System.Drawing.SystemColors.ActiveCaption,
                Location = location,
                Name = name,
                Font = new Font("Arial", 10, FontStyle.Regular),
                Size = new Size(180, 180),
                Text = text,
                BackColor = hasDarkBackground ? Color.FromArgb(2, 14, 25) : Color.Transparent
            };

            this.Controls.Add(checkBox);
            return checkBox;
        }

        #endregion

        private void SettingsMenu()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Label label11 = CreateSettingsLabel(new Point(250, 100), "label11", "Колір теми:");
            Label label12 = CreateSettingsLabel(new Point(250, 350), "label12", "Автоматично вимикати таймер за неактивності протягом:");
            Label label13 = CreateSettingsLabel(new Point(250, 450), "label13", "Якщо вас не задовільнило жодне значення, введіть власне значення в наступне поле:");
            Label label10 = CreateMainLabel("label10", "Налаштування роботи програми", 550, 30, new Size(500, 30), new Font("Arial", 16, FontStyle.Regular));
            label10.ForeColor = System.Drawing.SystemColors.ActiveCaption;

            CheckBox checkBox1 = CreateCheckBox(new Point(250, 150), "checkBox1", "Темна тема");
            CheckBox checkBox2 = CreateCheckBox(new Point(450, 150), "checkBox2", "Світла тема");
            CheckBox checkBox3 = CreateCheckBox(new Point(250, 200), "checkBox3", "Автозапуск програми", true);
            CheckBox checkBox4 = CreateCheckBox(new Point(250, 250), "checkBox4", "Дозволити програмі надсилати сповіщення:", true);
            CheckBox checkBox5 = CreateCheckBox(new Point(250, 400), "checkBox5", "5 хв", true);
            CheckBox checkBox6 = CreateCheckBox(new Point(450, 400), "checkBox6", "10 хв", true);
            CheckBox checkBox7 = CreateCheckBox(new Point(650, 400), "checkBox7", "15 хв", true);
            CheckBox checkBox8 = CreateCheckBox(new Point(250, 300), "checkBox8", "Відстежувати активність в браузері", true);
           // CheckBox checkBox9 = CreateCheckBox(new Point(250, 500), "checkBox9", " активність в браузері", true);

            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox1.Location = new Point(250, 500); 
            this.textBox1.AutoSize = false;
            this.textBox1.Size = new Size(250, 20);
            this.textBox1.BackColor = Color.FromArgb(6, 40, 68); 
            this.textBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBox1.Font = new Font("Arial", 9, FontStyle.Regular);
            this.textBox1.Text = "Введіть лише число за допомогою цифр";
            this.textBox1.BorderStyle = BorderStyle.None;
            this.Controls.Add(textBox1);

           // ProperColorTheme();
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

            checkBox5.CheckedChanged += (sender, e) =>
            {
                if (checkBox5.Checked &&
                (string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Введіть лише число за допомогою цифр" || textBoxText == null))
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

                   // ProperColorTheme();
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

                    //ProperColorTheme();
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

            textBox1.TextChanged += (sender, e) =>
            {
                if (!(string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Введіть лише число за допомогою цифр") || Form1.settings.TextBoxValue != 0)
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

                if (string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "Введіть лише число за допомогою цифр" || Form1.settings.TextBoxValue == 0)
                {
                    textBoxText = "Введіть лише число за допомогою цифр";
                    TextBoxValue = 0;

                    UpdateTextBoxValue(this, TextBoxValue);

                    CheckBox5Active = true;
                    checkBox5.Checked = true;
                    nonActiveTime = 5;
                }
            };

            textBox1.Enter += (sender, e) =>
            {
                if (textBox1.Text == "Введіть лише число за допомогою цифр")
                {
                    textBox1.Text = "";
                }
            };

            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
       /* private void ProperColorTheme()
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

        }*/

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

           // ProperColorTheme();

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
            this.label10 = CreateMainLabel("label10", "label10", 545, 30, new Size(530, 50), new Font("Arial", 16, FontStyle.Regular));
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaption; 
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
            this.label3 = ConfigureLabel(new Point(277, 100), new Size(pictureBox3.Width, pictureBox3.Height), "label3", image: (Image)resources.GetObject("label3.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label4 = ConfigureLabel(new Point(277, 150), new Size(pictureBox4.Width, pictureBox4.Height), "label4", image: (Image)resources.GetObject("label4.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label5 = ConfigureLabel(new Point(277, 200), new Size(pictureBox5.Width, pictureBox5.Height), "label5", image: (Image)resources.GetObject("label5.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label6 = ConfigureLabel(new Point(277, 250), new Size(pictureBox6.Width, pictureBox6.Height), "label6", image: (Image)resources.GetObject("label6.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label7 = ConfigureLabel(new Point(277, 300), new Size(pictureBox7.Width, pictureBox7.Height), "label7", image: (Image)resources.GetObject("label7.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label8 = ConfigureLabel(new Point(277, 350), new Size(pictureBox8.Width, pictureBox8.Height), "label8", image: (Image)resources.GetObject("label8.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label9 = ConfigureLabel(new Point(277, 400), new Size(pictureBox9.Width, pictureBox9.Height), "label9", image: (Image)resources.GetObject("label9.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label11 = ConfigureLabel(new Point(277, 450), new Size(pictureBox10.Width, pictureBox10.Height), "label11", image: (Image)resources.GetObject("label11.Image"), backColor: Color.FromArgb(6, 40, 68));

            this.ConfigurePictureBox(pictureBox11, new Point(338, 441), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox11.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox12, new Point(338, 488), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox12.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox13, new Point(338, 535), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox13.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox14, new Point(338, 582), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox14.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox15, new Point(338, 629), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox15.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox16, new Point(338, 676), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox16.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox17, new Point(338, 723), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox17.BackgroundImage"), backColor: commonBackColor);
            this.ConfigurePictureBox(pictureBox18, new Point(338, 770), new Size(255, 41), backgroundImage: (Image)resources.GetObject("pictureBox18.BackgroundImage"), backColor: commonBackColor);

            // Налаштовуємо Label з зображенням
            this.label12 = ConfigureLabel(new Point(542, 100), new Size(pictureBox11.Width, pictureBox11.Height), "label12", image: (Image)resources.GetObject("label12.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label13 = ConfigureLabel(new Point(542, 150), new Size(pictureBox12.Width, pictureBox12.Height), "label13", image: (Image)resources.GetObject("label13.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label14 = ConfigureLabel(new Point(542, 200), new Size(pictureBox13.Width, pictureBox13.Height), "label14", image: (Image)resources.GetObject("label14.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label15 = ConfigureLabel(new Point(542, 250), new Size(pictureBox14.Width, pictureBox14.Height), "label15", image: (Image)resources.GetObject("label15.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label16 = ConfigureLabel(new Point(542, 300), new Size(pictureBox15.Width, pictureBox15.Height), "label16", image: (Image)resources.GetObject("label16.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label17 = ConfigureLabel(new Point(542, 350), new Size(pictureBox16.Width, pictureBox16.Height), "label17", image: (Image)resources.GetObject("label17.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label18 = ConfigureLabel(new Point(542, 400), new Size(pictureBox17.Width, pictureBox17.Height), "label18", image: (Image)resources.GetObject("label18.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.label19 = ConfigureLabel(new Point(542, 450), new Size(pictureBox18.Width, pictureBox18.Height), "label19", image: (Image)resources.GetObject("label19.Image"), backColor: Color.FromArgb(6, 40, 68));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));

            this.button30 = CreateButton("button30", "-", new Point(807, 100), new Size(50, 41), this.button30_Click);
            this.button31 = CreateButton("button31", "-", new Point(807, 150), new Size(50, 41), this.button31_Click);
            this.button32 = CreateButton("button32", "-", new Point(807, 200), new Size(50, 41), this.button32_Click);
            this.button33 = CreateButton("button33", "-", new Point(807, 250), new Size(50, 41), this.button33_Click);
            this.button34 = CreateButton("button34", "-", new Point(807, 300), new Size(50, 41), this.button34_Click);
            this.button35 = CreateButton("button35", "-", new Point(807, 350), new Size(50, 41), this.button35_Click);
            this.button36 = CreateButton("button36", "-", new Point(807, 400), new Size(50, 41), this.button36_Click);
            this.button37 = CreateButton("button37", "-", new Point(807, 450), new Size(50, 41), this.button37_Click);

            this.button38 = CreateButton("button38", "Вверх", new Point(217, 100), new Size(50, 41), this.button38_Click);
            this.button39 = CreateButton("button39", "Вниз", new Point(217, 450), new Size(50, 41), this.button39_Click);
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
        private System.Windows.Forms.Button button14;

        private System.Windows.Forms.Button button26;
        private System.Windows.Forms.Button button27;
        private System.Windows.Forms.Button button28;

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

    }
}

