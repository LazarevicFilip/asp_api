using System;

namespace Application.Exceptions
{
    public class UseCaseConflctException : Exception
    {
        public UseCaseConflctException(string msg)
            : base(msg)
        {

        }
    }
}
