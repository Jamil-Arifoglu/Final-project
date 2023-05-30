using Gaming.ViewModels.Cookie;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Gaming.Services
{
    public class CookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetBasketCookie(CookieBasketVM basket)
        {
            string serializedBasket = JsonConvert.SerializeObject(basket);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("Basket", serializedBasket);
        }

        public CookieBasketVM GetBasketFromCookie()
        {
            string serializedBasket = _httpContextAccessor.HttpContext.Request.Cookies["Basket"];
            if (serializedBasket != null)
            {
                return JsonConvert.DeserializeObject<CookieBasketVM>(serializedBasket);
            }
            return null;
        }
    }
}