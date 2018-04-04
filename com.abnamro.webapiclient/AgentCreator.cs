using com.abnamro.agents;
using com.abnamro.webapiclient.Clients;
using com.abnamro.webapiclient.Tracing;

namespace com.abnamro.webapiclient
{
    public static class AgentCreator
    {
        public static IAuthenticationAgent CreateAuthenticationAgent(IWebapiConnectionInfoProvider webapiConnectionProvider, string uriString, IWebapiclientTracer tracer = null) => new AuthenticationWebapiClient(webapiConnectionProvider, uriString, tracer:tracer);

        public static IDeviceIdentificationAgent CreateDeviceIdentificationAgent(IWebapiConnectionInfoProvider webapiConnectionProvider, string uriString, IWebapiclientTracer tracer = null) => new DeviceIdentificationWebapiClient(webapiConnectionProvider, uriString, tracer:tracer);

        public static IEchoAgent CreateEchoAgent(IWebapiConnectionInfoProvider webapiConnectionProvider, string uriString, IWebapiclientTracer tracer = null) => new EchoWebapiClient(webapiConnectionProvider, uriString, tracer:tracer);

        public static IAggregatedGroupAvailabilityAgent CreateAggregatedGroupAvailabilityAgent(IWebapiConnectionInfoProvider webapiConnectionProvider, string uriString, IWebapiclientTracer tracer = null) => new AggregatedGroupAvailabilityWebapiClient(webapiConnectionProvider, uriString, tracer: tracer);

        public static IClientAccountAvailabilityAgent CreateClientAccountAvailabilityAgent(IWebapiConnectionInfoProvider webapiConnectionProvider, string uriString, IWebapiclientTracer tracer = null) => new ClientAccountAvailabilityWebapiClient(webapiConnectionProvider, uriString, tracer: tracer);

        public static IClientAccountsAvailabilitySummaryAgent CreateClientAccountsAvailabilitySummaryAgent(IWebapiConnectionInfoProvider webapiConnectionProvider, string uriString, IWebapiclientTracer tracer = null) => new ClientAccountsAvailabilitySummaryWebapiClient(webapiConnectionProvider, uriString, tracer: tracer);

        public static IGroupAvailabilityAgent CreateGroupAvailabilityAgent(IWebapiConnectionInfoProvider webapiConnectionProvider, string uriString, IWebapiclientTracer tracer = null) => new GroupAvailabilityWebapiClient(webapiConnectionProvider, uriString, tracer: tracer);

        public static IUserAgent CreateUserAgent(IWebapiConnectionInfoProvider webapiConnectionProvider, string uriString, IWebapiclientTracer tracer = null) => new UserWebapiClient(webapiConnectionProvider, uriString, tracer:tracer);
    }
}
