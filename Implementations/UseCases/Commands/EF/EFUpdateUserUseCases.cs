using Application.DTO;
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
    public class EFUpdateUserUseCases : EFUseCaseConnection, IUpdateUserUseCasesCommand
    {
        private UpdateUserUseCaseValidator _validator;
        public EFUpdateUserUseCases(LibaryContext context, UpdateUserUseCaseValidator validator)
            :base(context)
        {
            _validator = validator;
        }
        public int Id => 5;

        public string Name => "Use case for registering a user.";

        public string Description => "Use case for registering a user with EF.";

        public void Execute(UpdateUserUseCaseDto request)
        {
            _validator.ValidateAndThrow(request);
            var userUseCases = Context.UserUseCases.Where(x => x.UserId == request.UserId);
            Context.UserUseCases.RemoveRange(userUseCases);
            var newUserUseCases = request.UseCaseIds.Select(x => new UserUseCase
            {
                UserId = request.UserId,
                UseCaseId = x
            });
            Context.UserUseCases.AddRange(newUserUseCases);
            Context.SaveChanges();
        }
    }
}
