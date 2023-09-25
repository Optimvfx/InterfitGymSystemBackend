using BLL.Services;
using BLL.Services.Database;
using BLL.Services.TokenService;
using CLL.ControllersLogic.Interface;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Models;
using DAL.Entities.Access.AccessType;

namespace CLL.ControllersLogic;

public class AuthLogic : IAuthLogic
{
    private readonly AuthService _authService;
    private readonly ITokenService _tokenService;

    public AuthLogic(AuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    public async Task<Result<string>> TryGetJwt(string apiKey)
    {
        if (await _authService.AnyAccessByApiKey(apiKey) == false)
            return new();
        
        var id = await _authService.GetAccessByApiKey(apiKey);
        
        var token = _tokenService.GenerateJwtToken(id);
        return new(token);
    }

    public async Task<bool> AccessIsAdmin(Guid id)
    {
        if (await _authService.AnyAdminByAccess(id) == false)
            return false;

        return true;
    }

    public async Task<Result<Guid>> TryGetGymIdByAccess(Guid id)
    {
        if (await _authService.AnyTerminalByAccess(id) == false)
            return false;

        Terminal terminal = await _authService.GetTerminalByAccess(id);
        return new Result<Guid>(terminal.GymId);
    }
}