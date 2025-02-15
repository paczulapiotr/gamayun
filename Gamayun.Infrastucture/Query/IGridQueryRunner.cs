﻿using Gamayun.Infrastucture.Grid;

namespace Gamayun.Infrastucture.Query
{
    public interface IGridQueryRunner
    {
        GridResult<TResult> Run<TResult, TQuery>(GridFilters<TResult> filters, TQuery query)
          where TResult : IGridResultModel, new()
          where TQuery : IGridQuery;
    }
}
