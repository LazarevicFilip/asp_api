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
    public class CreateBookValidator : AbstractValidator<CreateBookDto>
    {
        private readonly LibaryContext _context;

        public CreateBookValidator(LibaryContext context)
        {
            _context = context;
            RuleFor(x => x.Title).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Book title is required parametar.");
            RuleFor(x => x.Isbn).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Book Isbn is required parametar.").Matches("^[0-9]{13}$").WithMessage("Isbn must contain 13 digits.")
                .Must(x => !_context.Books.Any(y => y.Isbn == x)).WithMessage("Book can not have same Isbn number.");
            RuleFor(x => x.Description).Length(5, 150).WithMessage("Book description length must be between 5 and 150 characters.");
            RuleFor(x => x.Format).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Book format is required parametar.").Matches("^[1-9]{1}[0-9]{1}(\\.5)?x[1-9]{1}[0-9]{1}(\\.5)?$").WithMessage("Book format must be in valid form. Ex: 20x20, 20.5x15...");
            RuleFor(x => x.PagesCount).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Book pagesCount is required parametar.").ExclusiveBetween(9,3001).WithMessage("Pages count must be between 10 and 3000 pages");
            RuleFor(x => x.Price).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Book price is required parametar.").ExclusiveBetween(194, 10001).WithMessage("Price must be between 195 and 10 000 rsd.");
            RuleFor(x => x.AuthorId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Book author is required parametar.").Must(x => context.Authors.Any(y => y.Id == x)).WithMessage("There is no author with provided Id");
            RuleFor(x => x.BookCategoryIds).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("CategoryIds parametar must have a value")
                .Must(x => x.Count() == x.Distinct().Count()).WithMessage("There is a duplicates in the set of the provided ids.");
            RuleForEach(x => x.BookCategoryIds).NotEmpty().WithMessage("Every provided id must have a value").Must(x => context.Categories.Any(z => z.Id == x)).WithMessage("There is no category with provided Id {PropertyValue}");

        }
    }
}
