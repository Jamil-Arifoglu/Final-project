
using System.ComponentModel.DataAnnotations;

namespace Gaming.Entities
{
    public class Basket : BaseEntitiy
    {
        public int BasketId { get; set; }
        public string UserId { get; set; }
        public bool IsOrdered { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<BasketItem> BasketItems { get; set; }
    }
}