using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Models;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Controllers._Base;
using GymCardSystemBackend.Controllers.Admin;
using GymCardSystemBackend.Controllers.BusinessOwner;
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

        if (await _abbonitureLogic.ClientHasActiveAbboniture(clientId))
            return BadRequest("Client has active abboniture");
            
        var result = await _abbonitureLogic.TryRegisterSale(clientId, request.AbbonitureId);
        
        if (result == false)
            return BadRequest();

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

        if (await _trainingLogic.Exists(gym, trainerId))
            return NotFound("No trainer founded.");

        if (await _trainingLogic.TrainerInGym(gym, trainerId) == false)
            return BadRequest("Trainer is not on work place.");
        
        if (await _trainingLogic.TrainerIsFree(gym, trainerId))
            return BadRequest("Trainer is not free.");
        
        var result = await _trainingLogic.TryRegisterTraining(trainerId, clientId, request.TotalHours);

        if (result == false)
            return BadRequest();

        return Ok();
    }
}

public class TrainerVM
{
}

public interface ITrainingLogic
{
    Task<bool> Exists(Guid gymId, Guid trainerId);
    Task<bool> TrainerIsFree(Guid gym, Guid trainerId);
    
    Task<bool> TryRegisterTraining(Guid trainerId, Guid clientId, uint requestTotalHours);
    Task<BasePaginationView<TrainerVM>> GetAllFree(Guid gym);
    Task<bool> TrainerInGym(Guid gym, Guid trainerId);
}

public interface IAbbonitureLogic
{
    Task<BasePaginationView<AbbonitureProfileVM>> GetAll();
    Task<bool> Exists(Guid abbonitureId);
    Task<bool> ClientHasActiveAbboniture(Guid clientId);
    Task<bool> TryRegisterSale(Guid clientId, Guid requestAbbonitureId);
    Task<bool> Create(CreateAbbonitureProfileRequest request);
    Task<bool> Edit(EditAbbonitureProfileRequest request);
    Task<bool> Delete(Guid id);
}

public class RegisterAbbonitureSaleRequest
{
    public byte[] ClientCardCode { get; set; }
    public Guid AbbonitureId { get; set; }
}

public class RegisterTrainingSaleRequest
{   
    public byte[] TrainerCardCode { get; set; }
    public byte[] ClienCardCode { get; set; }
    public uint TotalHours { get; set; }
}

public class AbbonitureProfileVM
{
}