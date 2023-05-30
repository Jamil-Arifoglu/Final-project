using Microsoft.AspNetCore.Mvc;

namespace Gaming.Areas.GamingArea.Controllers
{
    public class HomeController : Controller
    {
        [Area("GamingArea")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
