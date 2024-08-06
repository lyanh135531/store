using Application.Core.DTOs;

namespace Application.Core.Extensions;

public static class PaginatedListExtension
{
    public static IQueryable<T> ApplyPaginatedListQuery<T>(this IQueryable<T> source, PaginatedListQuery query)
    {
        return query.Limit == -1 ? source : source.Skip(query.Offset).Take(query.Limit);
    }
}