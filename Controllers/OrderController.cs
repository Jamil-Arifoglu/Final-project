using Gaming.DAL;
using Gaming.Entities;
using Gaming.Services;
using Gaming.ViewModels.Cookie;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gaming.Controllers
{
    public class OrderController : Controller
    {
        private readonly GamingDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly CookieService _cookieService;

        public OrderController(GamingDbContext context, UserManager<User> userManager, CookieService cookieService)
        {
            _context = context;
            _userManager = userManager;
            _cookieService = cookieService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CookieBasketItemVM product)
        {
            if (User.Identity.IsAuthenticated)
            {

                User user = await _userManager.GetUserAsync(User);

                CookieBasketVM cookieBasket = _cookieService.GetBasketFromCookie();


                if (cookieBasket == null)
                {
                    cookieBasket = new CookieBasketVM();
                }

                cookieBasket.Items.Add(product);


                _cookieService.SetBasketCookie(cookieBasket);


                Basket basket = await _context.Baskets
                    .Include(b => b.BasketItems)
                    .FirstOrDefaultAsync(b => b.UserId == user.Id && !b.IsOrdered);


                if (basket == null)
                {
                    basket = new Basket
                    {
                        UserId = user.Id,
                        IsOrdered = false,
                        BasketItems = new List<BasketItem>()
                    };

                    _context.Baskets.Add(basket);
                }


                BasketItem basketItem = new BasketItem
                {
                    BasketId = basket.BasketId,
                    ProductId = product.Id,
                    UnitPrice = (decimal)product.Price,
                    SaleQuantity = product.Quantity,
                    Title = product.Title,
                    ImageUrl = product.ImageUrl,


                };


                basket.BasketItems.Add(basketItem);


                await _context.SaveChangesAsync();
                return Ok();
            }

            return Unauthorized();
        }
    }
}