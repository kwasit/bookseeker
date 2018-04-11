namespace BookSeeker.Engine.Models
{
    public class Money
    {
        public decimal Amount { get; }
        public string CurrencyCode { get; }

        public Money(string currencyCode, decimal amount)
        {
            CurrencyCode = currencyCode;
            Amount = amount;
        }
    }
}