using DomainModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.DAL;
using Service.Interfaces;
using Service.ViewModels.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.Controllers
{
    public class BasketController : Controller
    {
        private readonly IProductService _productService;

        public BasketController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            string basket = Request.Cookies["Basket"];

            List<BasketVM> basketVMs = null;

            if (!string.IsNullOrWhiteSpace(basket))
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }
            else
            {
                basketVMs = new List<BasketVM>();
            }

            return View(await _getBasketAsync(basket));
        }

        public async Task<IActionResult> AddToBasket(int? id)
        {
            if (id == null) return BadRequest();

            Product product =await _productService.GetProductById(id);

            if (product == null) return NotFound();

            List<BasketVM> basketVMs = null;

            string cookie = HttpContext.Request.Cookies["Basket"];

            if (!string.IsNullOrWhiteSpace(cookie))
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookie);

                if (basketVMs.Exists(b => b.ProductId == id))
                {
                    basketVMs.Find(b => b.ProductId == id).Count++;
                }
                else
                {
                    BasketVM basketVM = new BasketVM
                    {
                        ProductId = (int)id,
                        Count = 1
                    };

                    basketVMs.Add(basketVM);
                }
            }
            else
            {
                basketVMs = new List<BasketVM>();

                BasketVM basketVM = new BasketVM
                {
                    ProductId = (int)id,
                    Count = 1
                };

                basketVMs.Add(basketVM);
            }

            string item = JsonConvert.SerializeObject(basketVMs);

            HttpContext.Response.Cookies.Append("Basket", item);

            return PartialView("_BasketPartial", await _getBasketAsync(item));
        }

        public async Task<IActionResult> DeleteFromBasket(int? id)
        {
            if (id == null) return BadRequest();

            if (await _productService.ExistsProduct(id)) return NotFound();

            string cookie = HttpContext.Request.Cookies["Basket"];

            if (!string.IsNullOrWhiteSpace(cookie))
            {
                List<BasketVM> basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookie);

                BasketVM basketVM = basketVMs.FirstOrDefault(b => b.ProductId == id);

                if (basketVM == null) return NotFound();

                basketVMs.Remove(basketVM);

                cookie = JsonConvert.SerializeObject(basketVMs);

                HttpContext.Response.Cookies.Append("Basket", cookie);

                return PartialView("_BasketPartial", await _getBasketAsync(cookie));
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<List<BasketVM>> _getBasketAsync(string cookie)
        {
            List<BasketVM> basketVMs = null;

            if (!string.IsNullOrWhiteSpace(cookie))
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookie);
            }
            else
            {
                basketVMs = new List<BasketVM>();
            }

            foreach (BasketVM basketVM in basketVMs)
            {
                Product product = await _productService.GetProductById(basketVM.ProductId);

                basketVM.Image = product.MainImage;
                basketVM.Price = product.DiscountPrice > 0 ? product.DiscountPrice : product.Price;
                basketVM.Name = product.Name;
            }

            return basketVMs;
        }
    }

}
