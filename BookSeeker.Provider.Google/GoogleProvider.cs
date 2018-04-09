using BookSeeker.Providers.Common;
using BookSeeker.Providers.Common.Models;
using Google.Apis.Books.v1;
using System.Threading.Tasks;

namespace BookSeeker.Provider.Google
{
    public class GoogleProvider : IBookDataProvider
    {
        private readonly BooksService _booksService;

        public GoogleProvider(BooksService booksService)
        {
            _booksService = booksService;
        }

        public async Task<SearchResult> SearchByTitleAsync(string title)
        {
            var request = _booksService.Volumes.List(title);

            var volumes = await request.ExecuteAsync();
            return new SearchResult();
        }

        public Task<SearchOffersResult> SearchOffersByIsbnAsync(string isbn)
        {
            throw new System.NotImplementedException();
        }
    }
}

