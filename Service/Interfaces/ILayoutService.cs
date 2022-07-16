using DomainModels.Models.Common;
using Service.ViewModels.Basket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ILayoutService
    {
        Task<IDictionary<string,string>> GetSettingsAsync();
        Task<List<BasketVM>> GetBasketAsync();
        Task<AppUser> GetUserAsync();
    }
}
