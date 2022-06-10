using Application.DTO;
using Microsoft.AspNetCore.Http;

namespace API.DTO
{
    public class CreateCategoryApiDto : CreateCategoryDto
    {
        public IFormFile File { get; set; }
    }
    public class CreateBookApiDto : UpdateBookDto
    {
        public IFormFile File { get; set; }
    }
}
