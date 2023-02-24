using Microsoft.EntityFrameworkCore;

namespace TimeToStudy.Models
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> options)
            : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Class> Classes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>().HasData(
                new Event {EventId = 1, EventDescription = "1", EventLabel = "2", EventLength = 3, Reccuring = false , EventTime = new System.DateTime(2023)},
                new Event {EventId = 2, EventDescription = "3", EventLabel = "4", EventLength = 5, Reccuring = true , EventTime = new System.DateTime(2023)}
            );
            modelBuilder.Entity<Class>().HasData(
              new Class {ClassId = 1, ClassName = "Calculus II", ClassStartTime = "11:00", ClassLength = 1, CreditHours = 3 /*, EventTime = new System.DateTime(2023)*/},
              new Class {ClassId = 2, ClassName = "Chemistry", ClassStartTime = "12:30", ClassLength = 2, CreditHours = 3 /*, EventTime = new System.DateTime(2023)*/}
          );
        }
    }
}   
