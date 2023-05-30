using System.ComponentModel.DataAnnotations.Schema;

namespace Gaming.Entities
{
    public class Category : BaseEntitiy
    {

        public string Name { get; set; }

        public List<GamingCategory> GamingCategory { get; set; }
        public string CategoryPath { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        public Category()
        {
            GamingCategory = new();
        }
    }
}
