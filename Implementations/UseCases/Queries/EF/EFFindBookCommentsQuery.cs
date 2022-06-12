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
    public class EFFindBookCommentsQuery : EFUseCaseConnection, IFindBookCommentsQuery
    {
        public EFFindBookCommentsQuery(LibaryContext context) : base(context)
        {
        }

        public int Id => 27;

        public string Name => "Use case for fetching all comments for a book.";

        public string Description => "Use case for fetching all comments for a book with EF.";

        public IEnumerable<GetCommentDto> Execute(int request)
        {
            var book = Context.Books.FirstOrDefault(x => x.Id == request);
            if(book == null)
            {
                throw new EntityNotFoundException(nameof(Book), request);
            }
            var comments = Context.Comments.Include(x => x.User).Include(x => x.Book).Where(x => x.BookId == request && x.IsActive);
            
                var response = comments.Select(x => new GetCommentDto
                {
                    Comment = x.Message,
                    Date = x.CreatedAt,
                    Book = x.Book.Title,
                    User = x.User.Email
                });

            return response;


        }
    }
}
