using System.Runtime.InteropServices.ComTypes;
using BLL.Services.DataCoder;
using CLL.ControllersLogic;
using GymCarSystemBackend.Controllers.Base;
using GymCarSystemBackend.ValidationAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace GymCarSystemBackend.Controllers;

[Authorize(Policy = "AdminPolicy")]
[ApiController]
[Route("api/admin")]
public class AdminApiController : BaseAdminController
{
    public AdminApiController(IDataCoder<Guid, string> guidCryptor, AuthLogic auth) : base(guidCryptor, auth)
    {
    }

    [HttpGet("addApiKey")]
    [EnableRateLimiting("KeyCreationLimiting")]
    public async Task<IActionResult> GetMyId()
    {
        var id = await GetAdminId();
        
        return Ok(new { id = EncryptGuid(id) });
    }
}