using System;

namespace com.abnamro.dl
{
    public class NotSupportedTypeException : Exception
    {
        public Type Type { get; }

        internal NotSupportedTypeException(Type type, string message) : base(message)
        {
            Type = type;
        }
    }
}
