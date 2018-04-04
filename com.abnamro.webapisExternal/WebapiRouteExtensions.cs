using com.abnamro.core;

namespace com.abnamro.webapisExternal
{
    internal static class WebapiRouteExtensions
    {
        internal static bool IsRegistration(this WebapiRoute webapiRoute)
        {
            switch (webapiRoute)
            {
                case WebapiRoute.detregstat:
                case WebapiRoute.detregstatasync:
                case WebapiRoute.authuser:
                case WebapiRoute.authuserasync:
                case WebapiRoute.isemailadrval:
                case WebapiRoute.isemailadrvalasync:
                case WebapiRoute.reqdevreg:
                case WebapiRoute.reqdevregasync:
                case WebapiRoute.regdev:
                case WebapiRoute.regdevasync:
                case WebapiRoute.devdereg:
                case WebapiRoute.devderegasync:
                    return true;
                default:
                    return false;
            }
        }

        internal static string UriString(this WebapiRoute webapiRoute)
        {
            const string authenticate = nameof(authenticate);

            return webapiRoute.IsAuthentication() ? authenticate : webapiRoute.ToString();
        }

        private static bool IsAuthentication(this WebapiRoute webapiRoute)
        {
            switch (webapiRoute)
            {
                case WebapiRoute.authuser:
                case WebapiRoute.authuserasync:
                case WebapiRoute.authdevice:
                case WebapiRoute.authdeviceasync:
                    return true;
                default:
                    return false;
            }
        }
    }
}
