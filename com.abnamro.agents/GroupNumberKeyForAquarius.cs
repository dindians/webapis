using Newtonsoft.Json;

namespace com.abnamro.agents
{
    public class GroupNumberKeyForAquarius
    {
        public int AquariusServiceCompanyId { get; }
        public int GroupNumber { get; }

        [JsonConstructor]
        private GroupNumberKeyForAquarius(int aquariusServiceCompanyId, int groupNumber)
        {
            AquariusServiceCompanyId = aquariusServiceCompanyId;
            GroupNumber = groupNumber;
        }

        public static GroupNumberKeyForAquarius Create(int aquariusServiceCompanyId, int groupNumber) => new GroupNumberKeyForAquarius(aquariusServiceCompanyId, groupNumber);
    }
}
