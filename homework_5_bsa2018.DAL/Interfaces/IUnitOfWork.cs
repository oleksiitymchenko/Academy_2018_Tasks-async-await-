using homework_5_bsa2018.DAL.Models;
using System.Threading.Tasks;

namespace homework_5_bsa2018.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Crew> Crews { get; }
        IRepository<Departure> Departures { get; }
        IRepository<Flight> Flights { get; }
        IRepository<Pilot> Pilots { get; }
        IRepository<Plane> Planes { get; }
        IRepository<PlaneType> PlaneTypes { get; }
        IRepository<Stewardess> Stewardesses { get; }
        IRepository<Ticket> Tickets { get; }

        Task SaveAsync();
    }
}
