using AutoMapper;
using BLL.Models.Employee;
using BLL.Models.Employee.Requests;
using BLL.Services.Abstract;
using BLL.Services.TimeService;
using Common.Extensions;
using DAL;
using DAL.Entities.Gym.Person;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class EmployeeService : DbSetService<Employee>
{
    private readonly IMapper _mapper;
    private readonly ITimeService _timeService;

    public EmployeeService(ApplicationDbContext db, IMapper mapper, ITimeService timeService) : base(db)
    {
        _mapper = mapper;
        _timeService = timeService;
    }

    public async Task<Guid> AddAsync(EmployeeCreationRequest request)
    {
        var entity = _mapper.Map<Employee>(request);
        return await InnerAdd(entity);
    }

    public async Task EditAsync(Guid id, EmployeeEditRequest request)
    {
        var entity = await GetAsync(id);

        request.ApplyEdit(entity);
        
        await Db.SaveChangesAsync();
    }
    
    public async Task<bool> HasActiveVacation(Guid employeeId)
    {
        var employee = await GetAsync(employeeId);

        if (employee.VacationId == null)
            return false;

        var vacation = await Db.Vacations.GetByIdAsync(employee.VacationId.Value);
        var vacationExceptionDate = vacation.CreationDate.AddDays((int)vacation.DurationInDays);
        var currentDate = DateOnly.FromDateTime(_timeService.GetCurrentDateTime());

        return vacationExceptionDate <= currentDate;
    }
    
    public async Task DeleteAsync(Guid id) => await DeleteAsync(id);
    
    protected override DbSet<Employee> GetDbSet(ApplicationDbContext dbContext) => dbContext.Employees;
}