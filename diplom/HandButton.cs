using System;
using System.Timers;

namespace diplom
{
    public class HandButton
    {
        private Timer timer;               //Таймер для відліку часу
        private TimeSpan totalTime;        //Загальний час, що пройшов
        private DateTime startTime;        //Час початку (для вимірювання часу кожного циклу)
        private bool isRunning;            //Статус таймера 

        public event Action<TimeSpan> OnTimeUpdated;

        public HandButton()
        {
            timer = new Timer(1000);       
            timer.Elapsed += Timer_Elapsed; 
            totalTime = TimeSpan.Zero;      
            isRunning = false;              
        }

        public void Start()
        {
            if (!isRunning)
            {
                startTime = DateTime.Now;   
                timer.Start();               
                isRunning = true;
            }
        }

        public void Pause()
        {
            if (isRunning)
            {
                timer.Stop();             
                totalTime += DateTime.Now - startTime; 
                isRunning = false;
            }
        }

        public bool IsRunning
        {
            get { return isRunning; }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimeSpan elapsed = totalTime + (DateTime.Now - startTime);
            OnTimeUpdated?.Invoke(elapsed); 
        }

        public TimeSpan GetAccumulatedTime()
        {
            return totalTime + (isRunning ? (DateTime.Now - startTime) : TimeSpan.Zero);
        }

        public void StartWithTime(TimeSpan time)
        {
            if (!isRunning)
            {
                totalTime = time;          
                startTime = DateTime.Now;   
                timer.Start();             
                isRunning = true;
            }
        }

        public void SetElapsedTime(TimeSpan elapsedTime)
        {
            totalTime = elapsedTime; 
            if (isRunning)
            {
                startTime = DateTime.Now; 
            }
        }

    }
}

