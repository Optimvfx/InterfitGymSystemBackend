using AutoMapper;
using BLL.Models.Fininaces;
using BLL.Services.Database;
using BLL.Services.SalaryCalculator;
using BLL.Services.WorkTimeCalculator;
using CLL.ControllersLogic.Interface;
using Common.Exceptions.General;
using Common.Models;
using DAL.Entities.Gym;
using DAL.Entities.Gym.Person;
using DAL.Entities.Gym.SalesLogic;

namespace CLL.ControllersLogic;

public class FinanceLogic : IFinanceLogic
{
    private readonly BaseSalaryCalculator _baseSalaryCalculator;
    private readonly BaseWorkTimeCalculator _workTimeCalculator;

    private readonly OrderService _orderService;
    private readonly EmployeeService _employeeService;
    private readonly GymService _gymService;
    private readonly SaleService _saleService;
    
    private readonly IMapper _mapper;

    public async Task<ProcentEarningsVM> GetProcentEarnings(ValueRange<DateOnly> dataRange)
    {
        throw new NotImplementedException();
    }

    public async Task<ProcentExpensesVM> GetProcentExpenses(ValueRange<DateOnly> dataRange)
    {
        throw new NotImplementedException();
    }

    public async Task<EarningsVM> GetEarnings(ValueRange<DateOnly> dataRange)
    {
        throw new NotImplementedException();
    }

    public async Task<ExpensesVM> GetExpenses(ValueRange<DateOnly> dataRange)
    {
        var expenses = GetAllExpenses(dataRange);

        return GetVM(expenses);
    }

    public async Task<ProcentEarningsVM> GetProcentEarnings(ValueRange<DateOnly> dataRange, Guid gymId)
    {
        throw new NotImplementedException();
    }

    public async Task<ProcentExpensesVM> GetProcentExpenses(ValueRange<DateOnly> dataRange, Guid gymId)
    {
        throw new NotImplementedException();
    }

    public async Task<EarningsVM> GetEarnings(ValueRange<DateOnly> dataRange, Guid gymId)
    {
        throw new NotImplementedException();
    }

    public async Task<ExpensesVM> GetExpenses(ValueRange<DateOnly> dataRange, Guid gymId)
    {
    }

    private ExpensesVM GetVM(Expenses expenses)
    {
        return _mapper.Map<ExpensesVM>(expenses);
    }

    private Earnings GetAllEarnings(ValueRange<DateOnly> dataRange)
    {
        var sales = GetSalas(_saleService.GetAll(), dataRange);
    }

    private IQueryable<Sale> GetSalas(IQueryable<Sale> sales, ValueRange<DateOnly> dataRange)
    {
        throw new NotImplementedException();
    }

    private async Task<Expenses> GetAllExpenses(ValueRange<DateOnly> dataRange, Guid gymId)
    {
        if (await _gymService.Any(gymId) == false)
            throw new NotFoundException(typeof(Gym), gymId);
        
        var salarys = GetSalarys( _gymService.GetAllEmployee(gymId), dataRange);
        var orders = GetOrders( _gymService.GetAllOrders(gymId), dataRange);
        
        return new Expenses(
            new SalaryExpenses(salarys),
            new OrderExpenses(orders)
        );
    }
    
    private Expenses GetAllExpenses(ValueRange<DateOnly> dataRange)
    {
        var salarys = GetSalarys( _employeeService.GetAll(), dataRange);
        var orders = GetOrders( _orderService.GetAll(), dataRange);
        
        return new Expenses(
            new SalaryExpenses(salarys),
            new OrderExpenses(orders)
        );
    }

    private IQueryable<Order> GetOrders(IQueryable<Order> orders, ValueRange<DateOnly> dataRange)
    {
        var min = dataRange.Min.ToDateTime(new TimeOnly(0));
        var max = dataRange.Max.ToDateTime(new TimeOnly(0));

        return orders.Where(order => order.Date >= min && order.Date <= max);
    }

    private IQueryable<Salary> GetSalarys(IQueryable<Employee> employees, ValueRange<DateOnly> dateRange)
    {
        var workTimeInfos = _workTimeCalculator.GetWorkTimeInfo(employees, dateRange);

        return _baseSalaryCalculator.Calculate(workTimeInfos);
    }
}

internal class Earnings
{
}

public class Expenses
{
    public readonly SalaryExpenses Salary;
    public readonly OrderExpenses Orders;

    public ulong Total => Salary.Total + Orders.Total;
    
    public Expenses(SalaryExpenses salary, OrderExpenses orders)
    {
        Salary = salary;
        Orders = orders;
    }
}

public class OrderExpenses
{
    public readonly IQueryable<Order> Orders;

    public ulong Total => (ulong)Orders.Sum(order => order.Payment);
    public ulong Prepayment => (ulong)Orders.Where(order => order.Finished == false).Sum(order => order.Payment);
    public ulong Finished => (ulong)Orders.Where(order => order.Finished).Sum(order => order.Payment);

    public OrderExpenses(IQueryable<Order> orders)
    {
        Orders = orders;
    }
}

public class SalaryExpenses
{
    public readonly IQueryable<Salary> Salaries;
    
    public ulong Total => (ulong)Salaries.Sum(s => s.Total);
    
    public ulong Basic  => (ulong)Salaries.Sum(s => s.MissedWorkPenalty);

    public ulong Vacation  => (ulong)Salaries.Sum(s => s.MissedWorkPenalty);

    public ulong RecyclingAward  => (ulong)Salaries.Sum(s => s.MissedWorkPenalty);
    public ulong MissedWorkPenalty => (ulong)Salaries.Sum(s => s.MissedWorkPenalty);
    
    public SalaryExpenses(IQueryable<Salary> salaries)
    {
        Salaries = salaries;
    }
}