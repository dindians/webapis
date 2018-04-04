using System;

namespace com.abnamro.agents
{
    public class DashboardRequest
    {
        public AggregatedGroupNumberKey AggregatedGroupNumberKey { get; }

        public DashboardRequest() {}

        public DashboardRequest(AggregatedGroupNumberKey aggregatedGroupNumberKey)
        {
            if (!(aggregatedGroupNumberKey is AggregatedGroupNumberKey)) throw new ArgumentNullException(nameof(aggregatedGroupNumberKey));

            AggregatedGroupNumberKey = aggregatedGroupNumberKey;
        }
    }
}
