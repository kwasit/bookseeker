using BookSeeker.Engine.Services;
using BookSeeker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookSeeker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookSearchService _bookSearchService;

        public HomeController(IBookSearchService bookSearchService)
        {
            _bookSearchService = bookSearchService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SearchBook()
        {
            _bookSearchService.SearchByTitleAsync("mvc");

            return RedirectToAction("Index");
        }
    }
}
