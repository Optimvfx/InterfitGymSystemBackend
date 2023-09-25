namespace BLL.Models.Sale;

public class RegisterAbbonitureSaleRequest
{
    public byte[] ClientCardCode { get; set; }
    public Guid AbbonitureId { get; set; }
}