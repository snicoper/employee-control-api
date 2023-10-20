using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.Json;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Common.EntityFramework.Filter;

public static class QueryableFilterExtensions
{
    public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> source, RequestData request)
    {
        if (string.IsNullOrEmpty(request.Filters))
        {
            return source;
        }

        var query = new StringBuilder();
        var itemsFilter = JsonSerializer.Deserialize<List<RequestFilter>>(request.Filters)?.ToArray() ??
                          Array.Empty<RequestFilter>();

        if (itemsFilter.Length == 0)
        {
            return source;
        }

        var values = itemsFilter
            .Select(filter => filter.RelationalOperator == FilterOperator.Contains
                ? filter.Value?.ToLower()
                : filter.Value);

        for (var position = 0; position < itemsFilter.Length; position++)
        {
            ComposeQuery(itemsFilter[position], query, position);
        }

        source = source.Where(query.ToString(), values);

        return source;
    }

    private static void ComposeQuery(RequestFilter filter, StringBuilder query, int valuePosition)
    {
        var relationalOperator = FilterOperator.GetRelationalOperator(filter.RelationalOperator.ToEmptyIfNull());
        var logicalOperator = !string.IsNullOrEmpty(filter.LogicalOperator)
            ? FilterOperator.GetLogicalOperator(filter.LogicalOperator)
            : string.Empty;

        query.Append(
            filter.RelationalOperator != FilterOperator.Contains
                ? $"{logicalOperator} {filter.PropertyName} {relationalOperator} @{valuePosition}"
                : $"{logicalOperator} {string.Format(filter.PropertyName + relationalOperator, valuePosition)}");
    }
}
