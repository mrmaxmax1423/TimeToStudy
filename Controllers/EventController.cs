using Microsoft.AspNetCore.Mvc;                          
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
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

        public IActionResult Calendar(DateTime? date)
        {
            //if Date wasn't passed to calendar, set date to today
            var startDate = date ?? DateTime.Today;
            startDate = startDate.Date; //gets rid of hours, mins, etc

            // Calculate the start and end dates for the week that includes the specified date

            //creates a list storing Events
            //Fills list with events from DB in chronological order (only in between dates given to avoid searching every event in DB)
            var events = context.Events.Where(e => e.EventTime != null && e.EventTime >= startDate.Date && e.EventTime < startDate.AddDays(7)).OrderBy(e => e.EventTime).ToList();

            //creates new model with a list of Events
            var model = new EventViewModel
            {

                Events = events,
                SelectedDate = startDate
            };
            //Binds list to view
            return View(model);
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
                .OrderBy(e => e.EventTime).ToList();

            //creates new model with a list of Events
            var model = new EventViewModel
            {
                Events = events
            };
            //Binds list to view
            return View(model);
        }


        [HttpPost]
        public IActionResult AddClassTime([FromRoute] string id, DateTime newMeetingTime, Class selected)
        {
            //Needs to use another form to take user input instead of constant string
            //string newDesc = "Test";//selected.EventDescription;
            //whichever event was selected
            selected = context.Classes.Find(selected.ClassId);
            //replace description
            selected.MeetingTime = newMeetingTime;
            context.Classes.Update(selected);
            context.SaveChanges();


            return RedirectToAction("Classes");
        }

        [HttpPost]
        public IActionResult AddClassDescription([FromRoute] string id, String newDescription, Class selected)
        {
            //Needs to use another form to take user input instead of constant string
            //string newDesc = "Test";//selected.EventDescription;
            //whichever event was selected
            selected = context.Classes.Find(selected.ClassId);
            //replace description
            selected.ClassDescription = newDescription;
            context.Classes.Update(selected);
            context.SaveChanges();


            return RedirectToAction("Classes");
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
                ModelState.AddModelError(string.Empty, "Invalid Input, Try again");
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
            //if user input is invalid
            else
            {
                System.Console.WriteLine("Fail");
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


        public async Task<IActionResult> CanvasList()
        {
            bool alreadyAdded = false;
            var classes = context.Classes.OrderBy(c => c.ClassId).ToList();
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
                        CanvasId = course.id,
                        ClassName = course.name,
                        Imported = false
                        //ClassDescription = course.public_description,
                        //StartDate = course.start_at,
                        //EndDate = course.end_at
                    };
                    alreadyAdded = false;
                    foreach(Class c in classes)
                    {
                        if(canvasClass.ClassName == c.ClassName)
                        {
                            alreadyAdded = true;
                        }
                    }
                    //only save class to DB if it hasn't already been added
                    if (!alreadyAdded)
                    {
                        context.Classes.Add(canvasClass);
                        context.SaveChanges();
                    }
                    //Console.WriteLine("Class ID: " + courseInfo.ClassId + " Class Name: " + courseInfo.ClassName + " Start Date: " /* + courseInfo.ClassDescription + " "  + courseInfo.StartDate + " End Date: " + courseInfo.EndDate*/);

                    //filteredClasses.Add(canvasClass);
                }
            }
            return RedirectToAction("Classes");
        }

        public async Task<IActionResult> CanvasAssignments([FromRoute] string id, Class selectedClass)
        {
            System.Console.WriteLine(selectedClass.ClassName);
            System.Console.WriteLine(selectedClass.Imported);
            if (selectedClass.Imported == false)
            {
                System.Console.WriteLine("pass");
                var courseID = selectedClass.CanvasId;
                var assignments = await _canvasApiService.GetCourseAssignments(courseID.Value);
                List<dynamic> rawAssignments = JsonConvert.DeserializeObject<List<dynamic>>(assignments);
                //Create a list of courses from the Json
                foreach (dynamic assignment in rawAssignments)
                {
                    string cleanDescription = Regex.Replace(assignment.description.ToString(), @"<[^>]+>|&nbsp;", string.Empty);
                    cleanDescription = cleanDescription.Substring(0, 50);
                    if (assignment.due_at == null || assignment.due_at > DateTime.Today.AddDays(-7))
                    {
                        Event canvasEvent = new Event
                        {
                            EventDescription = cleanDescription,
                            EventLabel = assignment.name,
                            DueDate = assignment.due_at
                        };
                        context.Events.Add(canvasEvent);
                        context.SaveChanges();
                    }
                }
                //List<Class> filteredClasses = new List<Class>();

                var entity = await context.Classes.FindAsync(selectedClass.ClassId);

                entity.Imported = true;
                context.Classes.Update(entity);
                context.SaveChanges();

                return RedirectToAction("Classes");
            }
            System.Console.WriteLine("fail");
            return RedirectToAction("Classes");
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

        //Find open time for selected event (button is only displayed when event has no assigned time)
        public IActionResult ScheduleEvent([FromRoute] string id, Event selected)
        {
            //finds selected event from DB
            selected = context.Events.Find(selected.EventId);

            DateTime openTime = DateTime.Today.AddDays(1);
            DateTime dueDate = DateTime.Today.AddDays(8);
            //use one to decide where the best time for an event is
            int averageHours = 0;
            int averageEvents = 0;

            //find due date, either input by user or assume in 8 days
            if(selected.DueDate != null)
            {
                dueDate = selected.DueDate.Value;
                System.Console.WriteLine("A");
            }
            else
            {
                dueDate.AddDays(8);
                System.Console.WriteLine("B");
            }
            //Add a day to avoid attempting to schedule event for the current day
            openTime.AddDays(0);

            List<DateTime> availableTimes = new List<DateTime>();
            TimeSpan range = dueDate - openTime;
            int intRange = (int)range.TotalDays;
            for(int days = 0; days < intRange; days++)
            {

                for (int hour = 6; hour <= 18; hour++)
                {
                    for (int minute = 0; minute < 60; minute += 15)
                    {
                        DateTime checktime = new DateTime(openTime.Year, openTime.Month, openTime.Day + days, hour, minute, 0);
                        availableTimes.Add(checktime);
                        //System.Console.WriteLine(checktime);
                    }
                }
            }

            
            //create a list of events with potential conflicting times
            var conflictEvents = context.Events.Where(e => e.EventTime != null && e.EventTime >= openTime.Date && e.EventTime < dueDate).OrderBy(e => e.EventTime).ToList();
            //System.Console.WriteLine(openTime);
            //System.Console.WriteLine(dueDate);
            //System.Console.WriteLine(availableTimes.Count);

            //removes occupied times from list of availableTimes
            foreach (Event s in conflictEvents)
            {
                System.Console.WriteLine(conflictEvents.Count);
                if(s.EventLength > 0)
                {
                    availableTimes.RemoveAll(availableTimes => (s.EventTime >= availableTimes.AddHours(-s.EventLength - .5) && s.EventTime <= availableTimes) && (availableTimes >= s.EventTime && availableTimes < s.EventTime.Value.AddHours(s.EventLength + .5)));
                    availableTimes.RemoveAll(availableTimes => (s.EventTime.Value.AddHours(s.EventLength) >= availableTimes && s.EventTime.Value.AddHours(s.EventLength) >= availableTimes));
                }
                else
                {
                    availableTimes.RemoveAll(availableTimes => (s.EventTime >= availableTimes.AddHours(-2 - .5) && s.EventTime <= availableTimes) && (availableTimes >= s.EventTime && availableTimes < s.EventTime.Value.AddHours(2 + .5)));
                    availableTimes.RemoveAll(availableTimes => (s.EventTime.Value.AddHours(2) >= availableTimes && s.EventTime.Value.AddHours(2) >= availableTimes));
                }

            }
            foreach (DateTime t in availableTimes)
            {
                System.Console.WriteLine(t);
            }
            //Maybe find average hours/events on each day of week and insert into least populated time
            //on that day, check what time is taken by comparing potential time and seeing if its at the same time as another, if so check another time. > eventTime && < eventTime + eventLength
            //update event and reload page
            selected.EventTime = availableTimes.ElementAt(5);
            selected.SetTime = true;
            context.Events.Update(selected);
            context.SaveChanges();
            return RedirectToAction("Homework");
        }
    }
}
