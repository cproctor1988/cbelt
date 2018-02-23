using Microsoft.EntityFrameworkCore;
 
namespace DojoActivity.Models
{
    public class ActivityContext : DbContext
    {
        
        // base() calls the parent class' constructor passing the "options" parameter along
        public ActivityContext(DbContextOptions<ActivityContext> options) : base(options) { }

        public DbSet<User> users {get;set;}
        public DbSet<Activity> activities {get;set;}
        public DbSet<Guest> guests {get;set;}
        
    }
}