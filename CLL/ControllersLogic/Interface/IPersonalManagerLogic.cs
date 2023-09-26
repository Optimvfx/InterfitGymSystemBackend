using BLL.Models.PersonalManager;
using Common.Models;
using Common.Models.PaginationView;

namespace CLL.ControllersLogic.Interface;

public interface IPersonalManagerLogic
{
    Task<bool> IsEnabled(Guid id);
    Task Enable(Guid id);
    Task<bool> Exist(Guid id);
    Task Disable(Guid id);
    Task Edit(Guid id, PersonalManagerEditRequest reqest);
    Task<BasePaginationView<PersonalManagerVM>> GetAll();
    Task<PersonalManagerVM> Get(Guid id);
    Task<Guid> Create(PersonalManagerCreationRequest request);
}