using DomainModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Repository.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SliderController : Controller
    {
        public readonly IRepository<Slider> _repository;

        public SliderController(IRepository<Slider> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync()); ;
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();
            var slider = await _repository.DetailsById(id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
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

            slider.MainTitle = slider.MainTitle.Trim();
            slider.SubTitle = slider.SubTitle.Trim();
            slider.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _repository.AddAsync(slider);


            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var slider = await _repository.DetailsById(id);

            if (slider == null) return NotFound();

            return View(slider);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Slider slider)
        {
            if (id != slider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(slider);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var slider = await _repository.DetailsById(id);

            slider.IsDeleted = true;

            slider.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _repository.DeleteAsync(slider);

            return RedirectToAction(nameof(Index));
        }
    }
}
