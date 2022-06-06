using Application.DTO;
using Application.Emails;
using Application.UseCases.Commands;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.UseCases.Commands.EF
{
    public class EFRegisteUserCommand : EFUseCaseConnection ,IRegisterUserCommand
    {
        private RegisterUserValidator _validator;
        private IEmailSender _sender;
        public EFRegisteUserCommand(LibaryContext context, RegisterUserValidator validator, IEmailSender sender) : base(context)
        {
            _validator = validator;
            _sender = sender;
        }

        public int Id => 4;

        public string Name => "Use case for registering a user.";

        public string Description => "Use case for registering a user with EF.";

        public void Execute(RegisterUserDto request)
        {  
            _validator.ValidateAndThrow(request);

            var user = new User
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = BCrypt.
                Net.BCrypt.HashPassword(request.Password),
            };
            Context.Users.Add(user);
            Context.SaveChanges();
            _sender.Send(new Email
            {
                To = user.Email,
                From = "ne_odgovraj@gmail.com",
                Title = "Potrdi registraciju.",
                Body = ""
            });

        }
    }
}
