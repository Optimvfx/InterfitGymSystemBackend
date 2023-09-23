using Common.Models;

namespace GymCardSystemBackend.Controllers.BusinessOwner;

public interface ISalaryLogic
{
    Task<BasePaginationView<SalaryVM>> GetAll(bool includeRecycling, ValueRange<DateOnly> dataRange);
    Task<BasePaginationView<SalaryVM>> GetAllByGym(Guid gymGuidId, bool includeRecycling, ValueRange<DateOnly> dataRange);
}