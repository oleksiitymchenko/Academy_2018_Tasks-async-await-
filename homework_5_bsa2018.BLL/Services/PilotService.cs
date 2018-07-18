using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System.Collections.Generic;

namespace homework_5_bsa2018.BLL.Services
{
    public class PilotService : IService<PilotDTO>
    {
        private IUnitOfWork _unitOfWork;

        public PilotService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PilotDTO> GetAll()
            => Mapper.Map<List<PilotDTO>>
            (_unitOfWork.Pilots.GetAllAsync());

        public PilotDTO Get(int id) =>
            Mapper.Map<PilotDTO>(_unitOfWork.Pilots.GetAsync(id));

        public void Create(PilotDTO pilot)
        {
            _unitOfWork.Pilots.Create(Mapper.Map<Pilot>(pilot));
            _unitOfWork.Save();
        }

        public void Update(int id, PilotDTO pilot)
        {
            _unitOfWork.Pilots.Update(id, Mapper.Map<Pilot>(pilot));
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Pilots.Delete(id);
            _unitOfWork.Save();
        }
    }

}
