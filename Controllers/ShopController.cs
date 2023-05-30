using Gaming.DAL;
using Gaming.Entities;
using Gaming.Utilities.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gaming.Controllers
{
    public class ShopController : Controller
    {

        readonly GamingDbContext _context;

        private readonly UserManager<User> _userManager;

        public ShopController(GamingDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Collection()
        {
            var categories = _context.Catagory.ToList();

            return View(categories);
        }
        public IActionResult shop(int id)
        {
            List<GamingShop> gamingList = _context.Gamings
                .Include(y => y.GamingImage)
                .Include(t => t.GamingTag).ThenInclude(tg => tg.Tag)
                .Include(c => c.GamingCategory).ThenInclude(ct => ct.Category)
                .Where(c => c.GamingCategory.Any(p => p.CategoryId == id))
                .OrderByDescending(p => p.Id)
                .Take(10)
                .ToList();

            if (gamingList.Count == 0)
            {
                // Belirtilen kategoriye ait ürün bulunamadı, uygun bir hata mesajı veya yönlendirme yapılabilir
                return RedirectToAction("NotFound");
            }

            return View(gamingList);
        }

        public IActionResult Detail(int id)
        {
            IQueryable<GamingShop> gaming = _context.Gamings.AsNoTracking().AsQueryable();
            GamingShop? gamings = gaming.Include(ci => ci.GamingImage)
                                               .Include(cc => cc.GamingCategory).ThenInclude(f => f.Category).Include(t => t.GamingTag).ThenInclude(th => th.Tag).Include(gc => gc.GamingColors).ThenInclude(c => c.Color).Include(gs => gs.GamingSizes).ThenInclude(s => s.Size).AsSingleQuery().FirstOrDefault(x => x.Id == id);

            ViewBag.RelatedGamings = _context.Gamings
        .Include(p => p.GamingImage).Include(t => t.GamingTag).ThenInclude(tg => tg.Tag)
                                    .OrderByDescending(p => p.Id)
                                            .ToList();

            return View(gamings);
        }

        [HttpPost]
        //public async Task<IActionResult> AddComment(Comment comment, int id)
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //    else
        //    {
        //        Clothes clother = await _context.Clothes.Include(pt => pt.Comments).FirstOrDefaultAsync(p => p.Id == id);
        //        User user = await _userManager.FindByNameAsync(User.Identity.Name);
        //        Comment newcomment = new Comment()
        //        {
        //            Text = comment.Text,
        //            User = user,
        //            CreationTime = DateTime.UtcNow,
        //            Clothes = clother

        //        };
        //        user.Comments.Add(newcomment);
        //        clother.Comments.Add(newcomment);
        //        await _context.Comments.AddAsync(newcomment);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Details), new { id });
        //    }
        //}
        [HttpPost]


        static List<GamingShop> RelatedClothes(IQueryable<GamingShop> queryable, GamingShop gaming, int id)
        {
            List<GamingShop> relateds = new();

            gaming.GamingCategory.ForEach(pc =>
            {
                List<GamingShop> related = queryable
                    .Include(p => p.GamingImage)
                        .Include(p => p.GamingCategory)
                            .ThenInclude(pc => pc.Category)

                                    .AsEnumerable()
                                        .Where(
                                        p => gaming.GamingCategory.Contains(pc, new GamingCategoryComparer())
                                        && p.Id != id
                                        && !relateds.Contains(p, new GamingComparer())
                                        )
                                        .ToList();
                relateds.AddRange(related);
            });
            return relateds;
        }
    }
}

