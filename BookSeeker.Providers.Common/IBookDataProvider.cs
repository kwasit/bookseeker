using BookSeeker.Providers.Common.Models;
using System.Threading.Tasks;

namespace BookSeeker.Providers.Common
{
    public interface IBookDataProvider
    {
        Task<SearchResult> SearchByTitleAsync(string title);
        Task<SearchOffersResult> SearchOffersByIsbnAsync(string isbn);
    }
}