using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Common.Extensions;

public static class IQuerableIdExtensions
{
    public static async Task<T> GetByIdAsync<T>(this IQueryable<T> queryable, Guid id)
        where T : IIndexSearchable
    {
        return await queryable.FirstAsync(e => e.Id == id);
    }
    
    public static async Task<bool> AnyByIdAsync<T>(this IQueryable<T> queryable, Guid id)
        where T : IIndexSearchable
    {
        return await queryable.AnyAsync(e => e.Id == id);
    }
    
    public static async Task<bool> NothingByIdAsync<T>(this IQueryable<T> queryable, Guid id)
        where T : IIndexSearchable
    {
        return await AnyByIdAsync(queryable, id) == false;
    }
}   