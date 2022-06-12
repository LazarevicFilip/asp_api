using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.Validators
{
    public class OrderLineValidator : AbstractValidator<OrderLineDto>
    {
        private readonly LibaryContext _context;
        public OrderLineValidator(LibaryContext context)
        {
            _context = context;
            RuleFor(x => x.Quantity).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Quantity is required parametar.").ExclusiveBetween(0, 21).WithMessage("Quantity must be between 1 and 20");
            RuleFor(x => x.BookName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("BookName is required parametar.").Must(x => context.Books.Any(y => y.Title == x)).WithMessage("There is no book with a provided title in the system.");
            RuleFor(x => x.BookPrice).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("BookPrice is required parametar.").Must(x => context.Books.Any(y => y.Price == x)).WithMessage("There is no book with a provided price in the system.");
            RuleFor(x => x.BookId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("BookId is required parametar.").Must(x => context.Books.Any(y => y.Id == x)).WithMessage("There is no book with a provided id in the system.");
            RuleFor(x => x.BookPublisherId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("BookPublisherId is required parametar.").Must(x => context.Books.Any(y => y.BookPublishers.Any(z => z.PublisherId == x))).WithMessage("There is no book publisher with a provided id in the system.");
            RuleFor(x => x).Must(x => context.Books.Find(x.BookId).Title == x.BookName && context.Books.Find(x.BookId).Price == x.BookPrice).WithMessage("The title and price must match the provided id");

        }
    }
}
