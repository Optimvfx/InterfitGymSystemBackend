using BLL.Models.Hardware.Breakdowm;
using BLL.Models.Hardware.Repair;
using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Models;
using Common.Models.PaginationView;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Controllers._Base;
using GymCardSystemBackend.Controllers.Terminal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCardSystemBackend.Controllers.BusinessOwner;

[Authorize(Policy = PolicyConsts.AdminPolicy)]
[ApiController]
[Route("api/breakdown")]
public class BreakdownBusinessOwnerController : BaseAdminController
{
    private readonly IBreakdownLogic _breakdownLogic;

    public BreakdownBusinessOwnerController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth, IBreakdownLogic breakdownLogic) : base(guidCryptor, auth)
    {
        _breakdownLogic = breakdownLogic;
    }

    [HttpGet("all/training_device")]
    [ProducesResponseType(typeof(IEnumerable<TrainingDeviceBreakdowmVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllTrainingDeviceBreakdowns(uint? page = null)
    {
        BasePaginationView<TrainingDeviceBreakdowmVM> paginationView = await _breakdownLogic.GetAllTrainingDevice();

        return PaginationView(paginationView, page);
    }

    [HttpGet("all/technical_hardware")]
    [ProducesResponseType(typeof(IEnumerable<TechnicalHardwareBreakdowmVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllTechnicalHardwareBreakdowns(uint? page = null)
    {
        BasePaginationView<TechnicalHardwareBreakdowmVM> paginationView = await _breakdownLogic.GetAllTechnicalHardware();

        return PaginationView(paginationView, page);
    }

    [HttpGet("all/consumable")]
    [ProducesResponseType(typeof(IEnumerable<ConsumableBreakdowmVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllConsumableBreakdowns(uint? page = null)
    {
        BasePaginationView<ConsumableBreakdowmVM> paginationView = await _breakdownLogic.GetAllConsumable();

        return PaginationView(paginationView, page);
    }

    [HttpPost("repair/training_device")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RegisterTrainingDeviceRepair(TrainingDeviceRepairRegisterRequest request)
    {
        if (await _breakdownLogic.AnyTrainingDevice(request.HardwareId) == false)
            return NotFound("Not found hardware");

        var result = await _breakdownLogic.RegisterTrainingDeviceRepair(request);

        if (result == false)
            return BadRequest();

        return Ok();
    }
    
    [HttpPost("repair/technical_hardware")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RegisterTechnicalHardwareRepair(TechnicalHardwareRepairRegisterRequest request)
    {
        if (await _breakdownLogic.AnyTechnicalHardware(request.HardwareId) == false)
            return NotFound("Not found hardware");

        var result = await _breakdownLogic.RegisterTechnicalHardwareRepair(request);

        if (result == false)
            return BadRequest();

        return Ok();
    }  
}