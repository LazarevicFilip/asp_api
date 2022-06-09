using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ForbbidenUseCaseException : Exception
    {
        public ForbbidenUseCaseException(string useCase,string user)
            : base($"User {user} has tried to execute {useCase} without having premission.")
        {

        }
    }
}
