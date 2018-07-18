using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homework_5_bsa2018.DAL.Repositories
{
    public class TicketRepository : IRepository<Ticket>
    {
        private AirportContext db;

        public TicketRepository(AirportContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync() =>
            await db.Tickets.ToListAsync();

        public async Task<Ticket> GetAsync(int id) => 
            await db.Tickets.FindAsync(id);

        public async Task Create(Ticket ticket)
        {
            await db.Tickets.AddAsync(ticket);
        }

        public async Task Update(int id, Ticket ticket)
        {
            var item = db.Tickets.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Tickets.Remove(item);
            await db.Tickets.AddAsync(ticket);
            
        }

        public void Delete(int id)
        {
            var item = db.Tickets.Find(id);
            if (item == null) throw new ArgumentNullException();
            db.Tickets.Remove(item);
        }
    }
}
