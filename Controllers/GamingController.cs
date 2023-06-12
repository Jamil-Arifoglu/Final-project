using Gaming.DAL;
using Gaming.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gaming.Controllers
{
    public class GamingController : Controller
    {
        readonly GamingDbContext _context;

        private readonly UserManager<User> _userManager;

        public GamingController(GamingDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            List<GamingShop> gamingList = _context.Gamings
              .Include(y => y.GamingImage)
              .Include(t => t.GamingTag).ThenInclude(tg => tg.Tag)
              .Include(c => c.GamingCategory).ThenInclude(ct => ct.Category)

              .OrderByDescending(p => p.Id)
              .Take(10)
              .ToList();



            return View(gamingList);
        }
    }
}
