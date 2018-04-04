using System;

namespace com.abnamro.dl
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
