using DomainModels.Models.BlogModel;
using DomainModels.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.Areas.Admin.ViewModels
{
    public class CommentVM
    {
        public IList<Blog> Blogs { get; set; }
        public Comment Comment { get; set; }
    }
}
