using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diplom
{
    public class UrlData
    {
        public string Url { get; set; }
        public string PageTitle { get; set; }
        public string Timestamp { get; set; }
        public int TimeSpent { get; set; } // В секундах
    }
}
