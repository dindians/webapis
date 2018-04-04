using System.Threading.Tasks;

namespace com.abnamro.biz
{
    internal interface IPendingPaymentsSelector
    {
        PendingPayment[] SelectPendingPayments(GroupNumberKeyForAmt groupNumberKeyForAmt);
        Task<PendingPayment[]> SelectPendingPaymentsAsync(GroupNumberKeyForAmt groupNumberKeyForAmt);
    }
}
