using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public IEnumerable<Pilot> GetAll() =>
            db.Pilots;

        public Pilot Get(int id) =>
            db.Pilots.Find(id);

        public void Create(Pilot pilot)
        {
            db.Pilots.Add(pilot);
        }

        public void Update(int id, Pilot pilot)
        {
            var item = db.Pilots.Find(id);
            if (item == null) throw new ArgumentNullException();

            db.Pilots.Remove(item);
            db.Pilots.Add(pilot);
            
        }

        public void Delete(int id)
        {
            var item = db.Pilots.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Pilots.Remove(item);
        }
    }
}
