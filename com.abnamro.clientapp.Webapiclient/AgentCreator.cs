using com.abnamro.agents;
using com.abnamro.clientapp.webapiclient.Clients;

namespace com.abnamro.clientapp.webapiclient
{
    public static class AgentCreator
    {
        public static IAuthenticator CreateAuthenticator(IWebapiContext webapiContext, bool postJson) => new AuthenticatorWebapiClient(webapiContext, postJson);

         public static IDeviceCultureAgent CreateDeviceCultureAgent(IWebapiContext webapiContext) => new DeviceCultureWebapiClient(webapiContext);

        public static IEchoAgent CreateEchoAgent(IWebapiContext webapiContext) => new EchoWebapiClient(webapiContext);

        public static IAggregatedGroupAvailabilityAgent CreateAggregatedGroupAvailabilityAgent(IWebapiContext webapiContext) => new AggregatedGroupAvailabilityWebapiClient(webapiContext);

        public static IAccountAvailabilityAgent CreateAccountAvailabilityAgent(IWebapiContext webapiContext) => new AccountAvailabilityWebapiClient(webapiContext);

        public static IAccountsOverviewAgent CreateAccountsOverviewAgent(IWebapiContext webapiContext) => new AccountsOverviewWebapiClient(webapiContext);

        public static IEmailaddressValidation CreateEmailaddressValidator(IWebapiContext webapiContext) => new EmailaddressValidationWebapiClient(webapiContext);

        public static IDeviceRegistrationRequestor CreateDeviceRegistrationRequestor(IWebapiContext webapiContext) => new DeviceRegistrationRequestorWebapiClient(webapiContext);

        public static IDeviceRegistration CreateDeviceRegistrator(IWebapiContext webapiContext) => new DeviceRegistratorWebapiClient(webapiContext);

        public static IDeviceDeregistrator CreateDeviceDeregistrator(IWebapiContext webapiContext) => new DeviceDeregistratorWebapiClient(webapiContext);

        public static IDeviceRegistrationStatusSelector CreateDeviceRegistrationStatusSelector(IWebapiContext webapiContext) => new RegistrationStatusSelectorWebapiClient(webapiContext);

        public static IDashboardService CreateDashboardService(IWebapiContext webapiContext) => new DashboardWebapiClient(webapiContext);

        public static ICompaniesOverviewService CreateCompaniesOverviewService(IWebapiContext webapiContext) => new CompaniesOverviewWebapiClient(webapiContext);
    }
}
