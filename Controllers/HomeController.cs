using Microsoft.AspNetCore.Mvc;
using PRN_Project.Models;
using PRN_Project.Services;
using System.Diagnostics;

namespace PRN_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.mostLikedAudio = new HomeServices().findMostLiked();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AudioList()
        {
            AudioMarketContext audioMarketContext = new AudioMarketContext();

            ViewBag.GenreList = audioMarketContext.Genres.ToList();
            ViewBag.MoodList = audioMarketContext.Moods.ToList();

            return View(new AudioMarketContext().Audios.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}