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

        //Loads the Calendar Page
        public IActionResult Calendar()
        {
            //creates a list storing Events
            List<Event> events;
            //Fills list with events from DB
            events = context.Events
                .OrderBy (e => e.EventId).ToList();

            //creates new model with a list of Events
            var model = new EventViewModel
            {
                Events = events
            };
            //Binds list to view
            return View(model);
        }

        [HttpPost]
        //adds event to database
        public IActionResult Add(EventViewModel model)
        {
            //if user input is valid
            if (ModelState.IsValid)
            {
                context.Events.Add(model.CurrentEvent);
                context.SaveChanges();
                return RedirectToAction("AddEvent");
            }
            //if user input is in
            else
            {
                return RedirectToAction("AddEvent");
                /*
                model.Event = context.Events.ToList();
                return View(model);
                */
            }
        }


        //Nonfunctional, need development to edit and delete events from the database
        [HttpPost]
        public IActionResult Edit([FromRoute] string id, Event selected)
        {
            context.Events.Update(selected);      //update selected event
            return RedirectToAction("AddEvent");
        }

        //Only Basics, Needs improvments
        [HttpPost]
        public IActionResult DeleteEvent(Event selectedEvent)
        {
            context.Events.Remove(selectedEvent); //remove selected event from DB
            //context.SaveChanges();                //save changes to DB
            return RedirectToAction("Calendar");
        }
        /*
        public IActionResult AddNewEvent()
        {
            return View();
        }
        */
    }

}
