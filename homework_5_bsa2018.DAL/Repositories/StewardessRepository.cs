using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homework_5_bsa2018.DAL.Repositories
{
    public class StewardessRepository : IRepository<Stewardess>
    {
        private AirportContext db;

        public StewardessRepository(AirportContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Stewardess>> GetAllAsync() =>
           await db.Stewardesses.ToListAsync();

        public async Task<Stewardess> GetAsync(int id) => 
            await db.Stewardesses.FindAsync(id);

        public async Task Create(Stewardess stewardess)
        {
            await db.Stewardesses.AddAsync(stewardess);
        }

        public async Task Update(int id, Stewardess stew)
        {
            var item = db.Stewardesses.Find(id);
            if (item == null) throw new ArgumentNullException();

            db.Stewardesses.Remove(item);
             await   db.Stewardesses.AddAsync(stew);
            
        }


        public void Delete(int id)
        {
            var item = db.Stewardesses.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Stewardesses.Remove(item);
        }
    }
}
