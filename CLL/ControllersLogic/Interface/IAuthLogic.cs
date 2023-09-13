using Common.Models;

namespace CLL.ControllersLogic.Interface;

public interface IAuthLogic
{
    Task<Result<string>> TryGetJwt(string apiKey);
    
    Task<Result<Guid>> TryGetAdminIdByAccess(Guid id);
}