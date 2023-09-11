using BLL.Services.DataCoder;
using CLL.ControllersLogic;
using GymCarSystemBackend.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace GymCarSystemBackend.Controllers;

[Authorize(Policy = "AdminPolicy")]
[ApiController]
[Route("api/admin")]
public class AdminApiController : BaseAdminController
{
    public AdminApiController(IDataCoder<Guid, string> guidCryptor, AuthControllerLogic authController) : base(guidCryptor, authController)
    {
    }

    [HttpGet("addApiKey")]
    [EnableRateLimiting("KeyCreationLimiting")]
    public async Task<IActionResult> GetMyApiKey()
    {
        var id = await GetAdminId();
        var crypted = EncryptGuid(id);
        return Ok(new { id, crypted });
    }}