using System.Collections.Generic;

namespace Gamayun.Infrastucture.Grid
{
    public interface IGridConfiguration
    {
        string GridSelector { get; }
        string SelectHref { get; }
        bool Selectable { get; } 
        IEnumerable<GridProperty> GetGridProperties();
    }
}
