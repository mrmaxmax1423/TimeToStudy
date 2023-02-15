using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TimeToStudy.Models
{
    public class EventViewModel
    {
        //allows events to be stored in a list
        public List<Event> Events { get; set; }

        public List<Class> Classes { get; set; }
        //stores event being added in AddEvent
        public Event CurrentEvent { get; set; }

        //stores class being manually added
        public Class CurrentClass { get; set; }
    }
}
