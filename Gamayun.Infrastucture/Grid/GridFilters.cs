using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.Infrastucture.Grid
{
    public class GridFilters<T> where T : IGridResultModel, new()
    {
        public T Filters { get; set; } = new T();
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public string SortOrder { get; set; }
    }
}
