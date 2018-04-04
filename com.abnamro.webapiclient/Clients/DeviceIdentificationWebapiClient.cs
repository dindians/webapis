using System;
using com.abnamro.agents;
using com.abnamro.webapiclient.Tracing;

namespace com.abnamro.webapiclient.Clients
{
    internal class DeviceIdentificationWebapiClient : WebapiClient, IDeviceIdentificationAgent
    {
        internal DeviceIdentificationWebapiClient(IWebapiConnectionInfoProvider webapiConnectionProvider, string uriString, BearerToken bearerToken = null, IWebapiclientTracer tracer = null) : base(webapiConnectionProvider, uriString, bearerToken, tracer) {}

        DeviceIdentificationData IDeviceIdentificationAgent.IdentifyDevice(DeviceIdentificationKey deviceIdentificationKey) => PostDeviceIdentificationData(deviceIdentificationKey);

        private DeviceIdentificationData PostDeviceIdentificationData(DeviceIdentificationKey deviceIdentificationKey)
        {
            if (deviceIdentificationKey == default(DeviceIdentificationKey)) throw new ArgumentNullException(nameof(deviceIdentificationKey));
            if (string.IsNullOrWhiteSpace(deviceIdentificationKey.DeviceId)) throw new ArgumentException($"Value-of-property {nameof(deviceIdentificationKey.DeviceId)} is null-or-whitespace.", nameof(deviceIdentificationKey));
            if (string.IsNullOrWhiteSpace(deviceIdentificationKey.PincodeHash)) throw new ArgumentException($"Value-of-property  {nameof(deviceIdentificationKey.PincodeHash)} is null-or-whitespace.", nameof(deviceIdentificationKey));

            Tracer?.TraceInfo($"{nameof(PostDeviceIdentificationData)} {nameof(UserIdentificationKey)}.{nameof(deviceIdentificationKey.DeviceId)} '{deviceIdentificationKey.DeviceId}'.");
            var response = Post<DeviceIdentificationKey, DeviceIdentificationData>(deviceIdentificationKey);
            Tracer?.TraceInfo($"response: {response}.");
            return response;
        }
    }
}
