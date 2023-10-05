using AutoMapper;
using BLL.Models.Visitation;
using BLL.Services.Database;
using BLL.Services.PaginationViewFactory;
using BLL.Services.VisitationResultFactory;
using CLL.ControllersLogic.Interface;
using Common.Exceptions.General;
using Common.Exceptions.General.NotFoundException;
using DAL.Entities.Gym;
using DAL.Entities.Gym.Person;
using DAL.Entities.Gym.SalesLogic;

namespace CLL.ControllersLogic;

public class VisitsLogic : IVisitsLogic
{
    private readonly GymService _gymService;
    private readonly PersonService _personService;
    private readonly VisitationService _visitationService;
    private readonly AbbonitureService _abbonitureService;
    
    private readonly IPaginationViewFactory _paginationViewFactory;
    private readonly IMapper _mapper;

    public VisitsLogic(GymService gymService, PersonService personService, VisitationService visitationService, AbbonitureService abbonitureService, IPaginationViewFactory paginationViewFactory, IMapper mapper)
    {
        _gymService = gymService;
        _personService = personService;
        _visitationService = visitationService;
        _abbonitureService = abbonitureService;
        _paginationViewFactory = paginationViewFactory;
        _mapper = mapper;
    }

    public async Task<VisitationVM> Register(Guid gymId, Guid personId)
    {
        if (await _gymService.Any(gymId) == false)
            throw new ValueNotFoundByIdException(typeof(Gym), gymId);

        if (await _personService.Any(personId) == false)
            throw new ValueNotFoundByIdException(typeof(Person), personId);
        
        if (await PersonInGym(personId))
            throw new ArgumentException("Person in gym already.");

        if (await PersonCanVisitGym(personId) == false)
            throw new ArgumentException("Person cant visit gym.");

        await _abbonitureService.TakeVisit(personId);
        return await _visitationService.Create(gymId, personId);
    }

    public async Task<VisitationVM> ContinumeVisit(Guid gymId, Guid personId)
    {
        if (await _gymService.Any(gymId) == false)
            throw new ValueNotFoundByIdException(typeof(Gym), gymId);

        if (await _personService.Any(personId) == false)
            throw new ValueNotFoundByIdException(typeof(Person), personId);
        
        if (await PersonInGym(gymId, personId) == false)
            throw new ArgumentException("Person not in gym.");
        
        return await _visitationService.Continume(gymId, personId);
    }

    public async Task Exit(Guid gymId, Guid personId)
    {
        if (await _gymService.Any(gymId) == false)
            throw new ValueNotFoundByIdException(typeof(Gym), gymId);

        if (await _personService.Any(personId) == false)
            throw new ValueNotFoundByIdException(typeof(Person), personId);
        
        if (await PersonInGym(gymId, personId) == false)
            throw new ArgumentException("Person not in gym.");
        
        await _visitationService.Exit(gymId, personId);
    }

    public async Task<bool> PersonInGym(Guid gymId, Guid personId)
    {
        if (await _gymService.Any(gymId) == false)
            throw new ValueNotFoundByIdException(typeof(Gym), gymId);

        if (await _personService.Any(personId) == false)
            throw new ValueNotFoundByIdException(typeof(Person), personId);

        return _visitationService.PersonInGym(gymId, personId);
    }

    public async Task<bool> PersonInGym(Guid personId)
    {
        if (await _personService.Any(personId) == false)
            throw new ValueNotFoundByIdException(typeof(Person), personId);

        return _visitationService.PersonInGym(personId);
    }

    public async Task<bool> PersonCanVisitGym(Guid personId)
    {
        if (await _personService.Any(personId) == false)
            throw new ValueNotFoundByIdException(typeof(Person), personId);

        return _abbonitureService.HasActiveAbboniture(personId);
    }
}