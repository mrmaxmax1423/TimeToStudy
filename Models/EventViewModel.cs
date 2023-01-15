using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeToStudy.Models
{
    public class EventViewModel
    {
        public string EventLabel { get; set; }
        public string EventDescription { get; set; }
        public double EventLength { get; set; }
        public bool Reccuring { get; set; }
        public bool SetTime { get; set; }
        public DateTime EventTime { get; set; }
    }

    /*
    public EventViewModel AddEvent()
    {
        string EventName = ;
    }
    */
}
