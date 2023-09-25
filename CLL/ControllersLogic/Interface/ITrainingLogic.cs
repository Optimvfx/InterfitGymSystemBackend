using BLL.Models.Trainer;
using Common.Models;
using Common.Models.PaginationView;

namespace CLL.ControllersLogic.Interface;

public interface ITrainingLogic
{
    Task<bool> Exists(Guid trainerId);
    Task<bool> TrainerIsFree(Guid trainerId);
    
    Task RegisterTraining(Guid trainerId, Guid clientId, uint totalHours);
    Task<BasePaginationView<TrainerVM>> GetAllFree(Guid gym);
    Task<bool> TrainerInGym(Guid gym, Guid trainerId);
    Task<bool> ValidTrainingTime(uint totalHours);
}