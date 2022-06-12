using Application.DTO;
using Application.Exceptions;
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
    public class EFUpdatePublisherCommand : EFUseCaseConnection, IUpdatePublisherCommand
    {
        private readonly UpdatePublisherValidator _validator;
        public EFUpdatePublisherCommand(LibaryContext context, UpdatePublisherValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 22;

        public string Name => "Use case for updating a publisher";

        public string Description => "Use case for updating a publisher with EF";

        public void Execute(UpdatePublisherDto request)
        {
            _validator.ValidateAndThrow(request);
            var publisher = Context.Publishers.FirstOrDefault(x => x.Id == request.Id && x.IsActive);
            if (publisher == null)
            {
                throw new EntityNotFoundException(nameof(Publisher), (int)request.Id);
            }
            publisher.Name = request.Name;
            Context.SaveChanges();
        }
    }
}
