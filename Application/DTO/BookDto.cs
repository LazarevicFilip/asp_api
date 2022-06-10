using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class BookDto
    {
        public string Title { get; set; }
        public string Isbn { get; set; }
        public decimal Price { get; set; }
    }
    public class ExtendendBookDto : BookDto
    {
        public string Description { get; set; }

        public int PagesCount { get; set; }
        public string Format { get; set; }
        public string Author { get; set; }
        public IEnumerable<CategoryDto> Category { get; set; }
       

    }
}
