namespace com.abnamro.core
{
    public enum WebapiRoute
    {
        /// <summary>
        /// aggregated group availability
        /// </summary>
        aga,
        /// <summary>
        /// aggregated group availability - asynchronous
        /// </summary>
        agaasync,
        /// <summary>
        /// authorized echo
        /// </summary>
        authecho,
        authechoasync,
        /// <summary>
        /// authenticate user
        /// </summary>
        authuser,
        /// <summary>
        /// authenticate user - asynchronous
        /// </summary>
        authuserasync,
        /// <summary>
        /// authenticate device
        /// </summary>
        authdevice,
        /// <summary>
        /// authenticate device - asynchronous
        /// </summary>
        authdeviceasync,
        /// <summary>
        /// account availability
        /// </summary>
        aa,
        /// <summary>
        /// client account availability - asynchronous
        /// </summary>
        aaasync,
        /// <summary>
        /// accounts overview: accounts availability summary
        /// </summary>
        ao,
        /// <summary>
        /// accounts overview: accounts availability summary - asynchronous
        /// </summary>
        aoasync,
        apioverview,
        apioverviewasync,
        claims,
        claimsasync,
        /// <summary>
        /// companies overview
        /// </summary>
        co,
        /// <summary>
        /// companies overview - asynchronous
        /// </summary>
        coasync,
        /// <summary>
        /// currency conversion rates
        /// </summary>
        ccr,
        /// <summary>
        /// currency conversion rates - asynchronous
        /// </summary>
        ccrasync,
        /// <summary>
        /// dashboard
        /// </summary>
        dashboard,
        /// <summary>
        /// dashboard - asynchronous
        /// </summary>
        dashboardasync,
        /// <summary>
        /// device culture
        /// </summary>
        devcult,
        /// <summary>
        /// device culture - asynchronous
        /// </summary>
        devcultasync,
        /// <summary>
        /// device deregistration
        /// </summary>
        devdereg,
        /// <summary>
        /// device deregistration - asynchronous
        /// </summary>
        devderegasync,
        /// <summary>
        /// determine registration status
        /// </summary>
        detregstat,
        /// <summary>
        /// determine registration status - asynchronous
        /// </summary>
        detregstatasync,
        /// <summary>
        /// echo webapi route
        /// </summary>
        echo,
        /// <summary>
        /// echo webapi route - asynchronous
        /// </summary>
        echoasync,
        /// <summary>
        /// internal authorized echo
        /// </summary>
        internalauthecho,
        internalauthechoasync,
        /// <summary>
        /// internal echo webapi route
        /// </summary>
        internalecho,
        internalechoasync,
        /// <summary>
        /// is emailaddress valid
        /// </summary>
        isemailadrval,
        /// <summary>
        /// is emailaddress valid - asynchronous
        /// </summary>
        isemailadrvalasync,
        /// <summary>
        /// device registration data
        /// </summary>
        devregdata,
        /// <summary>
        /// device registration data - asynchronous
        /// </summary>
        devregdataasync,
        /// <summary>
        /// register device
        /// </summary>
        regdev,
        /// <summary>
        /// register device - asynchronous
        /// </summary>
        regdevasync,
        /// <summary>
        /// request device registration
        /// </summary>
        reqdevreg,
        /// <summary>
        /// request device registration - asynchronous
        /// </summary>
        reqdevregasync,
        /// <summary>
        /// throw echo
        /// </summary>
        throwecho
    }
}
