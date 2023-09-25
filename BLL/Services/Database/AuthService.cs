using Common.Exceptions.General;
using Common.Extensions;
using DAL;
using DAL.Entities.Access.AccessType;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services.Database;

public class AuthService
{
    private readonly ApplicationDbContext _db;

    public AuthService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<bool> AnyAccessByApiKey(string key)
    {
        return await _db.Keys.AnyAsync(k => k.Key == key);
    }

    public async Task<Guid> GetAccessByApiKey(string key)
    {
        if (!await AnyAccessByApiKey(key))
            throw new NotFoundException("Key");
            
        var apiKey = await _db.Keys  
            .Include(k => k.Access)
            .FirstAsync(k => k.Key == key);

        return apiKey.AccessId;
    }

    public async Task<bool> AnyAdminByAccess(Guid id)
    {
        return await _db.ApiAdministrators.AnyByIdAsync(id);
    }

    public async Task<bool> AnyTerminalByAccess(Guid id)
    {
        return await _db.Terminals.AnyByIdAsync(id);
    }

    public async Task<Terminal> GetTerminalByAccess(Guid id)
    {
        if (await AnyTerminalByAccess(id) == false)
            throw new NotFoundException(typeof(Terminal), id);

        return await _db.Terminals
            .AsNoTracking()
            .GetByIdAsync(id);
    }
}