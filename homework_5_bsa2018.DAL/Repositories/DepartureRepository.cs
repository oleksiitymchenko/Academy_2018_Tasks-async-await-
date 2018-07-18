using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_5_bsa2018.DAL.Repositories
{
    public class DepartureRepository:IRepository<Departure>
    {
        private AirportContext db;

        public DepartureRepository(AirportContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Departure>> GetAllAsync() =>
            await db.Departures.Include(c=>c.Plane).Include(c=>c.Crew).ToListAsync();

        public async Task<Departure> GetAsync(int id) => 
            (await GetAllAsync()).FirstOrDefault(item => item.Id == id);

        public async Task Create(Departure departure)
        {
            await db.Departures.AddAsync(departure);
        }


        public async Task Update(int id, Departure departure)
        {
            var item = db.Departures.Find(id);
            if (item == null) throw new ArgumentNullException();
                db.Departures.Remove(item);
                await  db.Departures.AddAsync(departure);
            }
        

        public void Delete(int id)
        {
            var item = db.Departures.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Departures.Remove(item);
        }
    }
}
