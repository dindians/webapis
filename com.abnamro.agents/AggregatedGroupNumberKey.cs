namespace com.abnamro.agents
{
    public class AggregatedGroupNumberKey
    {
        public int AmtServiceProviderId { get; }
        public int AquariusServiceCompanyId { get; }
        public int GroupNumber { get; }

        public AggregatedGroupNumberKey(int amtServiceProviderId, int aquariusServiceCompanyId, int groupNumber)
        {
            AmtServiceProviderId = amtServiceProviderId;
            AquariusServiceCompanyId = aquariusServiceCompanyId;
            GroupNumber = groupNumber;
        }
    }
}
