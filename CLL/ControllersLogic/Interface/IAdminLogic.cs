namespace CLL.ControllersLogic.Interface;

public interface IAdminLogic
{
    void CreateTerminalApiKey(Guid adminId, Guid terminalId);
    void CreateTerminal(Guid terminalId, Guid terminalAdminId);
}