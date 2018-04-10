using AutoMapper;
using BookSeeker.Engine.Models;
using BookSeeker.Providers.Common.Models;

namespace BookSeeker.Engine
{
    public class EngineProfile : Profile
    {
        protected EngineProfile()
        {
            CreateMap<ProviderBookOffer, BookOffer>();
        }
    }
}