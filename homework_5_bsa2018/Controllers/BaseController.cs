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
        public virtual async Task<OkObjectResult> Get()
        {
            var collectionDTO = await _service.GetAllAsync();
            if (collectionDTO == null) return new OkObjectResult(StatusCode(400));
            return Ok(collectionDTO);
        }

        // GET api/TEntities/:id
        [HttpGet("{id}")]
        public virtual async Task<OkObjectResult> Get(int id)
        {
            var collectionDTO = await _service.GetAsync(id);
            if (collectionDTO == null) return new OkObjectResult(StatusCode(400)); 
            return Ok(collectionDTO);
        }

        // POST api/TEntities
        [HttpPost]
        public virtual async Task<HttpResponseMessage> Post([FromBody]TEntityDTO itemDTO)
        {
            if (ModelState.IsValid == false)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            try
            {
                await _service.CreateAsync(itemDTO);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        //PUT api/TEntities/:id
        [HttpPut("{id}")]
        public virtual async Task<HttpResponseMessage> Put(int id, [FromBody]TEntityDTO itemDTO)
        {
            if (ModelState.IsValid == false)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            try
            {
                await _service.UpdateAsync(id, itemDTO);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        //DELETE api/TEntity/:id
        [HttpDelete("{id}")]
        public virtual async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
