using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface.AccessLogic;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Controllers._Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCardSystemBackend.Controllers.Terminal;

[Authorize(Policy = PolicyConsts.AdminPolicy)]
[ApiController]
[Route("api/terminal/breakdown")]
public class BreakdownTerminalController : BaseTerminalController
{
    private readonly IBreakdownLogic _breakdownLogic;
    private readonly IHardwareLogic _hardwareLogic;

    protected BreakdownTerminalController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth, IBreakdownLogic breakdownLogic, IHardwareLogic hardwareLogic) : base(guidCryptor, auth)
    {
        _breakdownLogic = breakdownLogic;
        _hardwareLogic = hardwareLogic;
    }

    [HttpPost("register/training_device")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> RegisterTrainingDeviceBreakdown(TrainingDeviceBreakdownRegisterRequest request)
    {
        var gym = await GetGymId();
        
        if (await _hardwareLogic.ExistsTrainingDevice(gym, request.TrainingDeviceId) == false)
            return NotFound("Hardware not founded");

        if (await _hardwareLogic.ValidTrainingDeviceBreakdown(request.BreakdownId) == false)
            return BadRequest("Not valid breakdown.");

        var result = await _breakdownLogic.RegisterTrainingDeviceBreakdown(request);

        if (result == false)
            return BadRequest();

        return Ok();
    }
    
    [HttpPost("register/technical_hardware")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> RegisterTechnicalHardwareBreakdown(TechnicalHardwareBreakdownRegisterRequest request)
    {
        var gym = await GetGymId();
        
        if (await _hardwareLogic.ExistsTechnicalHardware(gym, request.TechnicalHardwareId) == false)
            return NotFound("Hardware not founded");

        var result = await _breakdownLogic.RegisterTechnicalHardwareBreakdown(request);

        if (result == false)
            return BadRequest();

        return Ok();
    }
    
    [HttpPost("register/consumable")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> RegisterConsumableBreakdown(ConsumableBreakdownRegisterRequest request)
    {
        var gym = await GetGymId();
        
        if (await _hardwareLogic.ExistsConsumable(gym, request.ConsumableId) == false)
            return NotFound("Hardware not founded");

        var result = await _breakdownLogic.RegisterConsumableBreakdowns(request);

        if (result == false)
            return BadRequest();

        return Ok();
    }
}