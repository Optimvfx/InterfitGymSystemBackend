using Common.Models;

namespace GymCardSystemBackend.Controllers.Admin;

public interface ITerminalLogic
{
    Task<Result<Guid>> Create(TerminalCreationRequest terminal);
    Task<Result<TerminalVM>> TryGet(Guid id);
    Task<BasePaginationView<TerminalVM>> GetAll();
    Task<bool> Edit(Guid id, TerminalEditRequest reqest);
    Task<bool> Exist(Guid id);
    Task<bool> IsEnabled(Guid id);
    Task Disable(Guid id);
    Task Enable(Guid id);
}