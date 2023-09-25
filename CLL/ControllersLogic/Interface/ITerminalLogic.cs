using BLL.Models.Terminal;
using Common.Models;
using Common.Models.PaginationView;

namespace CLL.ControllersLogic.Interface;

public interface ITerminalLogic
{
    Task<Guid> Create(TerminalCreationRequest terminal);
    Task<TerminalVM> TryGet(Guid id);
    Task<BasePaginationView<TerminalVM>> GetAll();
    Task Edit(Guid id, TerminalEditRequest reqest);
    Task<bool> Exist(Guid id);
    Task<bool> IsEnabled(Guid id);
    Task Disable(Guid id);
    Task Enable(Guid id);
}