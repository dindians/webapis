using com.abnamro.agents;
using com.abnamro.biz.SqlQueries.Amt;
using com.abnamro.biz.SqlQueries.Aquarius;
using com.abnamro.datastore;

namespace com.abnamro.biz
{
    internal static class SqlDataQueries
    {
        internal static IDataQuery<IDataRow, DeviceRegistrationStatus> CreateDeviceRegistrationStatusQuery(DeviceId deviceId) => new SelectDeviceRegistrationStatusQuery(deviceId);
        internal static IDataQuery<IDataRow, string> CreateUserEmailaddressQuery(UserId userId) => new SelectUserEmailaddressQuery(userId);
        internal static IDataQuery<IDataRow, UserHashedPassword> CreateUserHashedPasswordQuery(string userName) => new SelectUserHashedPasswordQuery(userName);
        internal static IDataQuery<IDataRow, DeviceUser> CreateDeviceUserQuery(UserDeviceId userDeviceId) => new SelectDeviceUserQuery(userDeviceId);
        internal static IDataQuery<IDataRow, string> CreateRegistrationcodeQuery(RegistrationcodeSelectorInput registrationcodeSelectorInput) => new SelectRegistrationcodeQuery(registrationcodeSelectorInput);
        internal static IDataQuery<IDataRow, ClientAccountOverview> CreateClientAccountsOverviewQuery(GroupNumberKeyForAquarius groupNumberKeyForAquarius) => new SelectClientAccountsOverviewQuery(groupNumberKeyForAquarius);
        internal static IDataQuery<IDataRow, InsertDeviceRegistrationRequestResponse> CreateInsertDeviceRegistrationRequestQuery(DeviceRegistrationRequestData deviceRegistrationRequestData) => new InsertDeviceRegistrationRequestQuery(deviceRegistrationRequestData);
        internal static IDataQuery<IDataRow, DeviceRegistrationResponse> CreateDeviceRegistrationQuery(DeviceRegistrationData deviceRegistrationData) => new InsertUserDeviceQuery(deviceRegistrationData);
        internal static IDataQuery<IDataRow, DeviceDeregistrationResponse> CreateDeregisterDeviceQuery(DeviceId deviceId) => new DeregisterDeviceQuery(deviceId);
        internal static IDataQuery<IDataRow, DeviceLogonInfo> CreateDeviceLogonInfoQuery(DeviceId deviceId) => new SelectDeviceLogonInfoQuery(deviceId);
        internal static IDataQuery<IDataRow, bool> CreateResetUserDeviceLogonAttemptsQuery(UserDeviceId userDeviceId) => new ResetUserDeviceLogonAttemptsQuery(userDeviceId);
        internal static IDataQuery<IDataRow, bool> CreateGroupAvailabilityApprovedQuery(GroupNumberKeyForAmt groupNumberKeyForAmt) => new SelectGroupAvailabilityApprovedQuery(groupNumberKeyForAmt);
    }
}
