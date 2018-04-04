using com.abnamro.agents;
using com.abnamro.biz;
using System;

namespace com.abnamro.webapisInternal
{
    internal static class DeviceUserExtensions
    {
        internal static AggregatedGroupNumberKey GetAggregatedGroupNumberKey(this DeviceUser deviceUser)
        {
            if (!(deviceUser is DeviceUser)) throw new ArgumentNullException(nameof(deviceUser));

            return new AggregatedGroupNumberKey(deviceUser.AmtServiceProviderId, deviceUser.AquariusServiceCompanyId, deviceUser.GroupNumber);
        }

        internal static GroupNumberKeyForAquarius GetGroupNumberKeyForAquarius(this DeviceUser deviceUser)
        {
            if (!(deviceUser is DeviceUser)) throw new ArgumentNullException(nameof(deviceUser));

            return GroupNumberKeyForAquarius.Create(deviceUser.AquariusServiceCompanyId, deviceUser.GroupNumber);
        }

        internal static GroupNumberKeyForAmt GetGroupNumberKeyForAmt(this DeviceUser deviceUser)
        {
            if (!(deviceUser is DeviceUser)) throw new ArgumentNullException(nameof(deviceUser));

            return GroupNumberKeyForAmt.Create(deviceUser.AmtServiceProviderId, deviceUser.GroupNumber);
        }
    }
}
