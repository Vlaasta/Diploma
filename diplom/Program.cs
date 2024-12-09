using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows.Forms;

namespace diplom
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Створення екземпляра HandButton (якщо потрібно)
           // HandButton handButton = new HandButton();

            // Створення екземпляра ProjectProcessor
            /*ProjectProcessor projectProcessor = new ProjectProcessor(handButton);

            // Запуск роботи класу ProjectProcessor
            projectProcessor.StartTimerForOpenedProjects();*/

            // Запускаємо основну форму
            Application.Run(new Form1());
        }
    }
}

