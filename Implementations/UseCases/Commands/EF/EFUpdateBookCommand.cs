using Application.DTO;
using Application.Exceptions;
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
    public class EFUpdateBookCommand : EFUseCaseConnection, IUpdateBookCommand
    {
        private readonly CreateBookValidator _validator;
        public EFUpdateBookCommand(LibaryContext context, CreateBookValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 18;

        public string Name => "Use case for updating a book.";

        public string Description => "Use case for updating a book with EF.";

        public void Execute(UpdateBookDto request)
        {
            _validator.ValidateAndThrow(request);
            var book = Context.Books.FirstOrDefault(x => x.Id == request.Id && x.IsActive);
            if (book == null)
            {
                throw new EntityNotFoundException(nameof(Book), (int)request.Id);
            }
            if (!string.IsNullOrEmpty(request.PathName))
            {
                var image = Context.BookImages.FirstOrDefault(x => x.BookId == request.Id);
                if(image != null)
                {
                    image.Path = request.PathName;
                }
            }
            book.Title = request.Title;
            book.Format = request.Format;
            book.Description = request.Description;
            book.Price = request.Price;
            book.PagesCount = request.PagesCount;
            book.Isbn = request.Isbn;
            book.AuthorId = request.AuthorId;
            book.BookCategories = request.BookCategoryIds.Select(x => new BookCategories
            {
                BookId = request.Id,
                CategoryId = x
            }).ToList();
            Context.SaveChanges();
        }
    }
}
