using Application.Exceptions;
using Application.Logging;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class GlobalExceptionHandler
    {
        //skup metoda koji imaju kao parametar http context i koje treba da se izvrsi za taj http request
        private readonly RequestDelegate _next;
        private readonly IExceptionLogger _logger;

        public GlobalExceptionHandler(RequestDelegate next, IExceptionLogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                httpContext.Response.ContentType = "application/json";
                object response = null;
                var statusCode = StatusCodes.Status500InternalServerError;
                if (ex is ForbbidenUseCaseException)
                {
                    statusCode = StatusCodes.Status403Forbidden;
                }
                if (ex is EntityNotFoundException)
                {
                    statusCode = StatusCodes.Status404NotFound;
                }
                if (ex is UseCaseConflctException)
                {
                    statusCode = StatusCodes.Status409Conflict;
                    response = new { Error = ex.Message };
                }
                if (ex is ValidationException e)
                {
                    statusCode = StatusCodes.Status422UnprocessableEntity;
                    response = new
                    {
                        errors = e.Errors.Select(x => new
                        {
                            errorMessge = x.ErrorMessage,
                            errorName = x.PropertyName
                        })
                    };

                }
                httpContext.Response.StatusCode = statusCode;
                if (response != null)
                {
                    await httpContext.Response.WriteAsJsonAsync(response);
                }
            }
        }
    }
}
