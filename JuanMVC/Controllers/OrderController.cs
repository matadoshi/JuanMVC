using DomainModels.Models;
using DomainModels.Models.Common;
using DomainModels.Models.OrderModel;
using JuanMVC.DomainModels.Enums;
using JuanMVC.ViewModels.AccountVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interfaces;
using Service.ViewModels.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(IProductService productService, UserManager<AppUser> userManager,IOrderService orderService)
        {
            _userManager = userManager;
            _productService = productService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            string basket = Request.Cookies["basket"];

            List<BasketVM> basketVMs = null;

            if (!string.IsNullOrWhiteSpace(basket))
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

                foreach (BasketVM basketVM in basketVMs)
                {
                    Product product = await _productService.GetProductById(basketVM.ProductId);

                    basketVM.Name = product.Name;
                    basketVM.Price = product.DiscountPrice > 0 ? product.DiscountPrice : product.Price;
                }
            }
            else
            {
                return BadRequest();
            }

            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            OrderVM orderVM = new OrderVM
            {
                Order = new Order
                {
                    Name = appUser.Name,
                    Surname = appUser.Surname,
                    Email = appUser.Email
                },
                BasketVMs = basketVMs
            };

            return View(orderVM);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            string basket = Request.Cookies["basket"];

            List<BasketVM> basketVMs = null;

            if (!string.IsNullOrWhiteSpace(basket))
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                List<OrderItem> orderItems = new List<OrderItem>();

                foreach (BasketVM basketVM in basketVMs)
                {
                    Product product = await _productService.GetProductById(basketVM.ProductId);

                    OrderItem orderItem = new OrderItem
                    {
                        Price = product.DiscountPrice > 0 ? product.DiscountPrice : product.Price,
                        Count = basketVM.Count,
                        ProductId = product.Id,
                        TotalPrice = basketVM.Count * (product.DiscountPrice > 0 ? product.DiscountPrice : product.Price)
                    };

                    orderItems.Add(orderItem);
                }

                order.OrderItems = orderItems;
            }
            else
            {
                return BadRequest();
            }

            order.OrderStatus = OrderStatus.Pending;
            order.Date = DateTime.UtcNow.AddHours(4);
            order.AppUserId = appUser.Id;
            order.TotalPrice = order.OrderItems.Sum(x => x.TotalPrice);

            if (await _orderService.SaveChanges(order))
            {
                Response.Cookies.Delete("Basket");

                return RedirectToAction("Index", "Home");
            }
            return BadRequest();

            
        }
    }
}
