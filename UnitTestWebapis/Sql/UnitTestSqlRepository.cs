using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using System.Collections.Generic;
using System.Data.SqlTypes;
using com.abnamro.datastore;
using com.abnamro.datastore.Sql;

namespace UnitTestWebapis.Sql
{
    [TestClass]
    public class UnitTestSqlRepository
    {
        private class DataQueryOfInt : IDataQuery<IDataRow, int>, IDataMapper<IDataRow, int>
        {
            private string _outputColumnName;

            internal DataQueryOfInt(string query = default(string), string outputColumnName = default(string))
            {
                Query = query;
                _outputColumnName = outputColumnName;
                if (!string.IsNullOrWhiteSpace(outputColumnName)) Query += $"as [{outputColumnName}]";
            }
            public string Query { get; private set; }

            public IDictionary<string, object> QueryParameters { get; internal set; }

            IDataMapper<IDataRow, int> IDataQuery<IDataRow, int>.DataMapper => this;

            public int MapData(IDataRow dataRow) => dataRow.GetInt(_outputColumnName);
        }

        private class DataQueryOfNullableInt : IDataQuery<IDataRow, int?>, IDataMapper<IDataRow, int?>
        {
            private string _outputColumnName;

            internal DataQueryOfNullableInt(string query = default(string), string outputColumnName = default(string))
            {
                Query = query;
                _outputColumnName = outputColumnName;
            }
            public string Query { get; private set; }

            public IDictionary<string, object> QueryParameters { get; internal set; }

            IDataMapper<IDataRow, int?> IDataQuery<IDataRow, int?>.DataMapper => this;

            public int? MapData(IDataRow dataRow) => dataRow.GetIntOrDefault(_outputColumnName);
        }

        private class MySqlDataInput
        {
            internal bool MyBool { get; }
            internal bool? MyNullableBool { get; }
            internal short MyShort { get; }
            internal short? MyNullableShort { get; }
            internal int MyInt { get; }
            internal int? MyNullableInt { get; }
            internal long MyLong { get; }
            internal long? MyNullableLong { get; }
            internal double MyDouble { get; }
            internal double? MyNullableDouble { get; }
            internal decimal MyDecimal { get; }
            internal decimal? MyNullableDecimal { get; }
            internal decimal MyMoney { get; }
            internal decimal? MyNullableMoney { get; }
            internal string MyString5 { get; }
            internal string MyNullString { get; }
            internal DateTime MyDateTime { get; }
            internal DateTime? MyNullableDateTime { get; }

            internal MySqlDataInput(bool myBool, bool? myNullableBool, short myShort, short? myNullableShort, int myInt, int? myNullableInt, long myLong, long? myNullableLong, double myDouble, double? myNullableDouble, decimal myDecimal, decimal? myNullableDecimal, decimal myMoney, decimal? myNullableMoney, string myString5, string myNullString, DateTime myDateTime, DateTime? myNullableDateTime)
            {
                MyBool = myBool;
                MyNullableBool = myNullableBool;
                MyShort = myShort;
                MyNullableShort = myNullableShort;
                MyInt = myInt;
                MyNullableInt = myNullableInt;
                MyLong = myLong;
                MyNullableLong = myNullableLong;
                MyDouble = myDouble;
                MyNullableDouble = myNullableDouble;
                MyDecimal = myDecimal;
                MyNullableDecimal = myNullableDecimal;
                MyMoney = myMoney;
                MyNullableMoney = myNullableMoney;
                MyString5 = myString5;
                MyNullString = myNullString;
                MyDateTime = myDateTime;
                MyNullableDateTime = myNullableDateTime;
            }
        }

        private class MySqlDataOutput : MySqlDataInput
        {
            internal decimal MyRoundedMoney { get; }

            public MySqlDataOutput(bool myBool, bool? myNullableBool, short myShort, short? myNullableShort, int myInt, int? myNullableInt, long myLong, long? myNullableLong, double myDouble, double? myNullableDouble, decimal myDecimal, decimal? myNullableDecimal, decimal myMoney, decimal? myNullableMoney, string myString5, string myNullString, DateTime myDateTime, DateTime? myNullableDateTime, decimal myRoundedMoney) : base(myBool, myNullableBool, myShort, myNullableShort, myInt, myNullableInt, myLong, myNullableLong, myDouble, myNullableDouble, myDecimal, myNullableDecimal, myMoney, myNullableMoney, myString5, myNullString, myDateTime, myNullableDateTime)
            {
                MyRoundedMoney = myRoundedMoney;
            }
        }

        private class MyDataQuery : IDataQuery<IDataRow, MySqlDataOutput>, IDataMapper<IDataRow, MySqlDataOutput>
        {
            private enum InputParameterName
            {
                  MyBool
                , MyNullableBool
                , MyShort
                , MyNullableShort
                , MyInt
                , MyNullableInt
                , MyLong
                , MyNullableLong
                , MyDouble
                , MyNullableDouble
                , MyDecimal
                , MyNullableDecimal
                , MyMoney
                , MyNullableMoney
                , MyString5
                , MyNullString
                , MyDateTime
                , MyNullableDateTime
            }
            private enum OutputColumnName
            {
                  MyBool
                , MyNullableBool
                , MyShort
                , MyNullableShort
                , MyInt
                , MyNullableInt
                , MyLong
                , MyNullableLong
                , MyDouble
                , MyNullableDouble
                , MyDecimal
                , MyNullableDecimal
                , MyMoney
                , MyNullableMoney
                , MyRoundedMoney
                , MyString5
                , MyNullString
                , MyDateTime
                , MyNullableDateTime
            }

            public IDictionary<string, object> QueryParameters { get; private set; }

            internal MyDataQuery(MySqlDataInput mySqlData)
            {
                if (mySqlData == default(MySqlDataInput)) throw new ArgumentNullException(nameof(mySqlData));

                QueryParameters = new Dictionary<string, object>
                {
                     [nameof(InputParameterName.MyBool)]             = mySqlData.MyBool
                    ,[nameof(InputParameterName.MyNullableBool)]     = mySqlData.MyNullableBool
                    ,[nameof(InputParameterName.MyShort)]            = mySqlData.MyShort
                    ,[nameof(InputParameterName.MyNullableShort)]    = mySqlData.MyNullableShort
                    ,[nameof(InputParameterName.MyInt)]              = mySqlData.MyInt
                    ,[nameof(InputParameterName.MyNullableInt)]      = mySqlData.MyNullableInt
                    ,[nameof(InputParameterName.MyLong)]             = mySqlData.MyLong
                    ,[nameof(InputParameterName.MyNullableLong)]     = mySqlData.MyNullableLong
                    ,[nameof(InputParameterName.MyDouble)]           = mySqlData.MyDouble
                    ,[nameof(InputParameterName.MyNullableDouble)]   = mySqlData.MyNullableDouble
                    ,[nameof(InputParameterName.MyDecimal)]          = mySqlData.MyDecimal
                    ,[nameof(InputParameterName.MyNullableDecimal)]  = mySqlData.MyNullableDecimal
                    ,[nameof(InputParameterName.MyMoney)]            = mySqlData.MyMoney
                    ,[nameof(InputParameterName.MyNullableMoney)]    = mySqlData.MyNullableMoney
                    ,[nameof(InputParameterName.MyString5)]          = mySqlData.MyString5
                    ,[nameof(InputParameterName.MyNullString)]       = mySqlData.MyNullString
                    ,[nameof(InputParameterName.MyDateTime)]         = mySqlData.MyDateTime
                    ,[nameof(InputParameterName.MyNullableDateTime)] = mySqlData.MyNullableDateTime
                };
            }

            public string Query => $@"
declare @_{nameof(InputParameterName.MyBool)}             bit           = @{nameof(InputParameterName.MyBool)}
declare @_{nameof(InputParameterName.MyNullableBool)}     bit           = @{nameof(InputParameterName.MyNullableBool)}
declare @_{nameof(InputParameterName.MyShort)}            smallint      = @{nameof(InputParameterName.MyShort)}
declare @_{nameof(InputParameterName.MyNullableShort)}    smallint      = @{nameof(InputParameterName.MyNullableShort)}
declare @_{nameof(InputParameterName.MyInt)}              int           = @{nameof(InputParameterName.MyInt)}
declare @_{nameof(InputParameterName.MyNullableInt)}      int           = @{nameof(InputParameterName.MyNullableInt)}
declare @_{nameof(InputParameterName.MyDouble)}           float         = @{nameof(InputParameterName.MyDouble)}
declare @_{nameof(InputParameterName.MyNullableDouble)}   float         = @{nameof(InputParameterName.MyNullableDouble)}
declare @_{nameof(InputParameterName.MyLong)}             bigint        = @{nameof(InputParameterName.MyLong)}
declare @_{nameof(InputParameterName.MyNullableLong)}     bigint        = @{nameof(InputParameterName.MyNullableLong)}
declare @_{nameof(InputParameterName.MyDecimal)}          decimal(13,6) = @{nameof(InputParameterName.MyDecimal)}
declare @_{nameof(InputParameterName.MyNullableDecimal)}  decimal(13,6) = @{nameof(InputParameterName.MyNullableDecimal)}
declare @_{nameof(InputParameterName.MyMoney)}            money         = @{nameof(InputParameterName.MyMoney)}
declare @_{nameof(InputParameterName.MyNullableMoney)}    money         = @{nameof(InputParameterName.MyNullableMoney)}
declare @_{nameof(InputParameterName.MyString5)}          nvarchar(5)   = @{nameof(InputParameterName.MyString5)}
declare @_{nameof(InputParameterName.MyNullString)}       nvarchar(5)   = @{nameof(InputParameterName.MyNullString)}
declare @_{nameof(InputParameterName.MyDateTime)}         datetime      = @{nameof(InputParameterName.MyDateTime)}
declare @_{nameof(InputParameterName.MyNullableDateTime)} datetime      = @{nameof(InputParameterName.MyNullableDateTime)}

select
    @_{nameof(InputParameterName.MyBool)}                          as [{nameof(OutputColumnName.MyBool)}]
   ,@_{nameof(InputParameterName.MyNullableBool)}                  as [{nameof(OutputColumnName.MyNullableBool)}]
   ,@_{nameof(InputParameterName.MyShort)}                         as [{nameof(OutputColumnName.MyShort)}]
   ,@_{nameof(InputParameterName.MyNullableShort)}                 as [{nameof(OutputColumnName.MyNullableShort)}]
   ,@_{nameof(InputParameterName.MyInt)}                           as [{nameof(OutputColumnName.MyInt)}]
   ,@_{nameof(InputParameterName.MyNullableInt)}                   as [{nameof(OutputColumnName.MyNullableInt)}]
   ,@_{nameof(InputParameterName.MyLong)}                          as [{nameof(OutputColumnName.MyLong)}]
   ,@_{nameof(InputParameterName.MyNullableLong)}                  as [{nameof(OutputColumnName.MyNullableLong)}]
   ,@_{nameof(InputParameterName.MyDouble)}                        as [{nameof(OutputColumnName.MyDouble)}]
   ,@_{nameof(InputParameterName.MyNullableDouble)}                as [{nameof(OutputColumnName.MyNullableDouble)}]
   ,@_{nameof(InputParameterName.MyDecimal)}                       as [{nameof(OutputColumnName.MyDecimal)}]
   ,@_{nameof(InputParameterName.MyNullableDecimal)}               as [{nameof(OutputColumnName.MyNullableDecimal)}]
   ,cast(@_{nameof(InputParameterName.MyMoney)} as money)          as [{nameof(OutputColumnName.MyMoney)}]
   ,cast(round(@_{nameof(InputParameterName.MyMoney)},2) as money) as [{nameof(OutputColumnName.MyRoundedMoney)}]
   ,cast(@_{nameof(InputParameterName.MyNullableMoney)} as money)  as [{nameof(OutputColumnName.MyNullableMoney)}]
   ,@_{nameof(InputParameterName.MyString5)}                       as [{nameof(OutputColumnName.MyString5)}]
   ,@_{nameof(InputParameterName.MyNullString)}                    as [{nameof(OutputColumnName.MyNullString)}]
   ,@_{nameof(InputParameterName.MyDateTime)}                      as [{nameof(OutputColumnName.MyDateTime)}]
   ,@_{nameof(InputParameterName.MyNullableDateTime)}              as [{nameof(OutputColumnName.MyNullableDateTime)}]
";

            IDataMapper<IDataRow, MySqlDataOutput> IDataQuery<IDataRow, MySqlDataOutput>.DataMapper => this;

            public MySqlDataOutput MapData(IDataRow dataRow) => new MySqlDataOutput(
                                                                 dataRow.GetBool(nameof(OutputColumnName.MyBool))
                                                               , dataRow.GetBoolOrDefault(nameof(OutputColumnName.MyNullableBool))
                                                               , dataRow.GetShort(nameof(OutputColumnName.MyShort))
                                                               , dataRow.GetShortOrDefault(nameof(OutputColumnName.MyNullableShort))
                                                               , dataRow.GetInt(nameof(OutputColumnName.MyInt))
                                                               , dataRow.GetIntOrDefault(nameof(OutputColumnName.MyNullableInt))
                                                               , dataRow.GetLong(nameof(OutputColumnName.MyLong))
                                                               , dataRow.GetLongOrDefault(nameof(OutputColumnName.MyNullableLong))
                                                               , dataRow.GetDouble(nameof(OutputColumnName.MyDouble))
                                                               , dataRow.GetDoubleOrDefault(nameof(OutputColumnName.MyNullableDouble))
                                                               , dataRow.GetDecimal(nameof(OutputColumnName.MyDecimal))
                                                               , dataRow.GetDecimalOrDefault(nameof(OutputColumnName.MyNullableDecimal))
                                                               , dataRow.GetDecimal(nameof(OutputColumnName.MyMoney))
                                                               , dataRow.GetDecimalOrDefault(nameof(OutputColumnName.MyNullableMoney))
                                                               , dataRow.GetString(nameof(OutputColumnName.MyString5))
                                                               , dataRow.GetString(nameof(OutputColumnName.MyNullString))
                                                               , dataRow.GetDateTime(nameof(OutputColumnName.MyDateTime))
                                                               , dataRow.GetDateTimeOrDefault(nameof(OutputColumnName.MyNullableDateTime))
                                                               , dataRow.GetDecimal(nameof(OutputColumnName.MyRoundedMoney)));
        }

        [TestMethod]
        public void TestSqlRepositoryWithoutQuery()
        {
            const string dataQuery = nameof(dataQuery);

            var logger = LogManager.GetLogger(nameof(UnitTestSqlRepository));
            logger.Info($"Start {nameof(TestSqlRepositoryWithoutQuery)}");

            DataQueryOfInt dataQueryOfInt = null;
            var sqlConnectionString = default(string);

            try
            {
                var result = SqlSingleOrDefaultSelector.Create(dataQueryOfInt, sqlConnectionString).SelectSingleOrDefault();
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentException));
                Assert.IsTrue(exception.Message.StartsWith("Invalid Entity-type:"));
            }
            try
            {
                var result = SqlSingleSelector.Create(dataQueryOfInt, sqlConnectionString).SelectSingle();
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentException));
                Assert.AreEqual((exception as ArgumentException).ParamName, nameof(dataQuery));
            }

            try
            {
                var result = SqlMultipleSelector.Create(dataQueryOfInt, sqlConnectionString).SelectMultiple();
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentException));
                Assert.AreEqual((exception as ArgumentException).ParamName, nameof(dataQuery));
            }

            dataQueryOfInt = new DataQueryOfInt();
            try
            {
                var result = SqlSingleSelector.Create(dataQueryOfInt, sqlConnectionString).SelectSingle();
            }
            catch (ArgumentNullException argumentNullException)
            {
                // as a result of invalid value for property dataQuery.Query
                Assert.AreEqual(argumentNullException.ParamName, "sqlQuery");
            }

            dataQueryOfInt = new DataQueryOfInt("my-invalid-SqlQuery");
            sqlConnectionString = "my-invalid-sql-connectionstring";
            try
            {
                var result = SqlSingleSelector.Create(dataQueryOfInt, sqlConnectionString).SelectSingle();
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(SqlDatastoreException));
                Assert.AreEqual((exception as SqlDatastoreException).Message, $"Invalid connectionstring '{sqlConnectionString}'.");
            }

            sqlConnectionString = SqlConnectionStrings.AmtConnectionString;
            try
            {
                var result = SqlSingleSelector.Create(dataQueryOfInt, sqlConnectionString).SelectSingle();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.StartsWith("Incorrect syntax"));
            }

            var dataQueryOfNullableInt = new DataQueryOfNullableInt("select 'x' where 1=0");
            sqlConnectionString = SqlConnectionStrings.AmtConnectionString;
            try
            {
                var result = SqlSingleSelector.Create(dataQueryOfNullableInt, sqlConnectionString).SelectSingle();
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(EntityCountMismatchException));
                Assert.AreEqual((exception as EntityCountMismatchException).EntityType, typeof(int?));
                Assert.IsTrue(exception.Message.Contains("found 0 instances"));
            }

            {
                var result = SqlSingleOrDefaultSelector.Create(dataQueryOfNullableInt, sqlConnectionString).SelectSingleOrDefault();
                Assert.AreEqual(result, default(int?));
            }

            const string columnName = nameof(columnName);
            {
                const int expectedInt = 13;
                dataQueryOfInt = new DataQueryOfInt($"select CAST({expectedInt} as int)");
                try
                {
                    var myInt = SqlSingleSelector.Create(dataQueryOfInt, sqlConnectionString).SelectSingle();
                }
                catch(Exception exception)
                {
                    Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                    Assert.AreEqual((exception as ArgumentNullException).ParamName, columnName);
                }
            }

            {
                const int expectedInt = 13;
                dataQueryOfInt = new DataQueryOfInt($"select CAST({expectedInt} as int)", columnName);
                var myInt = SqlSingleSelector.Create(dataQueryOfInt, sqlConnectionString).SelectSingle();
                Assert.AreEqual(myInt, expectedInt);
            }
        }

        [TestMethod]
        public void TestSqlRepositoryWithMySqlQuery()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestSqlRepository));
            logger.Info($"Start {nameof(TestSqlRepositoryWithMySqlQuery)}");

            var mySqlDataInput = new MySqlDataInput
                                        (
                                          myBool:             true
                                        , myNullableBool:     default(bool?)
                                        , myShort:            13
                                        , myNullableShort:    default(short?)
                                        , myInt:              23456789
                                        , myNullableInt:      default(int?)
                                        , myLong:             987654321000
                                        , myNullableLong:     default(long?)
                                        , myDouble:           321.45D
                                        , myNullableDouble:   default(double?)
                                        , myDecimal:          7654321.125456M
                                        , myNullableDecimal:  default(decimal?)
                                        , myMoney:            1234.5678901M
                                        , myNullableMoney:    default(decimal?)
                                        , myString5:          "abcdefg"
                                        , myNullString:       default(string)
                                        , myDateTime:         DateTime.Now
                                        , myNullableDateTime: default(DateTime?)
                                        );
            var mySqlDataOutput = SqlSingleOrDefaultSelector.Create(new MyDataQuery(mySqlDataInput), SqlConnectionStrings.AmtConnectionString).SelectSingleOrDefault();

            //const string dateTimeFormat = "yyyy-MM-dd HH:mm:ss:fff";
            //logger.Debug($"{nameof(mySqlDataOutput)}.{nameof(mySqlDataOutput.MyDecimal)}: {mySqlDataOutput.MyDecimal}");
            //logger.Debug($"{nameof(mySqlDataInput)}.{nameof(mySqlDataInput.MyDateTime)}: {mySqlDataInput.MyDateTime.ToString(dateTimeFormat)}");
            //logger.Debug($"{nameof(mySqlDataOutput)}.{nameof(mySqlDataOutput.MyDateTime)}: {mySqlDataOutput.MyDateTime.ToString(dateTimeFormat)}");
            //logger.Debug($"sql-ised input-datetime: {new SqlDateTime(mySqlDataInput.MyDateTime).Value.ToString(dateTimeFormat)}");
            //logger.Debug($"{nameof(mySqlDataOutput)}.{nameof(mySqlDataOutput.MyMoney)}: : {mySqlDataOutput.MyMoney}");
            //logger.Debug($"{nameof(mySqlDataOutput)}.{nameof(mySqlDataOutput.MyRoundedMoney)}: : {mySqlDataOutput.MyRoundedMoney}");

            Assert.AreEqual(mySqlDataInput.MyBool,                            mySqlDataOutput.MyBool);
            Assert.AreEqual(mySqlDataInput.MyNullableBool,                    mySqlDataOutput.MyNullableBool);
            Assert.AreEqual(mySqlDataInput.MyShort,                           mySqlDataOutput.MyShort);
            Assert.AreEqual(mySqlDataInput.MyNullableShort,                   mySqlDataOutput.MyNullableShort);
            Assert.AreEqual(mySqlDataInput.MyInt,                             mySqlDataOutput.MyInt);
            Assert.AreEqual(mySqlDataInput.MyNullableInt,                     mySqlDataOutput.MyNullableInt);
            Assert.AreEqual(mySqlDataInput.MyLong,                            mySqlDataOutput.MyLong);
            Assert.AreEqual(mySqlDataInput.MyNullableLong,                    mySqlDataOutput.MyNullableLong);
            Assert.AreEqual(mySqlDataInput.MyDouble,                          mySqlDataOutput.MyDouble);
            Assert.AreEqual(mySqlDataInput.MyNullableDouble,                  mySqlDataOutput.MyNullableDouble);
            Assert.AreEqual(mySqlDataInput.MyDecimal,                         mySqlDataOutput.MyDecimal);
            Assert.AreEqual(mySqlDataInput.MyNullableDecimal,                 mySqlDataOutput.MyNullableDecimal);
            Assert.AreEqual(mySqlDataInput.MyString5.Substring(0, 5),         mySqlDataOutput.MyString5);
            Assert.AreEqual(mySqlDataInput.MyNullString,                      mySqlDataOutput.MyNullString);
            // SQL Server money type has a precision of 4 digits (a ten-thousandth of a monetary unit).
            // a .NET (decimal) value is converted to sql money value using the System.Data.SqlTypes.SqlMoney class.
            Assert.AreEqual(mySqlDataInput.MyNullableMoney,                   mySqlDataOutput.MyNullableMoney);
            var mySqlMoney = new SqlMoney(mySqlDataInput.MyMoney).Value;
            Assert.AreEqual(mySqlMoney,                                       mySqlDataOutput.MyMoney);
            Assert.AreEqual(Math.Round(mySqlMoney,2),                         mySqlDataOutput.MyRoundedMoney);
            // SQL Server date-time precision differs from .NET date-time precision.
            // a .NET date-time value is converted to sql date-time value using the System.Data.SqlTypes.SqlDateTime class.
            Assert.AreEqual(new SqlDateTime(mySqlDataInput.MyDateTime).Value, mySqlDataOutput.MyDateTime);
            Assert.AreEqual(mySqlDataInput.MyNullableDateTime,                mySqlDataOutput.MyNullableDateTime);
        }
    }
}
