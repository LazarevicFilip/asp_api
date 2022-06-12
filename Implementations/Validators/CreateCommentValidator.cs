using Application.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.Validators
{
    public class CreateCommentValidator : AbstractValidator<CommentDto>
    {
        private LibaryContext _context;
        public CreateCommentValidator(LibaryContext context)
        {
            _context = context;
            RuleFor(x => x.Comment).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Comment is required parametar").MaximumLength(250).WithMessage("Comment maximum length is 250 characters.");
            RuleFor(x => x.UserId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("UserId is required parametar").Must(x => context.Users.Any(y => y.Id == x && y.IsActive)).WithMessage("There is no user with a provided Id");
            RuleFor(x => x.BookId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("BookId is required parametar").Must(x => context.Books.Any(y => y.Id == x && y.IsActive)).WithMessage("There is no book with a provided Id");
        }
    }
}
