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
    public class EFDeleteAuthorCommand : EFUseCaseConnection, IDeleteAuthorCommand
    {
        public EFDeleteAuthorCommand(LibaryContext context)
            :base(context)
        {

        }
        public int Id => 7;

        public string Name => "Use case for deleting a author.";

        public string Description => "Use case for deleting a author with EF.";

        public void Execute(int request)
        {
            var author = Context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == request && x.IsActive);
            if (author == null)
            {
                throw new EntityNotFoundException(nameof(Author), request);
            }
            if (author.Books.Any())
            {
                throw new UseCaseConflctException("Deleting author is denied because it contains books that reference to it." + 
                    string.Join(", ",author.Books.Select(x => x.Title)));
            }
            author.DeletedAt = DateTime.Now;
            author.IsActive = false;
         
            Context.SaveChanges();
        }
    }
}
