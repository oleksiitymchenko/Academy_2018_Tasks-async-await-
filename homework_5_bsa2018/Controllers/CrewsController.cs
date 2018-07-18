using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace homework_5_bsa2018.Controllers
{
    [Route("api/[controller]")]
    public class CrewsController : BaseController<CrewDTO>
    {
        public CrewsController(IService<CrewDTO> service) : base(service)
        {

        }
    }
}
