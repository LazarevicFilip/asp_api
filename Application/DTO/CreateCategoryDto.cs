using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CreateCategoryDto
    {
        public string PathName { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
    public class CategoryDto
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
