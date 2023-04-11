using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

//Model for creating DB
namespace TimeToStudy.Models
{
    public class EventContext : IdentityDbContext<IdentityUser>
    {
        public EventContext(DbContextOptions<EventContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Class> Classes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Call the method to populate the initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Create some initial data
            var event1 = new Event { EventId = 1, EventLabel = "IT Expo", EventDescription = "Time To Study Demo", EventTime = new DateTime(2023,4,11,9,00,00), EventLength = 4 };
            var event2 = new Event { EventId = 2, EventLabel = "Calc Test", EventDescription = "Calculus 2 test over chapters 1-3", EventTime = new DateTime(2023, 4, 15, 16, 30, 00), EventLength = 2 };
            var event3 = new Event { EventId = 3, EventLabel = "Lunch", EventDescription = "Lunch with Jacob at Center Court", EventTime = new DateTime(2023, 4, 12, 2, 00, 00), EventLength = .75 };
            var event4 = new Event { EventId = 4, EventLabel = "Science HW", EventDescription = "Biology Chapter 3 Homework", DueDate = new DateTime(2023, 4, 23, 23, 59, 59), EventLength = 1.5 };
            var event5 = new Event { EventId = 5, EventLabel = "History HW", EventDescription = "Latin American History HW, read textbook chapter 3", EventTime = new DateTime(2023, 4, 13, 14, 15, 00), DueDate = new DateTime(2023, 4, 23, 23, 59, 59) };
            var event6 = new Event { EventId = 6, EventLabel = "Spanish Class", EventDescription = "Spanish 101", EventTime = new DateTime(2023, 4, 12, 9, 30, 00), EventLength = 2 };
            var event7 = new Event { EventId = 7, EventLabel = "Math Class", EventDescription = "Calculus 101", EventTime = new DateTime(2023, 4, 14, 14, 30, 00), EventLength = 2 };

            // Add the data to the context
            modelBuilder.Entity<Event>().HasData(event1, event2, event3, event4, event5, event6, event7);

            var class1 = new Class { ClassId = 1, ClassName = "Spanish", ClassDescription = "Spanish 101, Fall Semester", MeetingTime = new DateTime(2023, 4, 12, 9, 30, 00)};
            var class2 = new Class { ClassId = 2, ClassName = "Math", ClassDescription = "Calculus 101, Fall Semester", MeetingTime = new DateTime(2023, 4, 13, 14, 30, 00) };
            var class3 = new Class { ClassId = 3, ClassName = "Science", ClassDescription = "Chemistry, Fall Semester", MeetingTime = new DateTime(2023, 4, 10, 9, 30, 00) };
            // Add the data to the context
            modelBuilder.Entity<Class>().HasData(class1, class2, class3);


        }
    }
}   
