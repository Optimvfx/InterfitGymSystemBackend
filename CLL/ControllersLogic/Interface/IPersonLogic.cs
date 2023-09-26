namespace CLL.ControllersLogic.Interface;

public interface IPersonLogic
{
    Task<bool> Exists(Guid id);
}