using Microsoft.EntityFrameworkCore;
using EventsApi.Models;

namespace EventsApi.Models
{
    public class EventsContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public EventsContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
        {
            options.UseNpgsql(Configuration.GetConnectionString("EventsDatabase"));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        }

        public DbSet<Event> Events { get; set; } = null!;
    }
}