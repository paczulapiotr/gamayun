using System.Collections.Generic;

namespace Gamayun.Infrastucture.Grid
{
    public interface IGridConfiguration
    {
        string GridSelector { get; }
        IEnumerable<GridProperty> GetGridProperties();
    }
}
