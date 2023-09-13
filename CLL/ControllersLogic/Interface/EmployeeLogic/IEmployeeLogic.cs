using BLL.Models.Employee;
using BLL.Models.Employee.Requests;
using DAL.Entities.Gym.Person;
using DAL.Entities.Gym.Person.Employeers;
using Timetable = BLL.Models.Employee.Timetable;

namespace CLL.ControllersLogic.Interface.EmployeeLogic;

public interface IEmployeeLogic
{
    IPositionLogic Position { get; }
    ITimetableLogic Timetable { get; }
    IVacationLogic Vacation { get; }
    
    Task<bool> ExistsEmployeeAsync(Guid id);
    Task<bool> EmployeeHaveVacationAsync(Guid id);
    Task<Guid> AddEmployeeAsync(EmployeeCreationRequest request);
    Task EditEmployeeAsync(Guid id, EmployeeEditRequest request);
    Task RemoveEmployeeAsync(Guid id); 
}
