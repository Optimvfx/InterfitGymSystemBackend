using BLL.Models.Personality;
using DAL.Entities.Gym.Person;

namespace BLL.Models.Employee;

public class EmployeeCreationRequest : PersonCreationRequest
{
    public Guid TimetableId { get; set; }
    public Guid PositionId { get; set; }
    
    public uint Wages { get; set; }
}