using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN_Project.Models;

namespace PRN_Project.Controllers
{
    public class AudioController : Controller
    {

        private readonly AudioMarketContext _context;

        public AudioController(AudioMarketContext context)
        {
            _context = context;
        }
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

        public IActionResult Favor(int userId, int audioId)
        {
            DbSet<Favorite> favorites = _context.Favorites;
            Favorite favorite = favorites.FirstOrDefault(f => f.userId == userId && f.audioId == audioId);

            if (favorite != null)
            {
                favorites.Remove(favorite);
            }
            else
            {
                favorites.Add(new Favorite { userId = userId, audioId = audioId });
            }

            _context.SaveChanges();

            return Ok();
        }

        public IActionResult Details(int id)
        {
            Audio audio = _context.Audios.FirstOrDefault(a => a.id == id);

            return View(audio);
        }
    }
}
