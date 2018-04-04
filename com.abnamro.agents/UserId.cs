using Newtonsoft.Json;
using System;

namespace com.abnamro.agents
{
    public class UserId
    {
        public int Value { get; }

        [JsonConstructor]
        private UserId(int value)
        {
            if (value < 1) throw new ArgumentException($"Value must be positive. Actual value is {value}.", nameof(value));

            Value = value;
        }

        public static UserId Create(int value) => new UserId(value);
    }
}
