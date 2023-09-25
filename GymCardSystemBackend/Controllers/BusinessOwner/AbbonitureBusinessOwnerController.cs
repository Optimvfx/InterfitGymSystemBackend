using BLL.Models.Abboniture;
using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface;
using CLL.ControllersLogic.Interface.AccessLogic;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Controllers._Base;
using GymCardSystemBackend.Controllers.Terminal;
using GymCardSystemBackend.ValidationAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCardSystemBackend.Controllers.BusinessOwner;

[Authorize(Policy = PolicyConsts.AdminPolicy)]
[ApiController]
[Route("api/abboniture")]
public class AbbonitureBusinessOwnerController : BaseAdminController
{
    private readonly IAbbonitureLogic _abbonitureLogic;

    public AbbonitureBusinessOwnerController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth, IAbbonitureLogic abbonitureLogic) : base(guidCryptor, auth)
    {
        _abbonitureLogic = abbonitureLogic;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Guid), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateNewAbbonitureProfile(CreateAbbonitureProfileRequest request)
    {
        await _abbonitureLogic.Create(request);

        return Ok();
    }
    
    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> EditAbbonitureProfile([GuidConvertible] string id, EditAbbonitureProfileRequest request)
    {
        var guidId = DecryptGuid(id);
        
        if (await _abbonitureLogic.Exists(guidId) == false)
            return NotFound();
        
        await _abbonitureLogic.Edit(guidId, request);

        return Ok();
    }
    
    [HttpDelete]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteAbbonitureProfile([GuidConvertible] string id)
    {
        var guidId = DecryptGuid(id);
        
        if (await _abbonitureLogic.Exists(guidId) == false)
            return NotFound();
        
        await _abbonitureLogic.Delete(guidId);

        return NoContent();
    }
}