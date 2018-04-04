using System;

namespace com.abnamro.datastore.Sql
{
    public class SqlDatastoreException : Exception
    {
        internal SqlDatastoreException(string message) : base(message) { }
        internal SqlDatastoreException(string message, Exception innerException) : base(message, innerException) { }
    }
}
