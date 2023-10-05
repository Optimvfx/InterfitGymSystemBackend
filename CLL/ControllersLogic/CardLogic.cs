using AutoMapper;
using BLL.Models.Card;
using BLL.Services.Database;
using BLL.Services.PaginationViewFactory;
using CLL.ControllersLogic.Interface;
using Common.Exceptions.General;
using Common.Exceptions.General.NotFoundException;
using Common.Helpers;
using Common.Models;
using Common.Models.PaginationView;
using DAL.Entities.Gym.Person;
using DAL.Entities.Primary;

namespace CLL.ControllersLogic;

public class CardLogic : ICardLogic
{
    private readonly CardService _cardService;
    private readonly ClientService _clientService;
    private readonly TrainerService _trainerService;
    private readonly PersonService _personService;

    private readonly IMapper _mapper;

    private readonly IPaginationViewFactory _paginationViewFactory;

    public CardLogic(CardService cardService, ClientService clientService, TrainerService trainerService, PersonService personService, IMapper mapper, IPaginationViewFactory paginationViewFactory)
    {
        _cardService = cardService;
        _clientService = clientService;
        _trainerService = trainerService;
        _personService = personService;
        _mapper = mapper;
        _paginationViewFactory = paginationViewFactory;
    }

    public async Task<Result<Guid>> GetCardId(byte[] cardCode)
    {
        if (await _cardService.ExistsByCode(cardCode) == false)
            return false;

        return _cardService.GetByCode(cardCode);
    }

    public async Task<Result<Guid>> TryGetPersonIdByCardCode(Guid id)
    {
        if (await _cardService.Any(id) == false)
            return false;

        return new(await _cardService.GetOwnerId(id));
    }

    public async Task<Result<Guid>> TryGetClientIdByCard(Guid id)
    {
        if (await _cardService.Any(id) == false)
            return false;

        var ownerId = await _cardService.GetOwnerId(id);

        if (await _clientService.Any(ownerId) == false)
            throw new ValueNotFoundByIdException(typeof(Client), ownerId);

        return new(ownerId);
    }

    public async Task<Result<Guid>> TryGetTrainerIdByCardCode(Guid id)
    {
        if (await _cardService.Any(id) == false)
            return false;

        var ownerId = await _cardService.GetOwnerId(id);

        if (await _trainerService.Any(ownerId) == false)
            throw new ValueNotFoundByIdException(typeof(Trainer), ownerId);

        return new(ownerId);
    }

    public async Task<bool> CardExistsInGym(Guid gymId, Guid cardId)
    {
        return await _cardService.Any(gymId, cardId);
    }

    public async Task<bool> CardIsTaked(Guid id)
    {
        if (await _cardService.Any(id) == false)
            throw new ValueNotFoundByIdException(typeof(Card), id);

        return await _cardService.IsTaked(id);
    }

    public async Task<bool> CardIsTakedByPerson(Guid id, Guid ownerId)
    {
        if (await _cardService.Any(id) == false)
            throw new ValueNotFoundByIdException(typeof(Card), id);

        return await _cardService.IsTaked(id, ownerId);
    }

    public async Task Link(Guid cardId, Guid personId)
    {
        if (await CardIsTaked(cardId))
            throw new ArgumentException("Card is already linked.");

        if (await _personService.Any(personId) == false)
            throw new ValueNotFoundByIdException(typeof(Person), personId);

        await _cardService.Link(cardId, personId);
    }

    public async Task UnLink(Guid cardId)
    {
        if (await CardIsTaked(cardId) == false)
            throw new ArgumentException("Card is not linked.");
        
        await _cardService.UnLink(cardId);
    }

    public async Task<Result<Guid>> Create(CardCreationRequest request)
    {
        return new(await _cardService.Create(request));
    }

    public async Task<Result<CardVM>> TryGet(Guid id)
    {
        if (await _cardService.Any(id) == false)
            return false;

        Card card = await _cardService.Get(id);
        var cardVm = _mapper.Map<CardVM>(card);

        return new(cardVm);
    }

    public async Task<BasePaginationView<CardVM>> GetAll(Guid? gymId = null)
    {
        IQueryable<Card> all;

        if (gymId == null)
            all = _cardService.GetAll();
        else
            all = _cardService.GetAll(gymId.Value);

        return _paginationViewFactory.CreatePaginationView<Card, CardVM>(all);
    }

    public async Task<BasePaginationView<CardVM>> GetAllNotLinked(Guid? gymId = null)
    {
        IQueryable<Card> all;

        if (gymId == null)
            all = _cardService.GetAll();
        else
            all = _cardService.GetAll(gymId.Value);

        return _paginationViewFactory.CreatePaginationView<Card, CardVM>(all.Where(c => c.OwnerId == null));
    }

    public async Task<BasePaginationView<CardVM>> GetAllLinked(Guid? gymId = null)
    {
        IQueryable<Card> all;

        if (gymId == null)
            all = _cardService.GetAll();
        else
            all = _cardService.GetAll(gymId.Value);

        return _paginationViewFactory.CreatePaginationView<Card, CardVM>(all.Where(c => c.OwnerId != null));
    }

    public async Task<bool> Exist(Guid id)
    {
        return await _cardService.Any(id);
    }

    public async Task<bool> IsEnabled(Guid id)
    {
        if (await _cardService.Any(id) == false)
            throw new ValueNotFoundByIdException(typeof(Card), id);

        return await _cardService.IsEnabled(id);
    }

    public async Task Disable(Guid id)
    {
        if (await IsEnabled(id) == false)
            throw new ArgumentException("Card is not enabled.");

        await _cardService.Disable(id);
    }

    public async Task Enable(Guid id)
    {
        if (await IsEnabled(id))
            throw new ArgumentException("Card is already enabled.");

        await _cardService.Enable(id);
    }

    public async Task Delete(Guid id)
    {
        if (await _cardService.Any(id) == false)
            throw new ValueNotFoundByIdException(typeof(Card), id);

        await _cardService.Delete(id);
    }
}

public class CardService
{
    public async Task<bool> ExistsByCode(byte[] cardCode)
    {
        throw new NotImplementedException();
    }

    public Result<Guid> GetByCode(byte[] cardCode)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Any(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<bool> Any(Guid gymId, Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> GetOwnerId(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsTaked(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<bool> IsTaked(Guid id, Guid ownerId)
    {
        throw new NotImplementedException();
    }

    public async Task Link(Guid cardId, Guid personId)
    {
        throw new NotImplementedException();
    }

    public async Task UnLink(Guid cardId)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> Create(CardCreationRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Card> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Card> GetAll()
    {
        throw new NotImplementedException();
    }
    
    public IQueryable<Card> GetAll(Guid gymId)
    {
        throw new NotImplementedException();
    }

    public  async Task<bool> IsEnabled(Guid id)
    {
        throw new NotImplementedException();
    }

    public  async Task Disable(Guid id)
    {
        throw new NotImplementedException();
    }

    public  async Task Enable(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}