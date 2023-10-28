using Microsoft.AspNetCore.Mvc;
using PRN_Project.Models;

namespace PRN_Project.Controllers
{

    public class UserController : Controller
    {
        public ActionResult Login()
        {
            return View();
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

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
