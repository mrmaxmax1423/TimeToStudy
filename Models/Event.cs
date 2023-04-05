using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

//Model for storing Events
namespace TimeToStudy.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "Please input a Label.")]
        public string EventLabel { get; set; }
        [Required(ErrorMessage = "Please input a Description.")]
        public string EventDescription { get; set; }
        [Required(ErrorMessage = "Please input Event Length.")]
        public double EventLength { get; set; }
        public bool Reccuring { get; set; }
        public bool SetTime { get; set; }

        //Accept nulls, when generating schedules search for open times for events with null eventtimes
        public DateTime EventTime { get; set; }
    }

}