using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using homework_5_bsa2018.Shared.LoadDTO;
using System.Net;

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


        public async Task<List<LoadCrewDTO>> LoadDataAsync()
        {
            List<LoadCrewDTO> crews;
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync("http://5b128555d50a5c0014ef1204.mockapi.io/crew"))
            using (HttpContent content = response.Content)
            {
                if (response.StatusCode != HttpStatusCode.OK) return null;

                string responsJson = await content.ReadAsStringAsync();

                crews = JsonConvert.DeserializeObject<List<LoadCrewDTO>>(responsJson);
            }
            var items = crews.Take(10).ToList();
            string path = $"log_{DateTime.Now.ToString("yy/MM/dd__H:mm")}.csv";
            Parallel.Invoke(
                async () => await LoadToDataBase(items),
                async () => await WriteToCSV(items,
                    Path.Combine(Environment.CurrentDirectory,
                        @"CrewsFromApi\", path)));
            return items;
        }

        private async Task WriteToCSV(List<LoadCrewDTO> list, string path)
        {
            using (var w = new StreamWriter(path))
            {
                await w.WriteLineAsync("id,pilot,stewardess");
                foreach (var row in list)
                {
                    var id = row.id;
                    var pilot = JsonConvert.SerializeObject(row.pilot.FirstOrDefault());
                    var stewardesses = JsonConvert.SerializeObject(row.stewardess);
                    var line = string.Format("{0},\"{1}\",\"{2}\"", id, pilot, stewardesses);
                    await w.WriteLineAsync(line);
                    w.Flush();
                }
            }
        }

        private async Task LoadToDataBase(List<LoadCrewDTO> input)
        {
            var stewardess = input.SelectMany(i => i.stewardess);
            var pilot = input.SelectMany(i => i.pilot);
            foreach (var i in stewardess)
            {
                i.Id = 0;
                await _unitOfWork.Stewardesses.Create(i);
            }

            foreach (var i in pilot)
            {
                i.Id = 0;
                await _unitOfWork.Pilots.Create(i);
            }

            foreach (var i in input)
            {
                i.id = 0;
                await _unitOfWork.Crews.Create(Mapper.Map<Crew>(i));
            }

            await _unitOfWork.SaveAsync();

        }
    }
}
