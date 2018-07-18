using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace homework_5_bsa2018.DAL.Repositories
{
    internal class CrewRepository : IRepository<Crew>
    {
        private AirportContext db;

        public CrewRepository(AirportContext context)
        {
            this.db = context;
        }

        public IEnumerable<Crew> GetAll() =>
            db.Crews.Include(c => c.Stewardesses).Include(c => c.Pilot);

        public Crew Get(int id) =>
           GetAll().FirstOrDefault(item => item.Id == id);

        public void Create(Crew crew)
        {
            db.Crews.Add(crew);
        }

        public void Update(int id, Crew crew)
        {
            var item = db.Crews.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Crews.Remove(item);
            db.Crews.Add(crew);
        }
        
        public void Delete(int id)
        {
            var item = db.Crews.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Crews.Remove(item);
        }
    }
}
