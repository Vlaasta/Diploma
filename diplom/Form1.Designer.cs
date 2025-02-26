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
            this.button7.Size = new System.Drawing.Size(235, 45);
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
            this.button27.Size = new System.Drawing.Size(235, 45);
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
            this.button28.Size = new System.Drawing.Size(235, 45);
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
            this.button8.Size = new System.Drawing.Size(235, 45);
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
            this.ClientSize = new System.Drawing.Size(1044, 734);
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
            this.pictureBox1.Image = Properties.Resources.ClockForDarkTheme;
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

            this.label7.AutoSize = false;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
           // this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label7.Location = new System.Drawing.Point(598, 488);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 16);
            this.label7.TabIndex = 14;
            this.label7.BackColor = Color.FromArgb(6, 40, 68);
            this.label7.Text = "label7";
            this.label7.Size = new System.Drawing.Size(this.pictureBox7.Width, this.pictureBox7.Height);
            this.label7.AutoEllipsis = true; // Додає "..." якщо текст не влізе
            this.label7.TextAlign = ContentAlignment.MiddleLeft;

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

            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.Location = new System.Drawing.Point(338, 441);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(255, 41);
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.BackColor = Color.FromArgb(6, 40, 68);
            // 
            // label3
            // 
            this.label3.AutoSize = false;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.Location = new System.Drawing.Point(338, 441);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(this.pictureBox3.Width, this.pictureBox3.Height);
            this.label3.TabIndex = 14;
            this.label3.BackColor = Color.FromArgb(6, 40, 68);
            this.label3.Text = "label3";
            this.label3.AutoEllipsis = true; // Додає "..." якщо текст не влізе
            this.label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = false;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
            this.label4.Location = new System.Drawing.Point(338, 488);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(this.pictureBox4.Width, this.pictureBox4.Height);
            this.label4.TabIndex = 15;
            this.label4.BackColor = Color.FromArgb(6, 40, 68);
            this.label4.Text = "label4";
            this.label4.AutoEllipsis = true; // Додає "..." якщо текст не влізе
            this.label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = false;
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.Location = new System.Drawing.Point(338, 535);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(this.pictureBox5.Width, this.pictureBox5.Height);
            this.label5.TabIndex = 16;
            this.label5.BackColor = Color.FromArgb(6, 40, 68);
            this.label5.Text = "label5";
            this.label5.AutoEllipsis = true; // Додає "..." якщо текст не влізе
            this.label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = false;
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.Location = new System.Drawing.Point(598, 441);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(this.pictureBox6.Width, this.pictureBox6.Height);
            this.label6.TabIndex = 17;
            this.label6.BackColor = Color.FromArgb(6, 40, 68);
            this.label6.Text = "label6";
            this.label6.AutoEllipsis = true; // Додає "..." якщо текст не влізе
            this.label6.TextAlign = ContentAlignment.MiddleLeft;
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
            // label8
            // 
            this.label8.AutoSize = false;
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label8.Image = ((System.Drawing.Image)(resources.GetObject("label8.Image")));
            this.label8.Location = new System.Drawing.Point(598, 535);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(this.pictureBox8.Width, this.pictureBox8.Height);
            this.label8.TabIndex = 19;
            this.label8.BackColor = Color.FromArgb(6, 40, 68);
            this.label8.Text = "label8";
            this.label8.AutoEllipsis = true; // Додає "..." якщо текст не влізе
            this.label8.TextAlign = ContentAlignment.MiddleLeft;
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
         

            if (CheckBox2Active == true || Form1.settings.ColorTheme == "light")
            {
                this.pictureBox1.Image = Properties.Resources.ClockForLightTheme;
                this.button1.BackColor = Color.FromArgb(182, 192, 196);
                this.button3.BackColor = Color.FromArgb(182, 192, 196);
                this.button10.BackColor = Color.FromArgb(182, 192, 196);

                this.button3.ForeColor = Color.FromArgb(82, 82, 82);
                this.button10.ForeColor = Color.FromArgb(82, 82, 82);
                this.button1.ForeColor = Color.FromArgb(82, 82, 82);
                this.button2.ForeColor = Color.FromArgb(82, 82, 82);
                this.button4.ForeColor = Color.FromArgb(82, 82, 82);

                this.button2.BackColor = Color.FromArgb(182, 192, 196);
                this.button4.BackColor = Color.FromArgb(182, 192, 196);

                this.pictureBox2.BackColor = Color.FromArgb(182, 192, 196);
                this.pictureBox3.BackColor = Color.FromArgb(182, 192, 196);
                this.pictureBox4.BackColor = Color.FromArgb(182, 192, 196);
                this.pictureBox5.BackColor = Color.FromArgb(182, 192, 196);
                this.pictureBox6.BackColor = Color.FromArgb(182, 192, 196);
                this.pictureBox7.BackColor = Color.FromArgb(182, 192, 196);
                this.pictureBox8.BackColor = Color.FromArgb(182, 192, 196);

                this.label2.BackColor = Color.FromArgb(182, 192, 196);
                this.label1.ForeColor = Color.FromArgb(82, 82, 82);
                this.label1.BackColor = Color.FromArgb(182, 192, 196);
                this.label3.ForeColor = Color.FromArgb(82, 82, 82);
                this.label3.BackColor = Color.FromArgb(182, 192, 196);
                this.label4.ForeColor = Color.FromArgb(82, 82, 82);
                this.label4.BackColor = Color.FromArgb(182, 192, 196);
                this.label5.ForeColor = Color.FromArgb(82, 82, 82);

                this.label5.BackColor = Color.FromArgb(182, 192, 196);
                this.label6.ForeColor = Color.FromArgb(82, 82, 82);
                this.label6.BackColor = Color.FromArgb(182, 192, 196);
                this.label7.ForeColor = Color.FromArgb(82, 82, 82);
                this.label7.BackColor = Color.FromArgb(182, 192, 196);
                this.label8.ForeColor = Color.FromArgb(82, 82, 82);
                this.label8.BackColor = Color.FromArgb(182, 192, 196);

                this.button5.ForeColor = Color.FromArgb(82, 82, 82);
                this.button5.BackColor = Color.FromArgb(182, 192, 196);

                this.button6.ForeColor = Color.FromArgb(82, 82, 82);
                this.button6.BackColor = Color.FromArgb(182, 192, 196);
            }

            if (CheckBox1Active == true || Form1.settings.ColorTheme == "dark")
            {
                this.pictureBox1.Image = Properties.Resources.ClockForDarkTheme;
                this.button3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button3.BackColor = Color.FromArgb(6, 40, 68);

                this.button10.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button10.BackColor = Color.FromArgb(6, 40, 68);

                this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button1.BackColor = Color.FromArgb(6, 40, 68);

                this.pictureBox2.BackColor = Color.FromArgb(6, 40, 68);
                this.pictureBox3.BackColor = Color.FromArgb(6, 40, 68);
                this.pictureBox4.BackColor = Color.FromArgb(6, 40, 68);
                this.pictureBox5.BackColor = Color.FromArgb(6, 40, 68);
                this.label2.BackColor = Color.FromArgb(186, 192, 196);
                this.pictureBox6.BackColor = Color.FromArgb(6, 40, 68);
                this.pictureBox7.BackColor = Color.FromArgb(6, 40, 68);
                this.pictureBox8.BackColor = Color.FromArgb(6, 40, 68);
                this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.label1.BackColor = Color.FromArgb(6, 40, 68);

                this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.label3.BackColor = Color.FromArgb(6, 40, 68);
                this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.label4.BackColor = Color.FromArgb(6, 40, 68);

                this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.label5.BackColor = Color.FromArgb(6, 40, 68);

                this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.label6.BackColor = Color.FromArgb(6, 40, 68);
                this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.label7.BackColor = Color.FromArgb(6, 40, 68);

                this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.label8.BackColor = Color.FromArgb(6, 40, 68);

                this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button2.BackColor = Color.FromArgb(6, 40, 68);

                this.button4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button4.BackColor = Color.FromArgb(6, 40, 68);

                this.button5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button5.BackColor = Color.FromArgb(6, 40, 68);

                this.button6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button6.BackColor = Color.FromArgb(6, 40, 68);
            }
            
            // 
            // Form1
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1044, 734);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
           
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
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
                Location = new Point(315, 100)
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
                lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(xPoints[i]), yPoints[i]));
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
            this.label9.Location = new System.Drawing.Point(370, 540);
            this.label9.Name = "label9";
            this.label9.Font = new Font("Arial", 16, FontStyle.Regular); // Arial, 16 пт, курсив
            this.label9.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
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

        private void StatisticsMainMenu()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
           
            
            this.button11 = new System.Windows.Forms.Button();
            this.button11.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button11.Location = new System.Drawing.Point(240, 620);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(240, 41);
           // this.button11.TabIndex = 24;
            this.button11.Text = "Статистика за попередній тиждень";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.BackColor = Color.FromArgb(6, 40, 68);
            this.button11.Click += new System.EventHandler(this.button11_Click);
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button11.FlatAppearance.BorderSize = 0;
            this.Controls.Add(this.button11);


            this.button12 = new System.Windows.Forms.Button();
            this.button12.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button12.Location = new System.Drawing.Point(505, 620); 
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(240, 41);
           // this.button12.TabIndex = 24;
            this.button12.Text = "Статистика за останній місяць";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.BackColor = Color.FromArgb(6, 40, 68);
            this.button12.Click += new System.EventHandler(this.button12_Click);
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.FlatAppearance.BorderSize = 0;
            this.Controls.Add(this.button12);


            this.button13 = new System.Windows.Forms.Button();
            this.button13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button13.Location = new System.Drawing.Point(770, 620);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(240, 41);
            // this.button12.TabIndex = 24;
            this.button13.Text = "Статистика за останній рік";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.BackColor = Color.FromArgb(6, 40, 68);
            this.button13.Click += new System.EventHandler(this.button13_Click);
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button13.FlatAppearance.BorderSize = 0;
            this.Controls.Add(this.button13);

            this.label10 = new System.Windows.Forms.Label();
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label10.Location = new System.Drawing.Point(452, 50);
            this.label10.Name = "label10";
            this.label10.Font = new Font("Arial", 16, FontStyle.Regular); // Arial, 16 пт, курсив
            this.label10.Size = new System.Drawing.Size(180, 180);
            // this.label9.TabIndex = 19;
            this.label10.BackColor = Color.FromArgb(2, 14, 25);
            this.label10.Text = "label10";
            this.Controls.Add(this.label10);


            if (CheckBox2Active == true)
            {
                this.button11.ForeColor = Color.FromArgb(82, 82, 82);
                this.button11.BackColor = Color.FromArgb(171, 176, 180);

                this.button12.ForeColor = Color.FromArgb(82, 82, 82);
                this.button12.BackColor = Color.FromArgb(171, 176, 180);

                this.button13.ForeColor = Color.FromArgb(82, 82, 82);
                this.button13.BackColor = Color.FromArgb(171, 176, 180);

                this.label10.ForeColor = Color.FromArgb(82, 82, 82);
                this.label10.BackColor = Color.FromArgb(212, 220, 225);
            }


            if (CheckBox1Active == true)
            {
                this.button11.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button11.BackColor = Color.FromArgb(6, 40, 68);

                this.button12.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button12.BackColor = Color.FromArgb(6, 40, 68);

                this.button13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.button13.BackColor = Color.FromArgb(6, 40, 68);

                this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                this.label10.BackColor = Color.FromArgb(2, 14, 25);
            }


            this.ResumeLayout(false);
            this.PerformLayout();
        }
      
        private void AnnualStatistics()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));


            this.button14 = new System.Windows.Forms.Button();
            this.button14.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button14.Location = new System.Drawing.Point(240, 500);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(240, 41);
            // this.buttn11.TabIndex = 24;
            this.button14.Text = "Статистика за Жовтень";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.BackColor = Color.FromArgb(6, 40, 68);
            this.button14.Click += new System.EventHandler(this.button14_Click);
            this.button14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button14.FlatAppearance.BorderSize = 0;
            this.Controls.Add(this.button14);


            this.button15 = new System.Windows.Forms.Button();
            this.button15.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button15.Location = new System.Drawing.Point(505, 500);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(240, 41);
            // this.button12.TabIndex = 24;
            this.button15.Text = "Статистика за Листопад";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.BackColor = Color.FromArgb(6, 40, 68);
            this.button15.Click += new System.EventHandler(this.button15_Click);
            this.button15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button15.FlatAppearance.BorderSize = 0;
            this.Controls.Add(this.button15);


            this.button16 = new System.Windows.Forms.Button();
            this.button16.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button16.Location = new System.Drawing.Point(770, 500);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(240, 41);
            // this.button12.TabIndex = 24;
            this.button16.Text = "Статистика за Грудень";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.BackColor = Color.FromArgb(6, 40, 68);
            this.button16.Click += new System.EventHandler(this.button16_Click);
            this.button16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button16.FlatAppearance.BorderSize = 0;
            this.Controls.Add(this.button16);


            this.button17 = new System.Windows.Forms.Button();
            this.button17.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button17.Location = new System.Drawing.Point(240, 400);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(240, 41);
            // this.buttn11.TabIndex = 24;
            this.button17.Text = "Статистика за Липень";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.BackColor = Color.FromArgb(6, 40, 68);
            this.button17.Click += new System.EventHandler(this.button17_Click);
            this.button17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button17.FlatAppearance.BorderSize = 0;
            this.Controls.Add(this.button17);


            this.button18 = new System.Windows.Forms.Button();
            this.button18.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button18.Location = new System.Drawing.Point(505, 400);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(240, 41);
            // this.button12.TabIndex = 24;
            this.button18.Text = "Статистика за Серпень";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.BackColor = Color.FromArgb(6, 40, 68);
            this.button18.Click += new System.EventHandler(this.button18_Click);
            this.button18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button18.FlatAppearance.BorderSize = 0;
            this.Controls.Add(this.button18);


            this.button19 = new System.Windows.Forms.Button();
            this.button19.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button19.Location = new System.Drawing.Point(770, 400);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(240, 41);
            // this.button12.TabIndex = 24;
            this.button19.Text = "Статистика за Вересень";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.BackColor = Color.FromArgb(6, 40, 68);
            this.button19.Click += new System.EventHandler(this.button19_Click);
            this.button19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button19.FlatAppearance.BorderSize = 0;
            this.Controls.Add(this.button19);



            this.button20 = new System.Windows.Forms.Button();
            this.button20.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button20.Location = new System.Drawing.Point(240, 300);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(240, 41);
            // this.buttn11.TabIndex = 24;
            this.button20.Text = "Статистика за Квітень";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.BackColor = Color.FromArgb(6, 40, 68);
            this.button20.Click += new System.EventHandler(this.button20_Click);
            this.button20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button20.FlatAppearance.BorderSize = 0;
            this.Controls.Add(this.button20);


            this.button21 = new System.Windows.Forms.Button();
            this.button21.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button21.Location = new System.Drawing.Point(505, 300);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(240, 41);
            // this.button12.TabIndex = 24;
            this.button21.Text = "Статистика за Травень";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.BackColor = Color.FromArgb(6, 40, 68);
            this.button21.Click += new System.EventHandler(this.button21_Click);
            this.button21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button21.FlatAppearance.BorderSize = 0;
            this.Controls.Add(this.button21);


            this.button22 = new System.Windows.Forms.Button();
            this.button22.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button22.Location = new System.Drawing.Point(770, 300);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(240, 41);
            // this.button12.TabIndex = 24;
            this.button22.Text = "Статистика за Червень";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.BackColor = Color.FromArgb(6, 40, 68);
            this.button22.Click += new System.EventHandler(this.button22_Click);
            this.button22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button22.FlatAppearance.BorderSize = 0;
            this.Controls.Add(this.button22);


            this.button23 = new System.Windows.Forms.Button();
            this.button23.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button23.Location = new System.Drawing.Point(240, 200);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(240, 41);
            // this.buttn11.TabIndex = 24;
            this.button23.Text = "Статистика за Січень";
            this.button23.UseVisualStyleBackColor = true;
            this.button23.BackColor = Color.FromArgb(6, 40, 68);
            this.button23.Click += new System.EventHandler(this.button23_Click);
            this.button23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button23.FlatAppearance.BorderSize = 0;
            this.Controls.Add(this.button23);


            this.button24 = new System.Windows.Forms.Button();
            this.button24.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button24.Location = new System.Drawing.Point(505, 200);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(240, 41);
            // this.button12.TabIndex = 24;
            this.button24.Text = "Статистика за Лютий";
            this.button24.UseVisualStyleBackColor = true;
            this.button24.BackColor = Color.FromArgb(6, 40, 68);
            this.button24.Click += new System.EventHandler(this.button24_Click);
            this.button24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button24.FlatAppearance.BorderSize = 0;
            this.Controls.Add(this.button24);


            this.button25 = new System.Windows.Forms.Button();
            this.button25.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button25.Location = new System.Drawing.Point(770, 200);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(240, 41);
            // this.button12.TabIndex = 24;
            this.button25.Text = "Статистика за Березень";
            this.button25.UseVisualStyleBackColor = true;
            this.button25.BackColor = Color.FromArgb(6, 40, 68);
            this.button25.Click += new System.EventHandler(this.button25_Click);
            this.button25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button25.FlatAppearance.BorderSize = 0;
            this.Controls.Add(this.button25);

            if(CheckBox1Active == true)
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
            this.button26 = new System.Windows.Forms.Button();
            this.button26.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button26.Location = new System.Drawing.Point(505, 650);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(240, 41);
            // this.button12.TabIndex = 24;
            this.button26.Text = "Повернутися назад";
            this.button26.UseVisualStyleBackColor = true;
            this.button26.BackColor = Color.FromArgb(6, 40, 68);
            this.button26.Click += new System.EventHandler(this.button26_Click);
            this.button26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button26.FlatAppearance.BorderSize = 0;
            this.Controls.Add(this.button26);
            this.ResumeLayout(false);
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
            this.label12.Text = "Автоматично вимикати таймер після ... хв неактивності";
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

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;

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
        private System.Windows.Forms.Button button26;
        private System.Windows.Forms.Button button27;
        private System.Windows.Forms.Button button28;

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;


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

        //налаштування програми
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;

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

