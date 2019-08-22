using Gamayun.Infrastucture.Grid;

namespace Gamayun.Infrastucture.Query
{
    public interface IQueryDispatcher
    {
        IQueryHandler<TResult, TConfig, TQuery> Resolve<TResult, TConfig, TQuery>()
          where TConfig : IGridConfiguration
          where TQuery : IQuery;
    }
}
