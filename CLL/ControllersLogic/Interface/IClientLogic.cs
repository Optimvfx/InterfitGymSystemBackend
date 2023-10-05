using BLL.Models.Client;
using Common.Models;

namespace CLL.ControllersLogic.Interface;

public interface IClientLogic
{
    Task<bool> Exists(Guid id);
    Task<Guid> Create(CreateClientRequest request);
    Task ApplyEdit(Guid id, EditClientRequest request);
}