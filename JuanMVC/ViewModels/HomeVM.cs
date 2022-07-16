using DomainModels.Models;
using DomainModels.Models.BlogModel;
using Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public Pagination<Product> Products { get; set; }
        public Pagination<Blog> Blogs { get; set; }
    }
}
