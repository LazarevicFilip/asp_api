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
    public  class UpdateAuthorValidator : AbstractValidator<AuthorDto>
    {
        private LibaryContext _context;
        public UpdateAuthorValidator(LibaryContext context)
        {
            _context = context;
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required parametar.")
                .MinimumLength(4).WithMessage("Name length must be over 3 charachters");
            RuleFor(x => x.Id).Cascade(CascadeMode.Stop).Must(x => _context.Authors.Any(z => z.Id == x)).WithMessage("Author wth a id {PropertyValue} doesnt exists.");
            RuleFor(x => x).Must(x => !_context.Authors.Any(y => y.Name == x.Name && x.Id != y.Id)).WithMessage("Author with a given name is already in use.");
          
        }
    }
}
