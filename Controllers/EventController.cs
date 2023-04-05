using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TimeToStudy.Models;

//Controller for pages and models regarding Classes and Events
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

        public IActionResult Calendar()
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
        public IActionResult Homework()
        {
            //creates a list storing Events
            List<Event> events;
            //Fills list with events from DB
            events = context.Events
                .OrderBy(e => e.EventId).ToList();

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
            //Create a list of courses from the Json
            List<dynamic> rawCourses = JsonConvert.DeserializeObject<List<dynamic>>(courses);
            //List<Class> filteredClasses = new List<Class>();

            foreach (dynamic course in rawCourses)
            {
                var courseCode = course.course_code?.ToString();
                if (!string.IsNullOrEmpty(courseCode) && Regex.IsMatch(courseCode, @"^\d{4}"))
                {
                    Class canvasClass = new Class
                    {
                        //ClassId = course.id,
                        ClassName = course.name,
                        ClassStartTime = "",
                        ClassLength = 3,
                        CreditHours = 2
                        //ClassDescription = course.public_description,
                        //StartDate = course.start_at,
                        //EndDate = course.end_at
                    };
                    context.Classes.Add(canvasClass);
                    context.SaveChanges();
                    //Console.WriteLine("Class ID: " + courseInfo.ClassId + " Class Name: " + courseInfo.ClassName + " Start Date: " /* + courseInfo.ClassDescription + " "  + courseInfo.StartDate + " End Date: " + courseInfo.EndDate*/);

                    //filteredClasses.Add(canvasClass);
                }
            }
            return RedirectToAction("Classes");
        }

        //Nonfunctional, need development to edit and delete events from the database
        [HttpPost]
        public IActionResult Edit([FromRoute] string id, Event selected)
        {
            //Needs to use another form to take user input instead of constant string
            string newDesc = "Test";//selected.EventDescription;
            //whichever event was selected
            selected = context.Events.Find(selected.EventId);
            //replace description
            selected.EventDescription = newDesc;
            context.Events.Update(selected);
            context.SaveChanges();


            return RedirectToAction("Calendar", new { ID = id });
        }

        [HttpPost]
        public IActionResult DeleteEvent([FromRoute] string id, Event selectedEvent)
        {
            context.Events.Remove(selectedEvent); //remove selected event from DB
            context.SaveChanges();                //save changes to DB
            return RedirectToAction("Homework", new { ID = id });
        }

        [HttpPost]
        public IActionResult DeleteClass([FromRoute] string id, Class selectedClass)
        {
            context.Classes.Remove(selectedClass); //remove selected event from DB
            context.SaveChanges();                //save changes to DB
            return RedirectToAction("Classes", new { ID = id });
        }

        public IActionResult ScheduleEvents()
        {
            var events = context.Events;
            foreach (var Event in events) //Check all events added by user
            {
                if (Event.EventTime == null) //If Event doesn't have a set time
                {
                    Event.EventTime = new System.DateTime(2015, 12, 31, 5, 10, 20);
                    context.Events.Update(Event);
                }
            }
            context.SaveChanges();
            return RedirectToAction("Classes");
        }
    }
}