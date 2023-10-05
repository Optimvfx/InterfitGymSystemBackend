using AutoMapper;
using BLL.Models.Abboniture;
using BLL.Services.Database;
using BLL.Services.PaginationViewFactory;
using CLL.ControllersLogic.Interface;
using Common.Exceptions.General;
using Common.Exceptions.General.NotFoundException;
using Common.Models;
using Common.Models.PaginationView;
using DAL.Entities.Gym.Person;
using DAL.Entities.Gym.Person.Clients;
using DAL.Entities.Gym.SalesLogic;

namespace CLL.ControllersLogic;

public class AbbonitureLogic : IAbbonitureLogic
{
    private readonly AbbonitureProfileService _abbonitureProfileService;
    private readonly ClientService _clientService;
    private readonly SaleService _saleService;
    
    private readonly IPaginationViewFactory _paginationViewFactory;
    private readonly IMapper _mapper;

    public AbbonitureLogic(AbbonitureProfileService abbonitureProfileService, ClientService clientService, SaleService saleService, IPaginationViewFactory paginationViewFactory, IMapper mapper)
    {
        _abbonitureProfileService = abbonitureProfileService;
        _clientService = clientService;
        _saleService = saleService;
        _paginationViewFactory = paginationViewFactory;
        _mapper = mapper;
    }

    public async Task<BasePaginationView<AbbonitureProfileVM>> GetAll()
    {
        IQueryable<AbbonitureProfile> all = _abbonitureProfileService.GetAll();

        return _paginationViewFactory.CreatePaginationView<AbbonitureProfile, AbbonitureProfileVM>(all);
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _abbonitureProfileService.Any(id);
    }

    public async Task<bool> ClientHaveActiveAbboniture(Guid clientId)
    {
        if (await _clientService.Any(clientId) == false)
            throw new ValueNotFoundByIdException(typeof(Client), clientId);

        return await _clientService.HaveActiveAbboniture(clientId);
    }

    public async Task RegisterSale(Guid clientId, Guid abbonitureId)
    {
        if (await _clientService.HaveActiveAbboniture(clientId))
            throw new AlreadyExistException(typeof(AbbonitureProfile));
        
        if (await _clientService.Any(clientId) == false)
            throw new ValueNotFoundByIdException(typeof(Client), clientId);
        
        if (await _abbonitureProfileService.Any(abbonitureId) == false)
            throw new ValueNotFoundByIdException(typeof(AbbonitureProfile), abbonitureId);

        await _saleService.Create(clientId, abbonitureId);
    }

    public async Task Create(CreateAbbonitureProfileRequest request)
    {
        await _abbonitureProfileService.Create(request);
    }

    public async Task Edit(Guid id, EditAbbonitureProfileRequest request)
    {
        if (await _abbonitureProfileService.Any(id) == false)
            throw new ValueNotFoundByIdException(typeof(AbbonitureProfile),id);

        await _abbonitureProfileService.Edit(id, request);
    }

    public async Task Delete(Guid id)
    {
        if (await _abbonitureProfileService.Any(id) == false)
            throw new ValueNotFoundByIdException(typeof(AbbonitureProfile),id);

        await _abbonitureProfileService.Delete(id);
    }
}