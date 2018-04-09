using BookSeeker.Providers.Common;
using BookSeeker.Providers.Common.Models;
using Nager.AmazonProductAdvertising;
using Nager.AmazonProductAdvertising.Model;
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

        public async Task<SearchResult> SearchByTitleAsync(string title)
        {
            var response = await _amazonWrapper.SearchAsync(title, AmazonSearchIndex.Books, AmazonResponseGroup.Small);
            return new SearchResult();
        }

        public async Task<SearchOffersResult> SearchOffersByIsbnAsync(string isbn)
        {
            var response = await _amazonWrapper.SearchAsync(isbn, AmazonSearchIndex.Books, AmazonResponseGroup.Offers);
            return new SearchOffersResult();
        }
    }
}