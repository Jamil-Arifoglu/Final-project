using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Gaming.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Gaming.ViewModels
{
    public class GamingVM
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 20)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }

        [NotMapped]
        public IFormFile? MainPhoto { get; set; }

        [NotMapped]
        public ICollection<int> CategoryIds { get; set; } = new List<int>();
        [NotMapped]
        public ICollection<int> TagIds { get; set; } = new List<int>();
        [NotMapped]
        public ICollection<int> SizeIds { get; set; } = new List<int>();
        [NotMapped]
        public ICollection<int> ColorIds { get; set; } = new List<int>();

        [NotMapped]
        public ICollection<IFormFile>? Images { get; set; }
        public ICollection<GamingImage>? SpecificImages { get; set; }
        public ICollection<int>? ImageIds { get; set; }
    }
}