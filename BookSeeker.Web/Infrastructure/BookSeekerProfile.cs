using AutoMapper;
using BookSeeker.Engine.Models;
using BookSeeker.Web.Models;

namespace BookSeeker.Web.Infrastructure
{
    public class BookSeekerProfile : Profile
    {
        public BookSeekerProfile()
        {
            CreateMap<BookSearchItem, SearchResultsViewModel.ResultItem>();
        }
    }
}