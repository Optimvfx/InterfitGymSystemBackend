using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Models;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Controllers._Base;
using GymCardSystemBackend.Controllers.PersonalManager;
using GymCardSystemBackend.ValidationAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCardSystemBackend.Controllers.Admin;

[Authorize(Policy = PolicyConsts.AdminPolicy)]
[ApiController]
[Route("api/admin/terminal")]
public class TerminalAdminController : BaseAdminController
{
    private readonly ITerminalLogic _terminalLogic;

    public TerminalAdminController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth, ITerminalLogic terminalLogic)
        : base(guidCryptor, auth)
    {
        _terminalLogic = terminalLogic;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Create(TerminalCreationRequest request)
    {
        Result<Guid> result = await _terminalLogic.Create(request);

        if (result.IsFailure())
            return BadRequest();

        return Ok(result.Value);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TerminalVM), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get([GuidConvertible] string id)
    {
        var guidId = DecryptGuid(id);
        Result<TerminalVM> result = await _terminalLogic.TryGet(guidId);

        if (result.IsFailure())
            return NotFound();

        return Ok(result.Value);
    }

    [HttpGet("all/{page}")]
    [ProducesResponseType(typeof(IEnumerable<TerminalVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAll(uint? page= null)
    {
        BasePaginationView<TerminalVM> paginationView = await _terminalLogic.GetAll();

        return PaginationView(paginationView, page);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Edit([GuidConvertible] string id, TerminalEditRequest reqest)
    {
        var guidId = DecryptGuid(id);
        
        if (await _terminalLogic.Exist(guidId) == false)
            return NotFound("No terminal by id founded.");

        var result = await _terminalLogic.Edit(guidId, reqest);

        if (result == false)
            return BadRequest();

        return Ok();
    }
    
    [HttpPut("{id}/disable")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Disable([GuidConvertible] string id)
    {
        var guidId = DecryptGuid(id);
        
        if (await _terminalLogic.Exist(guidId) == false)
            return NotFound("No terminal by id founded.");

        if (await _terminalLogic.IsEnabled(guidId) == false)
            return BadRequest("Terminal is already disabled.");

        await _terminalLogic.Disable(guidId);
        
        return Ok();
    }
    
    [HttpPut("{id}/enable")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Enable([GuidConvertible] string id)
    {
        var guidId = DecryptGuid(id);
        
        if (await _terminalLogic.Exist(guidId) == false)
            return NotFound("No terminal by id founded.");

        if (await _terminalLogic.IsEnabled(guidId))
            return BadRequest("Terminal is already enabled.");

        await _terminalLogic.Enable(guidId);
        
        return Ok();
    }
}