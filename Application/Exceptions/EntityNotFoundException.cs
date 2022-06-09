using System;

namespace Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entity,int id)
            : base($"Entity {entity} with an id of {id} was not found in the system.")
        {

        }
    }
}
