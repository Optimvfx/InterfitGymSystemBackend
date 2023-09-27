using BLL.Models.Employee;
using DAL.Entities.Gym.Person;

namespace BLL.Services.Database;

public class EmployeeService
{
    public IQueryable<Employee> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> Create(EmployeeCreationRequest employee)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Any(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<EmployeeVM> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Edit(EmployeeEditRequest reqest)
    {
        throw new NotImplementedException();
    }
}