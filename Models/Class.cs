using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

//Model for storing Class information
namespace TimeToStudy.Models
{
    //Needs more information regarding classes, ideally from canvas
    //Classes will store information about how many credit hours and class times to generate events
    public class Class
    {
        public int ClassId { get; set; }

        public int? CanvasId { get; set; }

        [Required(ErrorMessage = "Please input class name.")]
        public string ClassName { get; set; }

        //[Required(ErrorMessage = "Please input class start time.")]

        public string ClassDescription { get; set; }
    
        public DateTime? MeetingTime { get; set; }

        public bool Imported { get; set; } = false;
        //[Required(ErrorMessage = "Please input class length.")]
        //public double? ClassLength { get; set; }

        //[Required(ErrorMessage = "Please input class meeting days.")]
        //public List<DateTime> MeetingDays { get; set; }

        //public double? CreditHours { get; set; }


        //public DateTime? StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
    }
}