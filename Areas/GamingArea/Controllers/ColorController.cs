using Gaming.DAL;
using Gaming.Entities;
using Gaming.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Gaming.Areas.Gaming.Controllers
{
    [Area("GamingArea")]
    //[Authorize(Roles = "Admin, Moderator")]
    public class ColorController : Controller
    {
        private readonly GamingDbContext _context;

        public ColorController(GamingDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Color> color = _context.Colors.AsEnumerable();
            return View(color);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Creates(Color newcolor)
        {
            if (!ModelState.IsValid)
            {
                foreach (string message in ModelState.Values.SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage))
                {
                    ModelState.AddModelError("", message);
                }

                return View();
            }
            bool isDuplicated = _context.Colors.Any(c => c.Name == newcolor.Name);
            if (isDuplicated)
            {
                ModelState.AddModelError("", "You cannot duplicate value");
                return View();
            }
            _context.Colors.Add(newcolor);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();
            Color color = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (color is null) return NotFound();
            return View(color);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id, Color edited)
        {
            if (id == 0) return NotFound();
            Color color = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (color is null) return NotFound();
            bool duplicate = _context.Colors.Any(c => c.Name == edited.Name);
            if (duplicate)
            {
                ModelState.AddModelError("", "You cannot duplicate color name");
                return View();
            }
            color.Name = edited.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {


            if (id == 0) return NotFound();
            Color color = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (color is null) return NotFound();
            return View(color);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            Color color = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (color is null) return NotFound();
            return View(color);
        }


        [HttpPost]
        public IActionResult Delete(int id, Color delete)
        {

            if (id == 0) return NotFound();
            Color color = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (color is null) return NotFound();



            if (id == delete.Id)
            {
                _context.Colors.Remove(color);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(color);
        }

    }
}
