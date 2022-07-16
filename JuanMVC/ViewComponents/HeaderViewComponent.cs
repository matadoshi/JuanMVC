using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly ISettingService _settingService;

        public HeaderViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IDictionary<string, string> settings = _settingService.GetSettings();
            return await (Task.FromResult(View(settings)));
        }
    }
}
