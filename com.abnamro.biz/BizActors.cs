using com.abnamro.agents;
using com.abnamro.biz.Actors;
using com.abnamro.biz.PasswordHashing;

namespace com.abnamro.biz
{
    public static class BizActors
    {
        public static IAccountAvailabilityAgent CreateClientAccountAvailabilitySelector(string aquariusConnectionstring) => new ClientAccountAvailabilitySelector(aquariusConnectionstring);
        public static IClientAccountsOverviewSelector CreateClientAccountsOverviewSelector(string aquariusConnectionstring) => new ClientAccountsOverviewSelector(aquariusConnectionstring);
        public static ICompaniesOverviewSelector CreateCompaniesOverviewSelector(string aquariusConnectionstring) => new CompaniesOverviewSelector(aquariusConnectionstring);
        public static IDeviceRegistrationStatusSelector CreateDeviceRegistrationStatusSelector(string amtConnectionstring) => new DeviceRegistrationStatusSelector(amtConnectionstring);
        public static IUserEmailaddressSelector CreateUserEmailaddressSelector(string amtConnectionstring) => new UserEmailaddressSelector(amtConnectionstring);
        public static IRegistrationcodeSelector CreateRegistrationcodeSelector(string amtConnectionstring) => new RegistrationcodeSelector(amtConnectionstring);
        public static IDeviceDeregistrator CreateDeviceDeregistrator(string amtConnectionstring) => new DeviceDeregistrator(amtConnectionstring);
        public static IDeviceRegistrator CreateDeviceRegistrator(string amtConnectionstring) => new DeviceRegistrator(amtConnectionstring);
        public static IDeviceRegistrationRequestor CreateDeviceRegistrationRequestor(string amtConnectionstring, IMailClientProvider mailClientProvider) => new DeviceRegistrationRequestor(amtConnectionstring, mailClientProvider);
        public static IUserAuthenticator CreateUserAuthenticator(string amtConnectionstring) => new UserAuthenticator(amtConnectionstring);
        public static IDeviceAuthenticator CreateDeviceAuthenticator(string amtConnectionstring, byte maxLogonAttemptsAllowed) => new DeviceAuthenticator(amtConnectionstring, maxLogonAttemptsAllowed);
        public static IDashboardService CreateDashboardService(string aquariusConnectionstring, string amtConnectionstring) => new DashboardSelector(aquariusConnectionstring, amtConnectionstring);
        public static IAggregatedGroupAvailabilityAgent CreateAggregatedGroupAvailabilitySelector(string aquariusConnectionstring, string amtConnectionstring) => new AggregatedGroupAvailabilitySelector(aquariusConnectionstring, amtConnectionstring);
        internal static IUserHashedPasswordSelector CreateUserHashedPasswordSelector(string amtConnectionstring) => new UserHashedPasswordSelector(amtConnectionstring);
        internal static IDeviceUserSelector CreateDeviceUserSelector(string amtConnectionstring) => new DeviceUserSelector(amtConnectionstring);
        internal static IDeviceLogonAgent CreateDeviceLogonAgent(string amtConnectionstring, byte maxLogonAttemptsAllowed) => new DeviceLogonAgent(amtConnectionstring, PasswordChecker.CreateProvider(), maxLogonAttemptsAllowed);
        internal static IGroupAvailabilitySelector CreateGroupAvailabilitySelector(string aquariusConnectionstring) => new GroupAvailabilitySelector(aquariusConnectionstring);
        internal static ICurrencyConversionRatesSelector CreateCurrencyConversionRatesSelector(string aquariusConnectionstring) => new CurrencyConversionRatesSelector(aquariusConnectionstring);
        internal static IPendingPaymentsSelector CreatePendingPaymentsSelector(string amtConnectionstring) => new PendingPaymentsSelector(amtConnectionstring);
        internal static IGroupAvailabilityApprovedSelector CreateGroupAvailabilityApprovedSelector(string amtConnectionstring) => new GroupAvailabilityApprovedSelector(amtConnectionstring);
    }
}
