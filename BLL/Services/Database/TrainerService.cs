using DAL.Entities.Gym.Person;

namespace BLL.Services.Database;

public class TrainerService
{
    public async Task<bool> Any(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsFree(Guid id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Trainer> GetAllFree(Guid gym)
    {
        throw new NotImplementedException();
    }
}