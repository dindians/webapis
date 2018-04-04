using com.abnamro.agents;
using System;

namespace com.abnamro.biz
{
    internal static class AggregatedGroupNumberKeyExtensions
    {
        internal static GroupNumberKeyForAquarius ToGroupNumberKeyForAquarius(this AggregatedGroupNumberKey aggregatedGroupNumberKey)
        {
            if (aggregatedGroupNumberKey == default(AggregatedGroupNumberKey)) throw new ArgumentNullException(nameof(aggregatedGroupNumberKey));

            return GroupNumberKeyForAquarius.Create(aggregatedGroupNumberKey.AquariusServiceCompanyId, aggregatedGroupNumberKey.GroupNumber);
        }

        internal static GroupNumberKeyForAmt ToGroupNumberKeyForAmt(this AggregatedGroupNumberKey aggregatedGroupNumberKey)
        {
            if (aggregatedGroupNumberKey == default(AggregatedGroupNumberKey)) throw new ArgumentNullException(nameof(aggregatedGroupNumberKey));

            return GroupNumberKeyForAmt.Create(aggregatedGroupNumberKey.AmtServiceProviderId, aggregatedGroupNumberKey.GroupNumber);
        }

        internal static ServiceCompanyKey ToServiceCompanyKey(this AggregatedGroupNumberKey aggregatedGroupNumberKey)
        {
            if (aggregatedGroupNumberKey == default(AggregatedGroupNumberKey)) throw new ArgumentNullException(nameof(aggregatedGroupNumberKey));

            return ServiceCompanyKey.Create(aggregatedGroupNumberKey.AquariusServiceCompanyId);
        }
    }
}
