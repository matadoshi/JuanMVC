using DomainModels.Models.OrderModel;
using Service.ViewModels.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.ViewModels.AccountVM
{
    public class OrderVM
    {
        public Order Order { get; set; }
        public List<BasketVM> BasketVMs { get; set; }
    }
}
