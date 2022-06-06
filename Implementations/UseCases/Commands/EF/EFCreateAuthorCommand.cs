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
    public class EFCreateAuthorCommand : EFUseCaseConnection, ICreateAuthorCommand
    {
        private CreateAuthorValidator _validator;
        public int Id => 3;

        public string Name => "Use case for creating authors";

        public string Description => "Use case for creating authors with EF";
        public EFCreateAuthorCommand(LibaryContext context, CreateAuthorValidator validator)
            : base(context)
        {
            _validator = validator;
        }
        public void Execute(AuthorDto request)
        {
            _validator.ValidateAndThrow(request);

            var author = new Author
            {
                Name = request.Name
            };
            Context.Add(author);
            Context.SaveChanges();
        }
    }
}
