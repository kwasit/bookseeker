namespace BookSeeker.Engine.Services
{
    public interface IBookSearchService
    {
        void SearchByTitleAsync(string title);
    }
}