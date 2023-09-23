using BLL.Models.Employee.Requests;
using Common.Models;

namespace GymCardSystemBackend.Controllers.PersonalManager;

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