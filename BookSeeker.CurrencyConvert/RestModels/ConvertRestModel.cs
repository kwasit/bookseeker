using System;

namespace BookSeeker.CurrencyConvert.RestModels
{
    public class ConvertRestModel
    {
        public bool Success { get; set; }
        public ConvertQueryRestModel Query { get; set; }
        public DateTime Date { get; set; }
        public decimal Result { get; set; }
    }
}