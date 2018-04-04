using com.abnamro.biz.SqlQueries.Amt;
using com.abnamro.datastore.Sql;
using System.Threading.Tasks;

namespace com.abnamro.biz.Actors
{
    internal class PendingPaymentsSelector : IPendingPaymentsSelector
    {
        private readonly string _amtConnectionstring;

        internal PendingPaymentsSelector(string amtConnectionstring)
        {
            _amtConnectionstring = amtConnectionstring;
        }

        PendingPayment[] IPendingPaymentsSelector.SelectPendingPayments(GroupNumberKeyForAmt groupNumberKeyForAmt) => SqlMultipleSelector.Create(new SelectPendingPaymentsQuery(groupNumberKeyForAmt), _amtConnectionstring).SelectMultiple();

        async Task<PendingPayment[]> IPendingPaymentsSelector.SelectPendingPaymentsAsync(GroupNumberKeyForAmt groupNumberKeyForAmt) => await SqlMultipleSelector.Create(new SelectPendingPaymentsQuery(groupNumberKeyForAmt), _amtConnectionstring).SelectMultipleAsync();
    }
}
