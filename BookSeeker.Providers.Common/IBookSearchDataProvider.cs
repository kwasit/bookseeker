using BookSeeker.Providers.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSeeker.Providers.Common
{
    public interface IBookSearchDataProvider : IBookDataProvider
    {
        Task<IEnumerable<ProviderBookSearchItem>> SearchByTitleAsync(string title);
    }
}