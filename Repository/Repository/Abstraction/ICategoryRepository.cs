using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Abstraction
{
    public interface ICategoryRepository
    {
        Task<bool> ExistsCategory(string name);
    }
}
