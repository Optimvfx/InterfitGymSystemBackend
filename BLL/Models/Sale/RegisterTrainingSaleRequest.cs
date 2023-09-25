namespace BLL.Models.Sale;

public class RegisterTrainingSaleRequest
{   
    public byte[] TrainerCardCode { get; set; }
    public byte[] ClienCardCode { get; set; }
    public uint TotalHours { get; set; }
}