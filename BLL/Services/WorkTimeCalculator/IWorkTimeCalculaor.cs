using BLL.Services.SalaryCalculator;
using Common.Models;
using DAL.Entities.Gym.Person;

namespace BLL.Services.WorkTimeCalculator;

public abstract class BaseWorkTimeCalculator
{
    public IQueryable<WorkTimeInfo> GetWorkTimeInfo(IQueryable<Employee> queryable, ValueRange<DateOnly> dataRange)
    {
        return queryable
            .Select(e => GetWorkTimeInfo(e, dataRange));
    }

    public abstract WorkTimeInfo GetWorkTimeInfo(Employee employee, ValueRange<DateOnly> dataRange);
}