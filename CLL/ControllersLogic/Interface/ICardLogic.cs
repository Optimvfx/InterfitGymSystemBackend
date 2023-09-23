using BLL.Models;
using Common.Models;

namespace CLL.ControllersLogic.Interface;

public interface ICardLogic
{
    Task<Result<Guid>> GetCardId(byte[] cardCode);
    
    Task<Result<Guid>> TryGetPersonIdByCardCode(Guid cardId);
    Task<Result<Guid>> TryGetClientIdByCard(Guid requestClientCardId);
    Task<Result<Guid>> TryGetTrainerIdByCardCode(Guid requestTrainerCardId);
    Task<bool> CardExistsInGym(Guid gym, Guid cardId);
    Task<bool> CardIsTaked(Guid id);
    Task<bool> CardIsTakedByPerson(Guid id, Guid personId);
    Task Link(Guid cardId, Guid clientId);
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