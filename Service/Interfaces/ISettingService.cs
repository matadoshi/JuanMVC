using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface ISettingService
    {
        Dictionary<string, string> GetSettings();
    }
}
