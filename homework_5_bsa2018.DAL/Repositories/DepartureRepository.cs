using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace homework_5_bsa2018.DAL.Repositories
{
    public class DepartureRepository:IRepository<Departure>
    {
        private AirportContext db;

        public DepartureRepository(AirportContext context)
        {
            db = context;
        }

        public IEnumerable<Departure> GetAll() =>
            db.Departures.Include(c=>c.Plane).Include(c=>c.Crew);

        public Departure Get(int id) => 
            GetAll().FirstOrDefault(item => item.Id == id);

        public void Create(Departure departure)
        {
            db.Departures.Add(departure);
        }


        public void Update(int id, Departure departure)
        {
            var item = db.Departures.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Departures.Remove(item);
                db.Departures.Add(departure);
            }
        


        public void Delete(int id)
        {
            var item = db.Departures.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Departures.Remove(item);
        }
    }
}
