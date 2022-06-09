using Application.DTO;
using Application.Exceptions;
using Application.UseCases.Commands;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementations.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.UseCases.Commands.EF
{
    public class EFUpdateAuthorCommand : EFUseCaseConnection, IUpdateAuthorsCommand
    {
        private UpdateAuthorValidator _validator;
        public EFUpdateAuthorCommand(LibaryContext context, UpdateAuthorValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 9;

        public string Name => "Use case for updating a authors";

        public string Description => "Use case for updating a authors witg EF";

        public void Execute(AuthorDto request)
        {
             _validator.ValidateAndThrow(request);
           var author =  Context.Authors.FirstOrDefault(x => x.Id == request.Id);
            author.Name = request.Name;
         
            Context.SaveChanges();
        }
    }
}
