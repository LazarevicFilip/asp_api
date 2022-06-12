using Application.DTO;
using Application.DTO.Searches;
using Application.UseCases.Queries;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.UseCases.Queries.EF
{
    public class EFGetPublishersQuery : EFUseCaseConnection, IGetPublishersQuery
    {
        public EFGetPublishersQuery(LibaryContext context) : base(context)
        {
        }

        public int Id => 21;

        public string Name => "Use case for searching a publishers.";

        public string Description => "Use case for searching a publishers with EF.";

        public PagedResponse<GetPublisherDto> Execute(BasePagedSearch request)
        {
            var publishers = Context.Publishers.Include(x => x.PublisherBooks).ThenInclude(x => x.Book).Where(x => x.IsActive).AsQueryable();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                publishers = publishers.Where(x => x.Name.Contains(request.Keyword));
            }
            if(request.PerPage == null || request.PerPage < 10)
            {
                request.PerPage = 10;
            }
            if(request.Page == null || request.Page < 1)
            {
                request.Page = 1;
            }
            var toSkip = (request.Page - 1) * request.PerPage;
            var response = new PagedResponse<GetPublisherDto>();
            response.TotalCount = publishers.Count();
            response.ItemsPerPage = request.PerPage.Value;
            response.CurrentPage = request.Page.Value;
            response.Data = publishers.Skip(toSkip.Value).Take(request.PerPage.Value).Select(x => new GetPublisherDto
            {
                Id = x.Id,
                Name = x.Name,
                Books = x.PublisherBooks.Select(y => new BookDto
                {
                    Isbn = y.Book.Isbn,
                    Price = y.Book.Price,
                    Title = y.Book.Title
                })
            }).ToList();
           return response;
        }
    }
}
