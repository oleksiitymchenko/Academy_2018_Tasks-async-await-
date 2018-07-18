using homework_5_bsa2018.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace homework_5_bsa2018.Controllers
{
    public abstract class BaseController<TEntityDTO> : Controller
    {
        private IService<TEntityDTO> _service;

        public BaseController(IService<TEntityDTO> service)
        {
            _service = service;
        }

        // GET api/TEntities
        [HttpGet]
        public async Task<OkObjectResult> Get()
        {
            var collectionDTO = await _service.GetAll();
            if (collectionDTO == null) return new OkObjectResult(StatusCode(400));
            return Ok(collectionDTO);
        }

        // GET api/TEntities/:id
        [HttpGet("{id}")]
        public async Task<OkObjectResult> Get(int id)
        {
            var collectionDTO = await _service.Get(id);
            if (collectionDTO == null) return new OkObjectResult(StatusCode(400)); 
            return Ok(collectionDTO);
        }

        // POST api/TEntities
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]TEntityDTO itemDTO)
        {
            if (ModelState.IsValid == false)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            try
            {
                await _service.Create(itemDTO);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        //PUT api/TEntities/:id
        [HttpPut("{id}")]
        public async Task<HttpResponseMessage> Put(int id, [FromBody]TEntityDTO itemDTO)
        {
            if (ModelState.IsValid == false)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            try
            {
                await _service.Update(id, itemDTO);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        //DELETE api/TEntity/:id
        [HttpDelete("{id}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
