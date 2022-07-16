using DomainModels.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainModels.Models
{
    public class ProductImage:BaseEntity
    {
        [Required]
        public string Image { get; set; }
        public int Productid { get; set; }
        public Product Product { get; set; }
    }
}
