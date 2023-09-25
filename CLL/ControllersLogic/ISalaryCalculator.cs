namespace CLL.ControllersLogic;

public interface ISalaryCalculator
{
    Salary Calculate(WorkTimeInfo workTimeInfo);
}