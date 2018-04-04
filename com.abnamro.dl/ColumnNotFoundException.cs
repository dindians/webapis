using System;

namespace com.abnamro.dl
{
    public class ColumnNotFoundException : Exception
    {
        public string ColumnName { get; }

        internal ColumnNotFoundException(string columnName, string message) : base(message)
        {
            ColumnName = columnName;
        }
    }
}
