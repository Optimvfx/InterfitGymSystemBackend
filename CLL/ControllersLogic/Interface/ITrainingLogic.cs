using Common.Models;

namespace GymCardSystemBackend.Controllers.Terminal;

public interface ITrainingLogic
{
    Task<bool> Exists(Guid gymId, Guid trainerId);
    Task<bool> TrainerIsFree(Guid gym, Guid trainerId);
    
    Task<bool> TryRegisterTraining(Guid trainerId, Guid clientId, uint requestTotalHours);
    Task<BasePaginationView<TrainerVM>> GetAllFree(Guid gym);
    Task<bool> TrainerInGym(Guid gym, Guid trainerId);
}