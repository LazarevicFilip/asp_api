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
       
    }
}
