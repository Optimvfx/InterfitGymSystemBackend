using BLL.Models.Client;
using BLL.Services.Database;
using CLL.ControllersLogic.Interface;
using Common.Exceptions.General;
using Common.Exceptions.General.NotFoundException;
using Common.Models;
using DAL.Entities.Gym.Person;

namespace CLL.ControllersLogic;

public class ClientLogic : IClientLogic
{
    private ClientService _clientService;

    public ClientLogic(ClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _clientService.Any(id);
    }

    public async Task<Guid> Create(CreateClientRequest request)
    {
        return await _clientService.Create(request);
    }

    public async Task ApplyEdit(Guid id, EditClientRequest request)
    {
        if (await Exists(id) == false)
            throw new ValueNotFoundByIdException(typeof(Client), id);
        
        await _clientService.Edit(id, request);
    }
}