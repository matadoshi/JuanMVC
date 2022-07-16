using AutoMapper;
using DomainModels.Models;
using JuanMVC.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public readonly IRepository<Product> _repository;
        public readonly IRepository<ProductCategory> _categoryRepository;
        public readonly IRepository<Brand> _brandRepository;
        public readonly IMapper _mapper;

        public ProductController(IRepository<Product> repository,
                                    IMapper mapper,
                                    IRepository<ProductCategory> categoryRepository,
                                    IRepository<Brand> brandRepository
                                    )
        {
            _repository = repository;
            _mapper = mapper;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
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
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var product = await _repository.DetailsById(id);

            if (product == null) return NotFound();

            return View(product);
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