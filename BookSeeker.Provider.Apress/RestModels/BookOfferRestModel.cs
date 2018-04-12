namespace BookSeeker.Provider.Apress.RestModels
{
    public class BookOfferRestModel
    {
        public class PriceRestModel
        {
            public decimal BestPrice { get; set; }
            public decimal ListPrice { get; set; }
            public string BestPriceFmt { get; set; }
            public string ListPriceFmt { get; set; }
        }

        public string Id { get; set; }
        public PriceRestModel Price { get; set; }
        public bool ShowFrom { get; set; }
        public string Type { get; set; }
    }
}