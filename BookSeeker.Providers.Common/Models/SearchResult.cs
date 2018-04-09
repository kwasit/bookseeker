using System.Collections.Generic;

namespace BookSeeker.Providers.Common.Models
{
    public class SearchResult
    {
        public IEnumerable<SearchItem> Items { get; set; }
    }
}