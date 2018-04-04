using System;

namespace com.abnamro.agents
{
    public class ClientAccountKey
    {
        public long ClientAccountId { get; }

        public ClientAccountKey(long clientAccountId)
        {
            if (clientAccountId <= 0) throw new ArgumentException("Value can not be zero or negative.", nameof(clientAccountId));

            ClientAccountId = clientAccountId;
        }
        public static ClientAccountKey FromLong(long clientAccountId) => new ClientAccountKey(clientAccountId);
    }
}
