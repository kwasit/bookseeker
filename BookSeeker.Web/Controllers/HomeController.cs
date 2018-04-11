using AutoMapper;
using BookSeeker.CurrencyConvert;
using BookSeeker.Engine.Services;
using BookSeeker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BookSeeker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBookSearchService _bookSearchService;
        private readonly ICurrencyConvertClient _currencyConvertClient;

        public HomeController(IBookSearchService bookSearchService, IMapper mapper, ICurrencyConvertClient currencyConvertClient)
        {
            _bookSearchService = bookSearchService;
            _mapper = mapper;
            _currencyConvertClient = currencyConvertClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Search");
        }

        [HttpGet]
        public async Task<IActionResult> SearchBook(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                ModelState.AddModelError(string.Empty, "Search text is required.");
                return View("Search");
            }

            var searchResult = await _bookSearchService.SearchByTitleAsync(text);

            if (!searchResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong. Sorry...");
                return View("Search");
            }

            var resultsViewModel = new SearchResultsViewModel
            {
                SearchText = text,
                Items = _mapper.Map<IEnumerable<SearchResultsViewModel.ResultItem>>(searchResult.Data)
            };

            return View("SearchResults", resultsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SearchBookOffers(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
            {
                ModelState.AddModelError(string.Empty, "Isbn is required.");
                return View("Search");
            }

            var searchResult = await _bookSearchService.SearchBookOffersAsync(isbn);

            if (!searchResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong. Sorry...");
                return View("Search");
            }

            var offersViewModel = new SearchOffersViewModel
            {
                Isbn = isbn,
                Title = searchResult.Data.FirstOrDefault()?.Title,
                Offers = _mapper.Map<IEnumerable<SearchOffersViewModel.OfferItem>>(searchResult.Data)
            };


            return View("SearchOffers", offersViewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
