using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CreateCategoryDto : CategoryDto
    {
        public string PathName { get; set; }
     
    }
    public class CategoryDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
    public class FindCategoryDto : CreateCategoryDto
    {
        public IEnumerable<CategoryDto> ChildCategories { get; set; }
        public IEnumerable<BookDto> Books { get; set; }
    }
}
