using Gaming.DAL;
using Gaming.Entities;
using Gaming.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Gaming.Areas.Gaming.Controllers
{
    [Area("GamingArea")]
    //[Authorize(Roles = "Admin, Moderator")]
    public class SizeController : Controller
    {
        private readonly GamingDbContext _context;

        public SizeController(GamingDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Size> size = _context.Sizes.AsEnumerable();
            return View(size);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Creates(Size newzsize)
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
            bool isDuplicated = _context.Sizes.Any(c => c.Name == newzsize.Name);
            if (isDuplicated)
            {
                ModelState.AddModelError("", "You cannot duplicate value");
                return View();
            }
            _context.Sizes.Add(newzsize);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();
            Size newzsize = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (newzsize is null) return NotFound();
            return View(newzsize);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id, Size edited)
        {
            if (id == 0) return NotFound();
            Size newzsize = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (newzsize is null) return NotFound();
            bool duplicate = _context.Sizes.Any(c => c.Name == edited.Name);
            if (duplicate)
            {
                ModelState.AddModelError("", "You cannot duplicate size name");
                return View();
            }
            newzsize.Name = edited.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {


            if (id == 0) return NotFound();
            Size newzsize = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (newzsize is null) return NotFound();
            return View(newzsize);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            Size newzsize = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (newzsize is null) return NotFound();
            return View(newzsize);
        }


        [HttpPost]
        public IActionResult Delete(int id, Size delete)
        {

            if (id == 0) return NotFound();
            Size newzsize = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (newzsize is null) return NotFound();



            if (id == delete.Id)
            {
                _context.Sizes.Remove(newzsize);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(newzsize);
        }

    }
}
