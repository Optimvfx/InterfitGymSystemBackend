using BLL.Models.Client;
using Common.Models;

namespace CLL.ControllersLogic.Interface;

public interface IClientLogic
{
    Task<bool> Exists(Guid clientId);
    Task<Result<Guid>> Create(CreateClientRequest request);
    Task ApplyEdit(Guid guidId, EditClientRequest request);
}