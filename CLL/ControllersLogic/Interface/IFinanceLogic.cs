using BLL.Models.Fininaces;
using Common.Models;

namespace CLL.ControllersLogic.Interface;

public interface IFinanceLogic
{
    Task<ProcentEarningsVM> GetProcentEarnings(ValueRange<DateOnly> dataRange);
    Task<ProcentExpensesVM> GetProcentExpenses(ValueRange<DateOnly> dataRange);
    Task<EarningsVM> GetEarnings(ValueRange<DateOnly> dataRange);
    Task<ExpensesVM> GetExpenses(ValueRange<DateOnly> dataRange);
    
    Task<ProcentEarningsVM> GetProcentEarnings(ValueRange<DateOnly> dataRange, Guid gymId);
    Task<ProcentExpensesVM> GetProcentExpenses(ValueRange<DateOnly> dataRange,  Guid gymId);
    Task<EarningsVM> GetEarnings(ValueRange<DateOnly> dataRange,  Guid gymId);
    Task<ExpensesVM> GetExpenses(ValueRange<DateOnly> dataRange,  Guid gymId);
}