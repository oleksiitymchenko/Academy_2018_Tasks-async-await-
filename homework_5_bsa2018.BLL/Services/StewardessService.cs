using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System.Collections.Generic;

namespace homework_5_bsa2018.BLL.Services
{
    public class StewardessService:IService<StewardessDTO>
    {
        private IUnitOfWork _unitOfWork;

        public StewardessService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<StewardessDTO> GetAll()
            => Mapper.Map<List<StewardessDTO>>
            (_unitOfWork.Stewardesses.GetAll());

        public StewardessDTO Get(int id) =>
            Mapper.Map<StewardessDTO>(_unitOfWork.Stewardesses.Get(id));

        public void Create(StewardessDTO stew)
        {
            _unitOfWork.Stewardesses.Create(Mapper.Map<Stewardess>(stew));
            _unitOfWork.Save();
        }

        public void Update(int id, StewardessDTO stew)
        {
            _unitOfWork.Stewardesses.Update(id, Mapper.Map<Stewardess>(stew));
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Stewardesses.Delete(id);
            _unitOfWork.Save();
        }
    }
}
