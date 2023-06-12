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



        public BasketVM GetBasketFromCookie()
        {
            string serializedBasket = _httpContextAccessor.HttpContext.Request.Cookies["Basket"];
            if (serializedBasket != null)
            {
                return JsonConvert.DeserializeObject<BasketVM>(serializedBasket);
            }
            return null;
        }


    }
}