using System;

namespace com.abnamro.webapi.core
{
    public class UserAuthenticationException : Exception
    {
        public UserAuthenticationException(string message) : base(message) { }
    }
}
