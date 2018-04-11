namespace BookSeeker.Providers.Common.Models
{
    public class ProviderBookOffer
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public string CurrencyCode { get; set; }
        public string Url { get; set; }
        public string Provider { get; set; }
    }
}