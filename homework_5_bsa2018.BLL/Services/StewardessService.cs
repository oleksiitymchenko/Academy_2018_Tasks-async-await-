using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homework_5_bsa2018.BLL.Services
{
    public class StewardessService:IService<StewardessDTO>
    {
        private IUnitOfWork _unitOfWork;

        public StewardessService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StewardessDTO>> GetAll()
            => Mapper.Map<List<StewardessDTO>>
            (await _unitOfWork.Stewardesses.GetAllAsync());

        public async Task<StewardessDTO> Get(int id) =>
            Mapper.Map<StewardessDTO>(await _unitOfWork.Stewardesses.GetAsync(id));

        public async Task Create(StewardessDTO stew)
        {
            await _unitOfWork.Stewardesses.Create(Mapper.Map<Stewardess>(stew));
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(int id, StewardessDTO stew)
        {
            await _unitOfWork.Stewardesses.Update(id, Mapper.Map<Stewardess>(stew));
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(int id)
        {
            _unitOfWork.Stewardesses.Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
