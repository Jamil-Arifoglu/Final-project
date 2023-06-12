namespace Gaming.ViewModels.Cookie
{
    public class BasketVM
    {
        public int Id { get; set; }
        public List<CookieBasketItemVM> CookieBasketItems { get; set; }
        public double TotalPrice { get; set; }

        public BasketVM()
        {
            CookieBasketItems = new List<CookieBasketItemVM>();
        }
    }
}