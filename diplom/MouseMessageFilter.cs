using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace diplom
{
    // Клас для моніторингу миші
    public class ActivityMonitoring
    {
        private static TimeSpan lastElapsedTime = TimeSpan.Zero;
        public static bool ShouldSaveTimeToJson = true;
        private static HandButton timer = new HandButton();
        private static string currentActiveProject;

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        public static Func<string> GetLabel1TextDelegate { get; set; }
        private static bool notificationShown = false; 

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
                return ((uint)Environment.TickCount - info.dwTime) / 60000; // Idle time в хв
            }
            return 0;
        }

        private static void IfUserActive()
        {
            uint idleTime = GetIdleTime();
            int currentNonActiveTime = Form1.GetNonActiveTime();

            if (idleTime < currentNonActiveTime)
            {
                Console.WriteLine("Користувач активний");
                notificationShown = false; 
            }
            else
            {
                if (!notificationShown && Form1.notificationsOnOff)
                {
                    Notifications.Show("Таймер зупинено через неактивність більше " + currentNonActiveTime + " хв.");
                    notificationShown = true; 
                }
            }
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

            var activeProject = projects.FirstOrDefault(project => activeWindowTitle.IndexOf(Path.GetFileNameWithoutExtension(project.Path), StringComparison.OrdinalIgnoreCase) >= 0);

            if (activeProject != null)
            {
                IfUserActive();

                if (currentActiveProject != activeProject.Name)
                {
                    if (timer.IsRunning)
                    {
                        lastElapsedTime = timer.GetAccumulatedTime(); 
                        string sessionInfo = $"Проєкт: {currentActiveProject}, збережений час: {lastElapsedTime}";
                        CohereClient.sessionLog.Add(sessionInfo);
                        JsonProcessing.SaveSessionStop();
                        timer.Pause();
                        Notifications.Show("Таймер зупинено!");
                    }

                    if (!timer.IsRunning)
                    {
                        if (GetLabel1TextDelegate != null && TimeSpan.TryParseExact(GetLabel1TextDelegate.Invoke(), @"hh\:mm\:ss", null, out TimeSpan lastElapsed))
                        {
                            timer.SetElapsedTime(lastElapsed);
                        }
                        else
                        {
                            timer.SetElapsedTime(TimeSpan.Zero);
                        }
                        Console.WriteLine($"Запуск таймера для проєкту: {activeProject.Name}, продовження з часу: {lastElapsedTime}");
                        JsonProcessing.SaveSessionStart();
                        timer.Start();
                        Notifications.Show("Таймер запущено!");
                        currentActiveProject = activeProject.Name;
                        timer.OnTimeUpdated += (elapsed) =>
                        {
                            onTimeUpdated(elapsed);
                            if (ShouldSaveTimeToJson)
                                JsonProcessing.SaveCurrentDayTime(elapsed);
                        };
                    }
                }
            }
            else
            {
                if (timer.IsRunning)
                {
                    lastElapsedTime = timer.GetAccumulatedTime();
                    Console.WriteLine($"Зупинка таймера для проєкту: {currentActiveProject}, збережено час: {lastElapsedTime}");
                    JsonProcessing.SaveSessionStop();
                    timer.Pause();
                    currentActiveProject = null;
                }
                Console.WriteLine("Активне вікно не відповідає жодному з проєктів.");
            }
        }

        public static async Task MainLoopAsync()
        {
            while (true)
            {
                var projects = JsonProcessing.LoadProjects();

                if (projects.Count == 0)
                {
                    Console.WriteLine("Немає проектів для перевірки.");
                }
                else
                {
                    //await CompareProjectsWithOpenProcessesAsync(projects);
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

                await Task.Delay(1000); //пауза
            }
        }
    }

}
