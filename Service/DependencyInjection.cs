using Microsoft.Extensions.DependencyInjection;
using Service.BaseModels;
using Service.Interfaces;
using Service.Setting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IOrderService, OrderService>();
            //services.AddScoped<IBasketService, BasketService>();
            return services;
        }
    }
}
