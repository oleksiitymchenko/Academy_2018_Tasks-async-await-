using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System;
using System.Collections.Generic;

namespace homework_5_bsa2018.BLL.Services
{
    public class PlaneService:IService<PlaneDTO>
    {
        private IUnitOfWork _unitOfWork;

        public PlaneService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PlaneDTO> GetAll()
            => Mapper.Map<List<PlaneDTO>>
            (_unitOfWork.Planes.GetAllAsync());

        public PlaneDTO Get(int id) =>
            Mapper.Map<PlaneDTO>(_unitOfWork.Planes.GetAsync(id));

        public void Create(PlaneDTO plane)
        {
            _unitOfWork.Planes.Create(TransformPlane(plane));
            _unitOfWork.Save();
        }

        public void Update(int id, PlaneDTO plane)
        {
            _unitOfWork.Planes.Update(id, TransformPlane(plane));
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Planes.Delete(id);
            _unitOfWork.Save();
        }

        private Plane TransformPlane(PlaneDTO plane)
        {
            var type = _unitOfWork.PlaneTypes.GetAsync(plane.TypePlaneId);
            if (type == null) throw new ArgumentNullException();
            var lifetime = TimeSpan.Parse(plane.LifeTime);
            var created = DateTime.Parse(plane.Created);

            return new Plane()
            {
                Name = plane.Name,
                TypePlane = type,
                LifeTime = lifetime,
                Created = created
            };
        }
    }
}
