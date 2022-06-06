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

        

        public IEnumerable<AuthorDto> Execute(BaseSearch request)
        {
            var query = Context.Authors.AsQueryable();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }
            return query.Select(x => new AuthorDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
