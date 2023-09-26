using BLL.Services.SalaryCalculator;

namespace BLL.Models.Fininaces;

public class ExpensesVM
{
    public SalaryExpensesVM SalaryExpensesVM { get; set; }
    public OrderExpensesVM OrderExpensesVM { get; set; }
}

public class OrderExpensesVM
{
    public ulong Total { get; set; }
    public ulong Prepayment  { get; set; }
    public ulong Finished { get; set; }
    
    public ICollection<OrderExpenseVM> ExpensesVMs { get; set; }
}

public class OrderExpenseVM
{
}

public class SalaryExpensesVM
{
    public ulong Total { get; set; }
    
    public ulong Basic  { get; set; }
    public ulong Vacation  { get; set; }
    public ulong RecyclingAward  { get; set; }
    public ulong MissedWorkPenalty { get; set; }
    
    public ICollection<SalaryExpendVM> ExpendsVM { get; set; }
}

public class SalaryExpendVM
{
}