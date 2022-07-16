using DomainModels.Models;
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
    public class BrandController : Controller
    {
        private readonly IRepository<Brand> _repository;

        public BrandController(IRepository<Brand> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();
            var brand = await _repository.DetailsById(id);
            if (brand == null) return NotFound();
            return View(brand);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //if (await _repository.AnyAsync(b => b.Name.ToLower() == brand.Name.ToLower().Trim()))
            //{
            //    ModelState.AddModelError("Name", $"That {brand.Name} Already Exists");
            //    return View();
            //}

            brand.Name = brand.Name.Trim();
            brand.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _repository.AddAsync(brand);


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
        public async Task<IActionResult> Edit(int? id, Brand brand)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(brand);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
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
