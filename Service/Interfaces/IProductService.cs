using DomainModels.Models;
using Service.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(int? id);
        Task<bool> ExistsProduct(int? id);
    }
}
