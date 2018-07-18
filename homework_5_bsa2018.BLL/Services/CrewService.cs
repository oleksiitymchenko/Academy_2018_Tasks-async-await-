using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace homework_5_bsa2018.BLL.Services
{
    public class CrewService:IService<CrewDTO>
    {
        private IUnitOfWork _unitOfWork;

        public CrewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CrewDTO> GetAll() {
            var x = Mapper.Map<List<CrewDTO>>
           (_unitOfWork.Crews.GetAll());
            return x;
        }

        public CrewDTO Get(int id) =>
            Mapper.Map<CrewDTO>(_unitOfWork.Crews.Get(id));
        
        public void Create(CrewDTO crew)
        {
            var x = TransformCrew(crew);
            _unitOfWork.Crews.Create(x);
            _unitOfWork.Save();
        }

        public void Update(int id, CrewDTO crew)
        {
            _unitOfWork.Crews.Update(id, TransformCrew(crew));
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Crews.Delete(id);
            _unitOfWork.Save();

        }


        private Crew TransformCrew(CrewDTO crew)
        {

            var pilot = _unitOfWork.Pilots.Get(crew.PilotId);
            if (pilot==null) throw new ArgumentNullException();

            var stewardesses = crew.StewardressIds
                .Select(s => {
                    var stew = _unitOfWork.Stewardesses.Get(s);
                    if (stew != null) return stew;
                        else throw new ArgumentNullException();
            }).ToList();

            return new Crew()
            {
                Pilot = pilot,
                Stewardesses = stewardesses
            };
        }
    }
}
