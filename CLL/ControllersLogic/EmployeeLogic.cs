using BLL.Models.Employee;
using BLL.Models.Employee.Requests;
using BLL.Services;
using CLL.ControllersLogic.Interface;
using CLL.ControllersLogic.Interface.EmployeeLogic;
using DAL.Entities.Gym.Person;
using DAL.Entities.Gym.Person.Employeers;

namespace CLL.ControllersLogic;

public class EmployeeLogic : IEmployeeLogic
{
    public IPositionLogic Position { get; private set; }
    public ITimetableLogic Timetable { get; private set; }
    public IVacationLogic Vacation { get; private set; }
    
    private readonly EmployeeService _service;

    public EmployeeLogic(EmployeeService service)
    {
        _service = service;
    }

    public async Task<bool> ExistsEmployeeAsync(Guid id) 

    public async Task<bool> EmployeeHaveVacationAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> AddEmployeeAsync(EmployeeCreationRequest request) => await _service.AddAsync(request);

    public async Task EditEmployeeAsync(Guid id, EmployeeEditRequest request) => await _service.EditAsync();

    public async Task RemoveEmployeeAsync(Guid id) => await _service.DeleteAsync(id);
}