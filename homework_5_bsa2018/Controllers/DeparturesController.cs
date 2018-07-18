using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace homework_5_bsa2018.Controllers
{
    [Route("api/Departures")]
    public class DeparturesController : BaseController<DepartureDTO>
    {
        public DeparturesController(IService<DepartureDTO> service):base(service)
        {

        }
    }
}