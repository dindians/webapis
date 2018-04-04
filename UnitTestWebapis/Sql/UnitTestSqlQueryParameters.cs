using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NLog;
using System.Configuration;
using System;
using com.abnamro.datastore;
using com.abnamro.datastore.Sql;

namespace UnitTestWebapis.Sql
{
    [TestClass]
    public class UnitTestSqlQueryParameters : IDataQuery<IDataRow, int>, IDataMapper<IDataRow, int>
    {
        const string myIntParameter = nameof(myIntParameter);
        const string myIntColumn = nameof(myIntColumn);
        const int myIntValue = 13;

        public string Query { get; private set; }

        public IDictionary<string, object> QueryParameters { get; private set; }

        IDataMapper<IDataRow, int> IDataQuery<IDataRow, int>.DataMapper => this;

        public int MapData(IDataRow dataRow) =>  dataRow.GetInt(myIntColumn);

        [TestMethod]
        public void TestIntParameter()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestSqlQueryParameters));
            logger.Info($"Start {nameof(TestIntParameter)}");

            Query = $@"select @{myIntParameter} as {myIntColumn}";
            QueryParameters = new Dictionary<string, object>
            {
                [myIntParameter] = myIntValue
            };

            var myInt = SqlSingleSelector.Create(this, ConfigurationManager.AppSettings[AppSettingKeys.AmtConnectionString]).SelectSingle();
            Assert.AreEqual(myInt, myIntValue);
        }

        [TestMethod]
        public void TestInvalidParameterName()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestSqlQueryParameters));
            logger.Info($"Start {nameof(TestInvalidParameterName)}");

            Query = $@"--";
            QueryParameters = new Dictionary<string, object>
            {
                [""] = null
            };

            try
            {
                var myInt = SqlSingleSelector.Create(this, ConfigurationManager.AppSettings[AppSettingKeys.AmtConnectionString]).SelectSingle();
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
                Assert.AreEqual((exception as ArgumentNullException).ParamName, "parameterName");
            }
        }

        [TestMethod]
        public void TestInvalidParameterType()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestSqlQueryParameters));
            logger.Info($"Start {nameof(TestInvalidParameterType)}");

            Query = $@"
select @{myIntParameter} as {myIntColumn}
";
            QueryParameters = new Dictionary<string, object>
            {
                [myIntParameter] = this
            };

            try
            {
                var myInt = SqlSingleSelector.Create(this, ConfigurationManager.AppSettings[AppSettingKeys.AmtConnectionString]).SelectSingle();
            }
            catch(Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(NotSupportedTypeException));
                Assert.AreEqual((exception as NotSupportedTypeException).Type, GetType());
            }
        }

        [TestMethod]
        public void TestNullableParametersWithoutValue()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestSqlQueryParameters));
            logger.Info($"Start {nameof(TestNullableParametersWithoutValue)}");

            var myNullableBool = default(bool?);
            var myNullableShort = default(short?);
            var myNullableInt = default(int?);
            var myNullableLong = default(long?);
            var myNullableDouble = default(double?);
            var myNullableDecimal = default(decimal?);
            var myNullableDateTime = default(DateTime?);
            var myNullString = default(string);
            QueryParameters = new Dictionary<string, object>
            {
                 [nameof(myNullableBool)] = myNullableBool
                ,[nameof(myNullableShort)] = myNullableShort
                ,[nameof(myNullableInt)] = myNullableInt
                ,[nameof(myNullableLong)] = myNullableLong
                ,[nameof(myNullableDouble)] = myNullableDouble
                ,[nameof(myNullableDecimal)] = myNullableDecimal
                ,[nameof(myNullableDateTime)] = myNullableDateTime
                ,[nameof(myNullString)] = myNullString
            };

            Query = $@"
select 
  case when @{nameof(myNullableBool)} is null then 0 else 1 end
+ case when @{nameof(myNullableShort)} is null then 0 else 1 end
+ case when @{nameof(myNullableInt)} is null then 0 else 1 end
+ case when @{nameof(myNullableLong)} is null then 0 else 1 end
+ case when @{nameof(myNullableDouble)} is null then 0 else 1 end
+ case when @{nameof(myNullableDecimal)} is null then 0 else 1 end
+ case when @{nameof(myNullableDateTime)} is null then 0 else 1 end
+ case when @{nameof(myNullString)} is null then 0 else 1 end
as [{myIntColumn}]
";
            var myIntValue = SqlSingleSelector.Create(this, ConfigurationManager.AppSettings[AppSettingKeys.AmtConnectionString]).SelectSingle();
            Assert.AreEqual(myIntValue, 0);
        }

        [TestMethod]
        public void TestNullableParametersWithValue()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestSqlQueryParameters));
            logger.Info($"Start {nameof(TestNullableParametersWithValue)}");

            var myNullableBool = (bool?)true;
            var myNullableShort = (short?)1;
            var myNullableInt = (int?)2;
            var myNullableLong = (long?)3L;
            var myNullableDouble = (double?)4D;
            var myNullableDecimal = (decimal?)5M;
            var myNullableDateTime = (DateTime?)DateTime.Now;
            QueryParameters = new Dictionary<string, object>
            {
                 [nameof(myNullableBool)] = (bool?)true
                ,
                [nameof(myNullableShort)] = myNullableShort
                ,[nameof(myNullableInt)] = (short?)1
                ,
                [nameof(myNullableLong)] = myNullableLong
                ,[nameof(myNullableDouble)] = myNullableDouble
                ,[nameof(myNullableDecimal)] = myNullableDecimal
                ,[nameof(myNullableDateTime)] = myNullableDateTime
            };

            Query = $@"
select 
  case when @{nameof(myNullableBool)} is not null then 0 else 1 end
+ case when @{nameof(myNullableShort)} is not null then 0 else 1 end
+ case when @{nameof(myNullableInt)} is not null then 0 else 1 end
+ case when @{nameof(myNullableLong)} is not null then 0 else 1 end
+ case when @{nameof(myNullableDouble)} is not null then 0 else 1 end
+ case when @{nameof(myNullableDecimal)} is not null then 0 else 1 end
+ case when @{nameof(myNullableDateTime)} is not null then 0 else 1 end
as [{myIntColumn}]
";
            var myIntValue = SqlSingleSelector.Create(this, ConfigurationManager.AppSettings[AppSettingKeys.AmtConnectionString]).SelectSingle();
            Assert.AreEqual(myIntValue, 0);
        }
    }
}
