using API.DTO;
using Application.DTO;
using Application.DTO.Searches;
using Application.UseCases.Commands;
using Application.UseCases.Queries;
using FluentValidation;
using Implementations.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        public IActionResult Get([FromServices] IGetAuthorsQuery query,[FromQuery]BasePagedSearch dto)
        {
            return Ok(_handler.HandleQuery(query, dto));
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{id}")]
        public IActionResult Get([FromServices] IFindAuthorQuery query,int id)
        {
            return Ok(_handler.HandleQuery(query, id));
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
            _handler.HandleCommand(command, dto);
               return StatusCode(201);
            
        }

        // PUT api/<AuthorsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] AuthorDto dto,[FromServices] IUpdateAuthorsCommand command)
        {
            dto.Id = id;
            _handler.HandleCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<AuthorsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromServices] IDeleteAuthorCommand command ,int id)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
