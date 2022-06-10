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
    public class EfFindCategoryQuery : EFUseCaseConnection, IFindCategoryQuery
    {
        public EfFindCategoryQuery(LibaryContext context) : base(context)
        {
        }

        public int Id => 11;

        public string Name => "Use case for finding a category";

        public string Description => "Use case for finding a category with EF";

        public FindCategoryDto Execute(int request)
        {
            var category = Context.Categories.Include(x => x.SubCategories).Include(x => x.CategoryBooks).ThenInclude(x => x.Book).FirstOrDefault(c => c.Id == request && c.IsActive);
            if (category == null)
            {
                throw new EntityNotFoundException(nameof(Category), request);
            }
            return new FindCategoryDto
            {
                Name = category.Name,
                ParentId = category.ParentId,
                //PathName = category.Images.First().Path,
                Books = category.CategoryBooks.Select(y => new BookDto
                {
                    Isbn = y.Book.Isbn,
                    Price = y.Book.Price,
                    Title = y.Book.Title,
                }),
                ChildCategories = category.SubCategories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentId = x.ParentId,
                }).ToList(),

            };
        }
    }
}
