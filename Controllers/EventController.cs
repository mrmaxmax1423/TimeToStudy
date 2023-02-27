using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeToStudy.Models;
using Microsoft.AspNetCore.Authorization;
using RestSharp;
using Microsoft.Extensions.Configuration;

namespace TimeToStudy.Controllers
{
    public class EventController : Controller
    {
        private EventContext context;
        public EventController(EventContext ctx, IConfiguration configuration)
        {
            _canvasApiService = new CanvasApiService(configuration);
            context = ctx;
        }

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

        //Creates Model holding information for API call to canvas
        private readonly CanvasApiService _canvasApiService;

        //Returns a list of users courses to a blank webpage, needs to be processed and saved as classes (also filter for 
        public async Task<IActionResult> CanvasList()
        {
            var courses = await _canvasApiService.GetCoursesAsync();
            return Ok(courses);
        }

        //Nonfunctional, need development to edit and delete events from the database
        [HttpPost]
        public IActionResult Edit([FromRoute] string id, Event selected)
        {
            string newDesc = "Test";//selected.EventDescription;
            selected = context.Events.Find(selected.EventId);
            selected.EventDescription = newDesc;
            context.Events.Update(selected);
            Console.WriteLine("A");
            context.SaveChanges();

            return RedirectToAction("Calendar", new { ID = id });
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
