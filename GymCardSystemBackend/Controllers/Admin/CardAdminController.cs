using BLL.Models;
using BLL.Models.Card;
using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Models;
using Common.Models.PaginationView;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Controllers._Base;
using GymCardSystemBackend.Controllers.Terminal;
using GymCardSystemBackend.ValidationAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCardSystemBackend.Controllers.Admin;

[Authorize(Policy = PolicyConsts.AdminPolicy)]
[ApiController]
[Route("api/admin/card")]
public class CardAdminController : BaseAdminController
{
    private readonly ICardLogic _cardLogic;
    private readonly IPersonLogic _personLogic;
    
    public CardAdminController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth, ICardLogic cardLogic) : base(guidCryptor, auth)
    {
        _cardLogic = cardLogic;
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create(CardCreationRequest request)
    {
        Result<Guid> result = await _cardLogic.Create(request);

        if (result.IsFailure())
            return BadRequest();

        return Ok(result.Value);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get([GuidConvertible] string id)
    {
        var guidId = DecryptGuid(id);
        Result<CardVM> result = await _cardLogic.TryGet(guidId);

        if (result.IsFailure())
            return NotFound();

        return Ok(result.Value);
    }

    #region Global

    [HttpGet("all/{page}")]
    [ProducesResponseType(typeof(IEnumerable<CardVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAll(uint? page = null)
    {
        BasePaginationView<CardVM> paginationView = await _cardLogic.GetAll();

        return PaginationView(paginationView, page);
    }
    
    [HttpGet("all/{page}/linked")]
    [ProducesResponseType(typeof(IEnumerable<CardVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllLinked(uint? page = null)
    {
        BasePaginationView<CardVM> paginationView = await _cardLogic.GetAllLinked();

        return PaginationView(paginationView, page);
    }
    
    [HttpGet("all/{page}/not_linked")]
    [ProducesResponseType(typeof(IEnumerable<CardVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllNotLinked(uint? page = null)
    {
        BasePaginationView<CardVM> paginationView = await _cardLogic.GetAllNotLinked();

        return PaginationView(paginationView, page);
    }

    #endregion

    #region Gym

    [HttpGet("all/{gymId}/{page}")]
    [ProducesResponseType(typeof(IEnumerable<CardVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllByGym([GuidConvertible] string gymId, uint? page = null)
    {
        var gymGuidId = DecryptGuid(gymId);
        
        BasePaginationView<CardVM> paginationView = await _cardLogic.GetAll(gymGuidId);

        return PaginationView(paginationView, page);
    }
    
    [HttpGet("all/{gymId}/{page}/linked")]
    [ProducesResponseType(typeof(IEnumerable<CardVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllLinkedByGym([GuidConvertible] string gymId, uint? page = null)
    {
        var gymGuidId = DecryptGuid(gymId);
        
        BasePaginationView<CardVM> paginationView = await _cardLogic.GetAllLinked(gymGuidId);

        return PaginationView(paginationView, page);
    }
    
    [HttpGet("all/{gymId}/{page}/not_linked")]
    [ProducesResponseType(typeof(IEnumerable<CardVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllNotLinkedByGym([GuidConvertible] string gymId,uint? page = null)
    {
        var gymGuidId = DecryptGuid(gymId);
        
        BasePaginationView<CardVM> paginationView = await _cardLogic.GetAllNotLinked(gymGuidId);

        return PaginationView(paginationView, page);
    }

    #endregion
    
    [HttpPut("{id}/unlink")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Unlink([GuidConvertible] string id)
    {
        var guidId = DecryptGuid(id);

        if (await _cardLogic.Exist(guidId) == false)
            return NotFound("No card founded by id.");

        if (await _cardLogic.CardIsTaked(guidId) == false)
            return BadRequest("Card already unlinked.");

        await _cardLogic.UnLink(guidId);
        
        return Ok();
    }
    
    [HttpPut("{id}/link/{personId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Link([GuidConvertible] string id, [GuidConvertible] string personId)
    { 
        var guidId = DecryptGuid(id);
        var personGuidId = DecryptGuid(personId);
        
        if (await _cardLogic.Exist(guidId) == false)
            return NotFound("No card founded by id.");

        if (await _cardLogic.CardIsTaked(guidId))
            return BadRequest("Card already linked.");

        if (await _personLogic.Exists(personGuidId))
            return NotFound("Person not founded.");
            
        await _cardLogic.UnLink(guidId);
        
        return Ok();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete([GuidConvertible] string id)
    {
        var guidId = DecryptGuid(id);

        if (await _cardLogic.Exist(guidId) == false)
            return NotFound("No card founded by id.");

        await _cardLogic.Delete(guidId);

        return NoContent();
    }
}