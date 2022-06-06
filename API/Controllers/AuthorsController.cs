using API.Extensions;
using Application.DTO;
using Application.DTO.Searches;
using Application.UseCases.Commands;
using Application.UseCases.Queries;
using FluentValidation;
using Implementations.UseCases;
using Implementations.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private UseCaseHandler _handler;

        public AuthorsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<AuthorsController>
        [HttpGet]
        public IActionResult Get([FromServices] IGetAuthorsQuery query,[FromQuery]BaseSearch dto)
        {
            try
            {
                return Ok(_handler.HandleQuery(query, dto));
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        /// <summary>
        /// Creates a Author entity.
        /// </summary>
        /// <response code="201">Successfully created entity. </response>
        /// <response code="422">Dto object is not in good form. </response>
        ///<response code="500"> Unexpected server error. </response>
        // POST api/<AuthorsController>
        [HttpPost]
        public IActionResult Post(
            [FromServices] ICreateAuthorCommand command,
            [FromBody] AuthorDto dto)
        {
            try
            {
                _handler.HandleCommand(command, dto);
                return StatusCode(201);
            }
            catch (ValidationException e)
            {
               return e.Errors.ToUnprocessableEntity();
            }
            catch (Exception ex)
            {
               var msg = ex.InnerException;
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }

        // PUT api/<AuthorsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthorsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
