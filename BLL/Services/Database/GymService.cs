using BLL.Models.Gym;
using DAL.Entities.Gym;
using DAL.Entities.Gym.Person;
using DAL.Entities.Gym.SalesLogic;

namespace BLL.Services.Database;

public class GymService
{
    public async Task<bool> Any(Guid gymId)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Employee> GetAllEmployee(Guid gymId)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> Create(GymCreationRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Gym> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Gym> All()
    {
        throw new NotImplementedException();
    }

    public async Task Edit(GymEditRequest reqest)
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Disable(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Enable(Guid id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Order> GetAllOrders(Guid gymId)
    {
        throw new NotImplementedException();
    }
}