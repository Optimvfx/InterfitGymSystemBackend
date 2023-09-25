namespace CLL.ControllersLogic.Interface;

public interface IPesrsonLogic
{
    Task<bool> Exists(Guid personGuidId);
}