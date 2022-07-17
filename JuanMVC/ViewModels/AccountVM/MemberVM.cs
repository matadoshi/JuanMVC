using DomainModels.Models.OrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.ViewModels
{
    public class MemberVM
    {
        public ProfileVM ProfileVM { get; set; }
        public List<Order> Orders { get; set; }
    }
}
