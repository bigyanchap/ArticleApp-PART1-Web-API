using BilbaLeaf.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BilbaLeaf.Service.Extension
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, QueryObject queryObject, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (queryObject.IsSortAsc)
            {
                return query.OrderBy(columnsMap[queryObject.SortBy]).Skip((queryObject.Page-1) * queryObject.PageSize).Take(queryObject.PageSize);
            }
            else
            {
                return query.OrderByDescending(columnsMap[queryObject.SortBy]).Skip((queryObject.Page - 1) * queryObject.PageSize).Take(queryObject.PageSize);
            }
        }

        public static IQueryable<T> ApplyOrderingBool<T>(this IQueryable<T> query, QueryObject queryObject, Dictionary<string, Expression<Func<T, bool>>> columnsMap)
        {
            if (queryObject.IsSortAsc)
            {
                return query.OrderBy(columnsMap[queryObject.SortBy]).Skip((queryObject.Page - 1) * queryObject.PageSize).Take(queryObject.PageSize);
            }
            else
            {
                return query.OrderByDescending(columnsMap[queryObject.SortBy]).Skip((queryObject.Page - 1) * queryObject.PageSize).Take(queryObject.PageSize);
            }
        }

    }
}
