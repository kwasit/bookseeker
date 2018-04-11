namespace BookSeeker.Engine.Models
{
    public class BookOffer
    {
        public string Isbn { get; set; }
        public string Title { get; set; }

        public string Url { get; set; }
        public string Provider { get; set; }

        public Money OriginalPrice { get; set; }
        public Money LocalPrice { get; set; }
    }
}