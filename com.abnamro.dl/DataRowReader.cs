using System;
using System.Collections.Generic;
using System.Data;

namespace com.abnamro.dl
{
    internal class DataRowReader : IDataRow
    {
        private readonly IDataReader _dataReader;
        private readonly IDictionary<string, int> _fieldMap;

        internal DataRowReader(IDataReader dataReader)
        {
            if (dataReader == default(IDataReader)) throw new ArgumentNullException(nameof(dataReader));

            _dataReader = dataReader;
            _fieldMap = new Dictionary<string, int>(dataReader.FieldCount);
            for(var fieldIndex = 0; fieldIndex < dataReader.FieldCount; fieldIndex++)
            {
                _fieldMap.Add(dataReader.GetName(fieldIndex), fieldIndex);
            }
        }

        public bool GetBool(string columnName) => GetValue<bool>(columnName);
        public bool? GetBoolOrDefault(string columnName) => GetValueOrDefault<bool>(columnName);
        public byte GetByte(string columnName) => GetValue<byte>(columnName);
        public byte? GetByteOrDefault(string columnName) => GetValueOrDefault<byte>(columnName);
        public short GetShort(string columnName) => GetValue<short>(columnName);
        public short? GetShortOrDefault(string columnName) => GetValueOrDefault<short>(columnName);
        public int GetInt(string columnName) => GetValue<int>(columnName);
        public int? GetIntOrDefault(string columnName) => GetValueOrDefault<int>(columnName);
        public long GetLong(string columnName) => GetValue<long>(columnName);
        public long? GetLongOrDefault(string columnName) => GetValueOrDefault<long>(columnName);
        public double GetDouble(string columnName) => GetValue<double>(columnName);
        public double? GetDoubleOrDefault(string columnName) => GetValueOrDefault<double>(columnName);
        public decimal GetDecimal(string columnName) => GetValue<decimal>(columnName);
        public decimal? GetDecimalOrDefault(string columnName) => GetValueOrDefault<decimal>(columnName);
        public DateTime GetDateTime(string columnName) => GetValue<DateTime>(columnName);
        public DateTime? GetDateTimeOrDefault(string columnName) => GetValueOrDefault<DateTime>(columnName);

        public string GetString(string columnName)
        {
            var fieldIndex = GetIndex(columnName);
            return _dataReader.IsDBNull(fieldIndex) ? default(string) : _dataReader.GetString(fieldIndex);
        }

        private TValue GetValue<TValue>(string columnName) where TValue : struct
        {
            TValue? valueOrDefault = GetValueOrDefault<TValue>(columnName);
            if (valueOrDefault is TValue) return (TValue)valueOrDefault;

            throw new ColumnTypeMismatchException(columnName, $"Expected {typeof(TValue).Name} value for {nameof(columnName)} '{columnName}'. Found null.");
        }

        private TValue? GetValueOrDefault<TValue>(string columnName) where TValue : struct
        {
            var fieldIndex = GetIndex(columnName);
            if (_dataReader.IsDBNull(fieldIndex)) return default(TValue?);

            try
            {
                if (typeof(TValue) == typeof(bool)) return _dataReader.GetBoolean(fieldIndex) as TValue?;
                if (typeof(TValue) == typeof(byte)) return _dataReader.GetByte(fieldIndex) as TValue?;
                if (typeof(TValue) == typeof(short)) return _dataReader.GetInt16(fieldIndex) as TValue?;
                if (typeof(TValue) == typeof(int)) return _dataReader.GetInt32(fieldIndex) as TValue?;
                if (typeof(TValue) == typeof(long)) return _dataReader.GetInt64(fieldIndex) as TValue?;
                if (typeof(TValue) == typeof(double)) return _dataReader.GetDouble(fieldIndex) as TValue?;
                if (typeof(TValue) == typeof(decimal)) return _dataReader.GetDecimal(fieldIndex) as TValue?;
                if (typeof(TValue) == typeof(DateTime)) return _dataReader.GetDateTime(fieldIndex) as TValue?;
            }
            catch(InvalidCastException invalidCastException)
            {
                throw new ColumnTypeMismatchException(columnName, $"Expected column-type {typeof(TValue).Name} value for {nameof(columnName)} '{columnName}'.", invalidCastException);
            }

            throw new ColumnTypeMismatchException(columnName, $"Don't know how to retrieve {typeof(TValue).Name} value for {nameof(columnName)} {columnName}.");
        }

        private int GetIndex(string columnName)
        {
            if (string.IsNullOrWhiteSpace(columnName)) throw new ArgumentNullException(nameof(columnName));
            if (!_fieldMap.ContainsKey(columnName)) throw new ColumnNotFoundException(columnName, $"{nameof(columnName)} key '{columnName}' not found.");

            return _fieldMap[columnName];
        }
    }
}
