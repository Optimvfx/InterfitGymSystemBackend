using BLL.Models.Fininaces;
using Common.Models;

namespace CLL.ControllersLogic.Interface;

public interface IFinanceLogic
{
    Task<Result<ProcentEarningsVM>> TryGetProcentEarnings(ValueRange<DateOnly> dataRange);
    Task<Result<ProcentExpensesVM>> TryGetProcentExpenses(ValueRange<DateOnly> dataRange);
    Task<Result<EarningsVM>> TryGetEarnings(ValueRange<DateOnly> dataRange);
    Task<Result<ExpensesVM>> TryGetExpenses(ValueRange<DateOnly> dataRange);
    
    Task<Result<ProcentEarningsVM>> TryGetProcentEarnings(ValueRange<DateOnly> dataRange, Guid gymId);
    Task<Result<ProcentExpensesVM>> TryGetProcentExpenses(ValueRange<DateOnly> dataRange,  Guid gymId);
    Task<Result<EarningsVM>> TryGetEarnings(ValueRange<DateOnly> dataRange,  Guid gymId);
    Task<Result<ExpensesVM>> TryGetExpenses(ValueRange<DateOnly> dataRange,  Guid gymId);
}