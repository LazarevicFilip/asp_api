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
    public class EFCreatePublisherCommand : EFUseCaseConnection, ICreatePublisherCommand
    {
        private CreatePublisherValidator _validator;
        public EFCreatePublisherCommand(LibaryContext context, CreatePublisherValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 19;

        public string Name => "Use case for creating a publisher.";

        public string Description => "Use case for creating a publisher with EF.";

        public void Execute(PublisherDto request)
        {
            _validator.ValidateAndThrow(request);
            var publisher = new Publisher
            {
                Name = request.Name,
            };
            Context.Publishers.Add(publisher);
            Context.SaveChanges();
        }
    }
}
