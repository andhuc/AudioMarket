using Microsoft.AspNetCore.Mvc;
using PRN_Project.Models;

namespace PRN_Project.Controllers
{
    public class AudioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            AudioMarketContext audioMarketContext = new AudioMarketContext();

            ViewBag.GenreList = audioMarketContext.Genres.ToList();
            ViewBag.MoodList = audioMarketContext.Moods.ToList();

            return View(new AudioMarketContext().Audios.ToList());
        }
    }
}
