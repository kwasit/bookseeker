using System.Collections.Generic;

namespace BookSeeker.Engine.Models
{
    public class BookSearchItem
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public IEnumerable<string> Authors { get; set; }
        public IEnumerable<string> Providers { get; set; }
    }
}