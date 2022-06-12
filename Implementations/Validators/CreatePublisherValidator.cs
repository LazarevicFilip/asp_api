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
    public class CreatePublisherValidator : AbstractValidator<PublisherDto>
    {
        private readonly LibaryContext _context;
        public CreatePublisherValidator(LibaryContext context)
        {
            _context = context;
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required parametar.")
                .MinimumLength(4).WithMessage("Name length must be over 3 charachters")
                .Must(x => !_context.Publishers.Any(y => y.Name == x)).WithMessage("Publisher {PropertyValue} is already in use.");
        }
    }
}
