using API.Auth;
using API.Extensions;
using Application.DTO;
using Application.UseCases.Commands;
using FluentValidation;
using Implementations.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UseCaseHandler _handler;
        private JwtManager _manager;

        public AuthController(UseCaseHandler handler, JwtManager manager)
        {
            _handler = handler;
            _manager = manager;
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterUserDto dto, [FromServices] IRegisterUserCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost("token")]
        public IActionResult Token([FromBody] GenereteTokenDto dto )
        {
            try
            {
                var token = _manager.MakeToken(dto.Email, dto.Password);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        public class GenereteTokenDto
        {
            public string Email { get; set; }
            public string Password { get; set; }    
        }
    }
}
