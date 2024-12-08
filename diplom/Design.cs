using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class DesignForm : Form
{
    public void Design()
    {
        // Налаштування форми
        this.BackColor = Color.LightBlue; // Чорний фон форми
        this.Size = new Size(800, 600); // Розмір форми
    }

    // Метод для зміни кнопки на овальну
    public void MakeButtonOval(Button button)
    {
        // Округлення кнопки
        GraphicsPath graphicsPath = new GraphicsPath();
        int borderRadius = 30; // Розмір округлених кутів
        graphicsPath.AddArc(0, 0, borderRadius, borderRadius, 180, 90); // Верхній лівий кут
        graphicsPath.AddArc(button.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90); // Верхній правий кут
        graphicsPath.AddArc(button.Width - borderRadius, button.Height - borderRadius, borderRadius, borderRadius, 0, 90); // Нижній правий кут
        graphicsPath.AddArc(0, button.Height - borderRadius, borderRadius, borderRadius, 90, 90); // Нижній лівий кут
        graphicsPath.CloseFigure(); // Закриваємо шлях

        // Встановлюємо форму кнопки як округлу
        button.Region = new Region(graphicsPath);

        // Додавання градієнтного фону
        LinearGradientBrush gradientBrush = new LinearGradientBrush(button.ClientRectangle, Color.Gold, Color.DarkGoldenrod, 45f);
        Graphics g = button.CreateGraphics();
        g.FillRectangle(gradientBrush, button.ClientRectangle);

        // Встановлюємо стиль кнопки (плоский, без обведення)
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
    }
}

