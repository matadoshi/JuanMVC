using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Utilities
{
    public class Pagination<T>
    {
        public List<T> Datas { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
        public Pagination(List<T> datas, int pageNumber, int itemCount)
        {
            this.Datas = datas.Skip((pageNumber-1)*itemCount).Take(itemCount).ToList();
            this.CurrentPage = pageNumber;
            this.TotalPage= (int)Math.Ceiling((decimal)datas.Count() / itemCount);
            this.HasNext = CurrentPage < TotalPage;
            this.HasPrevious = CurrentPage > 1;
        }
    }
}
