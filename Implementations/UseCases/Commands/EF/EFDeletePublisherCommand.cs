using Application.Exceptions;
using Application.UseCases.Commands;
using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.UseCases.Commands.EF
{
    public class EFDeletePublisherCommand :EFUseCaseConnection, IDeletePublisherCommand
    {
        public EFDeletePublisherCommand(LibaryContext context) : base(context)
        {
        }

        public int Id => 23;

        public string Name => "Use case for deleting a categories";

        public string Description => "Use case for deleting a categories with EF";

        public void Execute(int request)
        {
            var publisher = Context.Publishers.Include(x => x.PublisherBooks).FirstOrDefault(x => x.Id == request && x.IsActive);
            if (publisher == null)
            {
                throw new EntityNotFoundException(nameof(Publisher), request);
            }
            if (publisher.PublisherBooks.Any())
            {
                throw new UseCaseConflctException("Deleting publishers is denied because it contains books that reference to it." +
                    string.Join(", ", publisher.PublisherBooks.Select(x => x.BookId)));
            }
            publisher.DeletedAt = DateTime.Now;
            publisher.IsActive = false;

            Context.SaveChanges();
        }
    }
}
