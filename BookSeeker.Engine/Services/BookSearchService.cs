using BookSeeker.Providers.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSeeker.Engine.Services
{
    public class BookSearchService : IBookSearchService
    {
        private readonly IEnumerable<IBookDataProvider> _bookDataProviders;

        public BookSearchService(IEnumerable<IBookDataProvider> bookDataProviders)
        {
            _bookDataProviders = bookDataProviders;
        }

        public async void SearchByTitleAsync(string title)
        {
            var tasks = _bookDataProviders.Select(x => x.SearchByTitleAsync(title));

            var searchResults = await Task.WhenAll(tasks);
            var searchItems = searchResults.SelectMany(x => x.Items).GroupBy(item => item.Isbn);
        }
    }
}