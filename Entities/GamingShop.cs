namespace Gaming.Entities
{
    public class GamingShop : BaseEntitiy
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }

        public List<GamingImage> GamingImage { get; set; }

        public List<GamingCategory> GamingCategory { get; set; }
        public List<GamingTag> GamingTag { get; set; }
        public List<Comment> Comments { get; set; }

        public List<GamingSize> GamingSizes { get; set; }

        public List<GamingColor> GamingColors { get; set; }
        public GamingShop()
        {

            GamingImage = new();
            GamingCategory = new();
            GamingTag = new();
            GamingSizes = new();
            GamingColors = new();
            Comments = new();
        }
    }
}
