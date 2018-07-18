using homework_5_bsa2018.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace homework_5_bsa2018.DAL
{
    public class AirportContext:DbContext
    {
        public virtual DbSet<Pilot> Pilots { get; set; }
        public virtual DbSet<Stewardess> Stewardesses { get; set; }
        public virtual DbSet<Crew> Crews { get; set; }
        public virtual DbSet<Plane> Planes { get; set; }
        public virtual DbSet<PlaneType> PlaneTypes { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Departure> Departures { get; set; }

        public AirportContext(DbContextOptions<AirportContext> contextOptions, SeedData seedData) :base(contextOptions)
        {
            Database.Migrate();
            Database.EnsureCreated();
           
            seedData.Initialize(this);
        }
        public AirportContext()
        {

        }
    }
}
