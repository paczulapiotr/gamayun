using Gamayun.Infrastucture.Grid;
using System.Collections.Generic;
using System.Linq;

namespace Gamayun.Infrastucture.Query
{
    public interface IGridQueryHandler<TResult, TQuery>
        where TResult : IGridResultModel, new()
        where TQuery : IGridQuery
    {
        GridResult<TResult> Execute(GridFilters<TResult> filters, TQuery query);
    }

    public abstract class GridQueryHandler<TResult, TQuery> : IGridQueryHandler<TResult, TQuery>
        where TResult : IGridResultModel, new()
        where TQuery : IGridQuery
    {
        protected readonly GamayunDbContext _dbContext;

        public GridQueryHandler(GamayunDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public abstract GridResult<TResult> Execute(GridFilters<TResult> filters, TQuery query);

        protected IEnumerable<T> ApplyPagination<T>(GridFilters<TResult> filters, IEnumerable<T> array)
            => array.Skip((filters.PageIndex - 1) * filters.PageSize).Take(filters.PageSize);
    }
}
