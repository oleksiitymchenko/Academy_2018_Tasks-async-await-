using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace homework_5_bsa2018.Controllers
{
    [Route("api/Flights")]
    public class FlightsController : BaseController<FlightDTO>
    {
        public FlightsController(IService<FlightDTO> service):base(service)
        {

        }
    }
}