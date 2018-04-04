namespace com.abnamro.biz
{
    internal class GroupAvailabilityDataFromAmt
    {
        internal bool AgreedByAcf { get; }
        internal PendingPayment[] AggregatedPendingPayments { get; }

        /// <summary>
        /// Note:
        /// For NewtonSoft Json deserialization to work, it is necessary that the constructor parameter-name match the corresponding property-name.
        /// In the matching, NewtonSoft Json ignores the casing.
        /// </summary>
        /// <param name="agreedByAcf"></param>
        /// <param name="aggregatedPendingPayments"></param>
        internal GroupAvailabilityDataFromAmt(bool agreedByAcf, PendingPayment[] aggregatedPendingPayments)
        {
            AgreedByAcf = agreedByAcf;
            AggregatedPendingPayments = aggregatedPendingPayments;
        }
    }
}
