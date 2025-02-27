using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace diplom
{
    internal static class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hmod, int dwThreadId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static readonly string ProjectsFilePath = @"E:\4 KURS\Диплом\DiplomaRepo\Diploma\data\projects.json";
        private static TimeSpan lastElapsedTime = TimeSpan.Zero;
        public static bool ShouldSaveTimeToJson = true;
        private static HandButton timer = new HandButton();
        private static string currentActiveProject;

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        public static Func<string> GetLabel1TextDelegate { get; set; }

        // private static TimeSpan ElapsedTime;

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
                return ((uint)Environment.TickCount - info.dwTime) / 60000; // Idle time in seconds
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

        public static async Task CompareProjectsWithOpenProcessesAsync(List<Project> projects)
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
                    // Ignore access errors
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

            await Task.Delay(100); // Імітація затримки
        }

        private static async Task MainLoopAsync()
        {
            while (true)
            {
                // Завантажуємо проєкти з файлу projects.json
                var projects = LoadProjects();

                if (projects.Count == 0)
                {
                    Console.WriteLine("Немає проектів для перевірки.");
                }
                else
                {
                    await CompareProjectsWithOpenProcessesAsync(projects);

                    // Якщо активне вікно - це браузер, не оновлюємо таймер проєкту
                    if (!BrowserMonitor.IsBrowserActive())
                    {
                        CheckActiveWindow(projects, elapsed =>
                        {
                            Form1.Instance?.Invoke(new Action(() =>
                            {
                                Form1.Instance?.HandButton_OnTimeUpdated(elapsed);
                            }));
                        });
                    }
                }

                // **Перевірка активного вікна браузера**
                BrowserMonitor.CheckActiveWindow();

                IfUserActive();
                await Task.Delay(1000); // Асинхронна пауза
            }
        }


        private static void IfUserActive()
        {
            uint idleTime = GetIdleTime();
            int currentNonActiveTime = Form1.GetNonActiveTime();

            if (idleTime < currentNonActiveTime)
            {
                Console.WriteLine("Користувач активний");
            }
            else
            {
                Console.WriteLine("Користувач неактивний більше" + currentNonActiveTime);
            }
        }

        [STAThread]
        private static void Main()
        {
            JsonProcessing.LoadSettings();

            // Запуск форми в окремому потоці
            Thread formThread = new Thread(() =>
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            });
            formThread.SetApartmentState(ApartmentState.STA);
            formThread.IsBackground = true;
            formThread.Start();

            // Асинхронний запуск основного циклу
            Task.Run(MainLoopAsync).Wait();
        }
    }
}




















