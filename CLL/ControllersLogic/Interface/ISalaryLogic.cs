using BLL.Models.Fininaces;
using Common.Models;
using Common.Models.PaginationView;

namespace CLL.ControllersLogic.Interface;

public interface ISalaryLogic
{
    Task<BasePaginationView<SalaryVM>> GetAll(ValueRange<DateOnly> dataRange);
    Task<BasePaginationView<SalaryVM>> GetAllByGym(Guid gymId, ValueRange<DateOnly> dataRange);
}