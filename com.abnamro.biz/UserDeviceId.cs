using System;

namespace com.abnamro.biz
{
    internal class UserDeviceId
    {
        internal int Value { get; }

        [JsonConstructor]
        private UserDeviceId(int value)
        {
            if (value < 0) throw new ArgumentNullException(nameof(value), "value must be greater than zero.");

            Value = value;
        }

        internal static UserDeviceId Create(int value) => new UserDeviceId(value);
    }
}
