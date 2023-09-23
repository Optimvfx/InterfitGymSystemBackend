using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface.AccessLogic;
using GymCardSystemBackend.Controllers._Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace GymCardSystemBackend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthApiController : BaseController
{
    private readonly IAuthLogic _authControllerLogic;

    public AuthApiController(IDataCoder<Guid, string> guidCryptor, IAuthLogic authControllerLogic) : base(guidCryptor)
    {
        _authControllerLogic = authControllerLogic;
    }

    [HttpPost("authenticate")]
    [EnableRateLimiting("AuthenticateRateLimiting")]
    public async Task<IActionResult> Authenticate(string apiKey, bool onlyString = true, bool addBearer = true)
    {
        var result = await _authControllerLogic.TryGetJwt(apiKey);
        
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