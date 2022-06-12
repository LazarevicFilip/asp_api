using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Searches
{
    public class BaseSearch
    {
        public string Keyword { get; set; }
    }
    public class PagedSearch
    {
        public int? Page { get; set; } = 1;
        public int? PerPage { get; set; } = 10;
    }
    public class BasePagedSearch : PagedSearch
    {
        public string Keyword { get; set; }
    }
    public class OrderBasePagedSearch : BasePagedSearch
    {
        public int UserId { get; set; }
    }
}
