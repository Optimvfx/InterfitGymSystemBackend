using BLL.Models.Card;
using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface;
using CLL.ControllersLogic.Interface.AccessLogic;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Controllers._Base;
using GymCardSystemBackend.ValidationAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCardSystemBackend.Controllers.Terminal;

[Authorize(Policy = PolicyConsts.AdminPolicy)]
[ApiController]
[Route("api/terminal/card")]
public class CardTerminalController : BaseTerminalController
{
    private readonly IClientLogic _clientLogic;
    private readonly ICardLogic _cardLogic;
    
    public CardTerminalController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth) : base(guidCryptor, auth)
    {
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> LinkCardToClient(LinkCardRequest request)
    {
        var cardId = await _cardLogic.GetCardId(request.CardCode);
        if (cardId.IsFailure())
            return BadRequest("Invalid card.");

        if (await _cardLogic.CardExistsInGym(await GetGymId(), cardId.Value) == false)
            return BadRequest("Card is not valid for this gym.");

        if (await _cardLogic.CardIsTaked(cardId.Value))
            return BadRequest("Card liked to other person.");

        if (await _clientLogic.Exists(request.ClientId) == false)
            return BadRequest("No client founded by id.");
        
        await _cardLogic.Link(cardId.Value, request.ClientId);

        return Ok();
    }
}