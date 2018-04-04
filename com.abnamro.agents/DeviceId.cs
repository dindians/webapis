using Newtonsoft.Json;
using System;

namespace com.abnamro.agents
{
    public class DeviceId
    {
        public string Value { get; }

        [JsonConstructor]
        private DeviceId(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        public static DeviceId Create(string value) => new DeviceId(value);
    }
}
