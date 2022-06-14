using Application;
using Application.DTO;
using Application.Logging;
using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Implementations.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IApplicationUser _user;
        private UseCaseHandler _handler;

        public UserController(IApplicationUser user, UseCaseHandler handler)
        {
            _user = user;
            _handler = handler;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_user);
        }
        [HttpPut]
        [AllowAnonymous]
        public IActionResult Put([FromBody] UpdateUserUseCaseDto dto,
            [FromServices] IUpdateUserUseCasesCommand command)
        {
            _handler.HandleCommand(command,dto);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpGet("logs")]
        public IActionResult GetLogs([FromQuery] UseCaseLogSearch dto,[FromServices] IGetUseCaseLogsQuery query)
        {
            return Ok(_handler.HandleQuery(query, dto));
        }

    }
}
