using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.DAL.Models;
using homework_5_bsa2018.DAL.Interfaces;
using System;
using System.Collections.Generic;
using AutoMapper;
using System.Threading.Tasks;

namespace homework_5_bsa2018.BLL.Services
{
    public class DepartureService : IService<DepartureDTO>
    {
        private IUnitOfWork _unitOfWork;

        public DepartureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DepartureDTO>> GetAllAsync()
            => Mapper.Map<List<DepartureDTO>>
            (await _unitOfWork.Departures.GetAllAsync());

        public async Task<DepartureDTO> GetAsync(int id) =>
            Mapper.Map<DepartureDTO>(await _unitOfWork.Departures.GetAsync(id));

        public async Task CreateAsync(DepartureDTO departure)
        {
           await _unitOfWork.Departures.Create(await TransformDeparture(departure));
           await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(int id,DepartureDTO departure)
        {
            await _unitOfWork.Departures.Update(id, await TransformDeparture(departure));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.Departures.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        private async Task<Departure> TransformDeparture(DepartureDTO departure)
        {
            var departureTime = DateTime.Parse(departure.DepartureTime);
            var plane = await _unitOfWork.Planes.GetAsync(departure.PlaneId);
            var crew = await _unitOfWork.Crews.GetAsync(departure.CrewId);
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
