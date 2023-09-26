using BLL.Models.Employee;
using BLL.Models.Fininaces;
using BLL.Services.SalaryCalculator;
using Common.Models;
using DAL.Entities.Gym.Person;
using DAL.Entities.Gym.Person.Employeers;
using DAL.Entities.Gym.Person.Persons;

namespace BLL.Services.WorkTimeCalculator;

public class WorkTimeCalculator : BaseWorkTimeCalculator
{
    public override WorkTimeInfo GetWorkTimeInfo(Employee employee, ValueRange<DateOnly> dataRange)
    {
        var visitsInRange = employee.Visitations
            .Where(v => dataRange.InRange(v.Date));
        
        var totalWorkTime = visitsInRange
            .Sum(v => v.ExitTime.Ticks - v.EnterTime.Ticks);

        var timetableWorkTimeTicks = visitsInRange
            .Sum(v => GetTimetableTicks(v, employee));

        var timetableWorkTime = TimeSpan.FromTicks(timetableWorkTimeTicks);
        
        var exceptedWorkTime = GetExceptedWorkTimeInTicks(employee, dataRange);

        var vacationTime = GetVacationTime(employee, dataRange);
        
        return new WorkTimeInfo(employee, TimeSpan.FromTicks(totalWorkTime), 
            timetableWorkTime, exceptedWorkTime,vacationTime);
    }

    private TimeSpan GetVacationTime(Employee employee, ValueRange<DateOnly> dataRange)
    {
       var vacations = employee.Vacations.Where(v => v.StartDate >= dataRange.Min && v.StartDate <= dataRange.Max);

       var total = TimeSpan.Zero;
       
       for (var day = dataRange.Min; day <= dataRange.Max; day = day.AddDays(1))
       {
           if(vacations.Any(v => day >= v.StartDate && day <= v.EndDate) == false)
               continue;
           
           var dayGraphic = GetExceptedDayGraphic(employee, day);
           total += dayGraphic.StopWorkAt - dayGraphic.StartWorkAt;
       }

       return total;
    }

    private TimeSpan GetExceptedWorkTimeInTicks(Employee employee, ValueRange<DateOnly> dataRange) 
    
    {
        var total = TimeSpan.Zero;
        
        for (var day = dataRange.Min; day <= dataRange.Max; day = day.AddDays(1))
        {
            var dayGraphic = GetDayGraphic(employee, day);
            
            if(dayGraphic == null)
                continue;

            total = total + dayGraphic.StopWorkAt.ToTimeSpan() - dayGraphic.StartWorkAt.ToTimeSpan();
        }

        return total;
    }
    
    private bool InDayGraphicRange(Visitation visitation, DayGraphic dayGraphic)
    {
        return visitation.EnterTime <= dayGraphic.StopWorkAt
               && dayGraphic.StartWorkAt <= visitation.ExitTime;
    }

    private long GetTimetableTicks(Visitation visitation, Employee employee)
    {
        const long NothingWorkTime = 0;
        
        var currentDayGraphic = GetDayGraphic(employee, visitation.Date);

        if (currentDayGraphic == null || InDayGraphicRange(visitation, currentDayGraphic) == false)
            return NothingWorkTime;

        var intersectionStart = Math.Max(visitation.EnterTime.Ticks, currentDayGraphic.StartWorkAt.Ticks);
        var intersectionEnd = Math.Min(visitation.ExitTime.Ticks, currentDayGraphic.StopWorkAt.Ticks);
        var intersection = intersectionEnd - intersectionStart;

        return intersection;
    }

    private DayGraphic? GetDayGraphic(Employee employee, DateOnly date)
    {
        if(employee.Vacations.Any(v => date >= v.StartDate && date <= v.EndDate))
             return null;

        return GetExceptedDayGraphic(employee, date);
    }
    
    private DayGraphic? GetExceptedDayGraphic(Employee employee, DateOnly date)
    {
        return employee.Timetable.DayGraphics
            .FirstOrDefault(g => date.DayOfWeek == g.DayOfWeek);
    }
}