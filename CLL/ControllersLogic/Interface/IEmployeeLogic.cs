using BLL.Models.Employee;
using BLL.Models.Fininaces;
using Common.Models;
using Common.Models.PaginationView;

namespace CLL.ControllersLogic.Interface;

public interface IEmployeeLogic
{
    Task<Guid> Create(EmployeeCreationRequest employee);
    Task<EmployeeVM> Get(Guid id);
    Task<BasePaginationView<EmployeeVM>> GetAll();
    Task<BasePaginationView<EmployeeVM>> GetAllInGym(Guid gymId);
    Task<BasePaginationView<EmployeeVM>> GetTopBySalary(bool reversed, ValueRange<uint> range);
    Task<BasePaginationView<WorkTimeInfoVM>> GetTopByWorkHours(bool reversed, bool includeRecyclingWorks, ValueRange<DateOnly> dateRange);
    Task<bool> Exists(Guid id);
    Task Edit(Guid id, EmployeeEditRequest reqest);
    Task Dismiss(Guid id);
    Task Reinstatement(Guid id);
}