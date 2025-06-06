using System.Collections.Generic;

namespace diplom
{
    public class TimerData
    {
        public string Date { get; set; } //"dd.MM.yyyy"
        public string Time { get; set; } //"hh:mm:ss"
        public List<Session> Sessions { get; set; } = new List<Session>();
    }
}
