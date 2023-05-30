using Microsoft.AspNetCore.Identity;

namespace Gaming.Entities
{
    public class User : IdentityUser
    {
        public string Fullname { get; set; }

        public virtual ICollection<User> Logins { get; set; }
        public ICollection<Basket> Baskets { get; set; }
        public List<Comment> Comments { get; set; }

        public User()
        {
            Baskets = new List<Basket>();
            Comments = new List<Comment>();
        }
    }
}