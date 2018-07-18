using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace homework_5_bsa2018.DAL.Repositories
{
    public class PilotRepository : IRepository<Pilot>
    {
        private AirportContext db;

        public PilotRepository(AirportContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Pilot>> GetAllAsync() =>
            await db.Pilots.ToListAsync();

        public async Task<Pilot> GetAsync(int id) =>
            await db.Pilots.FindAsync(id);
            
        public async Task Create(Pilot pilot)
        {
            await db.Pilots.AddAsync(pilot);
        }

        public async Task Update(int id, Pilot pilot)
        {
            var item = db.Pilots.Find(id);
            if (item == null) throw new ArgumentNullException();

            db.Pilots.Remove(item);
            await db.Pilots.AddAsync(pilot);
            
        }

        public void Delete(int id)
        {
            var item = db.Pilots.FirstOrDefaultAsync(p => p.Id == id).Result;
            if (item == null) throw new ArgumentNullException();
            db.Pilots.Remove(item);
        }
    }
}
