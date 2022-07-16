using DomainModels.Models.BlogModel;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.BaseModels
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        public BlogService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Blog>> GetBlogs()
        {
            try
            {
                List<Blog> blogs = await _context.Blogs
                    .Include(b=>b.Author)
                    .Include(b=>b.BlogCategory)
                      .Where(c => !c.IsDeleted)
                      .OrderByDescending(t => t.Id)
                      .ToListAsync();
                return blogs;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Blog> GetBlogById(int? id)
        {
            Blog blog = await _context.Blogs
                .Include(b => b.BlogCategory)
                .Include(b=>b.Author)
                .FirstOrDefaultAsync(p => p.Id == id);
            return blog;
        }

        public async Task<List<Blog>> GetLatestBlog()
        {
            try
            {
                List<Blog> blogs = await _context.Blogs
                    .Include(b => b.Author)
                      .Where(c => !c.IsDeleted)
                      .OrderByDescending(t => t.CreatedAt)
                      .ToListAsync();
                return blogs;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
