using Application.DTO.Searches;
using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Implementations.UseCases;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public BooksController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public IActionResult Get([FromServices] IGetBooksQuery query, [FromQuery] BasePagedSearch dto)
        {
            return Ok(_handler.HandleQuery(query, dto));
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public IActionResult Get([FromServices] IFindBookQuery query ,int id)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        // POST api/<BooksController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,[FromServices] IDeleteBookCommand command)
        {
            _handler.HandleCommand(command,id);
            return NoContent();
        }
    }
}
