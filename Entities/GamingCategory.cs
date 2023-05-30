namespace Gaming.Entities
{
    public class GamingCategory : BaseEntitiy
    {

        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public GamingShop GamingShop { get; set; }

    }
}
