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
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        private LibaryContext _context;
        public RegisterUserValidator(LibaryContext context)
        {
            _context = context;
            var nameRegex = "^[A-Z][a-z]{2,}(\\s[A-Z][a-z]{2,})*$";
            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Korisnicko ime je obavezan podatak")
                .MinimumLength(3).WithMessage("Korisnicko ime mora imati bar 3 karaktera")
                .MaximumLength(20).WithMessage("Korisnicko ime ne sme imati vise od 20 karaktera")
                .Matches("^(?=[a-zA-Z0-9._]{3,20}$)(?!.*[_.]{2})[^_.].*[^_.]$").WithMessage("Korisnicko ime se moze satojati od slova,cifara i _ (donjih crta)")
                .Must(x => !_context.Users.Any(y => y.UserName == x)).WithMessage("Korisnicko ime {PropertyValue} je vec iskorisceno.");

            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Ime je obavezan podatak")
                .MaximumLength(50).WithMessage("Ime ne sme imati vise od 50 karaktera.")
                .Matches(nameRegex).WithMessage("Ime mora imati brarem 3 karaktera i poceti velikim slovom.");

            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Prezime je obavezan podatak")
                .MaximumLength(50).WithMessage("Prezime ne sme imati vise od 50 karaktera.")
                .Matches(nameRegex).WithMessage("Prezime mora imati brarem 3 karaktera i poceti velikim slovom.");

            RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Email je obavezan podatak").EmailAddress().WithMessage("Podatak {PropertyValue} nije validna email adresa").Must(x => !_context.Users.Any(y => y.Email == x)).WithMessage("Email {PropertyValue} je vec iskoriscen.");

            RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Password je obavezan podatak").Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$").WithMessage("Password mora imati barem jedno malo,jdeno veliko slovo,broj i specijalni karakter.");

        }
    }
}
