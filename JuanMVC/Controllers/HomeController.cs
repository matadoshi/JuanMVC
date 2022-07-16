using AutoMapper;
using DomainModels.Models;
using DomainModels.Models.BlogModel;
using DomainModels.Models.Common;
using JuanMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Interfaces;
using Service.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IProductService _productService;
        private readonly IBlogService _blogService;

        public HomeController(ISliderService sliderService, IProductService productService, IBlogService blogService)
        {
            _sliderService = sliderService;
            _productService = productService;
            _blogService = blogService;

        }

        public async Task<IActionResult> Index()
        {
            List<Slider> slider =await _sliderService.GetSliders();
            List<Product> products = await _productService.GetProducts();
            Pagination<Product> paginationProduct = new Pagination<Product>(products, 1, 5);
            Pagination<Blog> paginationBlog = new Pagination<Blog>(await _blogService.GetLatestBlog(), 1, 4);
            HomeVM homeVM = new HomeVM()
            {
                Sliders = slider,
                Products = paginationProduct,
                Blogs=paginationBlog
            };
            return View(homeVM);
        }
    }
}
