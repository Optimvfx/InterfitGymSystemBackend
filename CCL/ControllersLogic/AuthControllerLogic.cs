using System.Security.Claims;
using BLL.Services;
using BLL.Services.TokinService;
using Common.Models;
using DAL.Entities;
using DAL.Entities.Access.AccessType;

namespace CCL.ControllersLogic;

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
        var terminal = new Terminal();

        if (terminal == null)
            return new();
        
        var token = _tokenService.GenerateJwtToken(terminal);
        return new(token);
    }
    
}