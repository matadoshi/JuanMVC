using DomainModels.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainModels.Models
{
    public class Brand:BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
