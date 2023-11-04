using Microsoft.AspNetCore.Mvc;

namespace PRN_Project.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserManage()
        {


            return View();
        }
    }
}
