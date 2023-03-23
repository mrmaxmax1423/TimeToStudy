using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
    }
}   
