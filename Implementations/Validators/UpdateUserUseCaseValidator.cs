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
    public class UpdateUserUseCaseValidator : AbstractValidator<UpdateUserUseCaseDto>
    {
        public UpdateUserUseCaseValidator(LibaryContext context)
        {
            RuleFor(x => x.UserId).Must(x => context.Users.Any(z => z.Id == x && z.IsActive))
                .WithMessage("User with provided ID does not exists");
            RuleFor(x => x.UseCaseIds).NotEmpty()
                .WithMessage("UseCaseIds should not be empty.")
                .Must(x => x.Distinct().Count() == x.Count())
                .WithMessage("UseCaseIds shold not contain duplicates.");
        }
    }
}
