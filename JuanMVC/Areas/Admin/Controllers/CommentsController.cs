using DomainModels.Models.BlogModel;
using DomainModels.Models.Common;
using JuanMVC.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repository.DAL;
using Repository.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.Areas.Admin.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentRepository _repository;
        private readonly IRepository<Blog> _blogRepository;
        public CommentsController(ICommentRepository repository,
                                  IRepository<Blog> blogRepository)
        {
            _repository = repository;
            _blogRepository = blogRepository;
        }

        public IActionResult Index()
        {
            return View(_repository.GetSomethingInclude()); ;
        }

        public async Task<IActionResult> Create()
        {
            return View(await _blogRepository.GetAllAsync());
        }

        public async Task<IActionResult> Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.AddAsync(comment);
                if (!result) return BadRequest("Something bad happenes");
                return RedirectToAction(nameof(Index));
            }
            CommentVM commentVM = new CommentVM
            {
                Comment = comment,
                Blogs = await _blogRepository.GetAllAsync()
            };
            return View(commentVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var comment = _repository.DetailsById(id);
            if (comment == null)
            {
                return NotFound();
            }

            CommentVM commentVM = new CommentVM
            {
                Comment = await comment,
                Blogs = await _blogRepository.GetAllAsync()
            };
            return View(commentVM);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _repository.DetailsById(id);
            if (comment == null)
            {
                return NotFound();
            }
            if (await _repository.DeleteComment(comment))
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }
    }
}
