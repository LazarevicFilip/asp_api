using API.DTO;
using Application.DTO.Searches;
using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Implementations.UseCases;
using Microsoft.AspNetCore.Http;
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
    public class CategoriesController : ControllerBase
    {
        private UseCaseHandler _handler;
        private IEnumerable<string> SupportedExtensions => new List<string> { ".jpg", ".png", ".jpeg" };

        public CategoriesController(UseCaseHandler handler)
        {
            _handler = handler;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public IActionResult Get([FromServices] IGetCategoriesQuery command, [FromQuery] BasePagedSearch dto)
        {
            return Ok(_handler.HandleQuery(command,dto));
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public IActionResult Post([FromServices] ICreateCategoryCommand command ,[FromForm] CreateCategoryApiDto dto)
        {
            if (dto.File != null)
            {
                var guid = Guid.NewGuid();
                var extension = Path.GetExtension(dto.File.FileName);
                if (!SupportedExtensions.Contains(extension))
                {
                    throw new InvalidOperationException("Invalid File Extension.");
                }
                var newFileName = guid + extension;
                var path = Path.Combine("wwwroot", "Images", newFileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    dto.File.CopyTo(fileStream);
                }
                dto.PathName = newFileName;
            }
            _handler.HandleCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
