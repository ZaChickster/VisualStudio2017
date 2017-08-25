using Microsoft.AspNetCore.Mvc;

namespace VisualStudio2017.Angular4.Controllers
{
	public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
