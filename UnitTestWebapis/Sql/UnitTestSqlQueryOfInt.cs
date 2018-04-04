using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NLog;
using System.Configuration;
using com.abnamro.datastore;
using com.abnamro.datastore.Sql;

namespace UnitTestWebapis.Sql
{
    [TestClass]
    public class UnitTestSqlQueryOfInt : IDataQuery<IDataRow, int>, IDataMapper<IDataRow, int>
    {
        const string myIntColumn = nameof(myIntColumn);

        public string Query { get; private set; }

        public IDictionary<string, object> QueryParameters { get; private set; }

        IDataMapper<IDataRow, int> IDataQuery<IDataRow, int>.DataMapper => this;

        public int MapData(IDataRow dataRow) => dataRow.GetInt(myIntColumn);

        [TestMethod]
        public void TestGetNullableInteger()
        {
            var logger = LogManager.GetLogger(nameof(UnitTestSqlQueryOfInt));
            logger.Info($"Start {nameof(TestGetNullableInteger)}");
            var expectedIntegerValue = 13;
            Query = $@"select {expectedIntegerValue} as {myIntColumn}";
            Assert.AreEqual(expectedIntegerValue, SqlSingleSelector.Create(this, ConfigurationManager.AppSettings[AppSettingKeys.AmtConnectionString]).SelectSingle());
        }
    }
}
