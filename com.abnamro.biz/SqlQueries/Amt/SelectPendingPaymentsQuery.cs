using com.abnamro.datastore;
using System;
using System.Collections.Generic;

namespace com.abnamro.biz.SqlQueries.Amt
{
    internal class SelectPendingPaymentsQuery : IDataQuery<IDataRow, PendingPayment>, IDataMapper<IDataRow, PendingPayment>
    {
        private enum InputParameterName
        {
             GroupNumber
           , ServiceProviderId
        }
        private enum OutputColumnName
        {
             CurrencyCode
           , PendingPayment
        }

        string IDataQuery<IDataRow, PendingPayment>.Query => $@"
/*
PaymentRequest-Status values:
    Error = -1
    NewRequest = 0
    Planned = 1
    Queued = 2
    Reviewed = 3
    Approved = 4
    Ongoing = 5
    NotAccepted = 6
    Accepted = 7
    Rejected = 8
    Archived = 9
    Retry = 10
    NotProcessed = 11
    Processed = 12

a payment request is pending if the status is one of the following: 
    Error = -1
    Planned = 1
    Queued = 2
    Reviewed = 3
    Approved = 4
    Ongoing = 5
    Accepted = 7
*/

--declare @serviceProviderId int = 99
--declare @groupNumber int = 6333

select
    CurrencyIsoCode                               as [{nameof(OutputColumnName.CurrencyCode)}]
   ,cast(round(isNull(sum(Amount),0),2) as money) as [{nameof(OutputColumnName.PendingPayment)}]
from PaymentRequests
where 
    ServiceProviderId = @{nameof(InputParameterName.ServiceProviderId)}
and ClientGroupNumber = @{nameof(InputParameterName.GroupNumber)}
and [Status]         in (-1, 1, 2 ,3, 4, 5, 7)
group by CurrencyIsoCode
";

        IDataMapper<IDataRow, PendingPayment> IDataQuery<IDataRow, PendingPayment>.DataMapper => this;

        public IDictionary<string, object> QueryParameters { get; private set; }

        internal SelectPendingPaymentsQuery(GroupNumberKeyForAmt groupNumberKeyForAmt)
        {
            if (groupNumberKeyForAmt == default(GroupNumberKeyForAmt)) throw new ArgumentNullException(nameof(groupNumberKeyForAmt));

            QueryParameters = new Dictionary<string, object>
            {
                [nameof(InputParameterName.GroupNumber)] = groupNumberKeyForAmt.GroupNumber
              , [nameof(InputParameterName.ServiceProviderId)] = groupNumberKeyForAmt.AmtServiceProviderId
            };
        }

        PendingPayment IDataMapper<IDataRow, PendingPayment>.MapData(IDataRow dataRow)
        {
            if (dataRow == default(IDataRow)) throw new ArgumentNullException(nameof(dataRow));

            return new PendingPayment(dataRow.GetString(nameof(OutputColumnName.CurrencyCode))
                                     ,dataRow.GetDecimal(nameof(OutputColumnName.PendingPayment)));
        }
    }
}
