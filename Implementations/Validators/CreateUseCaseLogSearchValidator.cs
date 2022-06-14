using Application.Logging;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.Validators
{
    public class CreateUseCaseLogSearchValidator : AbstractValidator<UseCaseLogSearch>
    {
        public CreateUseCaseLogSearchValidator()
        {
            RuleFor(x => x.DateFrom).NotEmpty().WithMessage("DateFrom is required parametar.");
            RuleFor(x => x.DateTo).NotEmpty().WithMessage("DateTo is required parametar.");
            RuleFor(x => x).Must(x => (x.DateTo - x.DateFrom).Days <= 30).WithMessage("Diffrence between DateFrom and DatoTo can't be bigger than 30 days.");
        }
    }
}
