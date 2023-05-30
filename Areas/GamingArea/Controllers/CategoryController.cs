using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Gaming.DAL;
using Gaming.Entities;
using Gaming.Migrations;
using Gaming.Entities;
using Gaming.Utilities.Extensions;

namespace Gaming.Areas.GamingArea.Controllers
{
    [Area("GamingArea")]
    //[Authorize(Roles = "Admin, Moderator")]
    public class CategoryController : Controller
    {
        private readonly GamingDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(GamingDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.Catagory.AsEnumerable();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Creates(Category? newCategory)
        {
            if (newCategory.Image is null)
            {
                ModelState.AddModelError("Image", "Please Select Image");
                return View();
            }
            if (!newCategory.Image.IsValidFile("image/"))
            {
                ModelState.AddModelError("Image", "Please Select Image Tag");
                return View();
            }
            if (!newCategory.Image.IsValidLength(2))
            {
                ModelState.AddModelError("Image", "Please Select Image which size max 2MB");
                return View();
            }

            string imagesFolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            newCategory.CategoryPath = await newCategory.Image.CreateImage(imagesFolderPath, "Products");
            _context.Catagory.Add(newCategory);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();

            Category category = _context.Catagory.FirstOrDefault(s => s.Id == id);
            if (category is null) return BadRequest();
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category edited)
        {
            if (id != edited.Id) return BadRequest();
            Category category = _context.Catagory.FirstOrDefault(s => s.Id == id);
            if (!ModelState.IsValid) return View(category);

            _context.Entry(category).CurrentValues.SetValues(edited);

            if (edited.Image is not null)
            {
                string imagesFolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
                string filePath = Path.Combine(imagesFolderPath, "products", category.CategoryPath);
                FileUpload.DeleteImage(filePath);
                category.CategoryPath = await edited.Image.CreateImage(imagesFolderPath, "Products");
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            if (id == 0) return NotFound();
            Category category = _context.Catagory.FirstOrDefault(s => s.Id == id);
            if (category is null) return NotFound();
            return View(category);
        }
        public IActionResult Delete(int id)
        {

            if (id == 0) return NotFound();
            Category category = _context.Catagory.FirstOrDefault(s => s.Id == id);
            if (category is null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(int id, Category delete)
        {

            if (id == 0) return NotFound();
            Category category = _context.Catagory.FirstOrDefault(s => s.Id == id);
            if (category is null) return NotFound();


            if (id == delete.Id)
            {
                _context.Catagory.Remove(category);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
