using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface.AccessLogic;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Controllers._Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCardSystemBackend.Controllers.BusinessOwner;

[Authorize(Policy = PolicyConsts.AdminPolicy)]
[ApiController]
[Route("api/purchases")]
public class PurchasesBusinessOwnerController: BaseAdminController
{
    private readonly IOrderLogic _orderLogic;

    public PurchasesBusinessOwnerController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth, IOrderLogic orderLogic) : base(guidCryptor, auth)
    {
        _orderLogic = orderLogic;
    }

    [HttpPost("request/order")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> RegisterOrder(OrderRegistrationRequest request)
    {
        var result = await _orderLogic.TryRegister(request);

        if (result == false)
            return BadRequest();

        return Ok();
    }
    
    [HttpPost("register/training_device")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> RegisterTrainingDeviceOrder(TrainingDeviceOrderRegistrationRequest request)
    {
        var result = await _orderLogic.TryRegisterTrainingDevice(request);

        if (result == false)
            return BadRequest();

        return Ok();
    }
    
    [HttpPost("register/consumable")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> RegisterConsumableOrder(ConsumableOrderRegistrationRequest request)
    {
        var result = await _orderLogic.TryRegisterConsumable(request);

        if (result == false)
            return BadRequest();

        return Ok();
    }
    
    [HttpPost("register/technical_hardware")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> RegisterTechnicalHardwareOrder(TechnicalHardwareOrderRegistrationRequest request)
    {
        var result = await _orderLogic.TryRegisterHardware(request);

        if (result == false)
            return BadRequest();

        return Ok();
    }
}