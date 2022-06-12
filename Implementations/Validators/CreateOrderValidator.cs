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
    public class CreateOrderValidator : AbstractValidator<MakeOrderDto>
    {
        private readonly LibaryContext _context;
        public CreateOrderValidator(LibaryContext context)
{
            _context = context;
            RuleFor(x => x.Phone).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Phone is required parametar").Matches(@"^(\+381[0-9]{8,9}|(06|01)[0-9]{7,8})$").WithMessage("Phonne number must start with +381 and followed by (7-9) digits");
            RuleFor(x => x.Recipient).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Recipient is required parametar").Matches("^[A-Z][a-z]{2,}(\\s[A-Z][a-z]{2,})*$").WithMessage("Recipient must contain only letters and digits.").MaximumLength(80).WithMessage("Recipient cannot be longer than 80 characters");
            RuleFor(x => x.Adress).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Adress is required parametar").Matches("^([A-ZŠĐĆČŽ][a-zšđčćž]{2,15}|[0-9])+(\\s[A-ZŠĐĆČŽ[a-zšđčćž0-9\\.\\-]{2,20})+$").WithMessage("Adress must be a valid adress.").MaximumLength(80).WithMessage("Adress cannot be longer than 80 characters");
            RuleFor(x => x.UserId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("UserId is requrired parametar").Must(x => context.Users.Any(y => y.Id == x && y.IsActive)).WithMessage("User with a provided id is a unvalid.");
            
            RuleFor(x => x.OrderLines).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("OrderLines is required parametar")
                 .Must(x => x.Count() > 0)
                 .WithMessage("Minimum number of order lines is 1.");
            RuleForEach(x => x.OrderLines).SetValidator(new OrderLineValidator(context));
        }
    }
}