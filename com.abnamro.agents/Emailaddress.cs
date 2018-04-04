using System;

namespace com.abnamro.agents
{
    public class Emailaddress
    {
        public string Value { get; }

        public Emailaddress(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(value);

            Value = value;
        }
    }
}
