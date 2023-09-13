using BLL.Models.Employee;
using DAL.Entities.Gym.Person.Employeers;

namespace CLL.ControllersLogic.Interface.EmployeeLogic;

public interface IVacationLogic
{
    Task<bool> ExistsVacationAsync(Guid id);
    Task<Vacation> GetVacationAsync(Guid id);
    Task<Guid> AddVacationAsync(Guid employeeId, VacationCreationRequest vacation);
}