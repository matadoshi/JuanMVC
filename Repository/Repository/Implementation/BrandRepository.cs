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
    public class BrandRepository:EfCoreRepository<Brand>,IBrandRepository
    {
        public BrandRepository(AppDbContext context):base(context)
        {

        }


        public AppDbContext appDbContext { get { return _context as AppDbContext; } }

        public async Task<bool> ExistsBrand(string name)
        {
            return await _context.ProductCategories.AnyAsync(b => b.Name.ToLower() == name.ToLower().Trim());
        }
    }
}
