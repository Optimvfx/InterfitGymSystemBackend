using Common.Interfaces;
using DAL.Entities.Gym.Person.Employeers;
using DAL.Entities.Structs;

namespace BLL.Models.Employee.Requests;

public class TimetableEditRequest : IEditRequest<TimetableEntity>
{
    public DayGraphic? MondaySchedule { get; private set; }
    public DayGraphic? TuesdaySchedule { get; private set; }
    public DayGraphic? WednesdaySchedule { get; private set; }
    public DayGraphic? ThursdaySchedule { get; private set; }
    public DayGraphic? FridaySchedule { get; private set; }
    public DayGraphic? SaturdaySchedule { get; private set; }
    public DayGraphic? SundaySchedule { get; private set; }

    public TimetableEntity ApplyEdit(TimetableEntity entity)
    {
        if (MondaySchedule != null)
            entity.MondaySchedule = MondaySchedule.Value;

        if (TuesdaySchedule != null)
            entity.TuesdaySchedule = TuesdaySchedule.Value;

        if (WednesdaySchedule != null)
            entity.WednesdaySchedule = WednesdaySchedule.Value;

        if (ThursdaySchedule != null)
            entity.ThursdaySchedule = ThursdaySchedule.Value;

        if (FridaySchedule != null)
            entity.FridaySchedule = FridaySchedule.Value;

        if (SaturdaySchedule != null)
            entity.SaturdaySchedule = SaturdaySchedule.Value;

        if (SundaySchedule != null)
            entity.SundaySchedule = SundaySchedule.Value;

        return entity;
    }
}