using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Models;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Controllers._Base;
using GymCardSystemBackend.ValidationAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCardSystemBackend.Controllers.BusinessOwner;

[Authorize(Policy = PolicyConsts.AdminPolicy)]
[ApiController]
[Route("api/financial")]
public class FinancialBusinessOwnerController : BaseAdminController
{
    private readonly IFinanceLogic _financeLogic;
    private readonly IGymLogic _gymLogic;

    public FinancialBusinessOwnerController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth, IFinanceLogic financeLogic, IGymLogic gymLogic) : base(guidCryptor, auth)
    {
        _financeLogic = financeLogic;
        _gymLogic = gymLogic;
    }

    #region Global

    [HttpGet("/total/expenses")]
    [ProducesResponseType(typeof(ExpensesVM), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetTotalExpenses(DateOnly from, DateOnly to)
    {
        if (to < from)
            return BadRequest("Data range is invalid.");

        var dataRange = new ValueRange<DateOnly>(from, to);

        Result<ExpensesVM> result = await _financeLogic.TryGetExpenses(dataRange);

        if (result.IsFailure())
            return BadRequest();

        return Ok(result.Value);
    }
    
    [HttpGet("/total/earnings")]
    [ProducesResponseType(typeof(EarningsVM), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetTotalEearnings(DateOnly from, DateOnly to)
    {
        if (to < from)
            return BadRequest("Data range is invalid.");

        var dataRange = new ValueRange<DateOnly>(from, to);

        Result<EarningsVM> result = await _financeLogic.TryGetEarnings(dataRange);

        if (result.IsFailure())
            return BadRequest();

        return Ok(result.Value);
    }
    
    [HttpGet("/procent/expenses")]
    [ProducesResponseType(typeof(ProcentExpensesVM), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetProcentExpenses(DateOnly from, DateOnly to)
    {
        if (to < from)
            return BadRequest("Data range is invalid.");

        var dataRange = new ValueRange<DateOnly>(from, to);

        Result<ProcentExpensesVM> result = await _financeLogic.TryGetProcentExpenses(dataRange);

        if (result.IsFailure())
            return BadRequest();

        return Ok(result.Value);
    }
    
    [HttpGet("/procent/earnings")]
    [ProducesResponseType(typeof(ProcentEarningsVM), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetProcentEearnings(DateOnly from, DateOnly to)
    {
        if (to < from)
            return BadRequest("Data range is invalid.");

        var dataRange = new ValueRange<DateOnly>(from, to);

        Result<ProcentEarningsVM> result = await _financeLogic.TryGetProcentEarnings(dataRange);

        if (result.IsFailure())
            return BadRequest();

        return Ok(result.Value);
    }

    #endregion
    
    #region Gym

    [HttpGet("/total/gym/expenses")]
    [ProducesResponseType(typeof(ExpensesVM), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetGymTotalExpenses([GuidConvertible] string gymId, DateOnly from, DateOnly to)
    {
        var gymGuidId = DecryptGuid(gymId);

        if (await _gymLogic.Exist(gymGuidId) == false)
            return NotFound("Gym is not founded by id.");
        
        if (to < from)
            return BadRequest("Data range is invalid.");

        var dataRange = new ValueRange<DateOnly>(from, to);

        var result = await _financeLogic.TryGetExpenses(dataRange, gymGuidId);

        if (result.IsFailure())
            return BadRequest();

        return Ok(result.Value);
    }
    
    [HttpGet("/total/gym/earnings")]
    [ProducesResponseType(typeof(EarningsVM), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetGymTotalEearnings([GuidConvertible] string gymId, DateOnly from, DateOnly to)
    {
        var gymGuidId = DecryptGuid(gymId);

        if (await _gymLogic.Exist(gymGuidId) == false)
            return NotFound("Gym is not founded by id.");
        
        if (to < from)
            return BadRequest("Data range is invalid.");

        var dataRange = new ValueRange<DateOnly>(from, to);

        var result = await _financeLogic.TryGetEarnings(dataRange, gymGuidId);

        if (result.IsFailure())
            return BadRequest();

        return Ok(result.Value);
    }
    
    [HttpGet("/procent/gym/expenses")]
    [ProducesResponseType(typeof(ProcentExpensesVM), 200)]
    [ProducesResponseType(400)]   
    public async Task<IActionResult> GetGymProcentExpenses([GuidConvertible] string gymId, DateOnly from, DateOnly to)
    {
        var gymGuidId = DecryptGuid(gymId);

        if (await _gymLogic.Exist(gymGuidId) == false)
            return NotFound("Gym is not founded by id.");
        
        if (to < from)
            return BadRequest("Data range is invalid.");

        var dataRange = new ValueRange<DateOnly>(from, to);

        var result = await _financeLogic.TryGetProcentExpenses(dataRange, gymGuidId);

        if (result.IsFailure())
            return BadRequest();

        return Ok(result.Value);
    }
    
    [HttpGet("/procent/gym/earnings")]
    [ProducesResponseType(typeof(ProcentEarningsVM), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetGymProcentEearnings([GuidConvertible] string gymId, DateOnly from, DateOnly to)
    {
        var gymGuidId = DecryptGuid(gymId);

        if (await _gymLogic.Exist(gymGuidId) == false)
            return NotFound("Gym is not founded by id.");
        
        if (to < from)
            return BadRequest("Data range is invalid.");

        var dataRange = new ValueRange<DateOnly>(from, to);

        var result = await _financeLogic.TryGetProcentEarnings(dataRange, gymGuidId);

        if (result.IsFailure())
            return BadRequest();

        return Ok(result.Value);
    }

    #endregion
}