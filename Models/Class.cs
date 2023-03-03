using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeToStudy.Models
{
    //Classes will store information about how many credit hours and class times to generate events
    public class Class
    {
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Please input class name.")]
        public string ClassName { get; set; }

        [Required(ErrorMessage = "Please input class start time.")]
        /* Figure out how to store TimeSpan in DB
        public TimeSpan ClassStartTime { get; set; }
        */


        public String ClassStartTime { get; set; }

        //[Required(ErrorMessage = "Please input class description.")]
        //public string? ClassDescription { get; set; }

        [Required(ErrorMessage = "Please input class length.")]
        public double ClassLength { get; set; }

        [Required(ErrorMessage = "Please input class meeting days.")]
        //public List<DateTime> MeetingDays { get; set; }

        public double CreditHours { get; set; }

        //public DateTime? StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
    }
}