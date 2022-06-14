using Application.Logging;
using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.Logging
{
    public class EFUseCaseLogger : IUseCaseLogger
    {
        private LibaryContext _context;

        public EFUseCaseLogger(LibaryContext context)
        {
            _context = context;
        }

        public IEnumerable<UseCaseLog> GetLogs(UseCaseLogSearch log)
        {
            var logs = _context.UseCaseLogs.Where(x => x.ExecutionTime >= log.DateFrom && x.ExecutionTime <= log.DateTo).AsQueryable();
            if (!string.IsNullOrEmpty(log.UseCaseName))
            {
                logs = logs.Where(x => x.UseCaseName.Contains(log.UseCaseName));
            }
            if (!string.IsNullOrEmpty(log.UserName))
            {
                logs = logs.Where(x => x.UserName.Contains(log.UserName));
            }
            if (log.UserId != null)
            {
                logs = logs.Where(x => x.UserId == log.UserId);
            }
            var response = logs.Select(x => new UseCaseLog
            {
                User = x.UserName,
                UseCaseName = x.UseCaseName,
                Data = x.Data,
                UserId = x.UserId,
                ExecutionDateTime = x.ExecutionTime,
                IsAuthorized = x.IsAuthorized
            }).ToList();
            return response;

        }

        public void Log(UseCaseLog log)
        {
            var logRecord = new UseCaseLogger
            {
                UserId = log.UserId,
                Data = log.Data,
                ExecutionTime = DateTime.Now,
                IsAuthorized = log.IsAuthorized,
                UseCaseName = log.UseCaseName,
                UserName = _context.Users.FirstOrDefault(x => x.Id == log.UserId).UserName
            };
            _context.UseCaseLogs.Add(logRecord);
            _context.SaveChanges();

        }
    }
}
