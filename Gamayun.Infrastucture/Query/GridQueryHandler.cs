using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gamayun.Infrastucture.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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
        private readonly MapperConfiguration _mapperConfiguration;

        public GridQueryHandler(GamayunDbContext dbContext, 
            MapperConfiguration mapperConfiguration)
        {
            _dbContext = dbContext;
            _mapperConfiguration = mapperConfiguration;
        }

        public abstract GridResult<TResult> Execute(GridFilters<TResult> filters, TQuery query);

        protected IEnumerable<T> ApplyPagination<T>(GridFilters<TResult> filters, IEnumerable<T> array)
            => array.Skip((filters.PageIndex - 1) * filters.PageSize).Take(filters.PageSize).ToList();

        protected GridResult<TResult> Result<T>(GridFilters<TResult> filters, IQueryable<T> query)
        {
            var props = typeof(TResult).GetProperties();
            var filterExpressions = GenerateFilterExpressions(filters.Filters, props);

            var qry = query.ProjectTo<TResult>(_mapperConfiguration);
            foreach (var exp in filterExpressions)
            {
                qry = qry.Where(exp);
            }
            qry = ApplyOrder(filters, qry);

            return new GridResult<TResult>
            {
                ItemsCount = qry.Count(),
                Data = ApplyPagination(filters, qry)
            };
        }

        protected IQueryable<T> ApplyOrder<T>(GridFilters<TResult> filters, IQueryable<T> query)
        {
            string methodName;
            if (filters.SortOrder?.ToLower() == "asc")
                methodName = nameof(Queryable.OrderBy);
            else if (filters.SortOrder?.ToLower() == "desc")
                methodName = nameof(Queryable.OrderByDescending);
            else
                return query;

            var elementType = typeof(T);
            var parameterExpression = Expression.Parameter(elementType);
            var propertyExpression = Expression.Property(parameterExpression, filters.SortField);
            var selector = Expression.Lambda(propertyExpression, parameterExpression);
            
            var orderByExpression = Expression.Call(
                typeof(Queryable), 
                methodName, 
                new[] { elementType, propertyExpression.Type },
                query.Expression,
                selector);

            return query.Provider.CreateQuery<T>(orderByExpression);
        }

        private IEnumerable<Expression<Func<TResult, bool>>> GenerateFilterExpressions(TResult filter, PropertyInfo[] props)
        {
            var filterExpressions = new List<Expression<Func<TResult, bool>>>();
            Expression<Func<TResult, bool>> expr;
            foreach (var prop in props)
            {
                if (prop.Name == nameof(IGridResultModel.Id))
                    continue;

                if (prop.PropertyType == typeof(string))
                {
                    expr = StringFilter(filter, prop);
                    if (expr == null)
                        continue;
                    filterExpressions.Add(expr);
                }
                else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                {
                    expr = IntegerFilter(filter, prop);
                    if (expr == null)
                        continue;
                    filterExpressions.Add(expr);
                }
                else if (prop.PropertyType == typeof(double) || prop.PropertyType == typeof(double?))
                {
                    expr = DoubleFilter(filter, prop);
                    if (expr == null)
                        continue;
                    filterExpressions.Add(expr);
                }
            }

            return filterExpressions;
        }

        protected Expression<Func<TResult, bool>> IntegerFilter(TResult filter, PropertyInfo propertyInfo)
            => NumericFilter<int>(filter, propertyInfo);

        protected Expression<Func<TResult, bool>> DoubleFilter(TResult filter, PropertyInfo propertyInfo)
           => NumericFilter<double>(filter, propertyInfo);

        protected Expression<Func<TResult, bool>> NumericFilter<T>(TResult filter, PropertyInfo propertyInfo)
            where T: struct
        {
            var value = propertyInfo.GetValue(filter);
            if (value == null)
                return null;
            var parameterExp = Expression.Parameter(typeof(TResult));
            var propertyExp = Expression.Property(parameterExp, propertyInfo.Name);
            var filterValue = Expression.Constant(value, typeof(T));
            var expression = Expression.MakeBinary(ExpressionType.Equal, propertyExp, filterValue);

            return Expression.Lambda<Func<TResult, bool>>(expression, parameterExp);
        }

        protected Expression<Func<TResult, bool>> StringFilter(TResult filter, PropertyInfo propertyInfo)
        {
            var value = propertyInfo.GetValue(filter);
            if (string.IsNullOrWhiteSpace(value as string))
                return null;
            var parameterExp = Expression.Parameter(typeof(TResult));
            var propertyExp = Expression.Property(parameterExp, propertyInfo.Name);
            var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var someValue = Expression.Constant(value, typeof(string));
            var expression = Expression.Call(propertyExp, method, someValue);

            return Expression.Lambda<Func<TResult,bool>>(expression, parameterExp);
        }
    
    }
}
