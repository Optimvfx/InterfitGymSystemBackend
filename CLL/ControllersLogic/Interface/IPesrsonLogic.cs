namespace GymCardSystemBackend.Controllers.Admin;

public interface IPesrsonLogic
{
    Task<bool> Exists(Guid personGuidId);
}