using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HttpRestWorkshop.Controllers
{
    using HttpRestWorkshop.DAL.Models;
    using HttpRestWorkshop.DAL.Service;

    [Route("api/[controller]")]
    public class BoardGamesController : Controller
    {
        private readonly BoardGamesService _service;

        public BoardGamesController(BoardGamesService service)
        {
            this._service = service;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var items = this._service.Get();
                return this.Ok(items);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var item = this._service.Get(id);
                return this.Ok(item);
            }
            catch (ArgumentException)
            {
                return this.NotFound();
            }
            catch (InvalidOperationException)
            {
                return this.Conflict();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]BoardGame value)
        {
            try
            {
                this._service.Add(value);
                return this.Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]BoardGame value)
        //{
        //    throw new NotImplementedException();
        //}

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this._service.Delete(id);
                return this.Ok();
            }
            catch (ArgumentException)
            {
                return this.NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
