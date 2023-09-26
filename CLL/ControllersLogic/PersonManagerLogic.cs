using AutoMapper;
using BLL.Models.PersonalManager;
using BLL.Services.PaginationViewFactory;
using CLL.ControllersLogic.Interface;
using Common.Exceptions.General;
using Common.Models;
using Common.Models.PaginationView;

namespace CLL.ControllersLogic;

public class PersonManagerLogic : IPersonalManagerLogic
{
    private readonly PersonalManagerService _personalManagerService;

    private readonly IPaginationViewFactory _paginationViewFactory;

    private readonly IMapper _mapper;

    public PersonManagerLogic(PersonalManagerService personalManagerService, IPaginationViewFactory paginationViewFactory, IMapper mapper)
    {
        _personalManagerService = personalManagerService;
        _paginationViewFactory = paginationViewFactory;
        _mapper = mapper;
    }

    public async Task<bool> IsEnabled(Guid id)
    {
        if (await Exist(id) == false)
            throw new NotFoundException(typeof(PersonalManager), id);
        
        return await _personalManagerService.IsEnabled(id);
    }

    public async Task Enable(Guid id)
    {
        if (await IsEnabled(id) == false)
            throw new ArgumentException("Already enable.");

        await _personalManagerService.Enable(id);
    }

    public async Task<bool> Exist(Guid id)
    {
        return await _personalManagerService.Any(id);
    }

    public async Task Disable(Guid id)
    {
        if (await IsEnabled(id))
            throw new ArgumentException("Already disable.");

        await _personalManagerService.Disable(id);
    }

    public async Task Edit(Guid id, PersonalManagerEditRequest reqest)
    {
        if (await Exist(id) == false)
            throw new NotFoundException(typeof(PersonalManager), id);

        await _personalManagerService.Edit(id, reqest);
    }

    public async Task<BasePaginationView<PersonalManagerVM>> GetAll()
    {
        var all = _personalManagerService.All();

        return _paginationViewFactory.CreatePaginationView<PersonalManager, PersonalManagerVM>(all);
    }

    public async Task<PersonalManagerVM> Get(Guid id)
    {
        if (await Exist(id) == false)
            throw new NotFoundException(typeof(PersonalManager), id);

        return _mapper.Map<PersonalManagerVM>(
            await _personalManagerService.Get(id));
    }

    public async Task<Guid> Create(PersonalManagerCreationRequest request)
    {
       return await _personalManagerService.Create(request);
    }
}

public class PersonalManager
{
}

public class PersonalManagerService
{
    public async Task<bool> IsEnabled(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Enable(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Any(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Disable(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Edit(Guid id, PersonalManagerEditRequest reqest)
    {
        throw new NotImplementedException();
    }

    public IQueryable<PersonalManager> All()
    {
        throw new NotImplementedException();
    }

    public async Task<PersonalManager> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> Create(PersonalManagerCreationRequest request)
    {
        throw new NotImplementedException();
    }
}