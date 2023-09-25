namespace BLL.Models.Abboniture;

public class CreateAbbonitureProfileRequest
{
    public string Title { get; set; }   
    
    public uint Price { get; set; }
    
    public uint? VisitLimit { get; set; }
    public uint? DateLimitInDays { get; set; }
}