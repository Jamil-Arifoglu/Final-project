using Microsoft.AspNetCore.Mvc;

namespace Gaming.Controllers
{
	public class GamingController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
