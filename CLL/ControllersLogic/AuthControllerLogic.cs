using BLL.Services;
using BLL.Services.TokenService;
using Common.Models;

namespace CLL.ControllersLogic;

public class AuthControllerLogic
{
    private readonly AuthService _authService;
    private readonly ITokenService _tokenService;

    public AuthControllerLogic(AuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    public async Task<Result<string>> TryAuthenticate(string apiKey)
    {
        if (await _authService.AnyAccessByApiKey(apiKey) == false)
            return new();
        
        var id = await _authService.GetAccessByApiKey(apiKey);
        
        var token = _tokenService.GenerateJwtToken(id);
        return new(token);
    }

    public async Task<bool> AnyAdminByAccess(Guid id)
    {
        return await _authService.AnyAdminByAccess(id);
        
        
    }

    public async Task<Guid> GetAdminByAccess(Guid id)
    {
        return await _authService.GetAdminIdByAccess(id);
    }
}