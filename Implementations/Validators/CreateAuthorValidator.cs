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
    public  class CreateAuthorValidator : AbstractValidator<AuthorDto>
    {
        private LibaryContext _context;

        public CreateAuthorValidator(LibaryContext context)
        {
            _context = context;
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required parametar.")
                .MinimumLength(4).WithMessage("Name length must be over 3 charachters")
                .Must(AuthorAlreadyExists).WithMessage("Author {PropertyValue} is already in use.");
        }

        
        private bool AuthorAlreadyExists(string name)
        {
            if(_context.Authors.Any(x => x.Name == name))
            {
                return false;
            }
            return true;
        }
    }
}
