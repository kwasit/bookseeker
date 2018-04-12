using BookSeeker.Providers.Common.Models;
using System.Threading.Tasks;

namespace BookSeeker.Providers.Common
{
    public interface IBookOffersDataProvider : IBookDataProvider
    {
        Task<ProviderBookOffer> SearchOffersByIsbnAsync(string isbn);
    }
}