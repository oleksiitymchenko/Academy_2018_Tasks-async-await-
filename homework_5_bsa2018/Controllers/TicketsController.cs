using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace homework_5_bsa2018.Controllers
{
    [Route("api/Tickets")]
    public class TicketsController : BaseController<TicketDTO>
    {
        public TicketsController(IService<TicketDTO> service):base(service)
        {

        }
    }
}