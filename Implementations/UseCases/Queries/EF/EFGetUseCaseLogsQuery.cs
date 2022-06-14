using Application.Logging;
using Application.UseCases.Queries;
using FluentValidation;
using Implementations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.UseCases.Queries.EF
{
    public class EFGetUseCaseLogsQuery : IGetUseCaseLogsQuery
    {
        private readonly CreateUseCaseLogSearchValidator _validator;
        private readonly IUseCaseLogger _logger;

        public EFGetUseCaseLogsQuery(IUseCaseLogger logger, CreateUseCaseLogSearchValidator validator)
        {
            _logger = logger;
            _validator = validator;
        }

        public int Id => 31;

        public string Name => "Use case for searching a use case logs";

        public string Description => "Use case for searching a use case logs with EF";

        public IEnumerable<UseCaseLog> Execute(UseCaseLogSearch request)
        {
            _validator.ValidateAndThrow(request);
            return _logger.GetLogs(request);
        }
    }
}
