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
    public class EFDeleteCategoryCommand : EFUseCaseConnection,IDeleteCategoryCommand
    {
        public EFDeleteCategoryCommand(LibaryContext context) : base(context)
        {
        }

        public int Id => 12;

        public string Name => "Use case for deleting a category.";

        public string Description => "Use case for deleting a category with EF.";

        public void Execute(int request)
        {
            var category = Context.Categories.Include(x => x.CategoryBooks).FirstOrDefault(x => x.Id == request && x.IsActive);
            if (category == null)
            {
                throw new EntityNotFoundException(nameof(Category), request);
            }
            if (category.CategoryBooks.Any())
            {
                throw new UseCaseConflctException("You can not delete category because it has books related to it.");
            }
            var images = Context.Images.Where(x => x.CategoryId == category.Id);
            foreach (var image in images)
            {
                image.IsActive = false;
                image.DeletedAt = DateTime.Now;
            }
           
            category.IsActive = false;
            category.DeletedAt = DateTime.Now;
            Context.SaveChanges();
        }
    }
}
