using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System;
using System.Collections.Generic;

namespace homework_5_bsa2018.DAL.Repositories
{
    public class StewardessRepository : IRepository<Stewardess>
    {
        private AirportContext db;

        public StewardessRepository(AirportContext context)
        {
            db = context;
        }

        public IEnumerable<Stewardess> GetAll() =>
            db.Stewardesses;

        public Stewardess Get(int id) => db.Stewardesses.Find(id);

        public void Create(Stewardess stewardess)
        {
            db.Stewardesses.Add(stewardess);
        }

        public void Update(int id, Stewardess stew)
        {
            var item = db.Stewardesses.Find(id);
            if (item == null) throw new ArgumentNullException();

            db.Stewardesses.Remove(item);
                db.Stewardesses.Add(stew);
            
        }


        public void Delete(int id)
        {
            var item = db.Stewardesses.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Stewardesses.Remove(item);
        }
    }
}
