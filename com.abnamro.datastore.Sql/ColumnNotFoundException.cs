using System;

namespace com.abnamro.datastore.Sql
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
