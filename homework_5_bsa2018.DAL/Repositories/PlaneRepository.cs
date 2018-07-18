using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System.Collections.Generic;
using System;

namespace homework_5_bsa2018.DAL.Repositories
{
    public class PlaneRepository : IRepository<Plane>
    {
        private AirportContext db;

        public PlaneRepository(AirportContext context)
        {
            db = context;
        }

        public IEnumerable<Plane> GetAll() =>
            db.Planes;

        public Plane Get(int id) => db.Planes.Find(id);

        public void Create(Plane plane)
        {
            db.Add(plane);
        }

        public void Update(int id, Plane plane)
        {
            var item = db.Planes.Find(id);
            if (item == null) throw new ArgumentNullException();

            db.Planes.Remove(item);
            db.Planes.Add(plane);
            
        }

        public void Delete(int id)
        {
            var item = db.Planes.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Planes.Remove(item);
        }
    }
}
