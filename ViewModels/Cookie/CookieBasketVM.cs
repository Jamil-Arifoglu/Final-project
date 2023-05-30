namespace Gaming.ViewModels.Cookie
{
    public class CookieBasketVM
    {
        public List<CookieBasketItemVM> Items { get; set; }
        public double TotalPrice { get; set; }

        public CookieBasketVM()
        {
            Items = new List<CookieBasketItemVM>();
        }
    }
}