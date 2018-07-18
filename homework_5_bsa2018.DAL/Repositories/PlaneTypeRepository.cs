using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System;
using System.Collections.Generic;

namespace homework_5_bsa2018.DAL.Repositories
{
    public class PlaneTypeRepository : IRepository<PlaneType>
    {
        private AirportContext db;

        public PlaneTypeRepository(AirportContext context)
        {
            db = context;
        }

        public IEnumerable<PlaneType> GetAll() =>
            db.PlaneTypes;

        public PlaneType Get(int id) => db.PlaneTypes.Find(id);

        public void Create(PlaneType planetype)
        {
            db.PlaneTypes.Add(planetype);
        }

        public void Update(int id, PlaneType planetype)
        {
            var item = db.PlaneTypes.Find(id);
            if (item == null) throw new ArgumentNullException();

            db.PlaneTypes.Remove(item);
                db.PlaneTypes.Add(planetype);
            
        }

        public void Delete(int id)
        {
            var item = db.PlaneTypes.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.PlaneTypes.Remove(item);
        }
    }
}
