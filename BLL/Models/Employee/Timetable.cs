using System.ComponentModel.DataAnnotations;
using DAL.Entities.Structs;

namespace BLL.Models.Employee;

public struct Timetable
{
    public DayGraphic MondaySchedule { get; private set; }
    public DayGraphic TuesdaySchedule { get; private set; }
    public DayGraphic WednesdaySchedule { get; private set; }
    public DayGraphic ThursdaySchedule { get; private set; }
    public DayGraphic FridaySchedule { get; private set; }
    public DayGraphic SaturdaySchedule { get; private set; }
    public DayGraphic SundaySchedule { get; private set; }

    public Timetable(DayGraphic mondaySchedule, DayGraphic tuesdaySchedule, DayGraphic wednesdaySchedule, DayGraphic thursdaySchedule, DayGraphic fridaySchedule, DayGraphic saturdaySchedule, DayGraphic sundaySchedule)
    {
        MondaySchedule = mondaySchedule;
        TuesdaySchedule = tuesdaySchedule;
        WednesdaySchedule = wednesdaySchedule;
        ThursdaySchedule = thursdaySchedule;
        FridaySchedule = fridaySchedule;
        SaturdaySchedule = saturdaySchedule;
        SundaySchedule = sundaySchedule;
    }
}