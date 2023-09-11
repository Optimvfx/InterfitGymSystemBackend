using AutoMapper;
using BLL.Services.TimeService;
using Common.Exceptions.General;
using Common.Extensions;
using Common.Helpers;
using DAL;
using DAL.Entities;
using DAL.Entities.Access;
using DAL.Entities.Access.AccessType;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

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
            throw new NotFoundException();
            
        var apiKey = await _db.Keys  
            .Include(k => k.Access)
            .FirstAsync(k => k.Key == key);

        return apiKey.AccessId;
    }

    public async Task<bool> AnyAdminByAccess(Guid id)
    {
        if (await _db.Accesses.NothingByIdAsync(id))
            throw new NotFoundException();

        var access = await _db.Accesses.GetByIdAsync(id);

        return await _db.ApiAdministrators.AnyByIdAsync(access.TypeId);
    }

    public async Task<Guid> GetAdminIdByAccess(Guid id)
    {
        if (await _db.Accesses.NothingByIdAsync(id))
            throw new NotFoundException();

        var access = await _db.Accesses.GetByIdAsync(id);

        return (await _db.ApiAdministrators.GetByIdAsync(access.TypeId)).Id;
    }
}