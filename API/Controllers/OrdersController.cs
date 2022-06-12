using Application.DTO;
using Application.DTO.Searches;
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
    public class OrdersController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public OrdersController(UseCaseHandler handler)
        {
            _handler = handler;
        }
        [HttpGet("user/{id}")]
        public IActionResult Get(int id,[FromQuery] OrderBasePagedSearch dto ,[FromServices] IGetUsersOrderQuery query)
        {
            dto.UserId = id;
            return Ok(_handler.HandleQuery(query, dto));
        }
        [HttpPost]
        public IActionResult Post([FromBody] MakeOrderDto dto,[FromServices] ICreateOrderCommand command)
        {
            _handler.HandleCommand(command,dto);
            return StatusCode(StatusCodes.Status201Created);
           
        }
    }
}
