using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_5_bsa2018.BLL.Services
{
    public class FlightService:IService<FlightDTO>
    {
        private IUnitOfWork _unitOfWork;

        public FlightService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<FlightDTO>> GetAll()
            => Mapper.Map<List<FlightDTO>>
            (await _unitOfWork.Flights.GetAllAsync());

        public async Task<FlightDTO> Get(int id) =>
            Mapper.Map<FlightDTO>(await _unitOfWork.Flights.GetAsync(id));

        public async Task Create(FlightDTO flight)
        {
            await _unitOfWork.Flights.Create(await TransformFlight(flight));
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(int id,FlightDTO flight)
        {
            await _unitOfWork.Flights.Update(id, await TransformFlight(flight));
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(int id)
        {
            _unitOfWork.Flights.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        private async Task<Flight> TransformFlight(FlightDTO flight)
        {
            var starttime = DateTime.Parse(flight.StartTime);
            var endtime = DateTime.Parse(flight.FinishTime);
            var ticketsList = await Task.WhenAll(flight.TicketIds
                .Select(s => {
                var ticket = _unitOfWork.Tickets.GetAsync(s);
                if (ticket != null) return ticket;
                else throw new ArgumentNullException();
            }));

            return new Flight()
            {
                Number = flight.Number,
                StartTime = starttime,
                StartPoint = flight.StartPoint,
                FinishTime = endtime,
                FinishPoint = flight.FinishPoint,
                Tickets = ticketsList.ToList()
            };
        }
    }
}
