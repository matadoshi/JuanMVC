using DomainModels.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModels.Models.BlogModel
{
    public class AuthorSocialMedia:BaseEntity
    {
        public string Facebook { get; set; }
        public string Pinterest { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
