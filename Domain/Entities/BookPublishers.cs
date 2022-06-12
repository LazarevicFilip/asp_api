using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookPublishers
    {
        public int BookId { get; set; }
        public int PublisherId { get; set; }
        public DateTime PublishedYear { get; set; }
        public Book Book { get; set; }
        public Publisher Publisher { get; set; }
    }
}
