using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using homework_5_bsa2018.DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace homework_5_bsa2018.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private AirportContext db;

        private IRepository<Crew> _crews;
        private IRepository<Departure> _departures;
        private IRepository<Flight> _flights;
        private IRepository<Pilot> _pilots;
        private IRepository<Plane> _planes;
        private IRepository<PlaneType> _planetypes;
        private IRepository<Stewardess> _stewardesses;
        private IRepository<Ticket> _tickets;

        public IRepository<Crew> Crews
        {
            get
            {
                if (_crews == null) _crews = new CrewRepository(db);
                return _crews;
            }
        }

        public IRepository<Departure> Departures
        {
            get
            {
                if (_departures == null) _departures = new DepartureRepository(db);
                return _departures;
            }
        }

        public IRepository<Flight> Flights
        {
            get
            {
                if (_flights == null) _flights = new FlightRepository(db);
                return _flights;
            }
        }

        public IRepository<Pilot> Pilots
        {
            get
            {
                if (_pilots == null) _pilots = new PilotRepository(db);
                return _pilots;
            }
        }

        public IRepository<Plane> Planes
        {
            get
            {
                if (_planes == null) _planes = new PlaneRepository(db);
                return _planes;
            }
        }

        public IRepository<PlaneType> PlaneTypes
        {
            get
            {
                if (_planetypes == null) _planetypes = new PlaneTypeRepository(db);
                return _planetypes;
            }
        }

        public IRepository<Stewardess> Stewardesses
        {
            get
            {
                if (_stewardesses == null) _stewardesses = new StewardessRepository(db);
                return _stewardesses;
            }
        }

        public IRepository<Ticket> Tickets
        {
            get
            {
                if (_tickets == null) _tickets = new TicketRepository(db);
                return _tickets;
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public UnitOfWork(AirportContext dataSource)
        {
            db = dataSource;
        }


        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
