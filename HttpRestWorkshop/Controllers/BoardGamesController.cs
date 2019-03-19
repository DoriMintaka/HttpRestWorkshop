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
        private BoardGamesService _service;

        public BoardGamesController(BoardGamesService service)
        {
            this._service = service;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<BoardGame> Get()
        {
            return this._service.Get();
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
                return StatusCode(503);
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
                return StatusCode(503);
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
            catch (Exception)
            {
                return StatusCode(503);
            }
        }
    }
}
