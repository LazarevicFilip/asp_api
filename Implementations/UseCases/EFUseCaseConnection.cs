using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.UseCases
{
    public abstract class EFUseCaseConnection
    {
        protected LibaryContext Context;

        protected EFUseCaseConnection(LibaryContext context)
        {
            Context = context;
        }
    }
}
