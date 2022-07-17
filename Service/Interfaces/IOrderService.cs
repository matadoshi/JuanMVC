using DomainModels.Models.OrderModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IOrderService
    {
        Task<bool> SaveChanges(Order order);
    }
}
