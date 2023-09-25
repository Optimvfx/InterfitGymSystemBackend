using BLL.Models.Gym;
using Common.Models;
using Common.Models.PaginationView;

namespace CLL.ControllersLogic.Interface;

public interface IGymLogic
{
    Task<bool> Exist(Guid id);
    Task<Result<Guid>> Create(GymCreationRequest request);
    Task<Result<GymVM>> TryGet(Guid id);
    Task<BasePaginationView<GymVM>> GetAll();
    Task<bool> Edit(Guid id, GymEditRequest reqest);
    Task<bool> IsEnabled(Guid id);
    Task Disable(Guid id);
    Task Enable(Guid id);
}