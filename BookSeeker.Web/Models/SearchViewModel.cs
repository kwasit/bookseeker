using System.ComponentModel.DataAnnotations;

namespace BookSeeker.Web.Models
{
    public class SearchViewModel
    {
        [Required]
        public string SearchText { get; set; }
    }
}