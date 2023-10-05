using AutoMapper;
using BLL.Models.Gym;
using BLL.Services.Database;
using BLL.Services.PaginationViewFactory;
using CLL.ControllersLogic.Interface;
using Common.Exceptions.General;
using Common.Exceptions.General.NotFoundException;
using Common.Models;
using Common.Models.PaginationView;
using DAL.Entities.Gym;

namespace CLL.ControllersLogic;

public class GymLogic : IGymLogic
{
    private readonly GymService _gymService;
    private readonly IPaginationViewFactory _paginationViewFactory;
    private readonly IMapper _mapper;

    public GymLogic(GymService gymService, IPaginationViewFactory paginationViewFactory, IMapper mapper)
    {
        _gymService = gymService;
        _paginationViewFactory = paginationViewFactory;
        _mapper = mapper;
    }

    public async Task<bool> Exist(Guid id)
    {
       return await _gymService.Any(id);
    }

    public async Task<Guid> Create(GymCreationRequest request)
    {
        return await _gymService.Create(request);
    }

    public async Task<GymVM> Get(Guid id)
    {
        if (await Exist(id) == false)
            throw new ValueNotFoundByIdException(typeof(Gym), id);

        return _mapper.Map<GymVM>(await _gymService.Get(id));
    }

    public async Task<BasePaginationView<GymVM>> GetAll()
    {
        IQueryable<Gym> gyms = _gymService.All();

        return _paginationViewFactory.CreatePaginationView<Gym, GymVM>(gyms);
    }

    public async Task Edit(Guid id, GymEditRequest reqest)
    {
        if (await Exist(id) == false)
            throw new ValueNotFoundByIdException(typeof(Gym), id);

        await _gymService.Edit(reqest);
    }

    public async Task<bool> IsEnabled(Guid id)
    {
        if (await Exist(id) == false)
            throw new ValueNotFoundByIdException(typeof(Gym), id);
        
        return _gymService.IsEnabled(id);
    }

    public async Task Enable(Guid id)
    {
        if (await IsEnabled(id) == false)
            throw new ArgumentException("Already enable.");

        await _gymService.Enable(id);
    }

    public async Task Disable(Guid id)
    {
        if (await IsEnabled(id))
            throw new ArgumentException("Already disable.");

        await _gymService.Disable(id);
    }
}