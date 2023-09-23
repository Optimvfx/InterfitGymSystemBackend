using Common.Models;
using GymCardSystemBackend.Controllers.BusinessOwner;

namespace GymCardSystemBackend.Controllers.Terminal;

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