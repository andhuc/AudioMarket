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

            if (ModelState.IsValid)
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
    }
}
