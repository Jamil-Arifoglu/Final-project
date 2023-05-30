using System.ComponentModel.DataAnnotations;

namespace Gaming.Entities
{
    public class BasketItem : BaseEntitiy
    {
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int SaleQuantity { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public virtual Basket Basket { get; set; }
    }
}