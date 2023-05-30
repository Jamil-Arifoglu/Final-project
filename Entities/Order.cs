namespace Gaming.Entities
{
    public class Order : BaseEntitiy
    {

        public int BasketId { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Basket Basket { get; set; }
    }
}
