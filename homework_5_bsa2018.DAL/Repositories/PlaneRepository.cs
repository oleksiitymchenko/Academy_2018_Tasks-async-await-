using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace homework_5_bsa2018.DAL.Repositories
{
    public class PlaneRepository : IRepository<Plane>
    {
        private AirportContext db;

        public PlaneRepository(AirportContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Plane>> GetAllAsync() =>
            await db.Planes.ToListAsync();

        public async Task<Plane> GetAsync(int id) =>
            await db.Planes.FindAsync(id);

        public async Task Create(Plane plane)
        {
            await db.AddAsync(plane);
        }

        public async Task Update(int id, Plane plane)
        {
            var item = db.Planes.Find(id);
            if (item == null) throw new ArgumentNullException();

            db.Planes.Remove(item);
            await db.Planes.AddAsync(plane);
            
        }

        public void Delete(int id)
        {
            var item = db.Planes.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Planes.Remove(item);
        }
    }
}
