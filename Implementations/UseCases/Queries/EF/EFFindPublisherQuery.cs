using Application.DTO;
using Application.Exceptions;
using Application.UseCases.Queries;
using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.UseCases.Queries.EF
{
    public class EFFindPublisherQuery : EFUseCaseConnection, IFindPublisherQuery
    {
        public EFFindPublisherQuery(LibaryContext context) : base(context)
        {
        }

        public int Id => 20;

        public string Name => "Use case for finding a publisher.";

        public string Description => "Use case for finding a publisher with EF.";

        public GetPublisherDto Execute(int request)
        {
            var publisher = Context.Publishers.Include(x => x.PublisherBooks).ThenInclude(x => x.Book).FirstOrDefault(x => x.Id == request && x.IsActive);
            if (publisher == null)
            {
                throw new EntityNotFoundException(nameof(Publisher), request);
            }
            return new GetPublisherDto
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Books = publisher.PublisherBooks.Select(x => new BookDto
                {
                    Title = x.Book.Title,
                    Isbn = x.Book.Isbn,
                    Price = x.Book.Price
                })
            };
        }
    }
}
