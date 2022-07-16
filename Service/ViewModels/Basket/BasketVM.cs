using System;
using System.Collections.Generic;
using System.Text;

namespace Service.ViewModels.Basket
{
    public class BasketVM
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
}
