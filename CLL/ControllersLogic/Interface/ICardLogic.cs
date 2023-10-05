using BLL.Models;
using BLL.Models.Card;
using Common.Models;
using Common.Models.PaginationView;

namespace CLL.ControllersLogic.Interface;

public interface ICardLogic
{
    Task<Result<Guid>> GetCardId(byte[] cardCode);
    
    Task<Result<Guid>> TryGetPersonIdByCardCode(Guid id);
    Task<Result<Guid>> TryGetClientIdByCard(Guid id);
    Task<Result<Guid>> TryGetTrainerIdByCardCode(Guid id);
    Task<bool> CardExistsInGym(Guid gymId, Guid cardId);
    Task<bool> CardIsTaked(Guid id);
    Task<bool> CardIsTakedByPerson(Guid id, Guid ownerId);
    Task Link(Guid cardId, Guid personId);
    Task UnLink(Guid cardId);
    Task<Result<Guid>> Create(CardCreationRequest request);
    Task<Result<CardVM>> TryGet(Guid id);
    Task<BasePaginationView<CardVM>> GetAll(Guid? gymId = null);
    Task<BasePaginationView<CardVM>> GetAllNotLinked(Guid? gymId = null);
    Task<BasePaginationView<CardVM>> GetAllLinked(Guid? gymId = null);
    Task<bool> Exist(Guid id);
    Task<bool> IsEnabled(Guid id);
    Task Disable(Guid id);
    Task Enable(Guid id);
    Task Delete(Guid id);
}