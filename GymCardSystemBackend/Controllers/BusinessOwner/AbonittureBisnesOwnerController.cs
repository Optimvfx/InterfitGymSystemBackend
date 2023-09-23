using BLL.Services.DataCoder;
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
        var result = await _abbonitureLogic.Create(request);

        if (result == false)
            return BadRequest();

        return Ok();
    }
    
    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> EditAbbonitureProfile(EditAbbonitureProfileRequest request)
    {
        if (await _abbonitureLogic.Exists(request.Id) == false)
            return NotFound();
        
        var result = await _abbonitureLogic.Edit(request);

        if (result == false)
            return BadRequest();

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
        
        var result = await _abbonitureLogic.Delete(guidId);

        if (result == false)
            return BadRequest();

        return Ok();
    }
}

public class EditAbbonitureProfileRequest
{
    public Guid Id { get; set; }
}

public class CreateAbbonitureProfileRequest
{
}