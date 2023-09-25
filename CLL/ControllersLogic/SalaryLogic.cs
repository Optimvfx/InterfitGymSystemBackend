using AutoMapper;
using BLL.Models.Employee;
using BLL.Models.Fininaces;
using BLL.Services.Database;
using BLL.Services.PaginationViewFactory;
using CLL.ControllersLogic.Interface;
using Common.Exceptions.General;
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
    
    private readonly ISalaryCalculator _salaryCalculator;

    private readonly IPaginationViewFactory _paginationViewFactory;
    private readonly IMapper _mapper;

    public SalaryLogic(EmployeeService employeeService, GymService gymService, ISalaryCalculator salaryCalculator, IPaginationViewFactory paginationViewFactory, IMapper mapper)
    {
        _employeeService = employeeService;
        _gymService = gymService;
        _salaryCalculator = salaryCalculator;
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
            throw new NotFoundException(typeof(Gym), gymId);
        
        IQueryable<Employee> employees = _gymService.GetAllEmployee(gymId);

        return await GetAll(employees, dataRange);
    }

    private async Task<BasePaginationView<SalaryVM>> GetAll(   IQueryable<Employee> employees, ValueRange<DateOnly> dataRange)
    {
        IQueryable<WorkTimeInfo> workTimeInfos = GetWorkTimeInfo(employees, dataRange);

        var salarys = GetSalarys(workTimeInfos);

        return _paginationViewFactory.CreatePaginationView(salarys);
    }
    
    private IQueryable<SalaryVM> GetSalarys(IQueryable<WorkTimeInfo> workTimeInfos)
    {
        return workTimeInfos.Select(wti => GetSalaryVM(wti));
    }
    
    private IQueryable<WorkTimeInfo> GetWorkTimeInfo(IQueryable<Employee> queryable, ValueRange<DateOnly> dataRange)
    {
        return queryable
            .Select(e => CreateWorkTimeInfo(e, dataRange));
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

    private WorkTimeInfo CreateWorkTimeInfo(Employee employee, ValueRange<DateOnly> dataRange)
    {
        var visitsInRange = employee.Visitations
            .Where(v => dataRange.InRange(v.Date));
        
        var totalWorkTime = visitsInRange
            .Sum(v => v.ExitTime.Ticks - v.EnterTime.Ticks);

        var timetableWorkTimeTicks = visitsInRange
            .Sum(v => GetTimetableTicks(v, employee));

        var timetableWorkTime = TimeSpan.FromTicks(timetableWorkTimeTicks);
        
        var exceptedWorkTime = GetExceptedWorkTimeInTicks(employee, dataRange);

        var vacationTime = GetVacationTime(employee, dataRange);
        
        return new WorkTimeInfo(employee, TimeSpan.FromTicks(totalWorkTime), 
            timetableWorkTime, exceptedWorkTime,vacationTime);
    }

    private TimeSpan GetVacationTime(Employee employee, ValueRange<DateOnly> dataRange)
    {
       var vacations = employee.Vacations.Where(v => v.StartDate >= dataRange.Min && v.StartDate <= dataRange.Max);

       var total = TimeSpan.Zero;
       
       for (var day = dataRange.Min; day <= dataRange.Max; day = day.AddDays(1))
       {
           if(vacations.Any(v => day >= v.StartDate && day <= v.EndDate) == false)
               continue;
           
           var dayGraphic = GetExceptedDayGraphic(employee, day);
           total += dayGraphic.StopWorkAt - dayGraphic.StartWorkAt;
       }

       return total;
    }

    private TimeSpan GetExceptedWorkTimeInTicks(Employee employee, ValueRange<DateOnly> dataRange) 
    
    {
        var total = TimeSpan.Zero;
        
        for (var day = dataRange.Min; day <= dataRange.Max; day = day.AddDays(1))
        {
            var dayGraphic = GetDayGraphic(employee, day);
            
            if(dayGraphic == null)
                continue;

            total = total + dayGraphic.StopWorkAt.ToTimeSpan() - dayGraphic.StartWorkAt.ToTimeSpan();
        }

        return total;
    }
    
    private bool InDayGraphicRange(Visitation visitation, DayGraphic dayGraphic)
    {
        return visitation.EnterTime <= dayGraphic.StopWorkAt
               && dayGraphic.StartWorkAt <= visitation.ExitTime;
    }

    private long GetTimetableTicks(Visitation visitation, Employee employee)
    {
        const long NothingWorkTime = 0;
        
        var currentDayGraphic = GetDayGraphic(employee, visitation.Date);

        if (currentDayGraphic == null || InDayGraphicRange(visitation, currentDayGraphic) == false)
            return NothingWorkTime;

        var intersectionStart = Math.Max(visitation.EnterTime.Ticks, currentDayGraphic.StartWorkAt.Ticks);
        var intersectionEnd = Math.Min(visitation.ExitTime.Ticks, currentDayGraphic.StopWorkAt.Ticks);
        var intersection = intersectionEnd - intersectionStart;

        return intersection;
    }

    private DayGraphic? GetDayGraphic(Employee employee, DateOnly date)
    {
        if(employee.Vacations.Any(v => date >= v.StartDate && date <= v.EndDate))
             return null;

        return GetExceptedDayGraphic(employee, date);
    }
    
    private DayGraphic? GetExceptedDayGraphic(Employee employee, DateOnly date)
    {
        return employee.Timetable.DayGraphics
            .FirstOrDefault(g => date.DayOfWeek == g.DayOfWeek);
    }
}