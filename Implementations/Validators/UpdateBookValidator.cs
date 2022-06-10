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
    public class UpdateBookValidator : AbstractValidator<CreateBookDto>
    {

        private readonly LibaryContext _context;
        public UpdateBookValidator(LibaryContext context)
        {
            _context = context;
            Include(new CreateBookValidator(context));
            //RuleFor(x => x.Isbn).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Book Isbn is required parametar.").Matches("^[0-9]{13}$").WithMessage("Isbn must contain 13 digits.")
             // .Must(x => !_context.Books.Any(y => y.Isbn == x && y.Id )).WithMessage("Book can not have same Isbn number.");
        }
    }
}
