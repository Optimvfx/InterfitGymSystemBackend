using BLL.Models.Terminal;
using DAL.Entities.Access.AccessType;

namespace BLL.Services.Database;

public class TerminalService
{
    public async Task<Guid> Create(TerminalCreationRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Any(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Terminal> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Terminal> All()
    {
        throw new NotImplementedException();
    }

    public async Task Edit(Guid id, TerminalEditRequest reqest)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Enabled(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Enable(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Disable(Guid id)
    {
        throw new NotImplementedException();
    }
}