namespace Gaming.Entities
{
    public class GamingSize : BaseEntitiy
    {


        public GamingShop GamingShop { get; set; }
        public int Quantity { get; set; }
        public Size Size { get; set; }
        public int SizeId { get; internal set; }
        public ICollection<BasketItem>? BasketItems { get; set; }


        public GamingSize()
        {
            BasketItems = new List<BasketItem>();

        }
    }
}
