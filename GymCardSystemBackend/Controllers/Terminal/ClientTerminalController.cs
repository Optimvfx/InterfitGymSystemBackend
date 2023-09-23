using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Models;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Controllers._Base;
using GymCardSystemBackend.ValidationAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCardSystemBackend.Controllers.Terminal;

[Authorize(Policy = PolicyConsts.AdminPolicy)]
[ApiController]
[Route("api/terminal/client")]
public class ClientTerminalController : BaseAdminController
{
    private readonly IClientLogic _clientLogic;
    private readonly ICardLogic _cardLogic;

    public ClientTerminalController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth, IClientLogic clientLogic, ICardLogic cardLogic) : base(guidCryptor, auth)
    {
        _clientLogic = clientLogic;
        _cardLogic = cardLogic;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid),200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateClient(CreateClientRequest request)
    {
        Result<Guid> result = await _clientLogic.Create(request);

        if (result.IsFailure())
            return BadRequest();

        return Ok(result.Value);
    }
    
    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> EditClient(byte[] clientCardCode, EditClientRequest request)
    {
        var clientCardId = await _cardLogic.GetCardId(clientCardCode);
        if (clientCardId.IsFailure())
            return BadRequest("Invalid client card.");
        
        var clientIdResult = await _cardLogic.TryGetClientIdByCard(clientCardId.Value);
        
        if (clientIdResult.IsFailure())
            return NotFound("No client connected to card code.");

        Guid clientId = clientIdResult.Value;

        if (await _clientLogic.Exists(clientId) == false)
            return NotFound("Client by id is not found");

        await _clientLogic.ApplyEdit(clientId, request);
        
        return Ok();
    }
}

public interface IClientLogic
{
    Task<bool> Exists(Guid clientId);
    Task<Result<Guid>> Create(CreateClientRequest request);
    Task ApplyEdit(Guid guidId, EditClientRequest request);
}

public class EditClientRequest
{
}

public class CreateClientRequest
{
}
  