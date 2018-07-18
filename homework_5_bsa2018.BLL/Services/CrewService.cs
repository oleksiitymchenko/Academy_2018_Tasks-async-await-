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

        public async Task<IEnumerable<CrewDTO>> GetAll() => 
            Mapper.Map<List<CrewDTO>>
           (await _unitOfWork.Crews.GetAllAsync());

        public async Task<CrewDTO> Get(int id) =>
            Mapper.Map<CrewDTO>(await _unitOfWork.Crews.GetAsync(id));
        
        public async Task Create(CrewDTO crew)
        {
            await _unitOfWork.Crews.Create(await TransformCrewAsync(crew));
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(int id, CrewDTO crew)
        {
            await _unitOfWork.Crews.Update(id, await TransformCrewAsync(crew));
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(int id)
        {
             _unitOfWork.Crews.Delete(id);
            await _unitOfWork.SaveAsync();

        }

        private async Task<Crew> TransformCrewAsync(CrewDTO crew)
        {

            var pilot = await _unitOfWork.Pilots.GetAsync(crew.PilotId);
            if (pilot==null) throw new ArgumentNullException();

            var stewardesses = await Task.WhenAll(crew.StewardressIds
                .Select(s => {
                    var stew =  _unitOfWork.Stewardesses.GetAsync(s);
                    if (stew != null) return stew;
                        else throw new ArgumentNullException();
            }));

            return new Crew()
            {
                Pilot = pilot,
                Stewardesses = stewardesses.ToList()
            };
        }
    }
}
