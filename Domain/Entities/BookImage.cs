using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class BookImage : Entity
    {
        public string Path { get; set; }
        public int BookId { get; set; }
        public Book Book  { get; set; }
    }
}
