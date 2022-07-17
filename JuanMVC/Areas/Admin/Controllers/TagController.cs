using DomainModels.Models.Common;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class TagController : Controller
    {
        private readonly IRepository<Tag> _repository;

        public TagController(IRepository<Tag> repository)
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
            var product = await _repository.DetailsById(id);
            if (product == null) return NotFound();
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View(await _repository.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _repository.AddAsync(tag);
            if (!result) return BadRequest("Something bad happenes");
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var tag = await _repository.DetailsById(id);

            if (tag == null) return NotFound();

            return View(tag);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Tag tag)
        {
            if (id != tag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(tag);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _repository.DetailsById(id);

            tag.IsDeleted = true;

            tag.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _repository.DeleteAsync(tag);

            return RedirectToAction(nameof(Index));
        }
    }
}
