using System.Security.Claims;
using BLL.Services.DataCoder;
using CLL.ControllersLogic;
using Common.Helpers;
using GymCarSystemBackend.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace GymCarSystemBackend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthApiController : BaseController
{
    private readonly AuthControllerLogic _authControllerLogic;

    public AuthApiController(IDataCoder<Guid, string> guidCryptor, AuthControllerLogic authControllerLogic) : base(guidCryptor)
    {
        _authControllerLogic = authControllerLogic;
    }

    [HttpPost("authenticate")]
    [EnableRateLimiting("AuthenticateRateLimiting")]
    public async Task<IActionResult> Authenticate(string apiKey, bool onlyString = true, bool addBearer = true)
    {
        var result = await _authControllerLogic.TryAuthenticate(apiKey);
        
        if(result.IsFailure())
            return Unauthorized(new { message = "Неправильний ключ API" });

        var token = result.Value;

        if (addBearer)
            token = $"Bearer {token}";

        if (onlyString)
            return Ok(token.ToString());
        
        return Ok(new { token });
    }
}