using Gamayun.Infrastucture.Grid;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gamayun.Infrastucture.Query
{
    public interface IQueryHandler<TResult, TConfig, TQuery> 
        where TConfig : IGridConfiguration 
        where TQuery : IQuery
    {
        IEnumerable<TResult> Execute(TConfig config, TQuery query);
    }
}
