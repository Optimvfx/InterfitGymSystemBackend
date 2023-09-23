using Common.Models;

namespace GymCardSystemBackend.Controllers.Terminal;

public interface IClientLogic
{
    Task<bool> Exists(Guid clientId);
    Task<Result<Guid>> Create(CreateClientRequest request);
    Task ApplyEdit(Guid guidId, EditClientRequest request);
}