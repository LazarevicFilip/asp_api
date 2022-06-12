using Application.Exceptions;
using Application.UseCases.Commands;
using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.UseCases.Commands.EF
{
    public class EFDeleteCommentCommand : EFUseCaseConnection, IDeleteCommentCommand
    {
        public EFDeleteCommentCommand(LibaryContext context) : base(context)
        {
        }

        public int Id => 28;

        public string Name => "Use case for deleting comment on a book.";

        public string Description => "Use case for deleting comment on a book with EF.";

        public void Execute(int request)
        {
            var comment = Context.Comments.FirstOrDefault(x => x.Id == request && x.IsActive);
            if (comment == null)
            {
                throw new EntityNotFoundException(nameof(Comment), request);
            }
            Context.Comments.Remove(comment);
            Context.SaveChanges();
        }
    }
}
