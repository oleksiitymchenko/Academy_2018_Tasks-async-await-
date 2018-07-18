using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using homework_5_bsa2018.BLL;

namespace homework_5_bsa2018.Controllers
{
    [Route("api/Flights")]
    public class FlightsController : BaseController<FlightDTO>
    {
        private IService<FlightDTO> _service;
        private Helpers _helpers;

        public FlightsController(IService<FlightDTO> service) : base(service)
        {
            _service = service;
            _helpers = new Helpers(service, 5000);
        }
       
        [HttpGet("delay")]
        public async Task<OkObjectResult> GetWithDelay()
        {
            var collectionDTO = await _helpers.GetFlightsDelay();
            if (collectionDTO == null) return new OkObjectResult(StatusCode(400));
            return Ok(collectionDTO);
        }
    }
}