namespace com.abnamro.biz
{
    public class GroupNumberKeyForAmt
    {
        public int AmtServiceProviderId { get; }
        public int GroupNumber { get; }

        [JsonConstructor]
        private GroupNumberKeyForAmt(int amtServiceProviderId, int groupNumber)
        {
            AmtServiceProviderId = amtServiceProviderId;
            GroupNumber = groupNumber;
        }

        public static GroupNumberKeyForAmt Create(int amtServiceProviderId, int groupNumber) => new GroupNumberKeyForAmt(amtServiceProviderId, groupNumber);
    }
}
