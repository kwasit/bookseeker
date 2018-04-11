using BookSeeker.Common;
using BookSeeker.Engine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSeeker.Engine.Services
{
    public interface IBookSearchService
    {
        Task<ServiceResult<IEnumerable<BookSearchItem>>> SearchByTitleAsync(string title);
        Task<ServiceResult<IEnumerable<BookOffer>>> SearchBookOffersAsync(string isbn);
    }
}