using BLL.Models.Employee;
using DAL.Entities.Gym.Person.Employeers;

namespace CLL.ControllersLogic.Interface.EmployeeLogic;

public interface ITimetableLogic
{
    Task<bool> ExistsTimetableAsync(Guid id);
    Task<TimetableEntity> GetTimetableAsync(Guid id);
    Task<Guid> AddTimetableAsync(string title, Timetable timetable);
    Task EditTimetableAsync(Guid id, TimetableEditRequest request);
    Task EditTimetableAsync(Guid id, string newTitle, TimetableEditRequest request);
    Task RemoveTimeTableAsync(Guid id);
}