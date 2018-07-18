using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace homework_5_bsa2018.BLL.Services
{
    public class PlaneService:IService<PlaneDTO>
    {
        private IUnitOfWork _unitOfWork;

        public PlaneService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PlaneDTO>> GetAll()
            => Mapper.Map<List<PlaneDTO>>
            (await _unitOfWork.Planes.GetAllAsync());

        public async Task<PlaneDTO> Get(int id) =>
            Mapper.Map<PlaneDTO>(await _unitOfWork.Planes.GetAsync(id));

        public async Task Create(PlaneDTO plane)
        {
            await _unitOfWork.Planes.Create(await TransformPlane(plane));
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(int id, PlaneDTO plane)
        {
            await _unitOfWork.Planes.Update(id, await TransformPlane(plane));
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(int id)
        {
            _unitOfWork.Planes.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        private async Task<Plane> TransformPlane(PlaneDTO plane)
        {
            var type = await _unitOfWork.PlaneTypes.GetAsync(plane.TypePlaneId);
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
