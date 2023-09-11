using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Common.Extensions;

public static class IQuerableNothingExtensions
{
    public static bool Nothing<T>(this IQueryable<T> queryable)
    {
        return queryable.Any() == false;
    }
    public static async Task<bool> NothingAsync<T>(this IQueryable<T> queryable)
    {
        return await queryable.AnyAsync() == false;
    }
}   