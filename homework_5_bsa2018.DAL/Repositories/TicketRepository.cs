using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System;
using System.Collections.Generic;

namespace homework_5_bsa2018.DAL.Repositories
{
    public class TicketRepository : IRepository<Ticket>
    {
        private AirportContext db;

        public TicketRepository(AirportContext context)
        {
            db = context;
        }

        public IEnumerable<Ticket> GetAll() =>
            db.Tickets;

        public Ticket Get(int id) => db.Tickets.Find(id);

        public void Create(Ticket ticket)
        {
            db.Tickets.Add(ticket);
        }

        public void Update(int id, Ticket ticket)
        {
            var item = db.Tickets.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Tickets.Remove(item);
            db.Tickets.Add(ticket);
            
        }

        public void Delete(int id)
        {
            var item = db.Tickets.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Tickets.Remove(item);
        }
    }
}
