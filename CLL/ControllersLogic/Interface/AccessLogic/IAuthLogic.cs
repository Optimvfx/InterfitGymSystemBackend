using Common.Models;

namespace CLL.ControllersLogic.Interface.AccessLogic;

public interface IAuthLogic
{
    Task<Result<string>> TryGetJwt(string apiKey);
    
    Task<bool> AccessIsAdmin(Guid id);
    Task<Result<Guid>> TryGetGymIdByAccess(Guid id);
}