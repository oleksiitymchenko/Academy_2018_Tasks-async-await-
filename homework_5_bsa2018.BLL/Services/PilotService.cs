using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homework_5_bsa2018.BLL.Services
{
    public class PilotService : IService<PilotDTO>
    {
        private IUnitOfWork _unitOfWork;

        public PilotService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PilotDTO>> GetAll()
            => Mapper.Map<List<PilotDTO>>
            (await _unitOfWork.Pilots.GetAllAsync());

        public async Task<PilotDTO> Get(int id) =>
            Mapper.Map<PilotDTO>(await _unitOfWork.Pilots.GetAsync(id));

        public async Task Create(PilotDTO pilot)
        {
            await _unitOfWork.Pilots.Create(Mapper.Map<Pilot>(pilot));
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(int id, PilotDTO pilot)
        {
            await _unitOfWork.Pilots.Update(id, Mapper.Map<Pilot>(pilot));
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(int id)
        {
            _unitOfWork.Pilots.Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }

}
