using Microsoft.AspNetCore.Mvc;
using PRN_Project.Models;
using PRN_Project.Services;
using System.Diagnostics;

namespace PRN_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AudioMarketContext _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = new AudioMarketContext();
        }

        public IActionResult Index()
        {
            ViewBag.mostLikedAudio = new HomeServices().findMostLiked();
            ViewBag.genreList = _context.Genres.ToList();
            ViewBag.moodList = _context.Moods.ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Test()
        {
            return View();
        }

    }
}