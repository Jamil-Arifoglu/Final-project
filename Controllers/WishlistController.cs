using Microsoft.AspNetCore.Mvc;

namespace Gaming.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Wishlist()
        {
            return View();
        }
    }
}
