namespace GymCardSystemBackend.Controllers.Terminal;

public class RegisterTrainingSaleRequest
{   
    public byte[] TrainerCardCode { get; set; }
    public byte[] ClienCardCode { get; set; }
    public uint TotalHours { get; set; }
}