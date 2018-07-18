using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_5_bsa2018.DAL.Repositories
{
    public class FlightRepository : IRepository<Flight>
    {
        private AirportContext db;

        public FlightRepository(AirportContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Flight>> GetAllAsync() =>
           await db.Flights.Include(c=>c.Tickets).ToListAsync();

        public async Task<Flight> GetAsync(int id) => 
            (await GetAllAsync()).FirstOrDefault(item=>item.Id == id);

        public async Task Create(Flight flight)
        {
           await db.Flights.AddAsync(flight);
        }

        public async Task Update(int id, Flight flight)
        {
            var item = db.Flights.Find(id);
            if (item == null) throw new ArgumentNullException();

            db.Flights.Remove(item);
            await    db.Flights.AddAsync(flight);
            
        }

        public void Delete(int id)
        {
            var item = db.Flights.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Flights.Remove(item);
        }
    }
}
