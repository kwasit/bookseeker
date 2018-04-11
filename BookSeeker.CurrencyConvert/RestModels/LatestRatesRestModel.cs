using System;

namespace BookSeeker.CurrencyConvert.RestModels
{
    public class LatestRatesRestModel
    {
        public bool Success { get; set; }
        public long Timestamp { get; set; }
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public object Rates { get; set; }
    }
}