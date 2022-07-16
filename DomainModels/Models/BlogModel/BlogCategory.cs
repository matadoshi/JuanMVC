using DomainModels.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModels.Models.BlogModel
{
    public class BlogCategory:BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
    }
}
