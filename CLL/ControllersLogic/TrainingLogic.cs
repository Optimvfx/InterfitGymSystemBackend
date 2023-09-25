using BLL.Models.Trainer;
using BLL.Services.Database;
using BLL.Services.PaginationViewFactory;
using BLL.Services.TimeService;
using CLL.ControllersLogic.Interface;
using Common.Exceptions.General;
using Common.Models;
using Common.Models.PaginationView;
using DAL.Entities.Gym;
using DAL.Entities.Gym.Person;

namespace CLL.ControllersLogic;

public class TrainingLogic : ITrainingLogic
{
    private readonly GymService _gymService;
    private readonly TrainerService _trainerService;
    private readonly ClientService _clientService;
    private readonly TrainingService _trainingService;
    private readonly VisitationService _visitationService;

    private readonly BaseTimeService _baseTimeService;
    private readonly IPaginationViewFactory _paginationViewFactory;

    public TrainingLogic(GymService gymService, TrainerService trainerService, ClientService clientService, TrainingService trainingService, VisitationService visitationService, BaseTimeService baseTimeService, IPaginationViewFactory paginationViewFactory)
    {
        _gymService = gymService;
        _trainerService = trainerService;
        _clientService = clientService;
        _trainingService = trainingService;
        _visitationService = visitationService;
        _baseTimeService = baseTimeService;
        _paginationViewFactory = paginationViewFactory;
    }

    public async Task<bool> Exists(Guid trainerId)
    {
        return await _trainerService.Any(trainerId);
    }

    public async Task<bool> TrainerIsFree(Guid trainerId)
    {
        if (await Exists(trainerId) == false)
            throw new NotFoundException(typeof(Trainer), trainerId);
        
        return await _trainerService.IsFree(trainerId);
    }

    public async Task RegisterTraining(Guid trainerId, Guid clientId, uint totalHours)
    {
        if (await TrainerIsFree(trainerId) == false)
            throw new ArgumentException("Trainer is not free.");

        if (await _clientService.Any(clientId) == false)
            throw new NotFoundException(typeof(Client), clientId);

        if (await _clientService.HaveActiveAbboniture(clientId) == false)
            throw new ArgumentException("Client not have abboniture.");
        
        if (await ValidTrainingTime(totalHours))
        {
            var currentTime = _baseTimeService.GetCurrentDateTime();
            var trainingEndTime = currentTime.AddHours(totalHours);
            
            throw new AggregateException($"Not valid training time {currentTime} - {trainingEndTime}");
        }
        
        await _trainingService.Create(trainerId, clientId, totalHours);
    }

    public async Task<BasePaginationView<TrainerVM>> GetAllFree(Guid gym)
    {
        var all = _trainerService.GetAllFree(gym);

        return _paginationViewFactory.CreatePaginationView<Trainer, TrainerVM>(all);
    }

    public async Task<bool> TrainerInGym(Guid gym, Guid trainerId)
    {
        if(await _gymService.Any(gym) == false)
            throw new NotFoundException(typeof(Gym), trainerId);
        
        if (await Exists(trainerId) == false)
            throw new NotFoundException(typeof(Trainer), trainerId);

        return await _visitationService.TrainerInGym(gym, trainerId);
    }

    public async Task<bool> ValidTrainingTime(uint totalHours)
    {
        var currentTime = _baseTimeService.GetCurrentDateTime();
        var trainingFinishTime = currentTime.AddHours(totalHours);

        return currentTime.Day == trainingFinishTime.Day;
    }
}