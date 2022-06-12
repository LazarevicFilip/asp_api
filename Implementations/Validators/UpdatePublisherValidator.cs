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
    public class UpdatePublisherValidator : AbstractValidator<UpdatePublisherDto>
    {
        private readonly LibaryContext _context;
        public UpdatePublisherValidator(LibaryContext context)
        {
            _context = context;
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required parametar.")
                .MinimumLength(4).WithMessage("Name length must be over 3 charachters");
            RuleFor(x => x).Must(x => !context.Publishers.Any(y => y.Name == x.Name && y.Id != x.Id)).WithMessage("Publisher with a given name {PropertyValue} is already in use.");
        }
    }
}
