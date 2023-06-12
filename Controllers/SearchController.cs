using Gaming.DAL;
using Gaming.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Gaming.Controllers
{
    public class SearchController : Controller
    {
        private readonly GamingDbContext _context;
        private readonly UserManager<User> _userManager;

        public SearchController(GamingDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToAction("Index");
            }

            List<GamingShop> products = _context.Gamings
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm) || p.SKU == searchTerm || p.Barcode == searchTerm)
                .ToList();

            return View(products);
        }
    }
}
