using Application.Seeders;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialDataController : ControllerBase
    {
        // GET: api/<InitialDataController>
        [HttpGet]
        public IActionResult Get([FromServices] IFakeDataSeed request )
        {
            try
            {
                request.Seed();
                return Ok();
            }
            catch(Exception ex)
            {
                var msg = ex.InnerException;
                return StatusCode(500);
               
            }
           
        }

        // GET api/<InitialDataController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InitialDataController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<InitialDataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InitialDataController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
