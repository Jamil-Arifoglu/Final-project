using Gaming.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gaming.DAL
{
    public class GamingDbContext : IdentityDbContext<User>
    {

        public GamingDbContext(DbContextOptions<GamingDbContext> options) : base(options)
        {


        }

        public DbSet<GamingTag> GamingTags { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<GamingShop> Gamings { get; set; }


        public DbSet<Basket> Baskets { get; set; }

        public DbSet<BasketItem> BasketItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Catagory { get; set; }

    }
}
