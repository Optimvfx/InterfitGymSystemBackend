using AutoMapper;
using BLL.Models.Terminal;
using BLL.Services.Database;
using BLL.Services.PaginationViewFactory;
using CLL.ControllersLogic.Interface;
using Common.Exceptions.General;
using Common.Models;
using Common.Models.PaginationView;
using DAL.Entities.Access.AccessType;
using DAL.Entities.Gym;
using DAL.Entities.Gym.Person.Employeers;

namespace CLL.ControllersLogic;

public class TerminalLogic : ITerminalLogic
{
    private readonly TerminalService _terminalService;
    private readonly GymService _gymService;
    private readonly TerminalAdministratorService _terminalAdministratorService;

    private readonly IMapper _mapper;
    private readonly IPaginationViewFactory _paginationViewFactory;

    public TerminalLogic(TerminalService terminalService, GymService gymService, TerminalAdministratorService terminalAdministratorService, IMapper mapper, IPaginationViewFactory paginationViewFactory)
    {
        _terminalService = terminalService;
        _gymService = gymService;
        _terminalAdministratorService = terminalAdministratorService;
        _mapper = mapper;
        _paginationViewFactory = paginationViewFactory;
    }

    public async Task<Guid> Create(TerminalCreationRequest terminal)
    {
        if (await _gymService.Any(terminal.GymId) == false)
            throw new NotFoundException(typeof(Gym), terminal.GymId);

        if (await _terminalAdministratorService.Any(terminal.AdministratorId) == false)
            throw new NotFoundException(typeof(TerminalAdministrator), terminal.AdministratorId);

        return await _terminalService.Create(terminal);
    }

    public async Task<TerminalVM> TryGet(Guid id)
    {
        if (await _terminalService.Any(id) == false)
            throw new NotFoundException(typeof(Terminal), id);

        var terminal = await _terminalService.Get(id);
        
        return _mapper.Map<TerminalVM>(terminal);
    }

    public async Task<BasePaginationView<TerminalVM>> GetAll()
    {
        var all = _terminalService.All();

        return _paginationViewFactory.CreatePaginationView<Terminal, TerminalVM>(all);
    }

    public async Task Edit(Guid id, TerminalEditRequest reqest)
    {
        if (await _terminalService.Any(id) == false)
            throw new NotFoundException(typeof(Terminal), id);

        await _terminalService.Edit(id, reqest);
    }

    public async Task<bool> Exist(Guid id)
    {
        return await _terminalService.Any(id);
    }

    public async Task<bool> IsEnabled(Guid id)
    {
        if (await _terminalService.Any(id) == false)
            throw new NotFoundException(typeof(Terminal), id);
        
        return await _terminalService.Enabled(id);
    }

    public async Task Disable(Guid id)
    {
        if (await _terminalService.Any(id) == false)
            throw new NotFoundException(typeof(Terminal), id);

        await _terminalService.Disable(id);
    }

    public async Task Enable(Guid id)
    {
        if (await _terminalService.Any(id) == false)
            throw new NotFoundException(typeof(Terminal), id);
        
        await _terminalService.Enable(id);
    }
}