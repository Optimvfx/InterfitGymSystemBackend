using Common.Models;

namespace GymCardSystemBackend.Controllers.Admin;

public interface IPersonalManagerLogic
{
    Task<bool> IsEnabled(Guid id);
    Task Enable(Guid id);
    Task<bool> Exist(Guid id);
    Task Disable(Guid id);
    Task<bool> Edit(Guid id, PersonalManagerEditRequest reqest);
    Task<BasePaginationView<PersonalManagerVM>> GetAll();
    Task<Result<PersonalManagerVM>> TryGet(Guid id);
    Task<Result<Guid>> Create(PersonalManagerCreationRequest request);
}