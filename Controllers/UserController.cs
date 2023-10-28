using Microsoft.AspNetCore.Mvc;
using PRN_Project.Models;

namespace PRN_Project.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            return View(); // Return a login view
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            
            User user = new AudioMarketContext().Users.FirstOrDefault(u => u.username == username);

            if (user == null || !user.password.Equals(password))
            {
                ViewBag.ErrorMessage = "Invalid username or password!";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("user", username);
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
