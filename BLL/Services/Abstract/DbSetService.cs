using Common.Extensions;
using DAL;
using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services.Abstract;

public abstract class DbSetService<T>
    where T : class, IIndexSearchable
{
    private readonly ApplicationDbContext _db;

    protected ApplicationDbContext Db => _db;
    
    protected DbSetService(ApplicationDbContext db)
    {
        _db = db;
    }

    public IQueryable<T> GetAll() => GetDbSet().AsNoTracking();
    public async Task<T> GetAsync(Guid id) => await GetDbSet().AsNoTracking().GetByIdAsync(id); 

    public async Task<bool> AnyAsync(Guid id) => await GetDbSet().AnyByIdAsync(id);

    protected async Task<T> GetAsTrackingAsync(Guid id) => await GetDbSet().GetByIdAsync(id);
    
    protected async Task DeleteAsync(Guid id)
    {
        var entity = await GetAsync(id);

        GetDbSet().Remove(entity);
        _db.SaveChangesAsync();
    }

    protected async Task<Guid> InnerAdd(T value)
    {
        await _db.AddAsync(value);
        await _db.SaveChangesAsync();
        return value.Id;
    }
    
    private DbSet<T> GetDbSet() => GetDbSet(_db);
    
    protected abstract DbSet<T> GetDbSet(ApplicationDbContext dbContext);
}