using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.BLL.Services;
using homework_5_bsa2018.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace homework_5_bsa2018.Controllers
{
    [Route("api/[controller]")]
    public class CrewsController : BaseController<CrewDTO>
    {
        private IService<CrewDTO> _service;

        public CrewsController(IService<CrewDTO> service) : base(service)
        {
            _service = service;
        }

        [HttpGet("Payload")]
        public async Task<IActionResult> LoadCrew()
        {
            try
            {
                var serv = _service as CrewService;
                return Ok(await serv.LoadDataAsync("http://5b128555d50a5c0014ef1204.mockapi.io/crew"));
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

    }
}
