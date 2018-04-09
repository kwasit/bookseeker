using System.Collections.Generic;

namespace BookSeeker.Providers.Common.Models
{
    public class SearchOffersResult
    {
        public IEnumerable<SearchOffer> Offers { get; set; }
    }
}