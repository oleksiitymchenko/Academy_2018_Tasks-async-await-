using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System.Collections.Generic;

namespace homework_5_bsa2018.BLL.Services
{
    public class PlaneTypeService:IService<PlaneTypeDTO>
    {
        private IUnitOfWork _unitOfWork;

        public PlaneTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PlaneTypeDTO> GetAll()
            => Mapper.Map<List<PlaneTypeDTO>>
            (_unitOfWork.PlaneTypes.GetAllAsync());

        public PlaneTypeDTO Get(int id) =>
            Mapper.Map<PlaneTypeDTO>(_unitOfWork.PlaneTypes.GetAsync(id));

        public void Create(PlaneTypeDTO pltype)
        {
            _unitOfWork.PlaneTypes.Create(Mapper.Map<PlaneType>(pltype));
            _unitOfWork.Save();
        }

        public void Update(int id,PlaneTypeDTO pltype)
        {
            _unitOfWork.PlaneTypes.Update(id, Mapper.Map<PlaneType>(pltype));
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.PlaneTypes.Delete(id);
            _unitOfWork.Save();
        }
    }
}
