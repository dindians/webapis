namespace com.abnamro.biz
{
    internal class ServiceCompanyKey
    {
        internal int ServiceCompanyId { get; }

        private ServiceCompanyKey(int serviceCompanyId)
        {
            ServiceCompanyId = serviceCompanyId;
        }

        internal static ServiceCompanyKey Create(int serviceCompanyId) => new ServiceCompanyKey(serviceCompanyId);
    }
}
