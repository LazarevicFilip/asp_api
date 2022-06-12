using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int PagesCount { get; set; }
        public string? Description { get; set; }
        public string Format { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual ICollection<BookCategories> BookCategories { get; set; } = new List<BookCategories>();
        public virtual ICollection<BookPublishers> BookPublishers { get; set; } = new List<BookPublishers>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
       
    }
}
