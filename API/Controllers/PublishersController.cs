using Application.DTO;
using Application.DTO.Searches;
using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Implementations.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PublishersController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public PublishersController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<PublishersController>
        [HttpGet]
        public IActionResult Get([FromQuery] BasePagedSearch dto, [FromServices] IGetPublishersQuery query)
        {
            return Ok(_handler.HandleQuery(query, dto));
        }

        // GET api/<PublishersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id,[FromServices] IFindPublisherQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        // POST api/<PublishersController>
        [HttpPost]
        public IActionResult Post([FromBody] PublisherDto dto,[FromServices] ICreatePublisherCommand command)
        {
            _handler.HandleCommand(command, dto);
            return NoContent();
        }

        // PUT api/<PublishersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdatePublisherDto dto, [FromServices] IUpdatePublisherCommand command)
        {
            dto.Id = id;
            _handler.HandleCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<PublishersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeletePublisherCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
