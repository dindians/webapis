using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace com.abnamro.dl
{
    internal class SqlParameterCreator
    {
        internal static IEnumerable<SqlParameter> CreateSqlParameters(IDictionary<string, object> parameters)
        {
            if ((parameters?.Count??0) == 0) yield break;

            foreach (var parameter in parameters)
            {
                yield return CreateSqlParameter(parameter.Key, parameter.Value);
            }
        }

        private static SqlParameter CreateSqlParameter(string parameterName, object parameterValue)
        {
            if (string.IsNullOrWhiteSpace(parameterName)) throw new ArgumentNullException(nameof(parameterName));

            if (parameterValue == default(object)) return new SqlParameter(parameterName, DBNull.Value);

            var sqlDbType = ToSqlDbType(parameterValue.GetType());
            return new SqlParameter(parameterName, sqlDbType) { Value = ToSqlValue(parameterValue, sqlDbType) };
        }

        private static SqlDbType ToSqlDbType(Type valueType)
        {
            if (valueType == default(Type)) throw new ArgumentNullException(nameof(valueType));

            if (valueType == typeof(bool)) return SqlDbType.Bit;
            if (valueType == typeof(short)) return SqlDbType.SmallInt;
            if (valueType == typeof(int)) return SqlDbType.Int;
            if (valueType == typeof(long)) return SqlDbType.BigInt;
            if (valueType == typeof(double)) return SqlDbType.Float;
            if (valueType == typeof(decimal)) return SqlDbType.Decimal;
            if (valueType == typeof(DateTime)) return SqlDbType.DateTime;
            if (valueType == typeof(string)) return SqlDbType.NVarChar;

            throw new NotSupportedTypeException(valueType, $"Unknown {nameof(valueType)} type {valueType.Name}");
        }

        private static object ToSqlValue(object value, SqlDbType sqlDbType)
        {
            if (value == default(object)) return DBNull.Value;

            if (sqlDbType == SqlDbType.DateTime)
            {
                if (!(value is DateTime)) throw new ArgumentException($"Invalid parameter type. Expected type {nameof(DateTime)}, Actual type {value.GetType().Name}.", nameof(value));
                return new SqlDateTime((DateTime)value).Value;
            }

            if(sqlDbType == SqlDbType.Money)
            {
                return new SqlMoney(Convert.ToDecimal(value)).Value;
            }

            return value;
        }
    }
}
