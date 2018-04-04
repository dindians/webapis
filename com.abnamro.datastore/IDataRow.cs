using System;

namespace com.abnamro.datastore
{
    public interface IDataRow
    {
        bool GetBool(string columnName);
        bool? GetBoolOrDefault(string columnName);
        byte GetByte(string columnName);
        byte? GetByteOrDefault(string columnName);
        short GetShort(string columnName);
        short? GetShortOrDefault(string columnName);
        int GetInt(string columnName);
        int? GetIntOrDefault(string columnName);
        long GetLong(string columnName);
        long? GetLongOrDefault(string columnName);
        double GetDouble(string columnName);
        double? GetDoubleOrDefault(string columnName);
        decimal GetDecimal(string columnName);
        decimal? GetDecimalOrDefault(string columnName);
        DateTime GetDateTime(string columnName);
        DateTime? GetDateTimeOrDefault(string columnName);
        string GetString(string columnName);
    }
}
