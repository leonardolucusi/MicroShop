using Microsoft.AspNetCore.Mvc;

namespace MicroShop.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RegisterPage()
        {
            return View();
        }
        public IActionResult LoginPage()
        {
            return View();
        }
    }
}
