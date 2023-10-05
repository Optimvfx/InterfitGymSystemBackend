namespace BLL.Models.Hardware.Breakdowm;

public class TrainingDeviceBreakdownRegisterRequest
{
    public Guid TrainingDeviceId { get; set; }
    public Guid BreakdownTypeId { get; set; }
}