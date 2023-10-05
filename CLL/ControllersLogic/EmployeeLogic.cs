using BLL.Models.Employee;
using BLL.Models.Fininaces;
using BLL.Services.Database;
using BLL.Services.PaginationViewFactory;
using BLL.Services.SalaryCalculator;
using BLL.Services.WorkTimeCalculator;
using CLL.ControllersLogic.Interface;
using Common.Exceptions.General;
using Common.Exceptions.General.NotFoundException;
using Common.Models;
using Common.Models.PaginationView;
using DAL.Entities.Gym;
using DAL.Entities.Gym.Person;

namespace CLL.ControllersLogic;

public class EmployeeLogic : IEmployeeLogic
{
    private readonly EmployeeService _employeeService;
    private readonly GymService _gymService;

    private readonly IPaginationViewFactory _paginationViewFactory;
    private readonly BaseWorkTimeCalculator _workTimeCalculator;

    public EmployeeLogic(EmployeeService employeeService, GymService gymService, IPaginationViewFactory paginationViewFactory, BaseWorkTimeCalculator workTimeCalculator)
    {
        _employeeService = employeeService;
        _gymService = gymService;
        _paginationViewFactory = paginationViewFactory;
        _workTimeCalculator = workTimeCalculator;
    }

    public async Task<Guid> Create(EmployeeCreationRequest employee)
    {
      return await _employeeService.Create(employee);
    }

    public async Task<EmployeeVM> Get(Guid id)
    {
        if (await _employeeService.Any(id) == false)
            throw new ValueNotFoundByIdException(typeof(Employee), id);

        return await _employeeService.Get(id);
    }

    public async Task<BasePaginationView<EmployeeVM>> GetAll()
    {
        var all = _employeeService.GetAll();

        return _paginationViewFactory.CreatePaginationView<Employee, EmployeeVM>(all);
    }

    public async Task<BasePaginationView<EmployeeVM>> GetAllInGym(Guid gymId)
    {
        if (await _gymService.Any(gymId) == false)
            throw new ValueNotFoundByIdException(typeof(Gym), gymId);
        
        var all = _gymService.GetAllEmployee(gymId);

        return _paginationViewFactory.CreatePaginationView<Employee, EmployeeVM>(all);
    }

    public async Task<BasePaginationView<EmployeeVM>> GetTopBySalary(bool reversed, ValueRange<uint> range)
    {
        IQueryable<Employee> top;

        top = _employeeService.GetAll().OrderBy(s => s.SalaryPerHour);

        if (reversed)
            top.Reverse();

        return _paginationViewFactory.CreatePaginationView<Employee, EmployeeVM>(top);
    }


    public async Task<BasePaginationView<WorkTimeInfoVM>> GetTopByWorkHours(bool reversed, bool includeRecyclingWorks,
        ValueRange<DateOnly> dateRange)
    {
        var all = _employeeService.GetAll();

        var workTimeInfo = _workTimeCalculator.GetWorkTimeInfo(all, dateRange);

        IQueryable<WorkTimeInfo> top;

        if (includeRecyclingWorks)
            top = workTimeInfo.OrderBy(wti => wti.TotalWorkTime);
        else
            top = workTimeInfo.OrderBy(wti => wti.TimetableWorkTime);

        if (reversed)
            top = top.Reverse();

        return _paginationViewFactory.CreatePaginationView<WorkTimeInfo, WorkTimeInfoVM>(top);
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _employeeService.Any(id);
    }

    public async Task Edit(Guid id, EmployeeEditRequest reqest)
    {
        if (await _employeeService.Any(id) == false)
            throw new ValueNotFoundByIdException(typeof(Employee), id);

        await _employeeService.Edit(reqest);
    }

    public async Task Dismiss(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Reinstatement(Guid id)
    {
        throw new NotImplementedException();
    }
}