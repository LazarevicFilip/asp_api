using Application.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.Logging
{
    public class ConsoleUseCaseLogger : IUseCaseLogger
    {
        public IEnumerable<UseCaseLog> GetLogs(UseCaseLogSearch log)
        {
            throw new NotImplementedException();
        }

        public void Log(UseCaseLog log)
        {
            Console.WriteLine($"UseCase: {log.UseCaseName}, User: {log.User}, {log.ExecutionDateTime}, Authorized: {log.IsAuthorized}");
            Console.WriteLine($"Use Case Data:  {log.Data}");

        }
    }
}
