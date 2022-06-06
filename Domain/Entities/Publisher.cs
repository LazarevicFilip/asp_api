using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Publisher : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<BookPublishers> PublisherBooks { get; set; } = new List<BookPublishers>();
    }
}
