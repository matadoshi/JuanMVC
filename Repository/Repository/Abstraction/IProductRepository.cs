using DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Abstraction
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<Product> FirstOrDefault(int id);
    }
}
