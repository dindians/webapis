namespace com.abnamro.agents
{
    public interface ICurrencyConverter
    {
        decimal ConvertAmount(decimal amount, string fromCurrency, string toCurrency);
    }
}
