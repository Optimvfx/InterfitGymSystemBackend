using DAL.Entities.Gym.Person;

namespace BLL.Services.SalaryCalculator;

public class WorkTimeInfo
{
    public readonly Employee Employee;
    public readonly TimeSpan TotalWorkTime;
    public readonly TimeSpan TimetableWorkTime;
    public readonly TimeSpan ExceptedTimetableWorkTime;
    public readonly TimeSpan PassedByVacationTime;
    
    public TimeSpan Recycling => TotalWorkTime - TimetableWorkTime;
    public TimeSpan MissedWorkTime => ExceptedTimetableWorkTime - TimetableWorkTime;   
    
    public WorkTimeInfo(Employee employee, TimeSpan totalWorkTime, TimeSpan timetableWorkTime, TimeSpan exceptedTimetableWorkTime, TimeSpan passedByVacationTime)
    {
        if (totalWorkTime.Ticks < 0 || timetableWorkTime.Ticks < 0 
                                    || exceptedTimetableWorkTime.Ticks < 0 || passedByVacationTime.Ticks < 0)
            throw new ArgumentException("Negative time interval not possible.");

        if(totalWorkTime < timetableWorkTime)
            throw new ArgumentException();

        if (exceptedTimetableWorkTime < timetableWorkTime)
            throw new ArgumentException();
        
        Employee = employee;
        TotalWorkTime = totalWorkTime;
        TimetableWorkTime = timetableWorkTime;
        ExceptedTimetableWorkTime = exceptedTimetableWorkTime;
        PassedByVacationTime = passedByVacationTime;
    }
}