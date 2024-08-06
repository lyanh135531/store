using System.Linq.Expressions;
using System.Reflection;
using Application.Core.DTOs;

namespace Application.Core.Extensions;

public static class FilterExtension
{
    public static IQueryable<T> ApplyPaginatedFilter<T>(this IQueryable<T> query, PaginatedListQuery queryFilter)
    {
        if (queryFilter.Filters.Count == 0) return query;

        var parameter = Expression.Parameter(typeof(T), "x");

        var combinedExpression = GetExpression<T>(queryFilter, parameter);

        if (combinedExpression is null) return query;

        var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);

        return query.Where(lambda);
    }

    private static Expression GetExpression<T>(PaginatedListQuery queryFilter, ParameterExpression parameter)
    {
        Expression combinedExpression = null;
        foreach (var filter in queryFilter.Filters)
        {
            var property = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(p => string.Equals(p.Name, filter.PropertyName, StringComparison.OrdinalIgnoreCase));
            if (property == null)
            {
                throw new ArgumentException($"Property '{filter.PropertyName}' not found on type '{typeof(T).Name}'");
            }

            var propertyExpression = Expression.Property(parameter, property);
            Expression constantExpression;

            try
            {
                constantExpression = Expression.Constant(Convert.ChangeType(filter.Value, property.PropertyType));
            }
            catch
            {
                throw new ArgumentException(
                    $"Value '{filter.Value}' cannot be converted to type '{property.PropertyType.Name}'");
            }

            Expression comparison;
            switch (filter.Operator)
            {
                case Operator.Equals:
                    comparison = Expression.Equal(propertyExpression, constantExpression);
                    break;
                case Operator.Contains:
                    if (property.PropertyType != typeof(string))
                        throw new ArgumentException("Contains operator can only be used with string properties.");
                    comparison = Expression.Call(propertyExpression,
                        typeof(string).GetMethod("Contains", [typeof(string)]) ?? throw new
                            InvalidOperationException(), constantExpression);
                    break;
                default:
                    throw new NotSupportedException($"Operator '{filter.Operator}' is not supported");
            }

            combinedExpression = combinedExpression == null
                ? comparison
                : Expression.AndAlso(combinedExpression, comparison);
        }

        return combinedExpression;
    }
}