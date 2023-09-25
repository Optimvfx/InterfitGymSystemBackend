namespace BLL.Models.Terminal;

public class TerminalCreationRequest
{
    public string Title { get; set; }
    public string? Description { get; set; }
    
    public Guid AdministratorId { get; set; }
    
    public Guid GymId { get; set; }
}