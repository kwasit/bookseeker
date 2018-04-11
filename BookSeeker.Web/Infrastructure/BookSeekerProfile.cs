using AutoMapper;
using BookSeeker.Common.Extensions;
using BookSeeker.Engine.Extensions;
using BookSeeker.Engine.Models;
using BookSeeker.Web.Models;

namespace BookSeeker.Web.Infrastructure
{
    public class BookSeekerProfile : Profile
    {
        public BookSeekerProfile()
        {
            CreateMap<BookSearchItem, SearchResultsViewModel.ResultItem>();
            CreateMap<BookOffer, SearchOffersViewModel.OfferItem>()
                .ForMember(x => x.PriceFormat, e => e.MapFrom(s => s.OriginalPrice.FormatCurrency()))
                .ForMember(x => x.LocalPriceFormat, e => e.MapFrom(s => s.LocalPrice.FormatCurrency()));
        }
    }
}