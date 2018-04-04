using com.abnamro.agents;
using com.abnamro.webapiclient.Tracing;
using System;

namespace com.abnamro.webapiclient.Clients
{
    internal class UserWebapiClient : WebapiClient, IUserAgent
    {
        internal UserWebapiClient(IWebapiConnectionInfoProvider webapiConnectionProvider, string uriString, BearerToken bearerToken = null, IWebapiclientTracer tracer = null) : base(webapiConnectionProvider, uriString, bearerToken, tracer) {}

        SimpleUserData IUserAgent.IdentifySimpleUser(UserIdentificationKey userIdentificationKey) => PostSimpleUserData(userIdentificationKey);

        private SimpleUserData GetSimpleUserData(UserIdentificationKey userIdentificationKey)
        {
            if (userIdentificationKey == default(UserIdentificationKey)) throw new ArgumentNullException(nameof(userIdentificationKey));

            Tracer?.TraceInfo($"{nameof(GetSimpleUserData)} {nameof(UserIdentificationKey)}.{nameof(userIdentificationKey.UserCode)} '{userIdentificationKey.UserCode}'.");
            var response = Get<UserIdentificationKey, SimpleUserData>(userIdentificationKey, UserIdentificationKeyToUri);
            Tracer?.TraceInfo($"response: {response}.");
            return response;
        }
        private SimpleUserData PostSimpleUserData(UserIdentificationKey userIdentificationKey)
        {
            if (userIdentificationKey == default(UserIdentificationKey)) throw new ArgumentNullException(nameof(userIdentificationKey));

            Tracer?.TraceInfo($"{nameof(PostSimpleUserData)} {nameof(UserIdentificationKey)}.{nameof(userIdentificationKey.UserCode)} '{userIdentificationKey.UserCode}'.");
            var response = Post<UserIdentificationKey, SimpleUserData>(userIdentificationKey);
            Tracer?.TraceInfo($"response: {response}.");
            return response;
        }

        private string UserIdentificationKeyToUri(UserIdentificationKey userIdentificationKey)
        {
            if (userIdentificationKey == default(UserIdentificationKey)) throw new ArgumentNullException(nameof(userIdentificationKey));

            return $"{UriString}/{userIdentificationKey.UserCode}";
        }
    }
}
