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

        var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
        var itemsFilter = JsonSerializer
            .Deserialize<List<QueryableFilter>>(request.Filters, options)?
            .ToArray() ?? [];

        if (itemsFilter.Length == 0)
        {
            return source;
        }

        var values = itemsFilter
            .Select(
                filter => filter.RelationalOperator == FilterOperator.Contains
                    ? filter.Value?.ToLower()
                    : filter.Value)
            .ToDynamicArray();

        var query = new StringBuilder();
        for (var position = 0; position < itemsFilter.Length; position++)
        {
            query = ComposeQuery(itemsFilter[position], query, position);
        }

        source = source.Where(query.ToString(), values);

        return source;
    }

    private static StringBuilder ComposeQuery(QueryableFilter filter, StringBuilder query, int valuePosition)
    {
        var propertyName = PropertyNameCaseUpper(filter.PropertyName);
        var relationalOperator = FilterOperator.GetRelationalOperator(filter.RelationalOperator ?? string.Empty);

        var logicalOperator = !string.IsNullOrEmpty(filter.LogicalOperator)
            ? FilterOperator.GetLogicalOperator(filter.LogicalOperator)
            : string.Empty;

        // Comprobar si es un operador de string o lógico.
        var filterResult = filter.RelationalOperator != FilterOperator.Contains &&
                           filter.RelationalOperator != FilterOperator.StartsWith &&
                           filter.RelationalOperator != FilterOperator.EndsWith
            ? $"{logicalOperator} {propertyName} {relationalOperator} @{valuePosition}"
            : $"{logicalOperator} {string.Format(propertyName + relationalOperator, valuePosition)}";

        query.Append(filterResult);

        return query;
    }

    private static string PropertyNameCaseUpper(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return string.Empty;
        }

        var propertyNameParts = propertyName.Split('.');

        var propertyNameResult = propertyNameParts
            .Aggregate(string.Empty, (current, part) => current + $"{part.UpperCaseFirst()}.");

        return propertyNameResult.TrimEnd('.');
    }
}
