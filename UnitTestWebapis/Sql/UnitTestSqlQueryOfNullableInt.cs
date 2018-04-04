using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System;
using NLog;
using com.abnamro.datastore;
using com.abnamro.datastore.Sql;

namespace UnitTestWebapis.Sql
{
    [TestClass]
    public class UnitTestSqlQueryOfNullableInt : IDataQuery<IDataRow, int?>, IDataMapper<IDataRow, int?>
    {
        const string myIntParameter = nameof(myIntParameter);
        const string myIntColumn = nameof(myIntColumn);
        const int myIntValue = 13;

        public string Query { get; private set; }

        public IDictionary<string, object> QueryParameters { get; private set; }

        IDataMapper<IDataRow, int?> IDataQuery<IDataRow, int?>.DataMapper => this;

        public int? MapData(IDataRow dataRow)
        {
            Assert.IsNotNull(dataRow);
            const string nonExistingColumnName = nameof(nonExistingColumnName);
            try
            {
                var result = dataRow.GetInt(nonExistingColumnName);
            }
            catch(Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ColumnNotFoundException));
                Assert.AreEqual((exception as ColumnNotFoundException).ColumnName, nonExistingColumnName);
            }

            try
            {
                var result = dataRow.GetBoolOrDefault(myIntColumn);
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ColumnTypeMismatchException));
                Assert.AreEqual((exception as ColumnTypeMismatchException).ColumnName, myIntColumn);
            }

            return dataRow.GetInt(myIntColumn);
        }

        [TestMethod]
        public void TestSqlQueryWithoutParameters()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestSqlQueryOfNullableInt));
            logger.Info($"Start {nameof(TestSqlQueryWithoutParameters)}");

            Query = "--";
            QueryParameters = null;
            var myInt = SqlSingleOrDefaultSelector.Create(this, ConfigurationManager.AppSettings[AppSettingKeys.AmtConnectionString]).SelectSingleOrDefault();
        }

        [TestMethod]
        public void TestSqlQueryWithEmptyParameter()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestSqlQueryOfNullableInt));
            logger.Info($"Start {nameof(TestSqlQueryWithEmptyParameter)}");

            Query = "--";
            QueryParameters = new Dictionary<string, object>
            {
                [myIntParameter] = default(object)
            };
            var myInt = SqlSingleOrDefaultSelector.Create(this, ConfigurationManager.AppSettings[AppSettingKeys.AmtConnectionString]).SelectSingleOrDefault();
        }

        [TestMethod]
        public void TestMapDataRow()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestSqlQueryOfNullableInt));
            logger.Info($"Start {nameof(TestMapDataRow)}");

            Query = "--";
            var myInt = SqlSingleOrDefaultSelector.Create(this, ConfigurationManager.AppSettings[AppSettingKeys.AmtConnectionString]).SelectSingleOrDefault();
        }

        [TestMethod]
        public void TestGetSingleOrDefault()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestSqlQueryOfNullableInt));
            logger.Info($"Start {nameof(TestGetSingleOrDefault)}");

            Query = "--";
            var myInt = SqlSingleOrDefaultSelector.Create(this, ConfigurationManager.AppSettings[AppSettingKeys.AmtConnectionString]).SelectSingleOrDefault();
            Assert.AreEqual(myInt, default(int?));

            try
            {
                myInt = SqlSingleOrDefaultSelector.Create(this, ConfigurationManager.AppSettings[AppSettingKeys.AmtConnectionString]).SelectSingleOrDefault();
            }
            catch(Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(EntityCountMismatchException));
                Assert.AreEqual((exception as EntityCountMismatchException).EntityType, typeof(int?));
            }

            Query = $@"
select {myIntValue} as {myIntColumn}
";
            myInt = SqlSingleOrDefaultSelector.Create(this, ConfigurationManager.AppSettings[AppSettingKeys.AmtConnectionString]).SelectSingleOrDefault();
            Assert.AreEqual(myInt, myIntValue);
        }

        [TestMethod]
        public void TestGetMultiple()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestSqlQueryOfNullableInt));
            logger.Info($"Start {nameof(TestGetMultiple)}");

            Query = "--";
            var myInts = SqlMultipleSelector.Create(this, ConfigurationManager.AppSettings[AppSettingKeys.AmtConnectionString]).SelectMultiple();
            Assert.IsNotNull(myInts);
            Assert.IsInstanceOfType(myInts, typeof(int?[]));
            Assert.AreEqual(myInts.Length, 0);

            const int myFirstInt = 13;
            const int mySecondInt = 37;
            Query = $@"
declare @myIntTable table
(
	MyIntValue int
)

insert into @myIntTable (MyIntValue) values ({myFirstInt})
insert into @myIntTable (MyIntValue) values ({mySecondInt})

select myIntTable.MyIntValue as [{myIntColumn}] from @myIntTable as myIntTable
";

            myInts = SqlMultipleSelector.Create(this, ConfigurationManager.AppSettings[AppSettingKeys.AmtConnectionString]).SelectMultiple();
            Assert.IsNotNull(myInts);
            Assert.IsInstanceOfType(myInts, typeof(int?[]));
            Assert.AreEqual(myInts.Length, 2);
            Assert.AreEqual(myInts[0], myFirstInt);
            Assert.AreEqual(myInts[1], mySecondInt);
        }
    }
}
