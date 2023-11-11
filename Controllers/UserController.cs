using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN_Project.Models;

namespace PRN_Project.Controllers
{

    public class UserController : Controller
    {

        private readonly AudioMarketContext _context;

        public UserController(AudioMarketContext context)
        {
            _context = context;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            
            User user = new AudioMarketContext().Users.FirstOrDefault(u => u.username == username && u.status);

            if (user == null || !user.password.Equals(password))
            {
                ViewBag.ErrorMessage = "Invalid username or password!";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("user", username);
                HttpContext.Session.SetInt32("userId", user.id);
                HttpContext.Session.SetInt32("userRole", user.role);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult LoginGoogle(string email, string name)
        {

            var user = _context.Users.FirstOrDefault(u => u.username == email && u.status);

            if (user == null)
            {
                User newUser = new User();
                newUser.username = email;
                newUser.name = name;
                newUser.status = true;
                
                _context.Users.Add(newUser);
                _context.SaveChanges();

                HttpContext.Session.SetString("user", email);
                HttpContext.Session.SetInt32("userId", newUser.id);
                HttpContext.Session.SetInt32("userRole", newUser.role);
            }
            else
            {
                HttpContext.Session.SetString("user", email);
                HttpContext.Session.SetInt32("userId", user.id);
                HttpContext.Session.SetInt32("userRole", user.role);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string displayname, string password, string repassword)
        {
            AudioMarketContext audioMarketContext = new AudioMarketContext();

            username = username.Trim();
            displayname = displayname.Trim();
            password = password.Trim();
            repassword = repassword.Trim();

            User user = audioMarketContext.Users.FirstOrDefault(u => u.username == username);

            if (user != null)
            {
                ViewBag.ErrorMessage = "Invalid username!";
                return View();
            }

            if (password != repassword)
            {
                ViewBag.ErrorMessage = "2 password aren't same!";
                return View();
            }

            User newUser = new User();

            newUser.username= username;
            newUser.password= password;
            newUser.name= displayname;

            audioMarketContext.Add(newUser);
            audioMarketContext.SaveChanges();

            return RedirectToAction("Login", "User");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string username, string oldpassword, string newpassword, string repassword)
        {
            AudioMarketContext audioMarketContext = new AudioMarketContext();

            username = username.Trim();
            oldpassword = oldpassword.Trim();
            newpassword = newpassword.Trim();
            repassword = repassword.Trim();

            User user = audioMarketContext.Users.FirstOrDefault(u => u.username == username);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid username!";
                return View();
            }

            if (newpassword != repassword)
            {
                ViewBag.ErrorMessage = "2 password aren't same!";
                return View();
            }
            else if (oldpassword != user.password)
            {
                ViewBag.ErrorMessage = "Wrong old password!";
                return View();
            }
            else
            {
                user.password = newpassword;
                audioMarketContext.SaveChanges();
                ViewBag.SuccessMessage = "Change Password successfully";
                return RedirectToAction("Profile", "User");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Upload()
        {
            if(HttpContext.Session.GetString("user")==null) return RedirectToAction("Login", "User");

            ViewBag.GenreList = _context.Genres.ToList();
            ViewBag.MoodList = _context.Moods.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Upload(string audioTitle, IFormFile audioFile, IFormFile audioImage, int genreId, int moodId)
        {
            String imagePath = "/audio/img/";

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

                    _context.Audios.Add(audio);
                    _context.SaveChanges();

                    return RedirectToAction("List", "Audio");
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

        public ActionResult Cart()
        {
            List<Audio> audioList = new List<Audio>();
            int? username = HttpContext.Session.GetInt32("userId");

            // Construct the cookie name based on the username.
            string cookieName = username + "-audiocart";

            // Read the cookie.
            string cookieValue = HttpContext.Request.Cookies[cookieName];

            if (cookieValue != null && cookieValue.Length>0)
            {
                cookieValue = cookieValue.Substring(0, cookieValue.Length - 1);

                List<int> audioIdList = cookieValue.Split('-').Select(int.Parse).ToList();
                audioList = _context.Audios
                            .Where(a => audioIdList.Contains(a.id))
                            .ToList();
            }

            return View(audioList);

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

        public IActionResult Order(int audioId)
        {
            string username = HttpContext.Session.GetString("user");
            User user = _context.Users.FirstOrDefault(u => u.username == username);
            if (user == null) return BadRequest();

            Order order = _context.Orders.FirstOrDefault(o => o.audioId == audioId && o.buyerId == user.id);

            if(order == null)
            {
                Order o = new Order();

                o.audioId = audioId;
                o.buyerId = user.id;
                o.purchaseDate = DateTime.Now;

                _context.Orders.Add(o);
                _context.SaveChanges();

                return Ok();
            } else
            {
                return BadRequest();
            }
        }

        public IActionResult MyAudio()
        {
            string username = HttpContext.Session.GetString("user");
            User user = _context.Users.FirstOrDefault(u => u.username == username);

            ViewBag.GenreList = _context.Genres.ToList();
            ViewBag.MoodList = _context.Moods.ToList();
            ViewBag.OrderList = _context.Orders
                                        .Where(o => o.Audio.artistId == user.id)
                                        .OrderBy(o => o.audioId)
                                        .ToList();

            return View(_context.Audios.Where(a => a.artistId == user.id).ToList());
        }

        public IActionResult MyOrder()
        {
            string username = HttpContext.Session.GetString("user");
            User user = _context.Users.FirstOrDefault(u => u.username == username);

            return View(_context.Orders.Where(o => o.buyerId == user.id).ToList());
        }

        public IActionResult Review(string comment, int audioId)
        {
            string username = HttpContext.Session.GetString("user");
            User user = _context.Users.FirstOrDefault(u => u.username == username);
            if (user == null) return BadRequest();

            Review review = new Review();

            review.userId = user.id;
            review.audioId = audioId;
            review.comment = comment;

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return Ok();
        }

        public IActionResult UpdateOrderStatus(int orderId, int status)
        {
            Order order = _context.Orders.FirstOrDefault(o => o.id == orderId);

            order.status = status;

            _context.SaveChanges();

            return Ok();
        }
    }
}
