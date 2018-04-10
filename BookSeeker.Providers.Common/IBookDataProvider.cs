using System.Collections.Generic;
using BookSeeker.Providers.Common.Models;
using System.Threading.Tasks;

namespace BookSeeker.Providers.Common
{
    public interface IBookDataProvider
    {
        string Name { get; }
        Task<IEnumerable<ProviderBookSearchItem>> SearchByTitleAsync(string title);
        Task<ProviderBookOffer> SearchOffersByIsbnAsync(string isbn);
    }
}