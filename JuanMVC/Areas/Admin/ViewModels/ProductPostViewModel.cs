using DomainModels.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JuanMVC.Areas.Admin.ViewModels
{
    public class ProductPostViewModel
    {
        //get
        public IList<ProductCategory> Categories { get; set; }
        public IList<Brand> Brands { get; set; }

        //post
        public Product Product { get; set; }
    }
}
