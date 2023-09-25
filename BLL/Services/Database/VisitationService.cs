using BLL.Models.Visitation;

namespace BLL.Services.Database;

public class VisitationService
{
    public async Task<VisitationVM> Create(Guid gymId, Guid personId)
    {
        throw new NotImplementedException();
    }

    public async Task<VisitationVM> Continume(Guid gymId, Guid personId)
    {
        throw new NotImplementedException();
    }

    public async Task Exit(Guid gymId, Guid personId)
    {
        throw new NotImplementedException();
    }

    public bool PersonInGym(Guid gymId, Guid personId)
    {
        throw new NotImplementedException();
    }
    
    public bool PersonInGym(Guid personId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> TrainerInGym(Guid gymId, Guid trainerId)
    {
        throw new NotImplementedException();
    }
}