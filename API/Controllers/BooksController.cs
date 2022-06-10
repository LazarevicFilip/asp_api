using API.DTO;
using Application.DTO;
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
    public class BooksController : ControllerBase
    {
        private readonly UseCaseHandler _handler;
        private IEnumerable<string> SupportedExtensions => new List<string> { ".jpg", ".png", ".jpeg" };

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
        public IActionResult Post([FromForm] CreateBookApiDto dto,[FromServices] ICreateBookCommand command)
        {
            UploadPhoto(dto);
            _handler.HandleCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] CreateBookApiDto dto,[FromServices] IUpdateBookCommand command)
        {
            dto.Id = id;
            UploadPhoto(dto);
            _handler.HandleCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,[FromServices] IDeleteBookCommand command)
        {
            _handler.HandleCommand(command,id);
            return NoContent();
        }
        private void UploadPhoto(CreateBookApiDto dto)
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
        }
    }
}
