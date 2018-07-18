using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_5_bsa2018.BLL.Services
{
    public class CrewService:IService<CrewDTO>
    {
        private IUnitOfWork _unitOfWork;

        public CrewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<CrewDTO>> GetAll() {
            var x = Mapper.Map<List<CrewDTO>>
           (_unitOfWork.Crews.GetAllAsync());
            return x;
        }

        public Task<CrewDTO> Get(int id) =>
            Mapper.Map<CrewDTO>(_unitOfWork.Crews.GetAsync(id));
        
        public async Task Create(CrewDTO crew)
        {
            var x = TransformCrew(crew);
            await _unitOfWork.Crews.Create(x);
            await _unitOfWork.Save();
        }

        public async Task Update(int id, CrewDTO crew)
        {
            await _unitOfWork.Crews.Update(id, await TransformCrew(crew));
            await _unitOfWork.Save();
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.Crews.Delete(id);
            await _unitOfWork.Save();

        }

        private async Task<Crew> TransformCrew(CrewDTO crew)
        {

            var pilot = await _unitOfWork.Pilots.GetAsync(crew.PilotId);
            if (pilot==null) throw new ArgumentNullException();

            var stewardesses = crew.StewardressIds
                .Select(async s => {
                    var stew = await _unitOfWork.Stewardesses.GetAsync(s);
                    if (stew != null) return stew;
                        else throw new ArgumentNullException();
            });

            return new Crew()
            {
                Pilot = pilot,
                Stewardesses = stewardesses
            };
        }
    }
}
