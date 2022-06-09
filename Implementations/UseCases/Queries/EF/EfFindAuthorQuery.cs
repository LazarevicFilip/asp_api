using Application.DTO;
using Application.Exceptions;
using Application.UseCases.Queries;
using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.UseCases.Queries.EF
{
    public class EfFindAuthorQuery : EFUseCaseConnection, IFindAuthorQuery
    {
        public EfFindAuthorQuery(LibaryContext context) : base(context)
        {
        }

        public int Id => 8;

        public string Name => "Use case for finding authors";

        public string Description => "Use case for finding authors with EF";

        public FindAuthorDto Execute(int request)
        {
            var author = Context.Authors.Include(x => x.Books)
                .FirstOrDefault(x => x.Id == request && x.IsActive);
            if(author == null)
            {
                throw new EntityNotFoundException(nameof(Author), request);
            }
            return new FindAuthorDto
            {
                Name = author.Name,
                Books = author.Books.Select(x => new BookDto
                {
                    Isbn = x.Isbn,
                    Title = x.Title,
                    Price = x.Price
                })
            };
        }
    }
}
