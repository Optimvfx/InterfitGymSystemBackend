using BLL.Models.Personality;

namespace BLL.Models.Employee.Requests;

public class EmployeeCreationRequest : PersonCreationRequest
{
    public Guid TimetableId { get; set; }
    public Guid PositionId { get; set; }
    
    public uint Wages { get; set; }
}