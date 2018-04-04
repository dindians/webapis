using System;

namespace com.abnamro.datastore.Sql
{
    public class EntityCountMismatchException : Exception
    {
        public Type EntityType { get; }

        internal EntityCountMismatchException(Type entityType, string message) : base(message)
        {
            EntityType = entityType;
        }
    }
}
