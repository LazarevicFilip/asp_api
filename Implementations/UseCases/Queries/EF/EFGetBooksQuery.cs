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
    public class EFGetBooksQuery : EFUseCaseConnection, IGetBooksQuery
    {
        public EFGetBooksQuery(LibaryContext context) : base(context)
        {
        }

        public int Id => 15;

        public string Name => "Use case for searching books";

        public string Description => "Use case for searching books with EF";

        public PagedResponse<ExtendendBookDto> Execute(BasePagedSearch request)
        {
            var query = Context.Books.Include(x => x.Author).Include(x => x.BookCategories).ThenInclude(x => x.Category).Where(x => x.IsActive).AsQueryable();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Title.Contains(request.Keyword) || x.Author.Name.Contains(request.Keyword) || x.Description.Contains(request.Keyword));
            }
            if (request.PerPage == null || request.PerPage < 10) {
                request.PerPage = 10;
            }
            if (request.Page == null || request.Page < 1)
            {
                request.Page = 1;
            }
            var toSkip = (request.Page - 1) * request.PerPage;
            var response = new PagedResponse<ExtendendBookDto>
            {
                TotalCount = query.Count(),
                CurrentPage = request.Page.Value,
                ItemsPerPage = request.PerPage.Value,
                Data = query.Skip(toSkip.Value).Take(request.PerPage.Value).Select(x => new ExtendendBookDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author.Name,
                    Isbn = x.Isbn,
                    Price = x.Price,
                    Format = x.Format,
                    Description = x.Description,
                    PagesCount = x.PagesCount,
                    Category = x.BookCategories.Select(y => new CategoryDto
                    {
                        Id = y.Category.Id,
                        Name = y.Category.Name,
                        ParentId = y.Category.ParentId
                    })
                }).ToList()
            };
            return response;
       
        }
    }
}
