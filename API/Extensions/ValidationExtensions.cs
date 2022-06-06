using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FluentValidation.Results;
using System.Collections.Generic;

namespace API.Extensions
{
    public static class ValidationExtensions
    {
        public static UnprocessableEntityObjectResult ToUnprocessableEntity(this IEnumerable<ValidationFailure> errors)
        {
            var errorMsgs = errors.Select(x => new
            {
                errorMessge = x.ErrorMessage,
                errorName = x.PropertyName
            });
            return new UnprocessableEntityObjectResult(errorMsgs);
            
        }
    }
}
