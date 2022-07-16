using DomainModels.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModels.Models.BlogModel
{
    public class BlogTag
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
