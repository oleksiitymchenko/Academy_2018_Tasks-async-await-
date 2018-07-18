using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.DAL.Models;
using homework_5_bsa2018.DAL.Interfaces;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace homework_5_bsa2018.BLL.Services
{
    public class DepartureService : IService<DepartureDTO>
    {
        private IUnitOfWork _unitOfWork;

        public DepartureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DepartureDTO> GetAll()
            => Mapper.Map<List<DepartureDTO>>
            (_unitOfWork.Departures.GetAll());

        public DepartureDTO Get(int id) =>
            Mapper.Map<DepartureDTO>(_unitOfWork.Departures.Get(id));

        public void Create(DepartureDTO departure)
        {
            _unitOfWork.Departures.Create(TransformDeparture(departure));
            _unitOfWork.Save();
        }

        public void Update(int id,DepartureDTO departure)
        {
            _unitOfWork.Departures.Update(id, TransformDeparture(departure));
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Departures.Delete(id);
            _unitOfWork.Save();
        }

        private Departure TransformDeparture(DepartureDTO departure)
        {
            var departureTime = DateTime.Parse(departure.DepartureTime);
            var plane = _unitOfWork.Planes.Get(departure.PlaneId);
            var crew = _unitOfWork.Crews.Get(departure.CrewId);
            if (crew == null || plane == null) throw new ArgumentNullException();

            return new Departure()
            {
                FlightNumber = departure.FlightNumber,
                DepartureTime = departureTime,
                Plane = plane,
                Crew = crew
            };
        }
    }
}
