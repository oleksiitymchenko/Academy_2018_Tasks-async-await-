﻿using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace homework_5_bsa2018.Controllers
{
    [Route("api/[controller]")]
    public class PlanesController : BaseController<PlaneDTO>
    {
        public PlanesController(IService<PlaneDTO> service):base(service)
        {

        }
    }
}