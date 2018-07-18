using homework_5_bsa2018;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;
using Newtonsoft.Json;
using System.Net;
using homework_5_bsa2018.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using homework_5_bsa2018.Shared.DTOs;
using System.Text;
using homework_5_bsa2018.BLL;
using homework_5_bsa2018.Shared;

namespace homework_6_bsa2018.Tests
{
    public class ApiHttpRequestsTest
    {
        private static readonly TestServer _server = new TestServer(
                new WebHostBuilder()
                .UseStartup<Startup>().UseKestrel());
        private static readonly HttpClient _client = _server.CreateClient();

        public ApiHttpRequestsTest()
        {
            var mapper = new MapperInitializator();
            mapper.Initialize();
        }

        [Fact]
        public async void Get_ShouldReturnAllPilots()
        {
            var response = await _client.GetAsync("/api/Pilots");
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<List<PilotDTO>>(await response.Content.ReadAsStringAsync());

            var options = new DbContextOptionsBuilder<AirportContext>();
            options.UseSqlServer(ConnectionString.Value);
            var seed = new SeedData();
            var contextAirport = new AirportContext(options.Options, seed);

            Assert.Equal(result.Count, contextAirport.Pilots.Count());
            Assert.IsType<List<PilotDTO>>(result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Get_ShouldReturnOnePilot()
        {
            var response = await _client.GetAsync("/api/Pilots/1");
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<PilotDTO>(await response.Content.ReadAsStringAsync());

            var options = new DbContextOptionsBuilder<AirportContext>();
            options.UseSqlServer(ConnectionString.Value);
            var seed = new SeedData();
            var contextAirport = new AirportContext(options.Options, seed);

            Assert.Equal(result.FirstName, contextAirport.Pilots.Find(1).FirstName);
            Assert.Equal(result.LastName, contextAirport.Pilots.Find(1).LastName);
            Assert.Equal(result.Experience, contextAirport.Pilots.Find(1).Experience);
            Assert.IsType<PilotDTO>(result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200()
        {

            string json = JsonConvert.SerializeObject(
                new PilotDTO() { FirstName = "Tom", LastName = "Smith", Experience = 5 });
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = await _client.PostAsync("/api/Pilots", httpContent);

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturn200()
        {
             var response = await _client.PutAsync("/api/Pilots/1", 
                new StringContent(JsonConvert.SerializeObject(
                    new PilotDTO{ FirstName = "Tom", LastName = "Smith" }),
            Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturn200()
        {
            var response = await _client.DeleteAsync("/api/Pilots/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
    }
