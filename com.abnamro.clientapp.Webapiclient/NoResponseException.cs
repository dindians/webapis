using System;

namespace com.abnamro.clientapp.webapiclient
{
    public class NoResponseException : Exception
    {
        internal NoResponseException(string message) : base(message) { }
    }
}
