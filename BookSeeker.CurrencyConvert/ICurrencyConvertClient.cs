namespace BookSeeker.CurrencyConvert
{
    public interface ICurrencyConvertClient
    {
        decimal Convert(string baseCode, string targetCode, decimal amount);
    }
}