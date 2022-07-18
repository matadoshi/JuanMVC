using DomainModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Service.Interfaces;
using Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int take = 3, int page = 1)
        {
            ViewBag.Take = take;
            IList<Product> products = await _productService.GetProducts();
            Pagination<Product> pagination = new Pagination<Product>(products, page,take);
            return View(pagination);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();
            Product product =await _productService.GetProductById(id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}
