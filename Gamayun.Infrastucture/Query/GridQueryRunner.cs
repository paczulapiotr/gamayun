using System;
using Gamayun.Infrastucture.Grid;
using Microsoft.Extensions.DependencyInjection;

namespace Gamayun.Infrastucture.Query
{
    public class GridQueryRunner : IGridQueryRunner
    {
        private readonly IServiceProvider _serviceProvider;

        public GridQueryRunner(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public GridResult<TResult> Run<TResult, TQuery>(GridFilters<TResult> filters, TQuery query) 
            where TResult : IGridResultModel, new()
            where TQuery : IGridQuery
            => _serviceProvider.GetService<IGridQueryHandler<TResult, TQuery>>().Execute(filters, query);
    }
}
