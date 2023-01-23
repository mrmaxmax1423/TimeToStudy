using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeToStudy.Models;

namespace TimeToStudy.Controllers
{
    public class EventController : Controller
    {
        private EventContext context;
        public EventController(EventContext ctx) => context = ctx;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddEvent()
        {
            return View();
        }

        public IActionResult Add()
        {
            EventViewModel model = new EventViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                context.Events.Add(model.CurrentEvent);
                context.SaveChanges();
                return RedirectToAction("AddEvent");
            }
            else
            {
                return RedirectToAction("Index");
                /*
                model.Event = context.Events.ToList();
                return View(model);
                */
            }
        }

        /*
        public IActionResult AddNewEvent()
        {
            return View();
        }
        */

        public IActionResult Calendar()
        {
            return View();
        }
    }

}
