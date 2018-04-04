using System;

namespace com.abnamro.agents
{
    public class CompaniesOverviewRequest
    {
        public GroupNumberKeyForAquarius GroupNumberKeyForAquarius { get; }

        public CompaniesOverviewRequest(GroupNumberKeyForAquarius groupNumberKeyForAquarius)
        {
            if (!(groupNumberKeyForAquarius is GroupNumberKeyForAquarius)) throw new ArgumentNullException(nameof(groupNumberKeyForAquarius));

            GroupNumberKeyForAquarius = groupNumberKeyForAquarius;
        }
    }
}
