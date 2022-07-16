using DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ISliderService
    {
        public Task<List<Slider>> GetSliders();
    }
}
