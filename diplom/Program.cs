using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace diplom
{
    internal static class Program
    {
        // Декларація Windows API для роботи з хуками
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hmod, int dwThreadId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        // Оголошення хуків
        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static readonly string ProjectsFilePath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\projects.json";

        private static TimeSpan lastElapsedTime = TimeSpan.Zero;
        public static bool ShouldSaveTimeToJson = true;  // Наприклад, збереження часу в JSON

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        private static HandButton timer = new HandButton();
        private static string currentActiveProject;

        public static Func<string> GetLabel1TextDelegate { get; set; }

        // Змінна для збереження часу
        private static TimeSpan ElapsedTime;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        public static uint GetIdleTime()
        {
            LASTINPUTINFO info = new LASTINPUTINFO();
            info.cbSize = (uint)Marshal.SizeOf(info);
            if (GetLastInputInfo(ref info))
            {
                return ((uint)Environment.TickCount - info.dwTime) / 1000; // Час простою у секундах
            }
            return 0;
        }

        public static bool IsFileLocked(string filePath)
        {
            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }
        }

        public static List<Project> LoadProjects()
        {
            if (!File.Exists(ProjectsFilePath))
            {
                Console.WriteLine("Файл projects.json не знайдено.");
                return new List<Project>();
            }

            try
            {
                var json = File.ReadAllText(ProjectsFilePath);
                return JsonConvert.DeserializeObject<List<Project>>(json) ?? new List<Project>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка читання файлу: {ex.Message}");
                return new List<Project>();
            }
        }

        public static void CompareProjectsWithOpenProcesses(List<Project> projects)
        {
            var openFiles = new List<string>();

            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    string windowTitle = process?.MainWindowTitle;

                    if (!string.IsNullOrEmpty(windowTitle))
                    {
                        openFiles.Add(windowTitle);
                    }
                }
                catch
                {
                    // Игноруємо помилки доступу
                }
            }

            foreach (var project in projects)
            {
                bool isFileOpen = openFiles.Any(title =>
                    title.IndexOf(Path.GetFileNameWithoutExtension(project.Path), StringComparison.OrdinalIgnoreCase) >= 0);

                bool isFileLocked = IsFileLocked(project.Path);

                bool isOpen = isFileOpen || isFileLocked;

                Console.WriteLine(isOpen
                    ? $"Файл відкритий: {project.Name}"
                    : $"Файл закритий: {project.Name}");
            }
        }

        public static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, buff, nChars) > 0)
            {
                return buff.ToString();
            }

            return "Без назви";
        }

        // Метод для встановлення часу
        public static void SetElapsedTime(TimeSpan elapsedTime)
        {
            ElapsedTime = elapsedTime;
        }

        private static void CheckActiveWindow(List<Project> projects, Action<TimeSpan> onTimeUpdated)
        {
            string activeWindowTitle = GetActiveWindowTitle();

            var activeProject = projects.FirstOrDefault(project =>
                activeWindowTitle.IndexOf(Path.GetFileNameWithoutExtension(project.Path), StringComparison.OrdinalIgnoreCase) >= 0);

            if (activeProject != null)
            {
                if (currentActiveProject != activeProject.Name)
                {
                    if (timer.IsRunning)
                    {
                        lastElapsedTime = timer.GetAccumulatedTime(); // Зберігаємо накопичений час перед зупинкою
                        Console.WriteLine($"Зупинка таймера для попереднього проєкту: {currentActiveProject}, збережено час: {lastElapsedTime}");
                        timer.Pause();
                    }

                    if (!timer.IsRunning)
                    {
                        // Логіка для передачі часу з label1.Text
                        if (GetLabel1TextDelegate != null)
                        {
                            string label1Text = GetLabel1TextDelegate.Invoke(); // Отримуємо текст з label1
                            if (TimeSpan.TryParseExact(label1Text, @"hh\:mm\:ss", null, out TimeSpan lastElapsed))
                            {
                                timer.SetElapsedTime(lastElapsed); // Використовуємо отриманий час
                            }
                            else
                            {
                                timer.SetElapsedTime(TimeSpan.Zero); // Якщо текст некоректний, починаємо з 0
                            }
                        }
                        else
                        {
                            timer.SetElapsedTime(TimeSpan.Zero); // Якщо делегат не налаштований, починаємо з 0
                        }

                        Console.WriteLine($"Запуск таймера для проєкту: {activeProject.Name}, продовження з часу: {lastElapsedTime}");
                        timer.Start();
                        currentActiveProject = activeProject.Name;
                       // IfUserActive();

                        timer.OnTimeUpdated += (elapsed) =>
                        {
                            onTimeUpdated(elapsed);

                            if (ShouldSaveTimeToJson)
                            {
                                JsonProcessing.SaveCurrentDayTime(elapsed);
                            }
                        };
                    }
                }
            }
            else
            {
                if (timer.IsRunning)
                {
                    lastElapsedTime = timer.GetAccumulatedTime(); // Зберігаємо перед зупинкою
                    Console.WriteLine($"Зупинка таймера для проєкту: {currentActiveProject}, збережено час: {lastElapsedTime}");
                    timer.Pause();
                    currentActiveProject = null;
                }
                Console.WriteLine("Активне вікно не відповідає жодному з проєктів.");
            }
        }

        private static void IfUserActive()
        {
            uint idleTime = GetIdleTime();
            if (idleTime < 3) // 5 секунд - поріг активності
            {
                Console.WriteLine("Користувач активний");
            }
            else
            {
                Console.WriteLine("Користувач неактивний більше 5 секунд");
            }
        }

        [STAThread]
        private static void Main()
        {
            // Запуск форми в окремому потоці
            Thread formThread = new Thread(() =>
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            });
            formThread.SetApartmentState(ApartmentState.STA); // Встановлюємо режим STA для окремого потоку
            formThread.IsBackground = true; // Потік закриється разом із головним
            formThread.Start();


            // Головний цикл перевірок
            while (true)
            {
                var stLP = new Stopwatch();
                stLP.Start();
                var projects = LoadProjects();
                stLP.Stop();
                Console.WriteLine($"stlp {stLP.Elapsed.TotalSeconds}");


                if (projects.Count == 0)
                {
                    Console.WriteLine("Немає проектів для перевірки.");
                }
                else
                {
                    var stLP2 = new Stopwatch();
                    stLP2.Start();

                    CompareProjectsWithOpenProcesses(projects);

                    stLP2.Stop();
                    Console.WriteLine($"stlp2 {stLP2.Elapsed.TotalSeconds}");


                    // Відправляємо Action для оновлення часу в Form1
                    CheckActiveWindow(projects, elapsed =>
                    {
                        // Потрібно викликати метод HandButton_OnTimeUpdated
                        Form1.Instance?.HandButton_OnTimeUpdated(elapsed);
                    });

                    IfUserActive();
                }

               // Console.WriteLine($"єбать. {DateTime.Now}");

                Thread.Sleep(1000); // Чекаємо 1 секунду
            }
        }
    }
}





















