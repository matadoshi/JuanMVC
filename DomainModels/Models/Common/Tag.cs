using DomainModels.Models.BlogModel;
using DomainModels.Models.ProductModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainModels.Models.Common
{
    public class Tag:BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public IEnumerable<ProductTag> ProductTags { get; set; }
        public IEnumerable<BlogTag> BlogTags { get; set; }
    }
}
