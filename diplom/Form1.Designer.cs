using System.Drawing; // Простір імен для Color
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Globalization;
using OxyPlot.Axes;

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
            this.panel3 = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();

            this.BackColor = Color.FromArgb(2, 14, 25);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
           // this.panel1.Controls.Add(this.panel2);
            this.panel1.BackColor = Color.FromArgb(4, 26, 44);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 733);
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
            this.button7.Size = new System.Drawing.Size(235, 55);
            this.button7.TabIndex = 21;
            this.button7.Text = "Статистика";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.FlatAppearance.BorderSize = 0;
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
            this.button8.Size = new System.Drawing.Size(235, 55);
            this.button8.TabIndex = 22;
            this.button8.Text = "Головне меню";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.FlatAppearance.BorderSize = 0;
            // 
            // Form1
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1044, 734);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InitializeComponentMainMenu()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            InitializeComponentMain();
           // this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
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
            this.SuspendLayout();
            // 
            // panel2
            // 
          /*  this.panel2.Controls.Add(this.button3);
            this.panel2.Location = new System.Drawing.Point(0, 251);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(225, 46);
            this.panel2.TabIndex = 22;*/
            // 
            // button3
            // 
           // this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button3.Location = new System.Drawing.Point(282, 394);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(50, 41);
           // this.button3.TabIndex = 21;
            this.button3.Text = "Вверх";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.BackColor = Color.FromArgb(6, 40, 68);
            //
            //
            //
            //
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button10.Location = new System.Drawing.Point(282, 535);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(50, 41);
            // this.button3.TabIndex = 21;
            this.button10.Text = "Вниз";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            this.button10.BackColor = Color.FromArgb(6, 40, 68);
            // 
            // pictureBox1
            // 
            // this.pictureBox1.Image = System.Drawing.Image.FromFile("E:\\4 KURS\\DiplomaRepo\\Диплом\\Diploma\\Design\\Clock.png");
            //this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("Clock")));
            this.pictureBox1.Image = Properties.Resources.Clock;
            this.pictureBox1.Location = new System.Drawing.Point(552, 62);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(194, 193);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            //this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Location = new System.Drawing.Point(859, 394);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 41);
            this.button1.TabIndex = 2;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.BackColor = Color.FromArgb(6, 40, 68);
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.FlatAppearance.BorderSize = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.Location = new System.Drawing.Point(338, 394);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(515, 41);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.BackColor = Color.FromArgb(6, 40, 68);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.Location = new System.Drawing.Point(338, 441);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(255, 41);
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.BackColor = Color.FromArgb(6, 40, 68);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.BackgroundImage")));
            this.pictureBox4.Location = new System.Drawing.Point(338, 488);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(255, 41);
            this.pictureBox4.TabIndex = 6;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.BackColor = Color.FromArgb(6, 40, 68);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox5.BackgroundImage")));
            this.pictureBox5.Location = new System.Drawing.Point(338, 535);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(255, 41);
            this.pictureBox5.TabIndex = 7;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.BackColor = Color.FromArgb(6, 40, 68);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            this.label2.Location = new System.Drawing.Point(624, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.BackColor = Color.FromArgb(186, 192, 196);
            this.label2.TabIndex = 8;
            this.label2.Text = "00:00:00";
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox6.BackgroundImage")));
            this.pictureBox6.Location = new System.Drawing.Point(598, 441);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(255, 41);
            this.pictureBox6.TabIndex = 12;
            this.pictureBox6.TabStop = false;
            this.pictureBox6.BackColor = Color.FromArgb(6, 40, 68);
            // 
            // pictureBox7
            // 
            // this.pictureBox7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox7.BackgroundImage")));
            this.pictureBox7.Location = new System.Drawing.Point(598, 488);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(255, 41);
            this.pictureBox7.TabIndex = 11;
            this.pictureBox7.TabStop = false;
            this.pictureBox7.BackColor = Color.FromArgb(6, 40, 68);
            // 
            // pictureBox8
            // 
            //this.pictureBox8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox8.BackgroundImage")));
            this.pictureBox8.Location = new System.Drawing.Point(598, 535);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(255, 41);
            this.pictureBox8.TabIndex = 10;
            this.pictureBox8.TabStop = false;
            this.pictureBox8.BackColor = Color.FromArgb(6, 40, 68);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            //this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.Location = new System.Drawing.Point(479, 407);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 16);
            this.label1.TabIndex = 13;
            this.label1.BackColor = Color.FromArgb(6, 40, 68);
            this.label1.Text = "Щоб додати новий проект, натисніть на  \"+\"";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.Location = new System.Drawing.Point(355, 453);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 14;
            this.label3.BackColor = Color.FromArgb(6, 40, 68);
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
            this.label4.Location = new System.Drawing.Point(355, 502);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 15;
            this.label4.BackColor = Color.FromArgb(6, 40, 68);
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.Location = new System.Drawing.Point(355, 549);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 16;
            this.label5.BackColor = Color.FromArgb(6, 40, 68);
            this.label5.Text = "label5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.Location = new System.Drawing.Point(611, 453);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 17;
            this.label6.BackColor = Color.FromArgb(6, 40, 68);
            this.label6.Text = "label6";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
            this.label7.Location = new System.Drawing.Point(611, 502);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 16);
            this.label7.TabIndex = 18;
            this.label7.BackColor = Color.FromArgb(6, 40, 68);
            this.label7.Text = "label7";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label8.Image = ((System.Drawing.Image)(resources.GetObject("label8.Image")));
            this.label8.Location = new System.Drawing.Point(611, 549);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 16);
            this.label8.TabIndex = 19;
            this.label8.BackColor = Color.FromArgb(6, 40, 68);
            this.label8.Text = "label8";
            //
            // 
            // button2
            // 
            // this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button2.Location = new System.Drawing.Point(543, 273);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 48);
            this.button2.TabIndex = 20;
            this.button2.Text = "Запустити таймер";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.BackColor = Color.FromArgb(6, 40, 68);
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.FlatAppearance.BorderSize = 0;
            // 
            // button4
            // 
            this.button4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button4.Location = new System.Drawing.Point(859, 441);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(50, 41);
            this.button4.TabIndex = 22;
            this.button4.Text = "-";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            this.button4.BackColor = Color.FromArgb(6, 40, 68);
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.FlatAppearance.BorderSize = 0;
            // 
            // button5
            // 
            this.button5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button5.Location = new System.Drawing.Point(859, 488);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(50, 41);
            this.button5.TabIndex = 23;
            this.button5.Text = "-";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.BackColor = Color.FromArgb(6, 40, 68);
            this.button5.Click += new System.EventHandler(this.button5_Click);
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.FlatAppearance.BorderSize = 0;
            // 
            // button6
            // 
            this.button6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button6.Location = new System.Drawing.Point(859, 537);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(50, 41);
            this.button6.TabIndex = 24;
            this.button6.Text = "-";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.BackColor = Color.FromArgb(6, 40, 68);
            this.button6.Click += new System.EventHandler(this.button6_Click);
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.FlatAppearance.BorderSize = 0;
            // 
            // Form1
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1044, 734);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
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
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void buildChart(List<DateTime> xPoints, List<int> yPoints)
        {
            var plotView = new PlotView
            {
                Dock = DockStyle.None,
                Size = new Size(600, 400),
                Location = new Point(300, 60)
            };

            var plotModel = new PlotModel
            {
                Title = "Статистика за останній тиждень",
                TitleColor = OxyColor.FromRgb(169, 169, 169),
                PlotAreaBorderColor = OxyColor.FromRgb(2, 14, 25)
            };

            var lineSeries = new LineSeries
            {
                Title = "Час (секунди)",
                MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(169, 169, 169)
            };

            for (int i = 0; i < xPoints.Count; i++)
            {
                lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(xPoints[i]), yPoints[i]));
            }

            plotModel.Series.Add(lineSeries);

            var axisX = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Дата",
                TitleColor = OxyColor.FromRgb(169, 169, 169),
                TextColor = OxyColor.FromRgb(169, 169, 169),
                StringFormat = "dd.MM.yyyy",
                MajorGridlineColor = OxyColor.FromRgb(169, 169, 169),
                MinorGridlineColor = OxyColor.FromRgb(169, 169, 169),
                IsZoomEnabled = false,
                AxislineColor = OxyColor.FromArgb(0, 0, 0, 0),
                TicklineColor = OxyColor.FromArgb(0, 0, 0, 0),
                IsAxisVisible = false
            };

            var axisY = new LinearAxis
            {
                Title = "Секунди",
                TitleColor = OxyColor.FromRgb(169, 169, 169),
                TextColor = OxyColor.FromRgb(169, 169, 169),
                MajorGridlineColor = OxyColor.FromRgb(169, 169, 169),
                MinorGridlineColor = OxyColor.FromRgb(169, 169, 169),
                IsZoomEnabled = false,
                AxislineColor = OxyColor.FromArgb(0, 0, 0, 0),
                TicklineColor = OxyColor.FromArgb(0, 0, 0, 0),
                IsAxisVisible = false
            };

            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            plotView.Model = plotModel;

            this.Controls.Add(plotView);
        }

      /*  private void StatisticsMainMenu()
        {
            // Зчитуємо дані з JSON файлу
            string jsonContent = File.ReadAllText(@"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\timerAmounts.json");
            List<TimerData> timerData = JsonSerializer.Deserialize<List<TimerData>>(jsonContent);

            // Створення PlotView для відображення графіка
            var plotView = new PlotView
            {
                Dock = DockStyle.None, // Вимикаємо автоматичне заповнення форми
                Size = new Size(600, 400), // Встановлюємо розміри графіка (ширина - 600, висота - 400)
                Location = new Point(300, 60) // Встановлюємо місце розташування (відстань від лівого верхнього кута форми)
               
            };

            // Створення моделі графіка
            var plotModel = new PlotModel
            {
                Title = "Статистика за останній тиждень",
                TitleColor = OxyColor.FromRgb(169, 169, 169), // Колір заголовку графіка
                PlotAreaBorderColor = OxyColor.FromRgb(2, 14, 25) // Колір межі (білий або інший, щоб вона була непомітною)
            };

            // Створення серії для лінійного графіка
            var lineSeries = new LineSeries
            {
                Title = "Час (секунди)",
                MarkerType = MarkerType.Circle,
                Color = OxyColor.FromRgb(169, 169, 169) // Колір лінії (сірий)
            };

            // Додаємо точки на графік
            var labels = new List<string>();
            foreach (var data in timerData)
            {
                DateTime date = DateTime.ParseExact(data.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                TimeSpan timeSpan = TimeSpan.Parse(data.Time);
                int totalSeconds = (int)timeSpan.TotalSeconds;

                lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), totalSeconds));
                labels.Add(date.ToString("dd.MM.yyyy"));
            }

            // Додаємо серію до моделі
            plotModel.Series.Add(lineSeries);

            // Налаштовуємо осі
            var axisX = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Дата",
                TitleColor = OxyColor.FromRgb(169, 169, 169),
                TextColor = OxyColor.FromRgb(169, 169, 169),
                StringFormat = "dd.MM.yyyy",  // Формат дати
                MajorGridlineColor = OxyColor.FromRgb(169, 169, 169),  // Колір сітки
                MinorGridlineColor = OxyColor.FromRgb(169, 169, 169),
                IsZoomEnabled = false,  // Вимкнути масштабування по осі X
                        AxislineColor = OxyColor.FromArgb(0, 0, 0, 0),  // Приховуємо лінію осі X
                TicklineColor = OxyColor.FromArgb(0, 0, 0, 0),  // Приховуємо позначки на осі X
                IsAxisVisible = false // Приховуємо саму вісь X
            };

            var axisY = new LinearAxis
            {
                Title = "Секунди",
                TitleColor = OxyColor.FromRgb(169, 169, 169),
                TextColor = OxyColor.FromRgb(169, 169, 169),
                MajorGridlineColor = OxyColor.FromRgb(169, 169, 169),
                MinorGridlineColor = OxyColor.FromRgb(169, 169, 169),
                IsZoomEnabled = false,  // Вимкнути масштабування по осі X
                        AxislineColor = OxyColor.FromArgb(0, 0, 0, 0),  // Приховуємо лінію осі X
                TicklineColor = OxyColor.FromArgb(0, 0, 0, 0),  // Приховуємо позначки на осі X
                IsAxisVisible = false // Приховуємо саму вісь X
            };

            // Додаємо осі до графіка
            plotModel.Axes.Add(axisX);
            plotModel.Axes.Add(axisY);

            // Призначаємо модель графіку компоненту PlotView
            plotView.Model = plotModel;

            // Додаємо PlotView на форму
            this.Controls.Add(plotView);

        }
      */

    #endregion



    private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
       // private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button10;
        // private System.Windows.Forms.PictureBox pictureBox9;
       // private OxyPlot.WindowsForms.PlotView plotView1;

       // private ZedGraphControl zedGraphControl;


    }
}

