using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Image : Entity
    {
        public string Path { get; set; }
        public int CategoryId { get; set; }
      //  public int IsCurrentlyShowing { get; set; }
        public virtual Category Category { get; set; } 
    }
}
