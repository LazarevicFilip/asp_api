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

namespace Implementations.UseCases.Commands.EF
{
    public class EfFindBookQuery : EFUseCaseConnection, IFindBookQuery
    {
        public EfFindBookQuery(LibaryContext context) : base(context)
        {
        }

        public int Id => 14;

        public string Name => "Use case for finding book";

        public string Description => "Use case for finding book with EF";


        public ExtendendBookDto Execute(int request)
        {
            var book = Context.Books.Include(x => x.Author).Include(x => x.BookCategories).ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == request && x.IsActive);
            if (book == null)
            {
                throw new EntityNotFoundException(nameof(Book), request);
            }
            return new ExtendendBookDto
            {
                Title = book.Title,
                Isbn = book.Isbn,
                Description = book.Description,
                Format = book.Format,
                PagesCount = book.PagesCount,
                Price = book.Price,
                Author = book.Author.Name,
                Category = book.BookCategories.Select(x => new CategoryDto
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name,
                    ParentId = x.Category.ParentId,
                })

            };
        }
    }
}
