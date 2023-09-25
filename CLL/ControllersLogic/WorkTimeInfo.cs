using DAL.Entities.Gym.Person;

namespace CLL.ControllersLogic;

public class WorkTimeInfo
{
    public Employee Employee { get; set; }
    public TimeSpan TotalWorkTime { get; set; }
    public TimeSpan TimetableWorkTime { get; set; }
    public TimeSpan ExceptedTimetableWorkTime { get; set; }
    public TimeSpan PassedByVacationTime { get; set; }
    
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