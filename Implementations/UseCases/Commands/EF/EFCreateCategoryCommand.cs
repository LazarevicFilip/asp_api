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
    public class EFCreateCategoryCommand : EFUseCaseConnection, ICreateCategoryCommand
    {
        private readonly CreateCategoryValidator _validator;

        public EFCreateCategoryCommand(LibaryContext context,CreateCategoryValidator validator)
            :base(context)
        {
            _validator = validator;
        }

        public int Id => 6;

        public string Name => "Use case for creating a category.";

        public string Description => "Use case for creating a category with EF.";

        public void Execute(CreateCategoryDto request)
        {
            _validator.ValidateAndThrow(request);
            var category = new Category
            {
                Name = request.Name,
                ParentId = request.ParentId
            };
            if (!string.IsNullOrEmpty(request.PathName))
            {
                var image = new Image
                {
                    Path = request.PathName
                };
                category.Images.Add(image);
            }
            Context.Categories.Add(category);
            Context.SaveChanges();
        }
    }
}
