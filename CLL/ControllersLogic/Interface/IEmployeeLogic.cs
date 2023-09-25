using BLL.Models.Employee;
using Common.Models;
using Common.Models.PaginationView;

namespace CLL.ControllersLogic.Interface;

public interface IEmployeeLogic
{
    Task<Result<Guid>> TryCreate(EmployeeCreationRequest employee);
    Task<Result<EmployeeVM>> TryGet(Guid id);
    Task<BasePaginationView<EmployeeVM>> GetAll();
    Task<BasePaginationView<EmployeeVM>> GetAllInGym(Guid gymId);
    Task<BasePaginationView<EmployeeVM>> GetTopBySalary(bool reversed, ValueRange<uint> range);
    Task<BasePaginationView<EmployeeVM>> GetTopByWorkHours(bool reversed, bool includeRecyclingWorks);
    Task<bool> Exists(Guid id);
    Task Edit(Guid id, EmployeeEditRequest reqest);
    Task Dismiss(Guid id);
    Task Reinstatement(Guid id);
}