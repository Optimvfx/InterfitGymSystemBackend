using BLL.Models.Employee;
using BLL.Services;
using CLL.ControllersLogic.Interface.EmployeeLogic;
using DAL.Entities.Gym.Person.Employeers;

namespace CLL.ControllersLogic;

public class VacationLogic : IVacationLogic
{
    private readonly VacationService _service;

    public VacationLogic(VacationService service)
    {
        _service = service;
    }

    public async Task<bool> ExistsVacationAsync(Guid id) => await _service.AnyAsync(id);

    public async Task<Vacation> GetVacationAsync(Guid id) => await _service.GetAsync(id);

    public async Task<Guid> AddVacationAsync(Guid employeeId, VacationCreationRequest vacation) => await _service.AddAsync(employeeId, vacation);
}