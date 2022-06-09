using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class AuthorDto : BaseDto
    {
        public string Name { get; set; }
       
    }
    public class FindAuthorDto
    {
        public string Name { get; set; }
        public IEnumerable<BookDto> Books { get; set; }
    }
}
