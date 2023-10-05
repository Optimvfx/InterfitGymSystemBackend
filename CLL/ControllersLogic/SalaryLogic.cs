using AutoMapper;
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
using DAL.Entities.Gym.Person.Employeers;
using DAL.Entities.Gym.Person.Persons;
using Microsoft.EntityFrameworkCore;

namespace CLL.ControllersLogic;

public class SalaryLogic : ISalaryLogic
{
    private readonly EmployeeService _employeeService;
    private readonly GymService _gymService;
    
    private readonly BaseSalaryCalculator _salaryCalculator;
    private readonly BaseWorkTimeCalculator _workTimeCalculator;
    
    private readonly IPaginationViewFactory _paginationViewFactory;
    private readonly IMapper _mapper;

    public SalaryLogic(EmployeeService employeeService, GymService gymService, BaseSalaryCalculator salaryCalculator, BaseWorkTimeCalculator workTimeCalculator, IPaginationViewFactory paginationViewFactory, IMapper mapper)
    {
        _employeeService = employeeService;
        _gymService = gymService;
        _salaryCalculator = salaryCalculator;
        _workTimeCalculator = workTimeCalculator;
        _paginationViewFactory = paginationViewFactory;
        _mapper = mapper;
    }

    public async Task<BasePaginationView<SalaryVM>> GetAll(ValueRange<DateOnly> dataRange)
    {
        IQueryable<Employee> employees = _employeeService.GetAll();

        return await GetAll(employees, dataRange);
    }

    public async Task<BasePaginationView<SalaryVM>> GetAllByGym(Guid gymId, ValueRange<DateOnly> dataRange)
    {
        if (await _gymService.Any(gymId) == false)
            throw new ValueNotFoundByIdException(typeof(Gym), gymId);
        
        IQueryable<Employee> employees = _gymService.GetAllEmployee(gymId);

        return await GetAll(employees, dataRange);
    }

    private async Task<BasePaginationView<SalaryVM>> GetAll(IQueryable<Employee> employees, ValueRange<DateOnly> dataRange)
    {
        IQueryable<WorkTimeInfo> workTimeInfos = _workTimeCalculator.GetWorkTimeInfo(employees, dataRange);

        var salarys = GetSalarys(workTimeInfos);

        return _paginationViewFactory.CreatePaginationView(salarys);
    }
    
    private IQueryable<SalaryVM> GetSalarys(IQueryable<WorkTimeInfo> workTimeInfos)
    {
        return workTimeInfos.Select(wti => GetSalaryVM(wti));
    }

    public SalaryVM GetSalaryVM(WorkTimeInfo workTimeInfo)
    {
        Salary salary = _salaryCalculator.Calculate(workTimeInfo);
        var salaryVM = _mapper.Map<SalaryVM>(salary);
        var workTimeInfoVM = _mapper.Map<WorkTimeInfoVM>(workTimeInfo);
        var employeeVM = _mapper.Map<EmployeeVM>(workTimeInfo.Employee);

        salaryVM.EmployeeVM = employeeVM;
        salaryVM.WorkTimeInfoVM = workTimeInfoVM;

        return salaryVM;
    }
}