using Microsoft.EntityFrameworkCore;

namespace TimeToStudy.Models
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> options)
            : base(options) { }

        public DbSet<Event> Events { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasData(
                new Event {Id = "001", EventDescription = "1", EventLabel = "2", EventLength = 3, Reccuring = false, },
                new Event {Id = "002", EventDescription = "3", EventLabel = "4", EventLength = 5, Reccuring = true, }
            ); 
        }
    }
}   
