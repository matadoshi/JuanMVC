using DomainModels.Models;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.BaseModels
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        public SliderService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Slider>> GetSliders()
        {
            try
            {
                List<Slider> sliders = await _context.Sliders
                      .Where(c => !c.IsDeleted)
                      .OrderByDescending(t => t.Id)
                      .ToListAsync();
                return sliders;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
