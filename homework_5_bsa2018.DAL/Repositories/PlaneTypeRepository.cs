using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homework_5_bsa2018.DAL.Repositories
{
    public class PlaneTypeRepository : IRepository<PlaneType>
    {
        private AirportContext db;

        public PlaneTypeRepository(AirportContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<PlaneType>> GetAllAsync() =>
            await db.PlaneTypes.ToListAsync();

        public async Task<PlaneType> GetAsync(int id) => 
            await db.PlaneTypes.FindAsync(id);

        public async Task Create(PlaneType planetype)
        {
            await db.PlaneTypes.AddAsync(planetype);
        }

        public async Task Update(int id, PlaneType planetype)
        {
            var item = db.PlaneTypes.Find(id);
            if (item == null) throw new ArgumentNullException();

            db.PlaneTypes.Remove(item);
            await    db.PlaneTypes.AddAsync(planetype);
            
        }

        public void Delete(int id)
        {
            var item = db.PlaneTypes.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.PlaneTypes.Remove(item);
        }
    }
}
