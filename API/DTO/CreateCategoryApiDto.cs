using Application.DTO;
using Microsoft.AspNetCore.Http;

namespace API.DTO
{
    public class CreateCategoryApiDto : CreateCategoryDto
    {
        public IFormFile File { get; set; }
    }
}
