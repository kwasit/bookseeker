using BookSeeker.Providers.Common;
using BookSeeker.Providers.Common.Models;
using Nager.AmazonProductAdvertising;
using Nager.AmazonProductAdvertising.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSeeker.Provider.Amazon
{
    public class AmazonProvider : IBookDataProvider
    {
        private readonly IAmazonWrapper _amazonWrapper;

        public AmazonProvider(IAmazonWrapper amazonWrapper)
        {
            _amazonWrapper = amazonWrapper;
        }

        public string Name => "amazon";

        public async Task<IEnumerable<ProviderBookSearchItem>> SearchByTitleAsync(string title)
        {
            var response = await _amazonWrapper.SearchAsync(title, AmazonSearchIndex.Books, AmazonResponseGroup.Small);
            return new List<ProviderBookSearchItem>();
        }

        public async Task<ProviderBookOffer> SearchOffersByIsbnAsync(string isbn)
        {
            var response = await _amazonWrapper.SearchAsync(isbn, AmazonSearchIndex.Books, AmazonResponseGroup.Offers);
            return new ProviderBookOffer();
        }
    }
}