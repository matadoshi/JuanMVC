using DomainModels.Models.BlogModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IBlogService
    {
        Task<List<Blog>> GetBlogs();
        Task<Blog> GetBlogById(int? id);
        Task<List<Blog>> GetLatestBlog();
    }
}
