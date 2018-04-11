namespace BookSeeker.CurrencyConvert.RestModels
{
    public class ConvertQueryRestModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public decimal Amount { get; set; }
    }
}