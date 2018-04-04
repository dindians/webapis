using System;
using System.Collections.Generic;

namespace com.abnamro.clientapp.webapiclient
{
    internal class Webapi
    {
        private readonly IWebapiConnectionInfo _webapiConnectionInfo;


        internal Webapi(IWebapiContext webapiContext)
        {
            if (webapiContext == default(IWebapiContext)) throw new ArgumentNullException(nameof(webapiContext));

            _webapiConnectionInfo = webapiContext.GetWebapiConnectionInfo();
            if (_webapiConnectionInfo == default(IWebapiConnectionInfo)) throw new ArgumentNullException(nameof(_webapiConnectionInfo));
            if (string.IsNullOrWhiteSpace(_webapiConnectionInfo.HostName)) throw new ArgumentException($"Value-of-property {nameof(_webapiConnectionInfo.HostName)} is null-or-whitespace.", nameof(_webapiConnectionInfo));
            if (!(new HashSet<string>(new[] { "http", "https" })).Contains(_webapiConnectionInfo.Scheme)) throw new ArgumentException($"Invalid-value-of-property {nameof(_webapiConnectionInfo.Scheme)}: '{_webapiConnectionInfo.Scheme}'.", nameof(_webapiConnectionInfo));
            if (_webapiConnectionInfo.Port < 80) throw new ArgumentException($"Invalid-value-of-property {nameof(_webapiConnectionInfo.Port)}: '{_webapiConnectionInfo.Port}'.", nameof(_webapiConnectionInfo));
        }

        /// <summary>
        /// <para>Composes the Uniform Resource Identifier (URI) for the ApiName instance.</para>
        /// <para>See alo RFC 3986 (https://tools.ietf.org/html/rfc398)</para>
        /// </summary>
        /// 
        /// The following are two example URIs and their component parts:
        ///    foo://example.com:8042/over/there?name=ferret#nos
        ///    \_/   \______________/\_________/ \_________/ \__/
        ///     |           |            |            |        |
        ///  scheme     authority       path        query   fragment
        ///     |   _____________________|__
        ///    / \ /                        \
        ///    urn:example:animal:ferret:nose
        /// <returns></returns>
        internal Uri ComposeResourceUri(string resourcePath, bool usePathPrefix = true)
        {
            return new Uri(new Uri($"{_webapiConnectionInfo.Scheme}://{_webapiConnectionInfo.HostName}:{_webapiConnectionInfo.Port}"), ComposeRelativeUri(usePathPrefix ? _webapiConnectionInfo.ResourcePathPrefix : default(string), resourcePath));
        }

        internal Uri ComposeResourceUri(string resourcePath, string resourceMethod)
        {
            return ComposeResourceUri(ComposeRelativeUri(resourcePath, resourceMethod));
        }

        private static string ComposeRelativeUri(string firstPart, string secondPart = default(string))
        {
            return string.Concat(firstPart, (!string.IsNullOrWhiteSpace(firstPart)) && (!string.IsNullOrWhiteSpace(secondPart)) ? "/" : string.Empty, secondPart)?.ToLower();
        }
    }
}
