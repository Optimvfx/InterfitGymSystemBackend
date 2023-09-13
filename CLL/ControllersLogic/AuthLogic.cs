using BLL.Services;
using BLL.Services.TokenService;
using CLL.ControllersLogic.Interface;
using Common.Models;

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

    public async Task<Result<Guid>> TryGetAdminIdByAccess(Guid id)
    {
        if (await _authService.AnyAdminByAccess(id) == false)
            return false;

        var admin = await _authService.GetAdminIdByAccess(id);
        return new Result<Guid>(admin);
    }
}