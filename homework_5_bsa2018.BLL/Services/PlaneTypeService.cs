using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homework_5_bsa2018.BLL.Services
{
    public class PlaneTypeService:IService<PlaneTypeDTO>
    {
        private IUnitOfWork _unitOfWork;

        public PlaneTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PlaneTypeDTO>> GetAll()
            => Mapper.Map<List<PlaneTypeDTO>>
            (await _unitOfWork.PlaneTypes.GetAllAsync());

        public async Task<PlaneTypeDTO> Get(int id) =>
            Mapper.Map<PlaneTypeDTO>(await _unitOfWork.PlaneTypes.GetAsync(id));

        public async Task Create(PlaneTypeDTO pltype)
        {
            await _unitOfWork.PlaneTypes.Create(Mapper.Map<PlaneType>(pltype));
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(int id,PlaneTypeDTO pltype)
        {
            await _unitOfWork.PlaneTypes.Update(id, Mapper.Map<PlaneType>(pltype));
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(int id)
        {
            _unitOfWork.PlaneTypes.Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
