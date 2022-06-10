using Application.DTO;
using Application.UseCases.Commands;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.UseCases.Commands.EF
{
    public class EFCreateBookCommand : EFUseCaseConnection ,ICreateBookCommand
    {
        private readonly CreateBookValidator _validator;

        public EFCreateBookCommand(LibaryContext context,CreateBookValidator validator)
            :base(context)
        {
            _validator = validator;
        }

        public int Id => 17;

        public string Name => "Use case for creating a book.";

        public string Description => "Use case for creating a book with EF.";

        public void Execute(CreateBookDto request)
        {
            _validator.ValidateAndThrow(request);
            var book = new Book();

            book.Title = request.Title;
            book.Price = request.Price;
            book.Format = request.Format;
            book.Isbn = request.Isbn;
            book.Description = request.Description;
            book.PagesCount = request.PagesCount;
            book.AuthorId = request.AuthorId;
            book.BookCategories = request.BookCategoryIds.Select(x => new BookCategories
               {
                   Book = book,
                   CategoryId = x
               }).ToList();
            if (!string.IsNullOrEmpty(request.PathName))
            {
                var image = new BookImage
                {
                    Path = request.PathName,
                    Book = book
                };
                Context.BookImages.Add(image);
            }
            Context.Books.Add(book);
            Context.SaveChanges();
        }
    }
}
