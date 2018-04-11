using AutoMapper;
using BookSeeker.Engine.Models;
using BookSeeker.Providers.Common.Models;

namespace BookSeeker.Engine
{
    public class EngineProfile : Profile
    {
        public EngineProfile()
        {
            CreateMap<ProviderBookOffer, BookOffer>()
                .ForMember(d => d.OriginalPrice, e => e.MapFrom(s => new Money(s.CurrencyCode, s.Price.Value)))
                .ForMember(d => d.LocalPrice, e => e.Ignore());
        }
    }
}