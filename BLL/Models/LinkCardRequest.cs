namespace GymCardSystemBackend.Controllers.Terminal;

public class LinkCardRequest
{
    public Guid ClientId { get; set; }
    public byte[] CardCode { get; set; }
}