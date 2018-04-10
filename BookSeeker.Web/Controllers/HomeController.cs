using AutoMapper;
using BookSeeker.Engine.Services;
using BookSeeker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BookSeeker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBookSearchService _bookSearchService;

        public HomeController(IBookSearchService bookSearchService, IMapper mapper)
        {
            _bookSearchService = bookSearchService;
            _mapper = mapper;
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
                return RedirectToAction("Index");
            }

            var searchResult = await _bookSearchService.SearchByTitleAsync(text);

            if (!searchResult.Succeeded)
            {
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }

            var searchResult = await _bookSearchService.SearchBookOffersAsync(isbn);


        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
