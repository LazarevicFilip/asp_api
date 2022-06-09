using Application.DTO;
using Application.DTO.Searches;
using Application.UseCases.Queries;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.UseCases.Queries.EF
{
    public class EFGetAuthorsQuery : EFUseCaseConnection, IGetAuthorsQuery
    {
        public EFGetAuthorsQuery(LibaryContext context)
            :base(context)
        {

        }
        public int Id => 1;

        public string Name => "Use case for searching authors";

        public string Description => "Use case for searching authors with EF";

        

        public PagedResponse<AuthorDto> Execute(BasePagedSearch request)
        {
            var query = Context.Authors.Where(x => x.IsActive).AsQueryable();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }
            if (request.PerPage == null || request.PerPage < 10)
            {
                request.PerPage = 10;
            }
            if(request.Page == null || request.Page < 1)
            {
                request.Page = 1;
            }
            var toSkip = (request.Page - 1) * request.PerPage;
            var response = new PagedResponse<AuthorDto>();
            response.CurrentPage = request.Page.Value;
            response.ItemsPerPage = request.PerPage.Value;
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip.Value).Take(request.PerPage.Value).Select(x => new AuthorDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return response;
        }
    }
}
