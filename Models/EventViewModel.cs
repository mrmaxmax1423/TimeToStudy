using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

//Model for displaying Events to View Pages
namespace TimeToStudy.Models
{
    public class EventViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime SelectedDate { get; set; }

        //allows events to be stored in a list
        public List<Event> Events { get; set; }

        public List<Class> Classes { get; set; }
        //stores event being added in AddEvent
        public Event CurrentEvent { get; set; }
        public Class CurrentClass { get; set; }

    }
}
