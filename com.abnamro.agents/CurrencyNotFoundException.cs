using System;

namespace com.abnamro.agents
{
    public  class CurrencyNotFoundException : Exception
    {
        public string CurrencyCode { get; }

        internal CurrencyNotFoundException(string currencyCode, string message) : base(message)
        {
            CurrencyCode = currencyCode;
        }
    }
}