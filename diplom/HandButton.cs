using System;
using System.Timers;

namespace diplom
{
    public class HandButton
    {
        private Timer timer;               // Таймер для відліку часу
        private TimeSpan totalTime;        // Загальний час, що пройшов
        private DateTime startTime;        // Час початку (для вимірювання часу кожного циклу)
        private bool isRunning;            // Статус таймера (запущено чи ні)

        public HandButton()
        {
            timer = new Timer(1000);        // Таймер, що спрацьовує кожну секунду
            timer.Elapsed += Timer_Elapsed; // Підключення обробника події
            totalTime = TimeSpan.Zero;      // Початковий час
            isRunning = false;              // Таймер не запущено
        }

        // Метод для запуску таймера
        public void Start()
        {
            if (!isRunning)
            {
                startTime = DateTime.Now;   // Фіксуємо час початку
                timer.Start();               // Запускаємо таймер
                isRunning = true;
            }
        }

        // Публічна властивість для перевірки статусу таймера
        public bool IsRunning
        {
            get { return isRunning; }
        }

        // Метод для призупинення таймера
        public void Pause()
        {
            if (isRunning)
            {
                timer.Stop();              // Зупиняємо таймер
                totalTime += DateTime.Now - startTime;  // Додаємо час, що пройшов
                isRunning = false;
            }
        }

        // Обробник події Elapsed (кожну секунду)
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimeSpan elapsed = totalTime + (DateTime.Now - startTime);
            OnTimeUpdated?.Invoke(elapsed);  // Викликаємо подію для оновлення часу
        }

        // Подія для оновлення часу
        public event Action<TimeSpan> OnTimeUpdated;

        // Метод для отримання поточного часу
        public string GetCurrentTime()
        {
            return (totalTime + (isRunning ? (DateTime.Now - startTime) : TimeSpan.Zero)).ToString(@"hh\:mm\:ss");
        }

        // Метод для отримання загального часу
        public TimeSpan GetAccumulatedTime()
        {
            return totalTime + (isRunning ? (DateTime.Now - startTime) : TimeSpan.Zero);
        }

        // Метод для запуску таймера з заданим часом
        public void StartWithTime(TimeSpan time)
        {
            if (!isRunning)
            {
                totalTime = time;          // Встановлюємо початковий час
                startTime = DateTime.Now;   // Фіксуємо час початку
                timer.Start();              // Запускаємо таймер
                isRunning = true;
            }
        }

        // Метод для встановлення часу вручну
        public void SetElapsedTime(TimeSpan elapsedTime)
        {
            totalTime = elapsedTime; // Встановлюємо накопичений час
            Console.WriteLine($"Час встановлено: {totalTime}");
            if (isRunning)
            {
                startTime = DateTime.Now; // Оновлюємо час початку
                Console.WriteLine("Таймер запущено після встановлення часу.");
            }
        }

        public TimeSpan ElapsedTime
        {
            get { return GetAccumulatedTime(); }
        }
    }
}

