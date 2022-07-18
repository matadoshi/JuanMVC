using AutoMapper;
using DomainModels.Models;
using JuanMVC.Areas.Admin.ViewModels;
using JuanMVC.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Repository.Abstraction;
using Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _repository;
        private readonly IRepository<ProductCategory> _categoryRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public ProductController(IRepository<Product> repository,
                                    IMapper mapper,
                                    IRepository<ProductCategory> categoryRepository,
                                    IRepository<Brand> brandRepository,
                                    IWebHostEnvironment env
                                    )
        {
            _repository = repository;
            _mapper = mapper;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _env = env;
        }
        public async Task<IActionResult> Index(int take = 3, int page = 1)
        {
            ViewBag.Take = take;
            var products = await _repository.GetAllAsync();
            Pagination<Product> pagination = new Pagination<Product>(products, page, take);
            return View(pagination);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();
            var product = await _repository.DetailsById(id);
            if (product == null) return NotFound();
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ProductPostViewModel model = new ProductPostViewModel
            {
                Categories = await _categoryRepository.GetAllAsync(),
                Brands = await _brandRepository.GetAllAsync()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _repository.AddAsync(product);
            if (!result) return BadRequest("Something bad happenes");
            if (product.MainFile != null)
            {
                if (product.MainFile.CheckFileContentType("image/jpeg"))
                {
                    ModelState.AddModelError("MainFile", "Please Select Correct Image Type. Must Be .jpg or .jpeg");
                    return View();
                }

                if (product.MainFile.CheckFileSize(50))
                {
                    ModelState.AddModelError("MainFile", "Please Select Correct Image Size. Must Be Max 50kb");
                    return View();
                }

                product.MainImage = await product.MainFile.CreateFileAsync(_env, "assets", "img", "product");
            }
            else
            {
                ModelState.AddModelError("MainFile", "Please Select Main Image");
                return View();
            }

            if (product.SecondFile != null)
            {
                if (product.SecondFile.CheckFileContentType("image/jpeg"))
                {
                    ModelState.AddModelError("SecondFile", "Please Select Correct Image Type. Must Be .jpg or .jpeg");
                    return View();
                }

                if (product.SecondFile.CheckFileSize(50))
                {
                    ModelState.AddModelError("SecondFile", "Please Select Correct Image Size. Must Be Max 50kb");
                    return View();
                }

                product.SecondImage = await product.SecondFile.CreateFileAsync(_env, "assets", "img", "product");
            }
            else
            {
                ModelState.AddModelError("SecondFile", "Please Select Second Image");
                return View();
            }

            if (product.Files != null && product.Files.Count > 0)
            {
                if (product.Files.Count > 5)
                {
                    ModelState.AddModelError("Files", "Can Select Maximum 5 Image");
                    return View();
                }

                List<ProductImage> productImages = new List<ProductImage>();

                foreach (IFormFile file in product.Files)
                {
                    if (file.CheckFileContentType("image/jpeg"))
                    {
                        ModelState.AddModelError("Files", "Please Select Correct Image Type. Must Be .jpg or .jpeg");
                        return View();
                    }

                    if (file.CheckFileSize(300))
                    {
                        ModelState.AddModelError("Files", "Please Select Correct Image Size. Must Be Max 50kb");
                        return View();
                    }

                    ProductImage productImage = new ProductImage
                    {
                        Image = await file.CreateFileAsync(_env, "assets", "img", "product")
                    };

                    productImages.Add(productImage);
                }

                product.ProductImages = productImages;
            }
            else
            {
                ModelState.AddModelError("Files", "Please Select Files");
                return View();
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var product = await _repository.DetailsById(id);

            if (product == null) return NotFound();
            ProductPostViewModel model = new ProductPostViewModel
            {
                Categories = await _categoryRepository.GetAllAsync(),
                Brands = await _brandRepository.GetAllAsync(),
                Product = product
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _repository.DetailsById(id);

            product.IsDeleted = true;

            product.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _repository.DeleteAsync(product);

            return RedirectToAction(nameof(Index));
        }
    }
}