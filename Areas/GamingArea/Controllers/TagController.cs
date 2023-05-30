using Gaming.DAL;
using Gaming.Entities;
using Gaming.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Gaming.Areas.Gaming.Controllers
{
    [Area("GamingArea")]
    //[Authorize(Roles = "Admin, Moderator")]
    public class TagController : Controller
    {
        private readonly GamingDbContext _context;

        public TagController(GamingDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Tag> tags = _context.Tags.AsEnumerable();
            return View(tags);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Creates(Tag newTags)
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
            bool isDuplicated = _context.Tags.Any(c => c.Name == newTags.Name);
            if (isDuplicated)
            {
                ModelState.AddModelError("", "You cannot duplicate value");
                return View();
            }
            _context.Tags.Add(newTags);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();
            Tag tags = _context.Tags.FirstOrDefault(c => c.Id == id);
            if (tags is null) return NotFound();
            return View(tags);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id, Tag edited)
        {
            if (id == 0) return NotFound();
            Tag tags = _context.Tags.FirstOrDefault(c => c.Id == id);
            if (tags is null) return NotFound();
            bool duplicate = _context.Colors.Any(c => c.Name == edited.Name);
            if (duplicate)
            {
                ModelState.AddModelError("", "You cannot duplicate tags name");
                return View();
            }
            tags.Name = edited.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {


            if (id == 0) return NotFound();
            Tag tags = _context.Tags.FirstOrDefault(c => c.Id == id);
            if (tags is null) return NotFound();
            return View(tags);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            Tag tags = _context.Tags.FirstOrDefault(c => c.Id == id);
            if (tags is null) return NotFound();
            return View(tags);
        }


        [HttpPost]
        public IActionResult Delete(int id, Tag delete)
        {

            if (id == 0) return NotFound();
            Tag tags = _context.Tags.FirstOrDefault(c => c.Id == id);
            if (tags is null) return NotFound();



            if (id == delete.Id)
            {
                _context.Tags.Remove(tags);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(tags);
        }

    }
}
