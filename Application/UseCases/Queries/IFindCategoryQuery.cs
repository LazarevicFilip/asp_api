using Application.DTO;
using Application.UseCases.Queris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries
{
    public interface IFindCategoryQuery : IQuery<int,FindCategoryDto>
    {
    }
}
