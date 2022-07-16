using DomainModels.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainModels.Models
{
    public class ProductCategory:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
