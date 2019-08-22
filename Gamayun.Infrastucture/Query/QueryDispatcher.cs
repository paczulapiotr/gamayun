using Gamayun.Infrastucture.Grid;
using System;
using System.Linq;
using System.Reflection;

namespace Gamayun.Infrastucture.Query
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IQueryHandler<TResult, TConfig, TQuery> Resolve<TResult, TConfig, TQuery>() 
            where TConfig : IGridConfiguration
            where TQuery : IQuery

        {
            var queryType = Assembly.GetExecutingAssembly()
               .GetTypes()
               .Where(t => t.GetInterfaces().Contains(typeof(IQueryHandler<TResult, TConfig, TQuery>)))
               .FirstOrDefault();

            return _serviceProvider.GetService(queryType) as IQueryHandler<TResult, TConfig, TQuery>;
        }
    }
}
