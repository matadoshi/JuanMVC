using DomainModels.Models;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Service.Interfaces;
using Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.BaseModels
{
    public class ProductService:IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductById(int? id)
        {
            Product product = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Brand)
                .Include(p => p.ProductTags).ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                List<Product> products = await _context.Products
                      .Where(c => !c.IsDeleted)
                      .OrderByDescending(t => t.Id)
                      .ToListAsync();
                return products;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> ExistsProduct(int? id)
        {
            var result = await _context.Products.AnyAsync(p => p.Id == id);
            return result;
        }
    }
}
