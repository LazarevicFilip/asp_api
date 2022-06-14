using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logging
{
    public interface IUseCaseLogger
    {
        public void Log(UseCaseLog log);
        public IEnumerable<UseCaseLog> GetLogs(UseCaseLogSearch log);
    }
    public class UseCaseLogSearch
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string UserName { get; set; }
        public int? UserId { get; set; }
        public string UseCaseName { get; set; }
    }
    public class UseCaseLog
    {
        public string UseCaseName { get; set; }
        public string User { get; set; }
        public int UserId { get; set; }
        public DateTime ExecutionDateTime { get; set; }
        public string Data { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
