using BLL.Models;
using BLL.Models.Gym;
using BLL.Models.PersonalManager;
using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Models;
using Common.Models.PaginationView;
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
    private readonly IPersonalManagerLogic _personalManagerLogic;

    public PersonalManagerAdminController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth, IPersonalManagerLogic personalManagerLogic) : base(guidCryptor, auth)
    {
        _personalManagerLogic = personalManagerLogic;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 200)]
    public async Task<IActionResult> Create(PersonalManagerCreationRequest request)
    {
        return Ok( await _personalManagerLogic.Create(request));
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PersonalManagerVM), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get([GuidConvertible] string id)    
    {
        var guidId = DecryptGuid(id);

        if (await _personalManagerLogic.Exist(guidId) == false)
            return NotFound("No personal manager by id founded.");
        
        return Ok(await _personalManagerLogic.Get(guidId));
    }

    [HttpGet("all/{page}")]
    [ProducesResponseType(typeof(IEnumerable<GymVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAll(uint? page = null)
    {
        BasePaginationView<PersonalManagerVM> paginationView = await _personalManagerLogic.GetAll();

        return PaginationView(paginationView, page);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Edit([GuidConvertible] string id, PersonalManagerEditRequest reqest)
    {
        var guidId = DecryptGuid(id);
        
        if (await _personalManagerLogic.Exist(guidId) == false)
            return NotFound("No personal manager by id founded.");

        await _personalManagerLogic.Edit(guidId, reqest);

        return Ok();
    }
    
    [HttpPut("{id}/disable")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DisableAccess([GuidConvertible] string id)
    {
        var guidId = DecryptGuid(id);
        
        if (await _personalManagerLogic.Exist(guidId) == false)
            return NotFound("No personal manager by id founded.");

        if (await _personalManagerLogic.IsEnabled(guidId) == false)
            return BadRequest("Personal manager access is already disabled.");

        await _personalManagerLogic.Disable(guidId);
        
        return Ok();
    }
    
    [HttpPut("{id}/enable")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> EnableAccess([GuidConvertible] string id)
    {
        var guidId = DecryptGuid(id);
        
        if (await _personalManagerLogic.Exist(guidId) == false)
            return NotFound("No personal manager by id founded.");

        if (await _personalManagerLogic.IsEnabled(guidId))
            return BadRequest("Personal manager access is already enabled.");

        await _personalManagerLogic.Enable(guidId);
        
        return Ok();
    }
}