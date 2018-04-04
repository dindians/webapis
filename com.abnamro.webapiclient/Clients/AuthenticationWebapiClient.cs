using System;
using com.abnamro.agents;
using com.abnamro.webapiclient.Tracing;

namespace com.abnamro.webapiclient.Clients
{
    internal class AuthenticationWebapiClient : WebapiClient, IAuthenticationAgent
    {
        internal AuthenticationWebapiClient(IWebapiConnectionInfoProvider webapiConnectionProvider, string uriString, BearerToken bearerToken = null, IWebapiclientTracer tracer = null) : base(webapiConnectionProvider, uriString, bearerToken, tracer) {}

        DeviceAuthenticationData IAuthenticationAgent.AuthenticateDevice(DeviceAuthenticationKey deviceAuthenticationKey) => PostDeviceAuthenticationData(deviceAuthenticationKey);

        private DeviceAuthenticationData PostDeviceAuthenticationData(DeviceAuthenticationKey deviceAuthenticationKey)
        {
            if (deviceAuthenticationKey == default(DeviceAuthenticationKey)) throw new ArgumentNullException(nameof(deviceAuthenticationKey));
            if (string.IsNullOrWhiteSpace(deviceAuthenticationKey.DeviceId)) throw new ArgumentException($"Value-of-property {nameof(deviceAuthenticationKey.DeviceId)} is null-or-whitespace.", nameof(deviceAuthenticationKey));
            if (string.IsNullOrWhiteSpace(deviceAuthenticationKey.PincodeHash)) throw new ArgumentException($"Value-of-property  {nameof(deviceAuthenticationKey.PincodeHash)} is null-or-whitespace.", nameof(deviceAuthenticationKey));

            Tracer?.TraceInfo($"{nameof(PostDeviceAuthenticationData)} {nameof(DeviceAuthenticationKey)}.{nameof(deviceAuthenticationKey.DeviceId)} '{deviceAuthenticationKey.DeviceId}'.");
            var bearerToken = GetBearerToken(deviceAuthenticationKey.DeviceId, deviceAuthenticationKey.PincodeHash);
            Tracer?.TraceInfo($"response: {bearerToken}.");
            return (bearerToken?.HasValue??false)? DeviceAuthenticationData.Create(bearerToken.Value): default(DeviceAuthenticationData);
        }
    }
}
