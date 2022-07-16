using DomainModels.Models.Common;
using DomainModels.Models.ProductModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainModels.Models
{
    public class Product:BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        [Required]
        public bool IsTopSeller { get; set; }
        [Required]
        public bool IsAvailability { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string MainImage { get; set; }
        [Required]
        public string SecondImage { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }
        public Brand Brand { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
        public IEnumerable<ProductTag> ProductTags { get; set; }
        [NotMapped]
        public IFormFile MainFile { get; set; }
        [NotMapped]
        public IFormFile SecondFile { get; set; }
        [NotMapped]
        public List<IFormFile> Files { get; set; }
    }
}
