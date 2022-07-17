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
    public class CategoryRepository:EfCoreRepository<ProductCategory>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext context):base(context)
        {

        }

        public async Task<bool> ExistsCategory(string name)
        {
            return await _context.ProductCategories.AnyAsync(b => b.Name.ToLower() == name.ToLower().Trim());
        }
        public AppDbContext appDbContext { get { return _context as AppDbContext; } }
    }
}
