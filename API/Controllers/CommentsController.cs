using Application.DTO;
using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Implementations.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private UseCaseHandler _handler;

        public CommentsController(UseCaseHandler handler)
        {
            _handler = handler;
        }
        [HttpGet("book/{id}")]
        public IActionResult Get(int id ,[FromServices] IFindBookCommentsQuery query)
        {
           
            return Ok(_handler.HandleQuery(query, id));
        }
        [HttpPost]
        public IActionResult Post([FromBody] CommentDto dto ,[FromServices] ICreateCommentCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public IActionResult Post(int id, [FromServices] IDeleteCommentCommand  command)
        {
            _handler.HandleCommand(command,id);
            return NoContent();
        }
    }
}
