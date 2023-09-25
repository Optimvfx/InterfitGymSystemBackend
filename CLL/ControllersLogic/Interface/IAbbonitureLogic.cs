using BLL.Models.Abboniture;
using Common.Models;
using Common.Models.PaginationView;

namespace CLL.ControllersLogic.Interface;

public interface IAbbonitureLogic
{
    Task<BasePaginationView<AbbonitureProfileVM>> GetAll();
    Task<bool> Exists(Guid id);
    Task<bool> ClientHaveActiveAbboniture(Guid clientId);
    Task RegisterSale(Guid clientId, Guid abbonitureId);
    Task Create(CreateAbbonitureProfileRequest request);
    Task Edit(Guid id, EditAbbonitureProfileRequest request);
    Task Delete(Guid id);
}