namespace GymCardSystemBackend.Controllers.Terminal;

public class TrainingDeviceBreakdownRegisterRequest
{
    public Guid TrainingDeviceId { get; set; }
    public Guid BreakdownId { get; set; }
}