using Application;
using Application.Exceptions;
using Application.Logging;
using Application.UseCases.Commands;
using Application.UseCases.Queris;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.UseCases
{
    public class UseCaseHandler
    {
        private IExceptionLogger _exceptionLogger;
        private IUseCaseLogger _useCaseLogger;
        private IApplicationUser _user;

        public UseCaseHandler(IExceptionLogger logger, IUseCaseLogger useCaseLogger, IApplicationUser user)
        {
            _exceptionLogger = logger;
            _useCaseLogger = useCaseLogger;
            _user = user;
        }

        public void HandleCommand<TRequest>(ICommand<TRequest> command,TRequest data)
        {
            //decorator
            try
            {
                LogUseCase(command, data);
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                command.Execute(data);
                stopwatch.Stop();
                Console.WriteLine(command.Name + " Duration: " + stopwatch.ElapsedMilliseconds + " ms.");
            }
            catch (Exception ex)
            {
                _exceptionLogger.Log(ex);
                //deligira ex dalje(global)
                throw;
            }
        }

      

        public TResponse HandleQuery<TRequest,TResponse>(IQuery<TRequest, TResponse> query, TRequest data)
        {
            try
            {
                LogUseCase(query, data);
                 var stopwatch = new Stopwatch();
                stopwatch.Start();

                var result = query.Execute(data);
               
                stopwatch.Stop();
                Console.WriteLine(query.Name + " Duration: " + stopwatch.ElapsedMilliseconds + " ms.");
                return result;
            }
            catch (Exception ex)
            {
                _exceptionLogger.Log(ex);
                //deligira ex dalje(global)
                throw;
            }
        }
        private void LogUseCase<TRequest>(IUseCase usecase, TRequest data)
        {
            var isAuthorized = _user.UseCaseIds.Contains(usecase.Id);
            var log = new UseCaseLog
            {
                User = _user.Identity,
                UseCaseName = usecase.Name,
                ExecutionDateTime = DateTime.Now,
                UserId = _user.Id,
                Data = JsonConvert.SerializeObject(data),
            
                IsAuthorized = isAuthorized

            };
            if (!log.IsAuthorized)
            {
                throw new ForbbidenUseCaseException(usecase.Name, _user.Email);
            }
        }
    }
}
