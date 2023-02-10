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

        [Route("[controller]s/{id?}")]
        public IActionResult Calendar(int id)
        {
            List<Event> events;
            events = context.Events
                .OrderBy (e => e.EventId).ToList();

            var model = new EventViewModel
            {
                Events = events
            };
            return View(model);
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
                return RedirectToAction("AddEvent");
                /*
                model.Event = context.Events.ToList();
                return View(model);
                */
            }
        }

        //Lists Events on the Calendar page


        //Missing Functionality
        [HttpPost]
        public IActionResult Edit([FromRoute] string id, Event selected)
        {
            context.Events.Update(selected);      //update selected event
            return RedirectToAction("AddEvent");
        }

        //Only Basics, Needs improvments
        [HttpPost]
        public IActionResult DeleteEvent([FromRoute] string id, Event selectedEvent)
        {
            context.Events.Remove(selectedEvent); //remove selected event from DB
            context.SaveChanges();                //save changes to DB
            return RedirectToAction("AddEvent");
        }
        /*
        public IActionResult AddNewEvent()
        {
            return View();
        }
        */
    }

}
