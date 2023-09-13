using BLL.Services;
using CLL.ControllersLogic.Interface.EmployeeLogic;
using DAL.Entities.Gym.Person.Employeers;

namespace CLL.ControllersLogic;

public class PositionLogic : IPositionLogic
{
    private readonly PositionService _service;

    public PositionLogic(PositionService service)
    {
        _service = service;
    }
    
    public async Task<bool> ExistsPositionAsync(Guid id) => await _service.AnyAsync(id);

    public async Task<Position> GetPositionAsync(Guid id) => await _service.GetAsync(id);

    public async Task<Guid> AddPositionAsync(string title, string? description) =>
        await _service.AddAsync(title, description);
    
    public async Task EditPositionAsync(Guid id, string newTitle, string newDescription) =>
        await _service.EditAsync(id, newTitle, newDescription);

    public async Task EditPositionAsync(Guid id, string newTitle)=>
        await _service.EditAsync(id, newTitle);

    public async Task RemovePositionAsync(Guid id) => await _service.DeleteAsync(id);
}