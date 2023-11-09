using Microsoft.AspNetCore.Mvc;
using PRN_Project.Models;

namespace PRN_Project.Controllers
{
    public class AdminController : Controller
    {
        private readonly AudioMarketContext _context;

        public AdminController(AudioMarketContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserManage()
        {
            AudioMarketContext audioMarketContext = new AudioMarketContext();

            ViewBag.UserList = audioMarketContext.Users.ToList();
            return View(new AudioMarketContext().Users.ToList());
        }

        //public IActionResult Details() { }

        [HttpPost]
        public IActionResult BanUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.status = false; // Set the status to "Banned"
                _context.SaveChanges();
            }
            return RedirectToAction("UserManage");
        }

        [HttpPost]
        public IActionResult UnbanUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.status = true; // Set the status to "Normal"
                _context.SaveChanges();
            }
            return RedirectToAction("UserManage");
        }

        public IActionResult UserUpdate(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                // Handle the case where the user with the provided ID is not found.
                return NotFound("User with ID " + id + " not found.");
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            if (id != updatedUser.id)
            {
                // Handle the case where the user ID in the URL and the user model don't match.
                return BadRequest();
            }

            if (id == updatedUser.id)
            {
                // Update user information in the database and save changes.
                var existingUser = _context.Users.Find(id);
                if (existingUser != null)
                {
                    existingUser.username = updatedUser.username;
                    existingUser.password = updatedUser.password;
                    existingUser.role = updatedUser.role;
                    existingUser.name = updatedUser.name;
                    existingUser.status = updatedUser.status;
                    _context.SaveChanges();
                }
                return RedirectToAction("UserManage");
            }

            // If the model is not valid, return to the edit form with validation errors.
            return View("UserUpdate", updatedUser);
        }
        public IActionResult Upload(string audioTitle, IFormFile audioFile, IFormFile audioImage, int genreId, int moodId)
        {
            String imagePath = "/audio/img/";
            AudioMarketContext audioMarketContext = new AudioMarketContext();

            try
            {
                // Check if audioFile and audioImage are not null
                if (audioFile != null && audioImage != null)
                {
                    // Save audioFile to a specified location
                    var audioFileName = Path.Combine("wwwroot/audio", audioFile.FileName);
                    using (var stream = new FileStream(audioFileName, FileMode.Create))
                    {
                        audioFile.CopyTo(stream);
                    }

                    // Save audioImage to a specified location
                    var imageFileName = Path.Combine("wwwroot/audio/img", audioImage.FileName);
                    using (var stream = new FileStream(imageFileName, FileMode.Create))
                    {
                        audioImage.CopyTo(stream);
                    }

                    Audio audio = new Audio();

                    audio.title = audioTitle;
                    audio.filename = "/audio/" + audioFile.FileName;
                    audio.image = "/audio/img/" + audioImage.FileName;
                    audio.artistId = HttpContext.Session.GetInt32("userId");
                    audio.genreId = genreId; audio.moodId = moodId;
                    audio.status = true;

                    audioMarketContext.Audios.Add(audio);
                    audioMarketContext.SaveChanges();

                    return RedirectToAction("Admin", "Audio");
                }
                else
                {
                    // Handle the case where audioFile or audioImage is not provided
                    return Redirect("./Upload?error");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, such as file I/O errors
                return Redirect("./Upload?error");
            }
        }

        public ActionResult Profile()
        {
            return View();
        }

        public IActionResult ManageOrder(int id)
        {
            ViewBag.OrderList = _context.Orders.ToList();
            return View();
        }

        public IActionResult ManageAudio(int id)
        {
            ViewBag.GenreList = _context.Genres.ToList();
            ViewBag.MoodList = _context.Moods.ToList();

            return View(_context.Audios.ToList());
        }

        public IActionResult UpdateAudioStatus(int audioId, Boolean status)
        {
            Audio audio = _context.Audios.FirstOrDefault(a => a.id == audioId);

            audio.status = status;
            _context.SaveChanges();

            return Ok();
        }
    }
}

