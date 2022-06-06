using Application.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.Logging
{
    public class ConsoleExceptionLogger : IExceptionLogger
    {
        public void Log(Exception ex)
        {
            Console.WriteLine("Occuret at " + DateTime.UtcNow);
            Console.WriteLine(ex.Message); 
            Console.WriteLine(ex.InnerException);
        }
    }
}
