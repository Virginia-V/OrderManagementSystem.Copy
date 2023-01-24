using Microsoft.EntityFrameworkCore;
using OMS.Common.Models.PagedRequest;
using System.Linq.Dynamic.Core;
using System.Text;

namespace OMS.Dal.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PagedRequest pagedRequest)
        {
            var entities = query.Skip(pagedRequest.PageIndex * pagedRequest.PageSize).Take(pagedRequest.PageSize);
            return entities;
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> query, PagedRequest pagedRequest)
        {
            if (!string.IsNullOrWhiteSpace(pagedRequest.ColumnNameForSorting))
            {
                query = query.OrderBy(pagedRequest.ColumnNameForSorting + " " + pagedRequest.SortDirection);
            }
            return query;
        }

        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, PagedRequest pagedRequest)
        {
            var predicate = new StringBuilder();
            var requestFilters = pagedRequest.RequestFilters;

            for (int i = 0; i < requestFilters.Filters.Count; i++)
            {
                if (i > 0)
                {
                    predicate.Append($"{requestFilters.LogicalOperator}");
                }
                predicate.Append(requestFilters.Filters[i].Path + $".{nameof(string.Contains)}(@{i})");
            }

            if (requestFilters.Filters.Any())
            {
                var propertyValues = requestFilters.Filters.Select(filter => filter.Value).ToArray();
                var predicateToString = predicate.ToString();
                query = query.Where(predicateToString, propertyValues);
            }
            return query;
        }
    }
}
