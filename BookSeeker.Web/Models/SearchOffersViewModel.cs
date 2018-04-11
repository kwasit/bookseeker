using System.Collections.Generic;

namespace BookSeeker.Web.Models
{
    public class SearchOffersViewModel
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public IEnumerable<OfferItem> Offers { get; set; }

        public class OfferItem
        {
            public string PriceFormat { get; set; }
            public string LocalPriceFormat { get; set; }
            public string Url { get; set; }
            public string Provider { get; set; }
        }
    }
}