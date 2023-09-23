using BLL.Models.Visitation;

namespace CLL.ControllersLogic.Interface;

public interface IVisitsLogic
{
    Task<VisitationVM> TryRegister(Guid gymId, Guid personId);
    Task<VisitationVM> ContinumeVisit(Guid gymId, Guid personId);
    Task Exit(Guid gymId, Guid personId);
    
    Task<bool> PersonInGym(Guid gymId, Guid personId);
    Task<bool> PersonInGym(Guid personId);
    Task<bool> PersonCanVisitGym(Guid personId);
}