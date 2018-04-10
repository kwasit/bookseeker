using System.Collections.Generic;

namespace BookSeeker.Web.Models
{
    public class SearchResultsViewModel
    {
        public string SearchText { get; set; }
        public IEnumerable<ResultItem> Items { get; set; }

        public class ResultItem
        {
            public string Isbn { get; set; }
            public string Title { get; set; }
            public IEnumerable<string> Authors { get; set; }
            public IEnumerable<string> Providers { get; set; }
        }
    }
}