namespace BLL.Models.Card;

public class LinkCardRequest
{
    public Guid ClientId { get; set; }
    public byte[] CardCode { get; set; }
}