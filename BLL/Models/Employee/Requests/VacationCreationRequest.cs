namespace BLL.Models.Employee.Requests;

public class VacationCreationRequest    
{
    public string? Reson { get; set; }
    public uint DurationInDays { get; set; }
}