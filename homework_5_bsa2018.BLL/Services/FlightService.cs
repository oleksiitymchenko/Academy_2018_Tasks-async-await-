using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace homework_5_bsa2018.BLL.Services
{
    public class FlightService:IService<FlightDTO>
    {
        private IUnitOfWork _unitOfWork;

        public FlightService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<FlightDTO> GetAll()
            => Mapper.Map<List<FlightDTO>>
            (_unitOfWork.Flights.GetAll());

        public FlightDTO Get(int id) =>
            Mapper.Map<FlightDTO>(_unitOfWork.Flights.Get(id));

        public void Create(FlightDTO flight)
        {
            _unitOfWork.Flights.Create(TransformFlight(flight));
            _unitOfWork.Save();
        }

        public void Update(int id,FlightDTO flight)
        {
            _unitOfWork.Flights.Update(id, TransformFlight(flight));
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Flights.Delete(id);
            _unitOfWork.Save();
        }

        private Flight TransformFlight(FlightDTO flight)
        {
            var starttime = DateTime.Parse(flight.StartTime);
            var endtime = DateTime.Parse(flight.FinishTime);
            var ticketsList = flight.TicketIds
                .Select(s => {
                var ticket = _unitOfWork.Tickets.Get(s);
                if (ticket != null) return ticket;
                else throw new ArgumentNullException();
            }).ToList();

            return new Flight()
            {
                Number = flight.Number,
                StartTime = starttime,
                StartPoint = flight.StartPoint,
                FinishTime = endtime,
                FinishPoint = flight.FinishPoint,
                Tickets = ticketsList
            };
        }
    }
}
