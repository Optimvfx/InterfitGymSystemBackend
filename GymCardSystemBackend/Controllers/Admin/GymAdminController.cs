using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Models;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Controllers._Base;
using GymCardSystemBackend.Controllers.BusinessOwner;
using GymCardSystemBackend.ValidationAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCardSystemBackend.Controllers.Admin;

[Authorize(Policy = PolicyConsts.AdminPolicy)]
[ApiController]
[Route("api/admin/gym")]
public class GymAdminController : BaseAdminController
{
    private readonly IGymLogic _gymLogic;

    public GymAdminController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth, IGymLogic gymLogic) : base(guidCryptor, auth)
    {
        _gymLogic = gymLogic;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 200)]
    public async Task<IActionResult> Create([FromBody] GymCreationRequest request)
    {
        Result<Guid> result = await _gymLogic.Create(request);

        if (result.IsFailure())
            return BadRequest();

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GymVM), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get([GuidConvertible] string id)    
    {
        var guidId = DecryptGuid(id);
        Result<GymVM> result = await _gymLogic.TryGet(guidId);

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
        BasePaginationView<GymVM> paginationView = await _gymLogic.GetAll();

        return PaginationView(paginationView, page);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Edit([GuidConvertible] string id, GymEditRequest reqest)
    {
        var guidId = DecryptGuid(id);
        
        if (await _gymLogic.Exist(guidId) == false)
            return NotFound("No gym by id founded.");

        var result = await _gymLogic.Edit(guidId, reqest);

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
        
        if (await _gymLogic.Exist(guidId) == false)
            return NotFound("No gym by id founded.");

        if (await _gymLogic.IsEnabled(guidId) == false)
            return BadRequest("Gym is already disabled.");

        await _gymLogic.Disable(guidId);
        
        return Ok();
    }
    
    [HttpPut("{id}/enable")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Enable([GuidConvertible] string id)
    {
        var guidId = DecryptGuid(id);
        
        if (await _gymLogic.Exist(guidId) == false)
            return NotFound("No gym by id founded.");

        if (await  _gymLogic.IsEnabled(guidId))
            return BadRequest("Gym is already enabled.");

        await _gymLogic.Enable(guidId);
        
        return Ok();
    }
}