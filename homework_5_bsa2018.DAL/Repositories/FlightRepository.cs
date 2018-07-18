using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace homework_5_bsa2018.DAL.Repositories
{
    public class FlightRepository : IRepository<Flight>
    {
        private AirportContext db;

        public FlightRepository(AirportContext context)
        {
            db = context;
        }

        public IEnumerable<Flight> GetAll() =>
            db.Flights.Include(c=>c.Tickets);

        public Flight Get(int id) => 
            GetAll().FirstOrDefault(item=>item.Id == id);

        public void Create(Flight flight)
        {
            db.Flights.Add(flight);
        }

        public void Update(int id, Flight flight)
        {
            var item = db.Flights.Find(id);
            if (item == null) throw new ArgumentNullException();

            db.Flights.Remove(item);
                db.Flights.Add(flight);
            
        }

        public void Delete(int id)
        {
            var item = db.Flights.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Flights.Remove(item);
        }
    }
}
