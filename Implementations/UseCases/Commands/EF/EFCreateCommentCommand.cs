using Application;
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
    public class EFCreateCommentCommand : EFUseCaseConnection, ICreateCommentCommand
    {
        private IApplicationUser _user;
        private CreateCommentValidator _validator;
        public EFCreateCommentCommand(LibaryContext context, IApplicationUser user, CreateCommentValidator validator) : base(context)
        {
            _user = user;
            _validator = validator;
        }

        public int Id => 26;

        public string Name => "Use case for creating comment";

        public string Description => "Use case for creating comment with EF";
        public void Execute(CommentDto request)
        {
            _validator.ValidateAndThrow(request);
           if(_user.Id != request.UserId)
            {
                throw new ForbbidenUseCaseException(Name, _user.Email);
            }
            var comment = new Comment
            {
                Message = request.Comment,
                BookId = request.BookId,
                UserId = request.UserId
            };
            Context.Comments.Add(comment);
            Context.SaveChanges();
        }
    }
}
