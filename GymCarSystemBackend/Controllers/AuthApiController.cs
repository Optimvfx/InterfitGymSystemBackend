using System.Security.Claims;
using CCL.Base;
using CCL.ControllersLogic;
using Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCarSystemBackend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthApiController : BaseController
{
    private readonly AuthControllerLogic _logic;

    public AuthApiController(AuthControllerLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(string apiKey)
    {
        var result = await _logic.TryAuthenticate(apiKey);
        
        if(result.IsFailure())
            return Unauthorized(new { message = "Неправильний ключ API" });

        var token = result.Value;
        return Ok(new { token });
    }
}