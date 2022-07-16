using DomainModels.Models.BlogModel;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index(int page=1,int take=2)
        {
            ViewBag.Take = take;
            List<Blog> blog =await _blogService.GetBlogs();
            Pagination<Blog> pagination = new Pagination<Blog>(blog, page, take);
            return View(pagination);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) BadRequest();
            Blog blog =await  _blogService.GetBlogById(id);
            if (blog == null) NotFound();
            return View(blog);
        }
    }
}
