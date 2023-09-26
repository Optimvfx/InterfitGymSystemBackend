using BLL.Models.Gym;
using Common.Models;
using Common.Models.PaginationView;

namespace CLL.ControllersLogic.Interface;

public interface IGymLogic
{
    Task<bool> Exist(Guid id);
    Task<Guid> Create(GymCreationRequest request);
    Task<GymVM> Get(Guid id);
    Task<BasePaginationView<GymVM>> GetAll();
    Task Edit(Guid id, GymEditRequest reqest);
    Task<bool> IsEnabled(Guid id);
    Task Disable(Guid id);
    Task Enable(Guid id);
}