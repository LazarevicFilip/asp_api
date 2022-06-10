using Application.Exceptions;
using Application.UseCases.Commands;
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
    public class EFDeleteBookCommand : EFUseCaseConnection, IDeleteBookCommand
    {
        public EFDeleteBookCommand(LibaryContext context) : base(context)
        {
        }

        public int Id => 16;

        public string Name => "Use case for deleting a book.";

        public string Description => "Use case for deleting a book with EF.";

        public void Execute(int request)
        {
            var book = Context.Books.Include(x => x.BookCategories).FirstOrDefault(x => x.Id == request && x.IsActive);
            if (book == null)
            {
                throw new EntityNotFoundException(nameof(Book), request);
            }
            var bookCategories = Context.BookCategories.Where(x => x.BookId == request);
            Context.RemoveRange(bookCategories);
            book.IsActive = false;
            book.DeletedAt = DateTime.UtcNow;
            Context.SaveChanges();
        }
    }
}
