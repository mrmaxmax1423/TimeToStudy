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
    public class Classes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Please input class name.")]
        public string ClassName { get; set; }

        [Required(ErrorMessage = "Please input class start time.")]
        public DateTime ClassStartTime { get; set; }
        public string ClassLength { get; set; }
        [Required(ErrorMessage = "Please input class length.")]
        public double CreditHours { get; set; }

    }
}