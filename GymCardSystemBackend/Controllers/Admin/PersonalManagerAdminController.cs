using BLL.Models;
using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Models;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Controllers._Base;
using GymCardSystemBackend.ValidationAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCardSystemBackend.Controllers.Admin;

[Authorize(Policy = PolicyConsts.AdminPolicy)]
[ApiController]
[Route("api/admin/personal_manager")]
public class PersonalManagerAdminController : BaseAdminController
{
    private readonly IPersonalManagerLogic _terminalLogic;

    public PersonalManagerAdminController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth, IPersonalManagerLogic terminalLogic) : base(guidCryptor, auth)
    {
        _terminalLogic = terminalLogic;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 200)]
    public async Task<IActionResult> Create([FromBody] PersonalManagerCreationRequest request)
    {
        Result<Guid> result = await _terminalLogic.Create(request);

        if (result.IsFailure())
            return BadRequest();

        return Ok(result.Value);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PersonalManagerVM), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get([GuidConvertible] string id)    
    {
        var guidId = DecryptGuid(id);
        Result<PersonalManagerVM> result = await _terminalLogic.TryGet(guidId);

        if (result.IsFailure())
            return NotFound();

        return Ok(result.Value);
    }

    [HttpGet("all/{page}")]
    [ProducesResponseType(typeof(IEnumerable<GymVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAll(uint? page= null)
    {
        BasePaginationView<PersonalManagerVM> paginationView = await _terminalLogic.GetAll();

        return PaginationView(paginationView, page);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Edit([GuidConvertible] string id, PersonalManagerEditRequest reqest)
    {
        var guidId = DecryptGuid(id);
        
        if (await _terminalLogic.Exist(guidId) == false)
            return NotFound("No personal manager by id founded.");

        var result = await _terminalLogic.Edit(guidId, reqest);

        if (result == false)
            return BadRequest();

        return Ok();
    }
    
    [HttpPut("{id}/disable")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DisableAccess([GuidConvertible] string id)
    {
        var guidId = DecryptGuid(id);
        
        if (await _terminalLogic.Exist(guidId) == false)
            return NotFound("No personal manager by id founded.");

        if (await _terminalLogic.IsEnabled(guidId) == false)
            return BadRequest("Personal manager access is already disabled.");

        await _terminalLogic.Disable(guidId);
        
        return Ok();
    }
    
    [HttpPut("{id}/enable")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> EnableAccess([GuidConvertible] string id)
    {
        var guidId = DecryptGuid(id);
        
        if (await _terminalLogic.Exist(guidId) == false)
            return NotFound("No personal manager by id founded.");

        if (await _terminalLogic.IsEnabled(guidId))
            return BadRequest("Personal manager access is already enabled.");

        await _terminalLogic.Enable(guidId);
        
        return Ok();
    }
}

public interface IPersonalManagerLogic
{
    Task<bool> IsEnabled(Guid id);
    Task Enable(Guid id);
    Task<bool> Exist(Guid id);
    Task Disable(Guid id);
    Task<bool> Edit(Guid id, PersonalManagerEditRequest reqest);
    Task<BasePaginationView<PersonalManagerVM>> GetAll();
    Task<Result<PersonalManagerVM>> TryGet(Guid id);
    Task<Result<Guid>> Create(PersonalManagerCreationRequest request);
}

public class PersonalManagerCreationRequest
{
}

public class PersonalManagerVM
{
}

public class PersonalManagerEditRequest
{
}