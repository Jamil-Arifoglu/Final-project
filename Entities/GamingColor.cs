namespace Gaming.Entities
{
    public class GamingColor : BaseEntitiy
    {
        public int color { get; set; }
        public int Quantity { get; set; }
        public Color Color { get; set; }

        public GamingShop GamingShop { get; set; }
        public ICollection<BasketItem>? BasketItems { get; set; }


        public GamingColor()
        {
            BasketItems = new List<BasketItem>();

        }

    }
}
