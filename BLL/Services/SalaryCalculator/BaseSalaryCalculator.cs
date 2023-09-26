namespace BLL.Services.SalaryCalculator;

public abstract class BaseSalaryCalculator
{
    public IQueryable<Salary> Calculate(IQueryable<WorkTimeInfo> workTimeInfo)
    {
        return workTimeInfo.Select(wti => Calculate(wti));
    }
    
    public abstract Salary Calculate(WorkTimeInfo workTimeInfo);
}