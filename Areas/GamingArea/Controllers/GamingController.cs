using Gaming.Utilities.Extensions;
using Gaming.DAL;
using Gaming.Entities;
using Gaming.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gaming.Areas.GamingArea.Controllers
{
    [Area("GamingArea")]
    public class GamingController : Controller
    {

        private readonly GamingDbContext _context;
        private readonly IWebHostEnvironment _env;

        public GamingController(GamingDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            IEnumerable<GamingShop> model = _context.Gamings.Include(p => p.GamingImage).Include(ct => ct.GamingCategory).ThenInclude(c => c.Category)
                                                         .Include(p => p.GamingSizes).ThenInclude(p => p.Size)
                                                         .Include(p => p.GamingColors).ThenInclude(p => p.Color)
                                                         .AsNoTracking().AsEnumerable();
            return View(model);
        }

        public IActionResult Create()
        {

            ViewBag.Catagory = _context.Catagory.AsEnumerable();
            ViewBag.Tags = _context.Colors.AsEnumerable();
            ViewBag.Sizes = _context.Sizes.AsEnumerable();
            ViewBag.Colors = _context.Catagory.AsEnumerable();


            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create(GamingVM newgaming)
        {
            ViewBag.Tags = _context.Colors.AsEnumerable();
            ViewBag.Sizes = _context.Sizes.AsEnumerable();
            ViewBag.Colors = _context.Catagory.AsEnumerable();

            TempData["InvalidImages"] = string.Empty;

            if (!ModelState.IsValid)
            {
                return View(newgaming);
            }

            if (!newgaming.MainPhoto.IsValidFile("image/"))
            {
                ModelState.AddModelError(string.Empty, "Please choose an image file");
                return View(newgaming);
            }

            if (!newgaming.MainPhoto.IsValidLength(1))
            {
                ModelState.AddModelError(string.Empty, "Please choose an image with a maximum size of 1MB");
                return View(newgaming);
            }

            GamingShop gaming = new GamingShop()
            {
                Name = newgaming.Name,
                Description = newgaming.Description,
                ShortDescription = newgaming.ShortDescription,
                Price = newgaming.Price,
                Discount = newgaming.Discount,
                DiscountPrice = newgaming.DiscountPrice,
                SKU = newgaming.SKU,
                Barcode = newgaming.Barcode,
            };

            string imageFolderPath = Path.Combine(_env.WebRootPath, "assets", "images");

            foreach (var image in newgaming.Images)
            {
                if (!image.IsValidFile("image/") || !image.IsValidLength(1))
                {
                    TempData["InvalidImages"] += image.FileName;
                    continue;
                }

                GamingImage gamingImage = new GamingImage()
                {
                    IsMain = false,
                    Path = await image.CreateImage(imageFolderPath, "Products")
                };

                gaming.GamingImage.Add(gamingImage);
            }

            if (newgaming.MainPhoto != null)
            {
                GamingImage main = new GamingImage()
                {
                    IsMain = true,
                    Path = await newgaming.MainPhoto.CreateImage(imageFolderPath, "Products")
                };

                gaming.GamingImage.Add(main);
            }

            foreach (int sizeId in newgaming.SizeIds)
            {
                GamingSize gamingSize = new GamingSize()
                {
                    SizeId = sizeId,
                    Quantity = 0
                };

                gaming.GamingSizes.Add(gamingSize);
            }

            foreach (int colorId in newgaming.ColorIds)
            {
                GamingColor gamingColor = new GamingColor()
                {
                    color = colorId,
                    Quantity = 0
                };

                gaming.GamingColors.Add(gamingColor);
            }

            foreach (int id in newgaming.CategoryIds)
            {
                GamingCategory category = new GamingCategory()
                {
                    CategoryId = id
                };

                gaming.GamingCategory.Add(category);
            }

            foreach (int id in newgaming.TagIds)
            {
                GamingTag tag = new GamingTag()
                {
                    TagId = id
                };

                gaming.GamingTag.Add(tag);
            }

            _context.Gamings.Add(gaming);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Gaming");
        }
        private async Task AdjustPlantPhotos(IFormFile image, GamingShop gaming, bool? isMain)
        {
            string photoPath = gaming.GamingImage.FirstOrDefault(p => p.IsMain == isMain).Path;
            string imagesFolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "Products");
            string filePath = Path.Combine(imagesFolderPath, "Products", photoPath);
            FileUpload.DeleteImage(filePath);
            gaming.GamingImage.FirstOrDefault(p => p.IsMain == isMain).Path = await image.CreateImage(imagesFolderPath, "Products");
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return BadRequest();
            GamingVM? model = Editedgaming(id);


            ViewBag.Catagory = _context.Catagory.AsEnumerable();
            ViewBag.Tags = _context.Colors.AsEnumerable();
            ViewBag.Sizes = _context.Sizes.AsEnumerable();
            ViewBag.Colors = _context.Catagory.AsEnumerable();



            if (model is null) return BadRequest();
            _context.SaveChanges();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, GamingVM edited)
        {

            ViewBag.Catagory = _context.Catagory.AsEnumerable();
            ViewBag.Tags = _context.Colors.AsEnumerable();
            ViewBag.Sizes = _context.Sizes.AsEnumerable();
            ViewBag.Colors = _context.Catagory.AsEnumerable();

            TempData["InvalidImages"] = string.Empty;

            GamingVM? model = Editedgaming(id);

            GamingShop? gaming = await _context.Gamings.Include(p => p.GamingImage).Include(p => p.GamingCategory).Include(p => p.GamingTag).FirstOrDefaultAsync(p => p.Id == id);
            if (gaming is null) return BadRequest();

            IEnumerable<string> removables = gaming.GamingImage.Where(p => !edited.ImageIds.Contains(p.Id)).Select(i => i.Path).AsEnumerable();
            string imageFolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "Products");
            foreach (string removable in removables)
            {
                string path = Path.Combine(imageFolderPath, "Products", removable);
                await Console.Out.WriteLineAsync(path);
                Console.WriteLine(FileUpload.DeleteImage(path));
            }


            if (edited.MainPhoto is not null)
            {
                await AdjustPlantPhotos(edited.MainPhoto, gaming, true);
            }
            //else if (edited.Images is not null)
            //{
            //    await AdjustPlantPhotos(edited.Images, clother, null);
            //}


            gaming.GamingImage.RemoveAll(p => !edited.ImageIds.Contains(p.Id));
            if (edited.Images is not null)
            {
                foreach (var item in edited.Images)
                {
                    if (!item.IsValidFile("images/") || !item.IsValidLength(1))
                    {
                        TempData["InvalidImages"] += item.FileName;
                        continue;
                    }
                    GamingImage plantImage = new()
                    {
                        IsMain = false,
                        Path = await item.CreateImage(imageFolderPath, "Products")
                    };
                    gaming.GamingImage.Add(plantImage);
                }
            }
            gaming.Name = edited.Name;
            gaming.Description = edited.Description;
            gaming.ShortDescription = edited.ShortDescription;
            gaming.Price = edited.Price;
            gaming.Discount = edited.Discount;
            gaming.DiscountPrice = edited.DiscountPrice;
            gaming.SKU = edited.SKU;
            gaming.ShortDescription = edited.ShortDescription;
            gaming.Barcode = edited.Barcode;
            gaming.GamingCategory.Clear();
            gaming.GamingTag.Clear();

            foreach (int categoryId in edited.CategoryIds ?? new List<int>())
            {
                Category category = await _context.Catagory.FirstOrDefaultAsync(c => c.Id == categoryId);
                if (category != null)
                {
                    GamingCategory plantCategory = new GamingCategory { Category = category };
                    gaming.GamingCategory.Add(plantCategory);
                }
            }

            foreach (int tagId in edited.TagIds ?? new List<int>())
            {
                Tag tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == tagId);
                if (tag != null)
                {
                    GamingTag ClothesTag = new GamingTag { Tag = tag };
                    gaming.GamingTag.Add(ClothesTag);
                }
            }
            foreach (var sizeId in edited.SizeIds)
            {
                GamingSize gamingSize = gaming.GamingSizes.FirstOrDefault(s => s.SizeId == sizeId);
                if (gamingSize != null)
                {
                    gamingSize.Quantity = 0;
                }
            }

            foreach (var colorId in edited.ColorIds)
            {
                GamingColor gamingColor = gaming.GamingColors.FirstOrDefault(c => c.color == colorId);
                if (gamingColor != null)
                {
                    gamingColor.Quantity = 0;
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private GamingVM? Editedgaming(int id)
        {
            GamingVM? model = _context.Gamings.Include(p => p.GamingCategory)
                                            .Include(p => p.GamingTag)
                                            .Include(p => p.GamingImage)
                                            .Include(p => p.GamingColors)
                                             .Include(p => p.GamingSizes)
                                            .Select(p =>
                                                new GamingVM
                                                {
                                                    Id = p.Id,
                                                    Name = p.Name,
                                                    SKU = p.SKU,
                                                    Description = p.Description,
                                                    Price = p.Price,
                                                    DiscountPrice = p.DiscountPrice,
                                                    Discount = p.Discount,

                                                    CategoryIds = p.GamingCategory.Select(pc => pc.CategoryId).ToList(),
                                                    TagIds = p.GamingTag.Select(pc => pc.TagId).ToList(),
                                                    SpecificImages = p.GamingImage.Select(p => new GamingImage
                                                    {
                                                        Id = p.Id,
                                                        Path = p.Path,
                                                        IsMain = p.IsMain
                                                    }).ToList()

                                                })
                                                .FirstOrDefault(p => p.Id == id);
            return model;
        }

        public IActionResult Details(int id)
        {
            if (id == 0) return NotFound();
            GamingShop gaming = _context.Gamings.FirstOrDefault(s => s.Id == id);
            if (gaming is null) return NotFound();
            return View(gaming);

        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            GamingShop gaming = _context.Gamings.FirstOrDefault(s => s.Id == id);
            if (gaming is null) return NotFound();
            return View(gaming);
        }


        [HttpPost]
        public IActionResult Delete(int id, GamingShop delete)
        {

            if (id == 0) return NotFound();
            GamingShop gaming = _context.Gamings.FirstOrDefault(s => s.Id == id);
            if (gaming is null) return NotFound();





            if (id == delete.Id)
            {
                _context.Gamings.Remove(gaming);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(gaming);
        }


    }
}
