using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamayun.Infrastucture.Grid
{
    public class GridResult<T> where T : IGridResultModel
    {
        public IEnumerable<T> Data { get; set; }
        public int ItemsCount { get; set; }

    }
}
