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
    public class EFUpdateCategoryCommand : EFUseCaseConnection, IUpdateCategoryCommand
    {
        private readonly UpdateCategoryValidator _validator;

        public EFUpdateCategoryCommand(LibaryContext context, UpdateCategoryValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 13;

        public string Name => "Use case for updating a category.";

        public string Description => "Use case for updating a category.";

        public void Execute(CreateCategoryDto request)
        {
            _validator.ValidateAndThrow(request);
            var category = Context.Categories.FirstOrDefault(x => x.Id == request.Id && x.IsActive);
            if(category == null)
            {
                throw new EntityNotFoundException(nameof(Category),(int)request.Id);
            }
            if (!string.IsNullOrEmpty(request.PathName))
            {
               
                var image = new Image
                {
                    Path = request.PathName
                };
                category.Images.Add(image);
            }
            category.Name = request.Name;
            category.ParentId = request.ParentId;
            Context.SaveChanges();
            
        }
    }
}
