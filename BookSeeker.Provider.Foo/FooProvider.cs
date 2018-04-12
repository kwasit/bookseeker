using BookSeeker.Providers.Common;
using BookSeeker.Providers.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSeeker.Provider.Foo
{
    public class FooProvider : IOffersBookDataProvider, ISearchBookDataProvider
    {
        public string Name => "foo";

        public async Task<IEnumerable<ProviderBookSearchItem>> SearchByTitleAsync(string title)
        {
            return await Task.Run(() => new List<ProviderBookSearchItem>
            {
                new ProviderBookSearchItem
                {
                    Isbn = new IsbnData
                    {
                        Id13Digits = "9781430228875",
                        Id10Digits = "1430228873"
                    },
                    Title = "Foo book 1",
                    Provider = "Foo"
                }
            });
        }

        public async Task<ProviderBookOffer> SearchOffersByIsbnAsync(string isbn)
        {
            return await Task.Run(() => new ProviderBookOffer());
        }
    }
}