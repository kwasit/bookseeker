using System.Collections.Generic;

namespace BookSeeker.Providers.Common.Models
{
    public class ProviderBookSearchItem
    {
        public string Provider { get; set; }
        public string Title { get; set; }
        public IsbnData Isbn { get; set; }
        public IEnumerable<string> Authors { get; set; }
    }
}