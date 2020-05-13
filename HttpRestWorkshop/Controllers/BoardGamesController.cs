using System;
using Microsoft.AspNetCore.Mvc;
using HttpRestWorkshop.DAL.Models;
using HttpRestWorkshop.DAL.Service;

namespace HttpRestWorkshop.Controllers
{
    [Route("api/[controller]")]
    public class BoardGamesController : Controller
    {
        private readonly IBoardGamesService service;

        public BoardGamesController(IBoardGamesService service)
        {
            this.service = service;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var items = service.Get();
                return Ok(items);
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
                var item = service.Get(id);
                return Ok(item);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (InvalidOperationException)
            {
                return Conflict();
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
                service.Add(value);
                return Ok();
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
                service.Delete(id);
                return Ok();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
