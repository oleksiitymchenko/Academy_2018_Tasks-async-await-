using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_5_bsa2018.DAL.Repositories
{
    internal class CrewRepository : IRepository<Crew>
    {
        private AirportContext db;

        public CrewRepository(AirportContext context)
        {
            this.db = context;
        }

        public async Task<IEnumerable<Crew>> GetAllAsync() =>
            await db.Crews.Include(c => c.Stewardesses).Include(c => c.Pilot).ToListAsync();

        public async Task<Crew> GetAsync(int id)
        {
            var all = await GetAllAsync();
            return all.FirstOrDefault(item => item.Id == id);
        }

        public async Task Create(Crew crew)
        {
            await db.Crews.AddAsync(crew);
        }

        public async Task Update(int id, Crew crew)
        {
            var item = db.Crews.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Crews.Remove(item);
            await db.Crews.AddAsync(crew);
        }
        
        public void Delete(int id)
        {
            var item = db.Crews.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Crews.Remove(item);
        }
    }
}
