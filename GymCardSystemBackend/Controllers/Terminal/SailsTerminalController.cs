using BLL.Models.Abboniture;
using BLL.Models.Sale;
using BLL.Models.Trainer;
using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Models;
using Common.Models.PaginationView;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Controllers._Base;
using GymCardSystemBackend.Controllers.Admin;
using GymCardSystemBackend.ValidationAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymCardSystemBackend.Controllers.Terminal;

[Authorize(Policy = PolicyConsts.AdminPolicy)]
[ApiController]
[Route("api/sails")]
public class SailsTerminalController: BaseTerminalController
{
    private readonly IAbbonitureLogic _abbonitureLogic;
    private readonly ITrainingLogic _trainingLogic;
    private readonly ICardLogic _cardLogic;

    protected SailsTerminalController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth, IAbbonitureLogic abbonitureLogic, ITrainingLogic trainingLogic, ICardLogic cardLogic) : base(guidCryptor, auth)
    {
        _abbonitureLogic = abbonitureLogic;
        _trainingLogic = trainingLogic;
        _cardLogic = cardLogic;
    }

    [HttpGet("all/abboniture")]
    [ProducesResponseType(typeof(IEnumerable<AbbonitureProfileVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAbbonitureProfiles(uint? page = null)
    {
        BasePaginationView<AbbonitureProfileVM> paginationView = await _abbonitureLogic.GetAll();

        return PaginationView(paginationView, page);
    }
    
    [HttpGet("all/free/trainers")]
    [ProducesResponseType(typeof(IEnumerable<TrainerVM>), 200)]
    [ProducesResponseType(typeof(ValueRange<uint>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetTrainers(uint? page = null)
    {
        var gym = await GetGymId();
        
        BasePaginationView<TrainerVM> trainers = await _trainingLogic.GetAllFree(gym);

        return PaginationView(trainers, page);
    }

    
    [HttpPost("register/abboniture")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RegisterAbbonitureSale(RegisterAbbonitureSaleRequest request)
    {
        if (await _abbonitureLogic.Exists(request.AbbonitureId) == false)
            return NotFound("No abboniture founded.");

        var cardId = await _cardLogic.GetCardId(request.ClientCardCode);
        if (cardId.IsFailure())
            return BadRequest("Invalid card.");
        
        var clientIdResult = await _cardLogic.TryGetClientIdByCard(cardId.Value);
        
        if (clientIdResult.IsFailure())
            return NotFound("No client connected to card code.");

        Guid clientId = clientIdResult.Value;

        if (await _abbonitureLogic.ClientHaveActiveAbboniture(clientId))
            return BadRequest("Client has active abboniture");
            
        await _abbonitureLogic.RegisterSale(clientId, request.AbbonitureId);
        
        return Ok();
    }
    
    [HttpPost("register/training")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RegisterTrainingSale(RegisterTrainingSaleRequest request)
    {
        var clientCardId = await _cardLogic.GetCardId(request.ClienCardCode);
        if (clientCardId.IsFailure())
            return BadRequest("Invalid client card.");
        
        var clientIdResult = await _cardLogic.TryGetClientIdByCard(clientCardId.Value);
        
        if (clientIdResult.IsFailure())
            return NotFound("No client connected to card code.");

        Guid clientId = clientIdResult.Value;
        
        var trainerCardId = await _cardLogic.GetCardId(request.TrainerCardCode);
        if (trainerCardId.IsFailure())
            return BadRequest("Invalid trainer card.");
        
        var trainerIdResult = await _cardLogic.TryGetTrainerIdByCardCode(trainerCardId.Value);
        
        if (trainerIdResult.IsFailure())
            return NotFound("No trainer connected to card code.");

        Guid trainerId = trainerIdResult.Value;
        
        var gym = await GetGymId();

        if (await _trainingLogic.Exists(trainerId))
            return NotFound("No trainer founded.");

        if (await _trainingLogic.TrainerInGym(gym, trainerId) == false)
            return BadRequest("Trainer is not on work place.");
        
        if (await _trainingLogic.TrainerIsFree(trainerId))
            return BadRequest("Trainer is not free.");
        
        await _trainingLogic.RegisterTraining(trainerId, clientId, request.TotalHours);

        return Ok();
    }
}