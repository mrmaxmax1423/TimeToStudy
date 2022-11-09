using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeToStudy.Models
{
    public class Event
    {
        [Required(ErrorMessage = "Please enter a name for the event.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description for the event.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a due date.")]
        public DateTime? DueDate { get; set; }
    }
}
