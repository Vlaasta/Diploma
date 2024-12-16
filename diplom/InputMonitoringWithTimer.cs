using System;
using System.Timers;

namespace diplom
{
    public class InputMonitoringWithTimer
    {
        private static Timer timer;

        public static void StartMonitoring()
        {
            // Налаштовуємо таймер на 1 хвилину (60000 мс)
            timer = new Timer(600000); // 10 хвилина
            timer.Elapsed += OnTimedEvent; // Метод, який викликається раз на хвилину
            timer.AutoReset = true; // Таймер перезапускається автоматично
            timer.Enabled = true; // Вмикаємо таймер*/

            // Запускаємо хуки одразу
            GlobalHooks.Start();
        }

        private static void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"Таймер спрацював: {DateTime.Now}");
            // Тут можна реалізувати логіку перевірки дій, якщо потрібно
            // У вашому випадку хуки вже працюють у GlobalHooks
        }

        public static void StopMonitoring()
        {
            // Зупиняємо таймер і зупиняємо хуки
            timer.Stop();
            GlobalHooks.Stop();
        }
    }
}
