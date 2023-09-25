using BLL.Models.Employee;

namespace BLL.Models.Fininaces;

public class SalaryVM
{
    public EmployeeVM EmployeeVM { get; set; }
    public WorkTimeInfoVM WorkTimeInfoVM { get; set; }

    public uint Total { get; set; }
    
    public uint Basic { get; set; }
    
    public uint Vacation { get; set; }

    public uint RecyclingAward { get; set; }
    public uint MissedWorkPenalty { get; set; }
}

public class WorkTimeInfoVM
{
    public TimeSpan TotalWorkTime { get; set; }
    public TimeSpan TimetableWorkTime { get; set; }
    public TimeSpan ExceptedTimetableWorkTime { get; set; }
    public TimeSpan PassedByVacationTime { get; set; }
    
    public TimeSpan Recycling { get; set; }
    public TimeSpan MissedWorkTime { get; set; }  
}