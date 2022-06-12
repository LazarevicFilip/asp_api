using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class PublisherDto
    {
        public string Name { get; set; }
    }
    public class UpdatePublisherDto : PublisherDto
    {
        public int Id { get; set; }
    }
    public class GetPublisherDto : UpdatePublisherDto
    {
       
        public IEnumerable<BookDto> Books { get; set; }
    }
}
