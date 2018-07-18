using homework_5_bsa2018.BLL;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.BLL.Services;
using homework_5_bsa2018.Controllers;
using homework_5_bsa2018.DAL;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.Shared;
using homework_5_bsa2018.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Xunit;

namespace homework_6_bsa2018.Tests
{

    public class IntegrationTest
    {
        private CrewsController controller;
        private IService<CrewDTO> crewService;
        private IUnitOfWork uow;
        private AirportContext contextAirport;

        private void Start()
        {
                var options = new DbContextOptionsBuilder<AirportContext>();
                options.UseSqlServer(ConnectionString.Value);
                var seed = new SeedData();
                contextAirport = new AirportContext(options.Options, seed);
                uow = new UnitOfWork(contextAirport);
                crewService = new CrewService(uow);
                controller = new CrewsController(crewService);

                var mapper = new MapperInitializator();
                mapper.Initialize();
        }

        [Fact]
        public void GetEntity_WhenObjectIsCorrect_ReturnsEntityAndStatusCode200()
        {
            Start();
            var entity = controller.Get(1);
            Assert.IsType<CrewDTO>(entity.Result.Value);
            Assert.Equal(200, entity.Result.StatusCode);
        }
  

        [Fact]
        public void CreateEntity_WhenCreateModelIsIncorrect_ReturnsStatusCode400()
        {
            Start();
            var crew = new CrewDTO() { StewardressIds = new List<int>() { 4, 5 } };
            HttpResponseMessage entity = new HttpResponseMessage();
            entity = controller.Post(crew).Result;
            Assert.Equal(HttpStatusCode.BadRequest, entity.StatusCode);
            contextAirport.Crews.Remove(contextAirport.Crews.LastOrDefault());
        }


        [Fact]
        public void DeleteEntity_WhenIdIsInCorrect_ReturnsStatusCode400()
        {
            Start();
            HttpResponseMessage responseMessage = controller.Delete(-1).Result;
            Assert.True(responseMessage.StatusCode == HttpStatusCode.BadRequest);
        }

        

    }
}
