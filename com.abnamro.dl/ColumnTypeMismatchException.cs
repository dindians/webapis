using System;

namespace com.abnamro.dl
{
    public class ColumnTypeMismatchException : Exception
    {
        public string ColumnName { get; }

        internal ColumnTypeMismatchException(string columnName, string message) : base(message)
        {
            ColumnName = columnName;
        }

        internal ColumnTypeMismatchException(string columnName, string message, Exception innerException) : base(message, innerException)
        {
            ColumnName = columnName;
        }
    }
}
