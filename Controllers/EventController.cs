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
        public IActionResult AddClass()
        {
            return View();
        }
        //Loads the Calendar Page with Events from DB
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

        //Loads the Calendar Page
        public IActionResult Classes()
        {
            //creates a list storing Events
            List<Class> classes;
            //Fills list with events from DB
            classes = context.Classes
                .OrderBy(c => c.ClassId).ToList();

            //creates new model with a list of Events
            var model = new EventViewModel
            {
                Classes = classes
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
            //if user input is invalid
            else
            {
                return RedirectToAction("AddEvent");
                /*
                model.Event = context.Events.ToList();
                return View(model);
                */
            }
        }

        public IActionResult InputClass(EventViewModel model)
        {
            //if user input is valid
            if (ModelState.IsValid)
            {
                context.Classes.Add(model.CurrentClass);
                context.SaveChanges();
                return RedirectToAction("AddClass");
            }
            //if user input is in
            else
            {
                return RedirectToAction("AddClass");
                /*
                model.Event = context.Events.ToList();
                return View(model);
                */
            }
        }

        //Pulls users From Canvas, missing conection and functionality
        public static async void GetCanvasClasses()
        {
            string baseURL = $""; //URL connection
            //API call should be in a TRY Catch
            try
            {

            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
            }
        }


        //Nonfunctional, need development to edit and delete events from the database
        [HttpPost]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectedEvent = await context.Events.FindAsync(id);
            if(selectedEvent == null)
            {
                return NotFound();
            }
            return View(selectedEvent);
        }

        //Only Basics, Needs improvments
        [HttpPost]
        public IActionResult DeleteEvent([FromRoute] string id, Event selectedEvent)
        {
            context.Events.Remove(selectedEvent); //remove selected event from DB
            context.SaveChanges();                //save changes to DB
            return RedirectToAction("Calendar", new { ID = id } );
        }
        /*
        public IActionResult AddNewEvent()
        {
            return View();
        }
        */
    }

}
