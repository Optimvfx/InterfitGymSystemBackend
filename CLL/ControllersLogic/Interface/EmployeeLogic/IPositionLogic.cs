using DAL.Entities.Gym.Person.Employeers;

namespace CLL.ControllersLogic.Interface.EmployeeLogic;

public interface IPositionLogic
{
    Task<bool> ExistsPositionAsync(Guid id);
    Task<Position> GetPositionAsync(Guid id);
    Task<Guid> AddPositionAsync(string title, string? description);
    Task EditPositionAsync(Guid id, string newTitle, string newDescription);
    Task EditPositionAsync(Guid id, string newTitle);
    Task RemovePositionAsync(Guid id); 
}