using BLL.Models.Visitation;
using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface;
using CLL.ControllersLogic.Interface.AccessLogic;
using DAL.Entities.Gym;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Controllers._Base;
using GymCardSystemBackend.ValidationAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCardSystemBackend.Controllers.Terminal;
[Authorize(Policy = PolicyConsts.AdminPolicy)]
[ApiController]
[Route("api/сard")]
public class VisitsTerminalController: BaseTerminalController
{
    private readonly IVisitsLogic _visitsLogic;
    private readonly ICardLogic _cardLogic;

    protected VisitsTerminalController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth, IVisitsLogic visitsLogic, ICardLogic cardLogic) : base(guidCryptor, auth)
    {
        _visitsLogic = visitsLogic;
        _cardLogic = cardLogic;
    }

    [HttpPost("enter")]
    [ProducesResponseType(typeof(VisitationVM), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RegisterVisitation(byte[] cardCode)
    {
        var gymId = await GetGymId();
        
        var cardId = await _cardLogic.GetCardId(cardCode);
        if (cardId.IsFailure())
            return BadRequest("Invalid card.");
        
        var result = await _cardLogic.TryGetPersonIdByCardCode(cardId.Value);
        
        if (result.IsFailure())
            return NotFound("No person connected to card code.");

        Guid personId = result.Value;

        if (await _visitsLogic.PersonInGym(personId))
            return BadRequest("Person already in gym");

        if (await _visitsLogic.PersonCanVisitGym(personId) == false)
            return BadRequest("Person can not visit gym.");
        
        var visitResult = await _visitsLogic.Register(gymId,personId);   
        return Ok(visitResult);
    }
    
    [HttpPost("continume")]
    [ProducesResponseType(typeof(VisitationVM), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> ContinueVisitation(byte[] cardCode)
    {
        var gymId = await GetGymId();
        
        var cardId = await _cardLogic.GetCardId(cardCode);
        if (cardId.IsFailure())
            return BadRequest("Invalid card.");
        
        var result = await _cardLogic.TryGetPersonIdByCardCode(cardId.Value);
        
        if (result.IsFailure())
            return NotFound("No person connected to card code.");

        Guid personId = result.Value;

        if (await _visitsLogic.PersonInGym(gymId, personId) == false)
            return BadRequest("Person not in gym");

        var visit = await _visitsLogic.ContinumeVisit(gymId, personId);

        return Ok(visit);
    }
    
    [HttpPost("exit")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RegisterExit(byte[] cardCode)
    {
        var gymId = await GetGymId();
        
        var cardId = await _cardLogic.GetCardId(cardCode);
        if (cardId.IsFailure())
            return BadRequest("Invalid card.");
        
        var result = await _cardLogic.TryGetPersonIdByCardCode(cardId.Value);
        
        if (result.IsFailure())
            return NotFound("No person connected to card code.");

        Guid personId = result.Value;

        if (await _visitsLogic.PersonInGym(gymId, personId) == false)
            return BadRequest("Person not in gym");

        await _visitsLogic.Exit(gymId, personId);
        
        return Ok();
    }
    
    [HttpGet("enter/can")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> CheckVisitationPossible(byte[] cardCode)
    {
        var cardId = await _cardLogic.GetCardId(cardCode);
        if (cardId.IsFailure())
            return BadRequest("Invalid card.");
        
        var result = await _cardLogic.TryGetPersonIdByCardCode(cardId.Value);
        
        if (result.IsFailure())
            return NotFound("No person connected to card code.");

        Guid personId = result.Value;

        if (await _visitsLogic.PersonInGym(personId))
            return Ok(false);
        
        return Ok(await _visitsLogic.PersonCanVisitGym(personId));
    }
}