using DomainModels.Models;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Repository.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Implementation
{
    public class ProductRepository : EfCoreRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context):base(context)
        {

        }
        public async Task<Product> FirstOrDefault(int id)
        {
            return await appDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
        public AppDbContext appDbContext { get { return _context as AppDbContext; } }
    }
}
